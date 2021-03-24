﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure.Migrations
{
    [DbContext(typeof(SocialCareContext))]
    partial class SocialCareContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ResidentsSocialCarePlatformApi.V1.Infrastructure.Address", b =>
                {
                    b.Property<long>("PersonAddressId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(9)
                        .HasColumnType("bigint")
                        .HasColumnName("ref_addresses_people_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AddressId")
                        .HasMaxLength(9)
                        .HasColumnType("bigint")
                        .HasColumnName("ref_address_id");

                    b.Property<string>("AddressLines")
                        .HasMaxLength(464)
                        .HasColumnType("character varying(464)")
                        .HasColumnName("address");

                    b.Property<string>("ContactAddressFlag")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("is_contact_address");

                    b.Property<string>("DisplayAddressFlag")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("is_display_address");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("end_date");

                    b.Property<long?>("PersonId")
                        .HasMaxLength(16)
                        .HasColumnType("bigint")
                        .HasColumnName("person_id");

                    b.Property<string>("PostCode")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("post_code");

                    b.Property<long?>("Uprn")
                        .HasMaxLength(15)
                        .HasColumnType("bigint")
                        .HasColumnName("unique_id");

                    b.HasKey("PersonAddressId");

                    b.HasIndex("PersonId");

                    b.ToTable("dm_addresses", "dbo");
                });

            modelBuilder.Entity("ResidentsSocialCarePlatformApi.V1.Infrastructure.CaseNote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(9)
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("completed_date");

                    b.Property<string>("CopiedBy")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("copied_by");

                    b.Property<DateTime?>("CopiedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("copied_date");

                    b.Property<long?>("CopyOfCaseNoteId")
                        .HasMaxLength(9)
                        .HasColumnType("bigint")
                        .HasColumnName("copy_of_case_note_id");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on");

                    b.Property<DateTime?>("EffectiveDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("effective_date");

                    b.Property<string>("LastUpdatedBy")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("last_updated_by");

                    b.Property<DateTime?>("LastUpdatedOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_updated_on");

                    b.Property<string>("Note")
                        .HasColumnType("text")
                        .HasColumnName("note");

                    b.Property<string>("NoteType")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("note_type");

                    b.Property<long>("PersonId")
                        .HasMaxLength(9)
                        .HasColumnType("bigint")
                        .HasColumnName("person_id");

                    b.Property<long?>("PersonVisitId")
                        .HasMaxLength(9)
                        .HasColumnType("bigint")
                        .HasColumnName("person_visit_id");

                    b.Property<long?>("RootCaseNoteId")
                        .HasMaxLength(9)
                        .HasColumnType("bigint")
                        .HasColumnName("root_case_note");

                    b.Property<string>("State")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("state");

                    b.Property<DateTime?>("TimeoutDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("timeout_date");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("case_notes", "dbo");
                });

            modelBuilder.Entity("ResidentsSocialCarePlatformApi.V1.Infrastructure.NoteType", b =>
                {
                    b.Property<string>("Type")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("note_type");

                    b.Property<string>("Description")
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("note_type_description");

                    b.HasKey("Type");

                    b.ToTable("dm_case_note_types", "dbo");
                });

            modelBuilder.Entity("ResidentsSocialCarePlatformApi.V1.Infrastructure.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(16)
                        .HasColumnType("bigint")
                        .HasColumnName("person_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AgeContext")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("context_flag");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(240)
                        .HasColumnType("character varying(240)")
                        .HasColumnName("email_address");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("first_name");

                    b.Property<string>("FullName")
                        .HasMaxLength(62)
                        .HasColumnType("character varying(62)")
                        .HasColumnName("full_name");

                    b.Property<string>("Gender")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("last_name");

                    b.Property<string>("Nationality")
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("nationality");

                    b.Property<long?>("NhsNumber")
                        .HasMaxLength(10)
                        .HasColumnType("bigint")
                        .HasColumnName("nhs_id");

                    b.Property<string>("PersonIdLegacy")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("person_id_legacy");

                    b.Property<string>("Restricted")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("restricted");

                    b.Property<string>("Title")
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("dm_persons", "dbo");
                });

            modelBuilder.Entity("ResidentsSocialCarePlatformApi.V1.Infrastructure.TelephoneNumber", b =>
                {
                    b.Property<long>("Id")
                        .HasMaxLength(9)
                        .HasColumnType("bigint")
                        .HasColumnName("telephone_number_id");

                    b.Property<long>("PersonId")
                        .HasMaxLength(16)
                        .HasColumnType("bigint")
                        .HasColumnName("person_id");

                    b.Property<string>("Number")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("telephone_number");

                    b.Property<string>("Type")
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("telephone_number_type");

                    b.HasKey("Id", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("dm_telephone_numbers", "dbo");
                });

            modelBuilder.Entity("ResidentsSocialCarePlatformApi.V1.Infrastructure.Worker", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(9)
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Accessible")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("accessible");

                    b.Property<string>("ContextCode")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("context_code");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(240)
                        .HasColumnType("character varying(240)")
                        .HasColumnName("email_address");

                    b.Property<string>("FirstNames")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("first_names");

                    b.Property<string>("LastNames")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("last_names");

                    b.Property<string>("SystemUserId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("system_user_id");

                    b.HasKey("Id");

                    b.ToTable("workers", "dbo");
                });

            modelBuilder.Entity("ResidentsSocialCarePlatformApi.V1.Infrastructure.Address", b =>
                {
                    b.HasOne("ResidentsSocialCarePlatformApi.V1.Infrastructure.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("ResidentsSocialCarePlatformApi.V1.Infrastructure.TelephoneNumber", b =>
                {
                    b.HasOne("ResidentsSocialCarePlatformApi.V1.Infrastructure.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });
#pragma warning restore 612, 618
        }
    }
}
