using System.Collections.Generic;

namespace ResidentsSocialCarePlatformApi.V1.Gateways
{
    public interface ISocialCareGateway
    {
        List<Domain.ResidentInformation> GetAllResidents(int cursor, int limit, long? id, string firstname = null, string lastname = null,
            string dateOfBirth = null, string postcode = null, string address = null, string contextFlag = null);

        Domain.ResidentInformation GetEntityById(long id, string contextflag = null);

        Domain.ResidentInformation InsertResident(string firstName, string lastName);

        List<Domain.CaseNoteInformation> GetAllCaseNotes(long personId);

        Domain.CaseNoteInformation GetCaseNoteInformationById(long caseNoteId);
    }
}
