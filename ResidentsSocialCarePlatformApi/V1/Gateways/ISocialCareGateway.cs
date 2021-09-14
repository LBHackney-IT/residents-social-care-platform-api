using System.Collections.Generic;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

namespace ResidentsSocialCarePlatformApi.V1.Gateways
{
    public interface ISocialCareGateway
    {
        List<Domain.ResidentInformation> GetAllResidents(int cursor, int limit, long? id, string firstname = null, string lastname = null,
            string dateOfBirth = null, string postcode = null, string address = null, string contextFlag = null);

        Domain.ResidentInformation GetEntityById(long id, string contextFlag = null);

        Domain.ResidentInformation InsertResident(string firstName, string lastName);

        List<Domain.CaseNoteInformation> GetAllCaseNotes(long personId);

        Domain.CaseNoteInformation GetCaseNoteInformationById(long caseNoteId);

        IEnumerable<VisitInformation> GetVisitInformationByPersonId(long personId);

        VisitInformation GetVisitInformationByVisitId(long visitId);
    }
}
