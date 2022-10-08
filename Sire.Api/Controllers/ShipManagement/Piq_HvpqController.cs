using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.Operator;
using Sire.Data.Dto.ShipManagement;
using Sire.Data.Entities.ShipManagement;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.Operator;
using Sire.Respository.ShipManagement;
using System.Collections.Generic;
using System.Linq;

namespace Sire.Api.Controllers.ShipManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class Piq_HvpqController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IPiq_HvpqRepository _piq_HvpqRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public Piq_HvpqController(IPiq_HvpqRepository piq_HvpqRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _piq_HvpqRepository = piq_HvpqRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }


        [AllowAnonymous]
        [HttpGet("GetPIQ")]
        public IActionResult GetPIQ()
        {
            var tests = _piq_HvpqRepository.FindByInclude(x => x.Type.ToLower() == "piq", x => x.PIQ_HVPQ_Response)
              .OrderByDescending(x => x.Id).ToList();

            var testsDto = _mapper.Map<IEnumerable<Piq_HvpqDto>>(tests);

            return Ok(testsDto);
        }


        [AllowAnonymous]
        [HttpGet("GetHVPQ")]
        public IActionResult GetHVPQ()
        {
            var tests = _piq_HvpqRepository.FindByInclude(x => x.Type.ToLower() == "hvpq", x => x.PIQ_HVPQ_Response)
              .OrderByDescending(x => x.Id).ToList();

            var testsDto = _mapper.Map<IEnumerable<Piq_HvpqDto>>(tests);

            return Ok(testsDto);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            //var tests = _piq_HvpqRepository.AllIncluding(x=>x.PIQ_HVPQ_Response)
            //    .OrderByDescending(x => x.Id).ToList();

            var tests = (from data in _uow.Context.Piq_Hvpq
                         join ans in _uow.Context.Vessel_Response_Piq_Hvpq on data.PIQHVPQCode equals ans.Piq_Hvpq_Id into aa
                         from ans in aa.DefaultIfEmpty()

                         select new Piq_HvpqDto
                         {
                             PIQHVPQCode = data.PIQHVPQCode,
                             piq_hvpq_question = data.piq_hvpq_question,
                             Type = data.Type,
                             ResponseType = data.ResponseType,
                             Answered = ans.Response
                         }).Distinct();

            var cData = tests.ToList();
            foreach (var item in cData)
            {
                var responselist = _uow.Context.PIQ_HVPQ_Response.Where(x => x.piq_hvpq_id == item.PIQHVPQCode).ToList();
                if (responselist.Count > 0)
                    item.PIQ_HVPQ_Response = _mapper.Map<List<PIQ_HVPQ_ResponseDto>>(responselist); ;
            }
            var testsDto = _mapper.Map<IEnumerable<Piq_HvpqDto>>(cData);
            var PVQ = testsDto.Where(x => x.Type.ToLower() == "piq").ToList();
            var HVPQ = testsDto.Where(x => x.Type.ToLower() == "hvpq").ToList();

            return Ok(new PIQ_HVPQWrapper { PIQ = PVQ, HVPQ = HVPQ });
        }
    }
}
