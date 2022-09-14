using AutoMapper;
using Sire.Data.Dto.Inspection;
using Sire.Data.Dto.Master;
using Sire.Data.Dto.Operator;
using Sire.Data.Dto.Question;
using Sire.Data.Dto.ShipManagement;
using Sire.Data.Dto.Training;
using Sire.Data.Dto.UserMgt;
using Sire.Data.Entities.Common;
using Sire.Data.Entities.Inspection;
using Sire.Data.Entities.Master;
using Sire.Data.Entities.Question;
using Sire.Data.Entities.ShipManagement;
using Sire.Data.Entities.Training;
using Sire.Data.Entities.UserMgt;

namespace Sire.Api.Helpers
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<BaseEntity, GridAuditBase>().ReverseMap();
            CreateMap<Test, TestDto>().ReverseMap();
            CreateMap<Sire.Data.Entities.Operator.Operator, OperatorDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Fleet, FleetDto>().ReverseMap();
            CreateMap<Piq_Hvpq, Piq_HvpqDto>().ReverseMap();
            CreateMap<Vessel, VesselDto>().ReverseMap();
            CreateMap<User_Vessel, User_VesselDto>().ReverseMap();
            CreateMap<PIQ_HVPQ_Response, PIQ_HVPQ_ResponseDto>().ReverseMap();
            CreateMap<Training, TrainingDto>().ReverseMap();
            CreateMap<Vessel_Response_Piq_Hvpq, Vessel_Response_Piq_HvpqDto>().ReverseMap();
            CreateMap<Training_Question, Training_QuestionDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();

            CreateMap<QuetionSection, QuetionSectionDto>().ReverseMap();
            CreateMap<QuetionSubSection, QuetionSubSectionDto>().ReverseMap();
            CreateMap<Inspection, InspectionDto>().ReverseMap();
            CreateMap<Inspection_Question, Inspection_QuestionDto>().ReverseMap();
            CreateMap<InspectionResponse, InspectionResponseDto>().ReverseMap();
            CreateMap<TraningResponse, TraningResponseDto>().ReverseMap();
            CreateMap<QuestionResponse, QuestionResponseDto>().ReverseMap();
            CreateMap<User_Rank, User_RankDto>().ReverseMap();
            CreateMap<Data.Entities.Master.License, LicenseDto>().ReverseMap();
            CreateMap<RankGroup, RankGroupDto>().ReverseMap();
            CreateMap<Training_Task, Training_TaskDto>().ReverseMap();
        }
    }
}