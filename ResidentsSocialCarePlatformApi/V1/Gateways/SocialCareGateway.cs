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

        public List<Domain.ResidentInformation> GetAllResidents(int cursor, int limit, long? id = null, string firstname = null,
            string lastname = null, string dateOfBirth = null, string postcode = null, string address = null, string contextflag = null)
        {
            var addressSearchPattern = GetSearchPattern(address);
            var postcodeSearchPattern = GetSearchPattern(postcode);

            var queryByAddress = postcode != null || address != null;

            var peopleIds = queryByAddress
                ? PeopleIdsForAddressQuery(cursor, limit, id, firstname, lastname, postcode, address, contextflag)
                : PeopleIds(cursor, limit, id, firstname, lastname, dateOfBirth, contextflag);

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

        public Domain.ResidentInformation GetEntityById(long id, string contextflag = null)
        {
            var contextFlagSearchPattern = GetSearchPattern(contextflag);
            var databaseRecord = _socialCareContext.Persons
                .Where(p => p.Id == id)
                .Where(p =>
                    string.IsNullOrEmpty(contextflag) || EF.Functions.ILike(p.AgeContext, contextFlagSearchPattern))
                .ToList();
            if (databaseRecord.FirstOrDefault() == null) return null;

            var addressesForPerson = _socialCareContext.Addresses
                .Where(a => a.PersonId == databaseRecord.FirstOrDefault().Id);
            var person = MapPersonAndAddressesToResidentInformation(databaseRecord.FirstOrDefault(), addressesForPerson);
            AttachPhoneNumberToPerson(person);

            return person;
        }
        public Domain.ResidentInformation InsertResident(string firstName, string lastName)
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

            return new Domain.ResidentInformation()
            {
                MosaicId = person.Id.ToString(),
                FirstName = person.FirstName,
                LastName = person.LastName,
            };
        }

        public List<CaseNoteInformation> GetAllCaseNotes(long personId)
        {
            var caseNotes = _socialCareContext.CaseNotes
            .Where(note => note.PersonId == personId)
            .Include(x => x.CreatedByWorker)
            .Include(x => x.LastUpdatedWorker)
            .Include(x => x.CopiedByWorker)
            .Include(x => x.NewNoteType)
            .ToList();

            return caseNotes.Select(x => x.ToDomain()).ToList();
        }

        public Domain.CaseNoteInformation GetCaseNoteInformationById(long caseNoteId)
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

            var lastUpdatedByWorker = _socialCareContext.Workers.FirstOrDefault(worker => worker.SystemUserId == caseNote.LastUpdatedBy);
            if (lastUpdatedByWorker != null)
            {
                caseNoteInformation.LastUpdatedName =
                    $"{lastUpdatedByWorker.FirstNames} {lastUpdatedByWorker.LastNames}";
                caseNoteInformation.LastUpdatedEmail = lastUpdatedByWorker.EmailAddress;
            }

            if (caseNote.CopiedBy != null)
            {
                var copiedByWorker = _socialCareContext.Workers.FirstOrDefault(worker => worker.SystemUserId == caseNote.CopiedBy);
                caseNoteInformation.CopiedByName = $"{copiedByWorker.FirstNames} {copiedByWorker.LastNames}";
                caseNoteInformation.CopiedByEmail = copiedByWorker.EmailAddress;
            }

            return caseNoteInformation;
        }

        public IEnumerable<VisitInformation?> GetVisitInformationByPersonId(long personId)
        {
            var visits = _socialCareContext.Visits
                .Where(visit => visit.PersonId == personId)
                .Include(x => x.Worker)
                .ToList();

            return visits.Select(x => x.ToDomain());
        }

        public VisitInformation? GetVisitInformationByVisitId(long visitId)
        {
            var visitInformation = _socialCareContext
                .Visits
                .Include(x => x.Worker)
                .FirstOrDefault(visit => visit.VisitId == visitId);

            return visitInformation?.ToDomain();
        }

        public Domain.PersonalRelationships GetPersonalRelationships(long personId)
        {
            var personalRelationships = _socialCareContext.PersonalRelationshipsView
                                            .Where(personalRelationship => personalRelationship.PersonId == personId)
                                            .ToList();

            return new Domain.PersonalRelationships()
            {
                Parents = FilterPersonalRelationships(personalRelationships, "parents"),
                Siblings = FilterPersonalRelationships(personalRelationships, "siblings"),
                Children = FilterPersonalRelationships(personalRelationships, "children"),
                Other = FilterPersonalRelationships(personalRelationships, "other")
            };
        }

        private List<long> FilterPersonalRelationships(List<PersonalRelationshipView> personalRelationships, string category)
        {
            return personalRelationships
                        .Where(personalRelationship => personalRelationship.Category == category)
                        .Select(personalRelationship => personalRelationship.OtherPersonId)
                        .ToList();
        }

        private List<long> PeopleIds(int cursor, int limit, long? id, string firstname, string lastname, string dateOfBirth, string contextflag)
        {
            var firstNameSearchPattern = GetSearchPattern(firstname);
            var lastNameSearchPattern = GetSearchPattern(lastname);
            var dateOfBirthSearchPattern = GetSearchPattern(dateOfBirth);
            var contextFlagSearchPattern = GetSearchPattern(contextflag);

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
                    string.IsNullOrEmpty(contextflag) || EF.Functions.ILike(person.AgeContext, contextFlagSearchPattern))
                .OrderBy(p => p.Id)
                .Take(limit)
                .Select(p => p.Id)
                .ToList();
        }

        private List<long> PeopleIdsForAddressQuery(int cursor, int limit, long? id, string firstname, string lastname,
            string postcode, string address, string contextflag)
        {
            var firstNameSearchPattern = GetSearchPattern(firstname);
            var lastNameSearchPattern = GetSearchPattern(lastname);
            var addressSearchPattern = GetSearchPattern(address);
            var postcodeSearchPattern = GetSearchPattern(postcode);
            var contextFlagSearchPattern = GetSearchPattern(contextflag);

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
                    string.IsNullOrEmpty(contextflag) || EF.Functions.ILike(add.Person.AgeContext, contextFlagSearchPattern))
                .Include(add => add.Person)
                .Where(add => add.PersonId > cursor)
                .OrderBy(add => add.PersonId)
                .GroupBy(add => add.PersonId)
                .Where(p => p.Key != null)
                .Take(limit)
                .Select(p => (long) p.Key)
                .ToList();
        }

        private Domain.ResidentInformation AttachPhoneNumberToPerson(Domain.ResidentInformation person)
        {
            var phoneNumbersForPerson = _socialCareContext.TelephoneNumbers
                .Where(n => n.PersonId == int.Parse(person.MosaicId));
            person.PhoneNumberList = phoneNumbersForPerson.Any() ? phoneNumbersForPerson.Select(n => n.ToDomain()).ToList() : null;
            return person;
        }

        private static Domain.ResidentInformation MapPersonAndAddressesToResidentInformation(Person person,
            IEnumerable<Infrastructure.Address> addresses, IEnumerable<TelephoneNumber> numbers = null)
        {
            var resident = person.ToDomain();
            var addressesDomain = addresses.Select(address => address.ToDomain()).ToList();
            resident.Uprn = GetMostRecentUprn(addresses);
            resident.AddressList = addressesDomain;
            resident.AddressList = addressesDomain.Any()
                ? addressesDomain
                : null;
            resident.PhoneNumberList = numbers == null || !numbers.Any()
                ? null
                : numbers.Select(n => n.ToDomain()).ToList();
            return resident;
        }

        private static string GetMostRecentUprn(IEnumerable<Infrastructure.Address> addressesForPerson)
        {
            if (!addressesForPerson.Any()) return null;
            var currentAddress = addressesForPerson.FirstOrDefault(a => a.EndDate == null);
            if (currentAddress != null)
            {
                return currentAddress.Uprn.ToString();
            }

            return addressesForPerson.OrderByDescending(a => a.EndDate).First().Uprn.ToString();
        }

        private static string GetSearchPattern(string str)
        {
            return $"%{str?.Replace(" ", "")}%";
        }

        private string LookUpNoteTypeDescription(string noteTypeCode)
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

            caseNoteInformation.LastUpdatedName = GetWorkerName(caseNote.LastUpdatedBy);
            caseNoteInformation.LastUpdatedEmail = GetWorkerEmailAddress(caseNote.LastUpdatedBy);

            caseNoteInformation.CopiedByName = GetWorkerName(caseNote.CopiedBy);
            caseNoteInformation.CopiedByEmail = GetWorkerEmailAddress(caseNote.CopiedBy);

            return caseNoteInformation;
        }
    }
}
