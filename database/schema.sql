create table DM_PERSONS
(
    PERSON_ID             numeric(16) not null
        constraint XPKDM_PERSONS
            primary key,
    SSDA903_ID            varchar(10),
    NHS_ID                numeric(10),
    SCN_ID                numeric(9),
    UPN_ID                varchar(13),
    FORMER_UPN_ID         varchar(13),
    FULL_NAME             varchar(62) not null,
    TITLE                 varchar(8),
    FIRST_NAME            varchar(30),
    LAST_NAME             varchar(30),
    DATE_OF_BIRTH         datetime,
    DATE_OF_DEATH         datetime,
    GENDER                varchar(1)  not null,
    RESTRICTED            varchar(1),
    PERSON_ID_LEGACY      varchar(16) not null,
    FULL_ETHNICITY_CODE   varchar(33),
    COUNTRY_OF_BIRTH_CODE varchar(16),
    IS_CHILD_LEGACY       varchar(1),
    IS_ADULT_LEGACY       varchar(1),
    NATIONALITY           varchar(80),
    RELIGION              varchar(80),
    MARITAL_STATUS        varchar(80),
    FIRST_LANGUAGE        varchar(100),
    FLUENCY_IN_ENGLISH    varchar(100),
    EMAIL_ADDRESS         varchar(240),
    CONTEXT_FLAG          varchar(1),
    SCRA_ID               varchar(13),
    INTERPRETER_REQUIRED  varchar(1)
)
go

create index xif1dm_persons
    on DM_PERSONS (FULL_ETHNICITY_CODE)
go

create unique index xif2dm_persons
    on DM_PERSONS (PERSON_ID_LEGACY)
go


create table DM_ADDRESSES
(
    REF_ADDRESSES_PEOPLE_ID numeric(9) not null
        constraint XPKDM_ADDRESSES
            primary key,
    REF_ADDRESS_ID          numeric(9) not null,
    PERSON_ID               numeric(16),
    START_DATE              datetime,
    END_DATE                datetime,
    ADDRESS                 varchar(464),
    POST_CODE               varchar(16),
    DISTRICT                varchar(80),
    PAF_AUTHORITY           varchar(80),
    WARD                    varchar(80),
    IS_IN_LA_AREA           varchar(1),
    ADDRESS_TYPE            varchar(16),
    IS_CONTACT_ADDRESS      varchar(1),
    IS_DISPLAY_ADDRESS      varchar(1),
    HOUSING_TENURE          varchar(16),
    IS_HMO                  varchar(1),
    OZ_LGA_NAME             varchar(80),
    OZ_LGA_NUMBER           numeric(9),
    OZ_SLA_ID               numeric(9),
    OZ_LHD_ID               numeric(9),
    OZ_HACC_ID              numeric(9),
    OZ_PHAMS_ID             numeric(9),
    ACCESS_NOTES            varchar(2000),
    EASTING                 numeric(10, 2),
    NORTHING                numeric(10, 2),
    UNIQUE_ID               numeric(15)
)
go

create table DM_TELEPHONE_NUMBERS
(
    TELEPHONE_NUMBER_ID   numeric(9)  not null,
    PERSON_ID             numeric(16) not null,
    TELEPHONE_NUMBER      varchar(32),
    TELEPHONE_NUMBER_TYPE varchar(80) not null,
    constraint XPKDM_TELEPHONE_NUMBERS
        primary key (TELEPHONE_NUMBER_ID, PERSON_ID)
)
go
