using Sire.Common.UnitOfWork;
using Sire.Helper;
using Sire.Respository.Master;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sire.Respository.UserMgt;
using Sire.Respository.Operator;
using Sire.Respository.ShipManagement;
using Sire.Respository.Training;
using Sire.Respository.Question;
using Sire.Respository.Inspection;

namespace Sire.Api.Helpers
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IUnitOfWork<>),
                typeof(UnitOfWork<>));

            services.AddScoped<IJwtTokenAccesser, JwtTokenAccesser>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IOperatorRepository, OperatorRepository>();
            services.AddScoped<IFleetRepository, FleetRepository>();
            services.AddScoped<IOperatorRepository, OperatorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVesselRepository, VesselRepository>();
            services.AddScoped<IUser_VesselRepository, User_VesselRepository>();
            services.AddScoped<IPiq_HvpqRepository, Piq_HvpqRepository>();
            services.AddScoped<IPIQ_HVPQReponseRepository, PIQ_HVPQResponseRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();
            services.AddScoped<IVessel_Response_Piq_HvpqRepository, Vessel_Response_Piq_HvpqRepository>();
            services.AddScoped<ITrainingQuestionRepository, TrainingQuestionRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();

            services.AddScoped<IQuetionSectionRepository,  QuetionSectionRepository>();
            services.AddScoped<IQuetionSubSectionRepository,  QuetionSubSectionRepository>();
            services.AddScoped<IInspection_QuestionRepository, Inspection_QuestionRepository>();
            services.AddScoped<IInspectionRepository, InspectionRepository>();
            services.AddScoped<IInspectionResponseRepository, InspectionResponseRepository>();
            services.AddScoped<ITraningResponseRepository, TraningResponseRepository>();
            services.AddScoped<IQuestionResponseRepository, QuestionResponseRepository>();
            services.AddScoped<IUser_RankRepository, User_RankRepository>();
            services.AddScoped<ILicenseRepository, LicenseRepository>();
            services.AddScoped<IRankGroupRepository, RankGroupRepository>();
            services.AddScoped<ITraining_TaskRepository, Training_TaskRepository>();
        }
    }
}