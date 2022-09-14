using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Master;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Master;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Api.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankGroupController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IRankGroupRepository _rankgroupRepository;
        private readonly IUnitOfWork<SireContext> _uow;
        public RankGroupController(IRankGroupRepository rankGroupRepository,
                IUnitOfWork<SireContext> uow, IMapper mapper,
                IJwtTokenAccesser jwtTokenAccesser)
        {
            _rankgroupRepository = rankGroupRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }
        [AllowAnonymous]
    [HttpGet]
    [HttpGet("{isDeleted:bool?}")]
    public IActionResult Get(bool isDeleted)
    {
        var tests = _rankgroupRepository.FindByInclude(x => x.IsDeleted == isDeleted)
            .OrderByDescending(x => x.Id).ToList();
        var testsDto = _mapper.Map<IEnumerable<RankGroupDto>>(tests);

        return Ok(testsDto);
    }

        [HttpGet]
        [Route("GetRankGroupDropDown")]
        public IActionResult GetRankGroupDropDown()
        {
            return Ok(_rankgroupRepository.GetRankGroupDropDown());
        }
    }
}
