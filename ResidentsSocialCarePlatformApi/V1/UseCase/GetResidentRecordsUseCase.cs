using System.Collections.Generic;
using System.Linq;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class GetResidentRecordsUseCase : IGetResidentRecordsUseCase
    {
        private readonly IGetVisitInformationByPersonId _getVisitInformationByPersonId;
        private readonly IGetAllCaseNotesUseCase _getAllCaseNotesUseCase;

        public GetResidentRecordsUseCase(
            IGetVisitInformationByPersonId getVisitInformationByPersonId,
            IGetAllCaseNotesUseCase getAllCaseNotesUseCase)
        {
            _getVisitInformationByPersonId = getVisitInformationByPersonId;
            _getAllCaseNotesUseCase = getAllCaseNotesUseCase;
        }

        public List<ResidentHistoricRecord> Execute(long personId)
        {
            var visits = _getVisitInformationByPersonId.Execute(personId);
            var caseNotes = _getAllCaseNotesUseCase.Execute(personId).CaseNotes;

            var visitRecords = (from visit in visits select visit.ToSharedResponse(personId)).ToList();
            var caseNoteRecords = (from caseNote in caseNotes select caseNote.ToSharedResponse(personId)).ToList();

            var residentHistoricRecords = new List<ResidentHistoricRecord>();

            residentHistoricRecords.AddRange(visitRecords.Cast<ResidentHistoricRecord>());
            residentHistoricRecords.AddRange(caseNoteRecords.Cast<ResidentHistoricRecord>());

            return residentHistoricRecords;
        }
    }
}
