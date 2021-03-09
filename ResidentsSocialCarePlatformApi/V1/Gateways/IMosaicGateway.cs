using System.Collections.Generic;
using ResidentInformation = ResidentsSocialCarePlatformApi.V1.Domain.ResidentInformation;

namespace ResidentsSocialCarePlatformApi.V1.Gateways
{
    public interface IMosaicGateway
    {
        List<Domain.ResidentInformation> GetAllResidents(int cursor, int limit, long? id, string firstname = null, string lastname = null,
            string dateOfBirth = null, string postcode = null, string address = null, string contextFlag = null);

        Domain.ResidentInformation GetEntityById(long id, string contextflag = null);

        Domain.ResidentInformation InsertResident(string firstName, string lastName);
    }
}