using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

#nullable enable
namespace ResidentsSocialCarePlatformApi.V1.Gateways
{
    public class SocialCareGateway : ISocialCareGateway
    {
        private readonly SocialCareContext _socialCareContext;

        public SocialCareGateway(SocialCareContext socialCareContext)
        {
            _socialCareContext = socialCareContext;
        }

        public List<ResidentInformation> GetAllResidents(int cursor, int limit, long? id = null, string? firstname = null,
            string? lastname = null, string? dateOfBirth = null, string? postcode = null, string? address = null, string? contextFlag = null)
        {
            var queryByAddress = postcode != null || address != null;

            var peopleIds = queryByAddress
                ? PeopleIdsForAddressQuery(cursor, limit, id, firstname, lastname, postcode, address, contextFlag)
                : PeopleIds(cursor, limit, id, firstname, lastname, dateOfBirth, contextFlag);

            var dbRecords = _socialCareContext.Persons
                .Where(p => peopleIds.Contains(p.Id))
                .Select(p => new
                {
                    Person = p,
                    Addresses = _socialCareContext
                        .Addresses
                        .Where(add => add.PersonId == p.Id)
                        .Distinct()
                        .ToList(),
                    TelephoneNumbers = _socialCareContext.TelephoneNumbers.Where(n => n.PersonId == p.Id).Distinct().ToList()
                }).ToList();

            return dbRecords.Select(x => MapPersonAndAddressesToResidentInformation(x.Person, x.Addresses, x.TelephoneNumbers)
            ).ToList();
        }

        public ResidentInformation? GetEntityById(long id, string? contextFlag = null)
        {
            var contextFlagSearchPattern = GetSearchPattern(contextFlag);
            var databaseRecord = _socialCareContext.Persons
                .Where(p => p.Id == id)
                .Where(p =>
                    string.IsNullOrEmpty(contextFlag) || EF.Functions.ILike(p.AgeContext, contextFlagSearchPattern))
                .ToList();

            var personRecord = databaseRecord.FirstOrDefault();
            if (personRecord == null)
            {
                return null;
            }

            var addressesForPerson = _socialCareContext.Addresses
                .Where(a => a.PersonId == personRecord.Id);
            var person = MapPersonAndAddressesToResidentInformation(personRecord, addressesForPerson);
            AttachPhoneNumberToPerson(person);

            return person;
        }
        public ResidentInformation InsertResident(string firstName, string lastName)
        {
            Person person = new Person()
            {
                FirstName = firstName,
                LastName = lastName,
                FullName = $"{firstName} {lastName}", // Cannot be null
                Gender = "-", // Cannot be null
                PersonIdLegacy = "-", // Cannot be null
            };

            _socialCareContext.Persons.Add(person);
            _socialCareContext.SaveChanges();

            return new ResidentInformation()
            {
                MosaicId = person.Id.ToString(),
                FirstName = person.FirstName,
                LastName = person.LastName,
            };
        }

        public List<CaseNoteInformation> GetAllCaseNotes(long personId)
        {
            var caseNotes = _socialCareContext.CaseNotes.Where(note => note.PersonId == personId).ToList();

            return caseNotes.Select(AddRelatedInformationToCaseNote).ToList();
        }

        public CaseNoteInformation? GetCaseNoteInformationById(long caseNoteId)
        {
            var caseNote = _socialCareContext.CaseNotes.FirstOrDefault(caseNote => caseNote.Id == caseNoteId);

            if (caseNote == null) return null;

            var caseNoteInformation = caseNote.ToDomain();

            var noteType = _socialCareContext.NoteTypes.FirstOrDefault(noteType => noteType.Type == caseNote.NoteType);
            caseNoteInformation.NoteType = noteType?.Description;

            var createdByWorker = _socialCareContext.Workers.FirstOrDefault(worker => worker.SystemUserId == caseNote.CreatedBy);
            if (createdByWorker != null)
            {
                caseNoteInformation.CreatedByName = $"{createdByWorker.FirstNames} {createdByWorker.LastNames}";
                caseNoteInformation.CreatedByEmail = createdByWorker.EmailAddress;
            }

            return caseNoteInformation;
        }

        public IEnumerable<VisitInformation?> GetVisitInformationByPersonId(long personId)
        {
            var visits = _socialCareContext.Visits
                .Where(visit => visit.PersonId == personId)
                .Include(visit => visit.Worker)
                .ToList();

            return visits.Select(x => x.ToDomain());
        }

        public VisitInformation? GetVisitInformationByVisitId(long visitId)
        {
            var visitInformation = _socialCareContext.Visits
                .Include(visit => visit.Worker)
                .FirstOrDefault(visit => visit.VisitId == visitId);

            return visitInformation?.ToDomain();
        }

        private List<long> PeopleIds(int cursor, int limit, long? id, string? firstname, string? lastname, string? dateOfBirth, string? contextFlag)
        {
            var firstNameSearchPattern = GetSearchPattern(firstname);
            var lastNameSearchPattern = GetSearchPattern(lastname);
            var dateOfBirthSearchPattern = GetSearchPattern(dateOfBirth);
            var contextFlagSearchPattern = GetSearchPattern(contextFlag);

            return _socialCareContext.Persons
                .Where(person => person.Id > cursor)
                .Where(person =>
                    id == null || EF.Functions.ILike(person.Id.ToString(), id.ToString()))
                .Where(person =>
                    string.IsNullOrEmpty(firstname) || EF.Functions.ILike(person.FirstName.Replace(" ", ""), firstNameSearchPattern))
                .Where(person =>
                    string.IsNullOrEmpty(lastname) || EF.Functions.ILike(person.LastName, lastNameSearchPattern))
                .Where(person =>
                    string.IsNullOrEmpty(dateOfBirth) || EF.Functions.ILike(person.DateOfBirth.ToString(), dateOfBirthSearchPattern))
                .Where(person =>
                    string.IsNullOrEmpty(contextFlag) || EF.Functions.ILike(person.AgeContext, contextFlagSearchPattern))
                .OrderBy(p => p.Id)
                .Take(limit)
                .Select(p => p.Id)
                .ToList();
        }

        private List<long> PeopleIdsForAddressQuery(int cursor, int limit, long? id, string? firstname, string? lastname,
            string? postcode, string? address, string? contextFlag)
        {
            var firstNameSearchPattern = GetSearchPattern(firstname);
            var lastNameSearchPattern = GetSearchPattern(lastname);
            var addressSearchPattern = GetSearchPattern(address);
            var postcodeSearchPattern = GetSearchPattern(postcode);
            var contextFlagSearchPattern = GetSearchPattern(contextFlag);

            return _socialCareContext.Addresses
                .Where(add =>
                    id == null || EF.Functions.ILike(add.PersonId.ToString(), id.ToString()))
                .Where(add =>
                    string.IsNullOrEmpty(address) || EF.Functions.ILike(add.AddressLines.Replace(" ", ""), addressSearchPattern))
                .Where(add =>
                    string.IsNullOrEmpty(postcode) || EF.Functions.ILike(add.PostCode.Replace(" ", ""), postcodeSearchPattern))
                .Where(add =>
                    string.IsNullOrEmpty(firstname) || EF.Functions.ILike(add.Person.FirstName.Replace(" ", ""), firstNameSearchPattern))
                .Where(add =>
                    string.IsNullOrEmpty(lastname) || EF.Functions.ILike(add.Person.LastName, lastNameSearchPattern))
                .Where(add =>
                    string.IsNullOrEmpty(contextFlag) || EF.Functions.ILike(add.Person.AgeContext, contextFlagSearchPattern))
                .Include(add => add.Person)
                .Where(add => add.PersonId > cursor)
                .OrderBy(add => add.PersonId)
                .GroupBy(add => add.PersonId)
                .Where(p => p.Key != null)
                .Take(limit)
                .Select(p => (long) p.Key!)
                .ToList();
        }

        private void AttachPhoneNumberToPerson(ResidentInformation person)
        {
            var phoneNumbersForPerson = _socialCareContext.TelephoneNumbers
                .Where(n => n.PersonId == int.Parse(person.MosaicId));
            person.PhoneNumberList = phoneNumbersForPerson.Any() ? phoneNumbersForPerson.Select(n => n.ToDomain()).ToList() : null;
        }

        private static ResidentInformation MapPersonAndAddressesToResidentInformation(Person person,
            IEnumerable<Infrastructure.Address> addresses, IEnumerable<TelephoneNumber>? numbers = null)
        {
            var listAddresses = addresses.ToList();
            var listNumbers = numbers?.ToList();

            var resident = person.ToDomain();
            var addressesDomain = listAddresses.Select(address => address.ToDomain()).ToList();
            resident.Uprn = GetMostRecentUprn(listAddresses);
            resident.AddressList = addressesDomain;
            resident.AddressList = addressesDomain.Any()
                ? addressesDomain
                : null;
            resident.PhoneNumberList = numbers == null || !listNumbers.Any()
                ? null
                : listNumbers.Select(n => n.ToDomain()).ToList();
            return resident;
        }

        private static string? GetMostRecentUprn(IEnumerable<Infrastructure.Address> addressesForPerson)
        {
            var listAddresses = addressesForPerson.ToList();
            if (!listAddresses.Any()) return null;
            var currentAddress = listAddresses.FirstOrDefault(a => a.EndDate == null);
            if (currentAddress != null)
            {
                return currentAddress.Uprn.ToString();
            }

            return listAddresses.OrderByDescending(a => a.EndDate).First().Uprn.ToString();
        }

        private static string GetSearchPattern(string? str)
        {
            return $"%{str?.Replace(" ", "")}%";
        }

        private string? LookUpNoteTypeDescription(string noteTypeCode)
        {
            return _socialCareContext.NoteTypes.FirstOrDefault(type => type.Type.Equals(noteTypeCode))?.Description;
        }


        private string? GetWorkerName(string? actionDoneById = null, long? workerId = null)
        {
            if (workerId != null)
            {
                var workerById = _socialCareContext.Workers.FirstOrDefault(w => w.Id.Equals(workerId));
                return workerById != null ? $"{workerById.FirstNames} {workerById.LastNames}" : null;
            }

            var worker = _socialCareContext.Workers.FirstOrDefault(w => w.SystemUserId.Equals(actionDoneById));
            return worker != null ? $"{worker.FirstNames} {worker.LastNames}" : null;
        }

        private string? GetWorkerEmailAddress(string? actionDoneById = null, long? workerId = null)
        {
            if (workerId != null)
            {
                return _socialCareContext.Workers.FirstOrDefault(worker => worker.Id.Equals(workerId))?.EmailAddress;
            }

            return _socialCareContext.Workers.FirstOrDefault(worker => worker.SystemUserId.Equals(actionDoneById))
                ?.EmailAddress;
        }

        private CaseNoteInformation AddRelatedInformationToCaseNote(CaseNote caseNote)
        {
            var caseNoteInformation = caseNote.ToDomain();
            caseNoteInformation.CaseNoteContent = null;
            caseNoteInformation.NoteType = LookUpNoteTypeDescription(caseNote.NoteType);
            caseNoteInformation.CreatedByName = GetWorkerName(caseNote.CreatedBy);
            caseNoteInformation.CreatedByEmail = GetWorkerEmailAddress(caseNote.CreatedBy);

            return caseNoteInformation;
        }
    }
}
