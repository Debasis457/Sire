﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sire.Domain.Context;

namespace Sire.Domain.Migrations
{
    [DbContext(typeof(SireContext))]
    partial class SireContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sire.Data.Entities.Inspection.Inspection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Completed_at");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Description");

                    b.Property<int?>("InspectionType");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("Operator_id");

                    b.Property<DateTime?>("Started_at");

                    b.Property<int>("Vessel_Id");

                    b.HasKey("Id");

                    b.ToTable("Inspection");
                });

            modelBuilder.Entity("Sire.Data.Entities.Inspection.Inspection_Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("UserId");

                    b.Property<bool?>("assesment_completed");

                    b.Property<int?>("assessor_id");

                    b.Property<bool?>("comment_by_reviewer");

                    b.Property<int?>("inspection_id");

                    b.Property<int>("question_id");

                    b.Property<bool?>("review_completed");

                    b.Property<int?>("reviewer_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Inspection_Question");
                });

            modelBuilder.Entity("Sire.Data.Entities.Inspection.InspectionResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int>("Inspection_Question_id");

                    b.Property<bool>("Is_Answerable");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("ResponseType");

                    b.Property<string>("Response_Comment");

                    b.Property<string>("Response_Value");

                    b.Property<string>("media_link_1");

                    b.Property<string>("media_link_2");

                    b.Property<string>("media_link_3");

                    b.HasKey("Id");

                    b.ToTable("InspectionResponse");
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.Fleet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int>("Fleet_Head_Id");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Fleet_Head_Id");

                    b.ToTable("Fleet");
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.License", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<DateTime>("Created_At");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Expires_At");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("Vessel_Id");

                    b.HasKey("Id");

                    b.HasIndex("Vessel_Id");

                    b.ToTable("License");
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.QuetionSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("QuetionSection");
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.QuetionSubSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("QuetionSectionId");

                    b.HasKey("Id");

                    b.HasIndex("QuetionSectionId");

                    b.ToTable("QuetionSubSection");
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.RankGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Rank_Group");

                    b.HasKey("Id");

                    b.ToTable("RankGroup");
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Anticoagulant");

                    b.Property<int?>("CompanyId");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Notes");

                    b.Property<int>("TestGroupId");

                    b.Property<string>("TestName");

                    b.HasKey("Id");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.User_Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Rank");

                    b.HasKey("Id");

                    b.ToTable("User_Rank");
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.User_Vessel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("FleetId");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("User_Id");

                    b.Property<int?>("VesselId");

                    b.Property<int>("Vessel_Id");

                    b.Property<bool?>("is_own_vessel");

                    b.HasKey("Id");

                    b.HasIndex("FleetId");

                    b.HasIndex("User_Id");

                    b.HasIndex("VesselId");

                    b.ToTable("User_Vessel");
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.Vessel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Flag");

                    b.Property<int>("Fleet_id");

                    b.Property<string>("IMO");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("Operator_id");

                    b.Property<int>("Vessel_Type");

                    b.HasKey("Id");

                    b.HasIndex("Fleet_id");

                    b.HasIndex("Operator_id");

                    b.ToTable("Vessel");
                });

            modelBuilder.Entity("Sire.Data.Entities.Operator.Operator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Description");

                    b.Property<string>("Imo_Number");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("Vessel_Count");

                    b.HasKey("Id");

                    b.ToTable("Operator");
                });

            modelBuilder.Entity("Sire.Data.Entities.Question.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Chapter");

                    b.Property<string>("Checklist");

                    b.Property<bool?>("Chemical");

                    b.Property<bool?>("Conditional");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int>("DAssessore");

                    b.Property<int>("DReviewer");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Expected_Evidence");

                    b.Property<int>("Hardware_Response_Type");

                    b.Property<int>("Human_Response_Type");

                    b.Property<string>("Industry_Guidance");

                    b.Property<string>("Inspection_Guidance");

                    b.Property<bool?>("LNG");

                    b.Property<bool?>("LPG");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<bool?>("OIL");

                    b.Property<string>("Objective");

                    b.Property<string>("Potential_for_Negative");

                    b.Property<int>("Process_Response_Type");

                    b.Property<int>("Question_Number");

                    b.Property<string>("Question_Type");

                    b.Property<string>("Questions");

                    b.Property<int>("Rank");

                    b.Property<int>("Rank_Group_Id");

                    b.Property<int>("Section");

                    b.Property<string>("Section_Name");

                    b.Property<string>("Short_Question");

                    b.Property<string>("Suggested_Inspection_actions");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("Sire.Data.Entities.Question.QuestionResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("QuestionId");

                    b.Property<int>("Response_Type");

                    b.Property<int>("Response_Type_Cateogary");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("QuestionResponse");
                });

            modelBuilder.Entity("Sire.Data.Entities.Question.QuestionRoviq", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("QuestionId");

                    b.Property<string>("Roviq_Type");

                    b.HasKey("Id");

                    b.ToTable("QuestionRoviq");
                });

            modelBuilder.Entity("Sire.Data.Entities.ShipManagement.Piq_Hvpq", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Operand");

                    b.Property<string>("PIQHVPQCode");

                    b.Property<int?>("QuestionId");

                    b.Property<string>("Response");

                    b.Property<int>("ResponseType");

                    b.Property<string>("Type");

                    b.Property<string>("piq_hvpq_question");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Piq_Hvpq");
                });

            modelBuilder.Entity("Sire.Data.Entities.ShipManagement.PIQ_HVPQ_Response", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("Piq_HvpqId");

                    b.Property<string>("piq_hvpq_id");

                    b.Property<string>("value");

                    b.HasKey("Id");

                    b.HasIndex("Piq_HvpqId");

                    b.ToTable("PIQ_HVPQ_Response");
                });

            modelBuilder.Entity("Sire.Data.Entities.ShipManagement.Vessel_Response_Piq_Hvpq", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Piq_Hvpq_Id");

                    b.Property<string>("Response");

                    b.Property<int>("Vessel_Id");

                    b.HasKey("Id");

                    b.ToTable("Vessel_Response_Piq_Hvpq");
                });

            modelBuilder.Entity("Sire.Data.Entities.Training.Training", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Completed_at");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Description");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("Operator_id");

                    b.Property<DateTime?>("Started_at");

                    b.Property<int>("Training_number");

                    b.Property<int>("Vessel_Id");

                    b.HasKey("Id");

                    b.ToTable("Training");
                });

            modelBuilder.Entity("Sire.Data.Entities.Training.Training_Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Completed");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("Question_Id");

                    b.Property<int>("Trainee_Id");

                    b.Property<int>("Training_Id");

                    b.HasKey("Id");

                    b.ToTable("Training_Question");
                });

            modelBuilder.Entity("Sire.Data.Entities.Training.Training_Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Accompanying_Officer");

                    b.Property<string>("Assessor");

                    b.Property<int>("CH_Eng");

                    b.Property<int>("CH_Off");

                    b.Property<int>("Cargo_Eng_Jr");

                    b.Property<int>("Cargo_Eng_Sr");

                    b.Property<int>("Catering");

                    b.Property<string>("Certificates_Checklists_Records");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int>("Deck_Rating");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Demonstrate1_Procedure");

                    b.Property<string>("Demonstrate1_RG1");

                    b.Property<string>("Demonstrate1_RG2");

                    b.Property<string>("Demonstrate2_Procedure");

                    b.Property<string>("Demonstrate2_RG1");

                    b.Property<string>("Demonstrate2_RG2");

                    b.Property<int>("ETO_Jr");

                    b.Property<int>("ETO_Sr");

                    b.Property<int>("Eng_Rating");

                    b.Property<string>("Familiar1_Procedure");

                    b.Property<string>("Familiar1_RG_1");

                    b.Property<string>("Familiar1_RG_2");

                    b.Property<string>("Familiar2_Procedure");

                    b.Property<string>("Familiar2_RG_1");

                    b.Property<string>("Familiar2_RG_2");

                    b.Property<int>("Fourth_Eng");

                    b.Property<string>("Interview1_Procedure");

                    b.Property<string>("Interview1_RG1");

                    b.Property<string>("Interview1_RG2");

                    b.Property<string>("Interview2_Procedure");

                    b.Property<string>("Interview2_RG1");

                    b.Property<string>("Interview2_RG2");

                    b.Property<int>("Jr_Off");

                    b.Property<string>("LogBooks_Entries");

                    b.Property<string>("Manuals_Plans_Procedures");

                    b.Property<int>("Master");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Others");

                    b.Property<string>("Reviewer");

                    b.Property<int>("Second_Eng");

                    b.Property<int>("Second_Off");

                    b.Property<string>("Task_Title");

                    b.Property<int>("Third_Eng");

                    b.Property<int>("Third_Off");

                    b.Property<string>("Wbs_Number");

                    b.HasKey("Id");

                    b.ToTable("Training_Task");
                });

            modelBuilder.Entity("Sire.Data.Entities.Training.TraningResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("Question_Id");

                    b.Property<int>("Trainee_Id");

                    b.Property<int>("Training_Id");

                    b.HasKey("Id");

                    b.ToTable("TraningResponse");
                });

            modelBuilder.Entity("Sire.Data.Entities.UserMgt.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Description")
                        .HasColumnName("Description")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("RoleType");

                    b.Property<int>("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Sire.Data.Entities.UserMgt.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DeletedBy");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("EmailId");

                    b.Property<int>("MobileNo");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Password");

                    b.Property<int?>("Rank_Group_Id");

                    b.Property<int?>("Rank_Id");

                    b.Property<string>("UserID");

                    b.Property<string>("UserName");

                    b.Property<int>("UserType");

                    b.Property<int?>("Vessel_Id");

                    b.Property<bool?>("is_admin");

                    b.HasKey("Id");

                    b.HasIndex("Rank_Group_Id");

                    b.HasIndex("Rank_Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Sire.Data.Entities.Inspection.Inspection_Question", b =>
                {
                    b.HasOne("Sire.Data.Entities.UserMgt.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.Fleet", b =>
                {
                    b.HasOne("Sire.Data.Entities.UserMgt.User", "User")
                        .WithMany()
                        .HasForeignKey("Fleet_Head_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.License", b =>
                {
                    b.HasOne("Sire.Data.Entities.Master.Vessel", "Vessel")
                        .WithMany()
                        .HasForeignKey("Vessel_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.QuetionSubSection", b =>
                {
                    b.HasOne("Sire.Data.Entities.Master.QuetionSection", "QuetionSection")
                        .WithMany("QuetionSubSection")
                        .HasForeignKey("QuetionSectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.User_Vessel", b =>
                {
                    b.HasOne("Sire.Data.Entities.Master.Fleet", "Fleet")
                        .WithMany()
                        .HasForeignKey("FleetId");

                    b.HasOne("Sire.Data.Entities.UserMgt.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sire.Data.Entities.Master.Vessel", "Vessel")
                        .WithMany()
                        .HasForeignKey("VesselId");
                });

            modelBuilder.Entity("Sire.Data.Entities.Master.Vessel", b =>
                {
                    b.HasOne("Sire.Data.Entities.Master.Fleet", "Fleet")
                        .WithMany()
                        .HasForeignKey("Fleet_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sire.Data.Entities.Operator.Operator", "Operator")
                        .WithMany()
                        .HasForeignKey("Operator_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sire.Data.Entities.Question.Question", b =>
                {
                    b.HasOne("Sire.Data.Entities.UserMgt.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Sire.Data.Entities.ShipManagement.Piq_Hvpq", b =>
                {
                    b.HasOne("Sire.Data.Entities.Question.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("Sire.Data.Entities.ShipManagement.PIQ_HVPQ_Response", b =>
                {
                    b.HasOne("Sire.Data.Entities.ShipManagement.Piq_Hvpq")
                        .WithMany("PIQ_HVPQ_Response")
                        .HasForeignKey("Piq_HvpqId");
                });

            modelBuilder.Entity("Sire.Data.Entities.UserMgt.Role", b =>
                {
                    b.HasOne("Sire.Data.Entities.UserMgt.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sire.Data.Entities.UserMgt.User", b =>
                {
                    b.HasOne("Sire.Data.Entities.Master.RankGroup", "RankGroup")
                        .WithMany()
                        .HasForeignKey("Rank_Group_Id");

                    b.HasOne("Sire.Data.Entities.Master.User_Rank", "User_Rank")
                        .WithMany()
                        .HasForeignKey("Rank_Id");
                });
#pragma warning restore 612, 618
        }
    }
}
