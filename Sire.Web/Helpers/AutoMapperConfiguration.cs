using AutoMapper;
using Sire.Data.Dto.Question;
using Sire.Web.Models;

namespace Sire.Web.Helpers
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<QuestionDto, InspectionQuestionDtoModel>().ReverseMap();
        }
    }
}
