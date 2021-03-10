using System;

namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
{
    public class CaseNoteInformation
    {

        public string MosaicId { get; set; }

        public int CaseNoteId { get; set; }

        public int PersonVisitId { get; set; }

        public string NoteType { get; set; }

        public string CaseNoteTitle { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public string CaseNoteContent { get; set; }



        //id                                       numeric(9)  not null,
        //person_id                                numeric(9)  not null,
        // SKIP episode_id                               numeric(9),
        //person_visit_id                          numeric(9),
        //note_type                                varchar(16) not null,
        //title                                    varchar(100),
        //effective_date                           timestamp,
        //created_on                               timestamp,
        //created_by                               varchar(30),
        //last_updated_by                          varchar(64),
        //last_updated_on                          timestamp,
        //frozen_flag                              varchar(1),
        //note                                     text,
        root_case_note                           numeric(9),
        // SKIP created_acting_for                       varchar(30),
        // SKIP updated_acting_for                       varchar(30),
        // SKIP significant_event_flag                   varchar(1),
        completed_date                           timestamp,
            timeout_date                             timestamp,
        state                                    varchar(16) not null,
        copy_of_case_note_id                     numeric(9),
        copied_by                                varchar(30),
        copied_acting_for                        varchar(30),
        copied_date                              timestamp,
            message_sent_acting_for                  varchar(30),
        message_sent_by                          varchar(30),
        message_sent_datetime                    timestamp,
            message_text                             varchar(4000),


    }
}
