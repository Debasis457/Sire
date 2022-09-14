using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sire.Domain.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inspection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Operator_id = table.Column<int>(nullable: false),
                    Vessel_Id = table.Column<int>(nullable: false),
                    InspectionType = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Started_at = table.Column<DateTime>(nullable: true),
                    Completed_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InspectionResponse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Inspection_Question_id = table.Column<int>(nullable: false),
                    ResponseType = table.Column<int>(nullable: false),
                    Response_Value = table.Column<string>(nullable: true),
                    Response_Comment = table.Column<string>(nullable: true),
                    Is_Answerable = table.Column<bool>(nullable: false),
                    media_link_1 = table.Column<string>(nullable: true),
                    media_link_2 = table.Column<string>(nullable: true),
                    media_link_3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionResponse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operator",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piq_Hvpq",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    question_id = table.Column<int>(nullable: false),
                    piq_hvpq_question = table.Column<string>(nullable: true),
                    Operand = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: true),
                    ResponseType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piq_Hvpq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Chapter = table.Column<int>(nullable: false),
                    Section = table.Column<int>(nullable: false),
                    Question_Number = table.Column<int>(nullable: false),
                    Questions = table.Column<string>(nullable: true),
                    Short_Text = table.Column<string>(nullable: true),
                    Question_Type = table.Column<string>(nullable: true),
                    Chemical = table.Column<bool>(nullable: true),
                    LNG = table.Column<bool>(nullable: true),
                    LPG = table.Column<bool>(nullable: true),
                    OIL = table.Column<bool>(nullable: true),
                    Conditional = table.Column<bool>(nullable: true),
                    Hardware_Response_Type = table.Column<string>(nullable: true),
                    Human_Response_Type = table.Column<string>(nullable: true),
                    Process_Response_Type = table.Column<string>(nullable: true),
                    Objective = table.Column<string>(nullable: true),
                    Industry_Guidance = table.Column<string>(nullable: true),
                    Inspection_Guidance = table.Column<string>(nullable: true),
                    Suggested_Inspection_actions = table.Column<string>(nullable: true),
                    Potential_for_Negative = table.Column<string>(nullable: true),
                    Checklist = table.Column<string>(nullable: true),
                    Expected_Evidence = table.Column<string>(nullable: true),
                    Rank = table.Column<int>(nullable: false),
                    Rank_Group_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionResponse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Response_Type = table.Column<int>(nullable: false),
                    Response_Type_Cateogary = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionResponse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuetionSection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuetionSection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RankGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Rank_Group = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    RoleType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    TestName = table.Column<string>(nullable: true),
                    TestGroupId = table.Column<int>(nullable: false),
                    Anticoagulant = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Operator_id = table.Column<int>(nullable: false),
                    Vessel_Id = table.Column<int>(nullable: false),
                    Training_number = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Started_at = table.Column<DateTime>(nullable: true),
                    Completed_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Training_Question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Training_Id = table.Column<int>(nullable: false),
                    Trainee_Id = table.Column<int>(nullable: false),
                    Question_Id = table.Column<int>(nullable: false),
                    Completed = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training_Question", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Training_Task",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Wbs_Number = table.Column<string>(nullable: true),
                    Task_Title = table.Column<string>(nullable: true),
                    Assessor = table.Column<string>(nullable: true),
                    Reviewer = table.Column<string>(nullable: true),
                    Master = table.Column<int>(nullable: false),
                    CH_Off = table.Column<int>(nullable: false),
                    Second_Off = table.Column<int>(nullable: false),
                    Third_Off = table.Column<int>(nullable: false),
                    Jr_Off = table.Column<int>(nullable: false),
                    CH_Eng = table.Column<int>(nullable: false),
                    Second_Eng = table.Column<int>(nullable: false),
                    Third_Eng = table.Column<int>(nullable: false),
                    Fourth_Eng = table.Column<int>(nullable: false),
                    ETO_Sr = table.Column<int>(nullable: false),
                    ETO_Jr = table.Column<int>(nullable: false),
                    Cargo_Eng_Sr = table.Column<int>(nullable: false),
                    Cargo_Eng_Jr = table.Column<int>(nullable: false),
                    Deck_Rating = table.Column<int>(nullable: false),
                    Eng_Rating = table.Column<int>(nullable: false),
                    Catering = table.Column<int>(nullable: false),
                    Others = table.Column<string>(nullable: true),
                    Accompanying_Officer = table.Column<string>(nullable: true),
                    Familiar1_RG_1 = table.Column<string>(nullable: true),
                    Familiar1_RG_2 = table.Column<string>(nullable: true),
                    Familiar1_Procedure = table.Column<string>(nullable: true),
                    Familiar2_RG_1 = table.Column<string>(nullable: true),
                    Familiar2_RG_2 = table.Column<string>(nullable: true),
                    Familiar2_Procedure = table.Column<string>(nullable: true),
                    Interview1_RG1 = table.Column<string>(nullable: true),
                    Interview1_RG2 = table.Column<string>(nullable: true),
                    Interview1_Procedure = table.Column<string>(nullable: true),
                    Interview2_RG1 = table.Column<string>(nullable: true),
                    Interview2_RG2 = table.Column<string>(nullable: true),
                    Interview2_Procedure = table.Column<string>(nullable: true),
                    Demonstrate1_RG1 = table.Column<string>(nullable: true),
                    Demonstrate1_RG2 = table.Column<string>(nullable: true),
                    Demonstrate1_Procedure = table.Column<string>(nullable: true),
                    Demonstrate2_RG1 = table.Column<string>(nullable: true),
                    Demonstrate2_RG2 = table.Column<string>(nullable: true),
                    Demonstrate2_Procedure = table.Column<string>(nullable: true),
                    Manuals_Plans_Procedures = table.Column<string>(nullable: true),
                    Certificates_Checklists_Records = table.Column<string>(nullable: true),
                    LogBooks_Entries = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training_Task", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TraningResponse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Training_Id = table.Column<int>(nullable: false),
                    Trainee_Id = table.Column<int>(nullable: false),
                    Question_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraningResponse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Rank",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Rank = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Rank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PIQ_HVPQ_Response",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    piq_hvpq_id = table.Column<int>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PIQ_HVPQ_Response", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PIQ_HVPQ_Response_Piq_Hvpq_piq_hvpq_id",
                        column: x => x.piq_hvpq_id,
                        principalTable: "Piq_Hvpq",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vessel_Response_Piq_Hvpq",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Vessel_Id = table.Column<int>(nullable: false),
                    Response = table.Column<string>(nullable: true),
                    Piq_Hvpq_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vessel_Response_Piq_Hvpq", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vessel_Response_Piq_Hvpq_Piq_Hvpq_Vessel_Id",
                        column: x => x.Vessel_Id,
                        principalTable: "Piq_Hvpq",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuetionSubSection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    QuetionSectionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuetionSubSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuetionSubSection_QuetionSection_QuetionSectionId",
                        column: x => x.QuetionSectionId,
                        principalTable: "QuetionSection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: true),
                    Operator_Id = table.Column<int>(nullable: false),
                    Full_Name = table.Column<string>(nullable: true),
                    is_admin = table.Column<bool>(nullable: true),
                    Rank_Id = table.Column<int>(nullable: false),
                    Rank_Group_Id = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_RankGroup_Rank_Group_Id",
                        column: x => x.Rank_Group_Id,
                        principalTable: "RankGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_User_Rank_Rank_Id",
                        column: x => x.Rank_Id,
                        principalTable: "User_Rank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fleet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Fleet_Head_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fleet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fleet_User_Fleet_Head_Id",
                        column: x => x.Fleet_Head_Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inspection_Question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    inspection_id = table.Column<int>(nullable: false),
                    question_id = table.Column<int>(nullable: false),
                    assessor_id = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    reviewer_id = table.Column<int>(nullable: false),
                    comment_by_reviewer = table.Column<bool>(nullable: false),
                    assesment_completed = table.Column<bool>(nullable: false),
                    review_completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspection_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspection_Question_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_Vessel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    User_Id = table.Column<int>(nullable: false),
                    Vessel_Id = table.Column<int>(nullable: false),
                    is_own_vessel = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Vessel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Vessel_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vessel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Operator_id = table.Column<int>(nullable: false),
                    Fleet_id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Vessel_Type = table.Column<int>(nullable: false),
                    IMO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vessel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vessel_Fleet_Fleet_id",
                        column: x => x.Fleet_id,
                        principalTable: "Fleet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vessel_Operator_Operator_id",
                        column: x => x.Operator_id,
                        principalTable: "Operator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Vessel_Id = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Expires_At = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.Id);
                    table.ForeignKey(
                        name: "FK_License_Vessel_Vessel_Id",
                        column: x => x.Vessel_Id,
                        principalTable: "Vessel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fleet_Fleet_Head_Id",
                table: "Fleet",
                column: "Fleet_Head_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inspection_Question_UserId",
                table: "Inspection_Question",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_License_Vessel_Id",
                table: "License",
                column: "Vessel_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PIQ_HVPQ_Response_piq_hvpq_id",
                table: "PIQ_HVPQ_Response",
                column: "piq_hvpq_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuetionSubSection_QuetionSectionId",
                table: "QuetionSubSection",
                column: "QuetionSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Rank_Group_Id",
                table: "User",
                column: "Rank_Group_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Rank_Id",
                table: "User",
                column: "Rank_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Vessel_User_Id",
                table: "User_Vessel",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vessel_Fleet_id",
                table: "Vessel",
                column: "Fleet_id");

            migrationBuilder.CreateIndex(
                name: "IX_Vessel_Operator_id",
                table: "Vessel",
                column: "Operator_id");

            migrationBuilder.CreateIndex(
                name: "IX_Vessel_Response_Piq_Hvpq_Vessel_Id",
                table: "Vessel_Response_Piq_Hvpq",
                column: "Vessel_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inspection");

            migrationBuilder.DropTable(
                name: "Inspection_Question");

            migrationBuilder.DropTable(
                name: "InspectionResponse");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "PIQ_HVPQ_Response");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "QuestionResponse");

            migrationBuilder.DropTable(
                name: "QuetionSubSection");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Training_Question");

            migrationBuilder.DropTable(
                name: "Training_Task");

            migrationBuilder.DropTable(
                name: "TraningResponse");

            migrationBuilder.DropTable(
                name: "User_Vessel");

            migrationBuilder.DropTable(
                name: "Vessel_Response_Piq_Hvpq");

            migrationBuilder.DropTable(
                name: "Vessel");

            migrationBuilder.DropTable(
                name: "QuetionSection");

            migrationBuilder.DropTable(
                name: "Piq_Hvpq");

            migrationBuilder.DropTable(
                name: "Fleet");

            migrationBuilder.DropTable(
                name: "Operator");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "RankGroup");

            migrationBuilder.DropTable(
                name: "User_Rank");
        }
    }
}
