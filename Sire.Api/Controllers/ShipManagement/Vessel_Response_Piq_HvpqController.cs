using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sire.Common.UnitOfWork;
using Sire.Data.Dto.ShipManagement;
using Sire.Data.Dto.Training;
using Sire.Data.Entities.ShipManagement;
using Sire.Domain.Context;
using Sire.Helper;
using Sire.Respository.ShipManagement;
using System;
using System.Collections.Generic;

namespace Sire.Api.Controllers.ShipManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class Vessel_Response_Piq_HvpqController : ControllerBase
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        private readonly IMapper _mapper;
        private readonly IVessel_Response_Piq_HvpqRepository _Vessel_Response_Piq_HvpqRepository;
        private readonly IUnitOfWork<SireContext> _uow;

        public Vessel_Response_Piq_HvpqController(IVessel_Response_Piq_HvpqRepository Vessel_Response_Piq_HvpqRepository,
            IUnitOfWork<SireContext> uow, IMapper mapper,
            IJwtTokenAccesser jwtTokenAccesser)
        {
            _Vessel_Response_Piq_HvpqRepository = Vessel_Response_Piq_HvpqRepository;
            _uow = uow;
            _mapper = mapper;
            _jwtTokenAccesser = jwtTokenAccesser;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] List<Vessel_Response_Piq_HvpqDto> HVPQResponseDto)
        {
            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);
            var test = _mapper.Map<List<Vessel_Response_Piq_Hvpq>>(HVPQResponseDto);

            foreach (var item in test)
            {
                if (item.Id == 0)
                    _Vessel_Response_Piq_HvpqRepository.Add(item);
                else
                    _Vessel_Response_Piq_HvpqRepository.Update(item);
            }
 
            if (_uow.Save() <= 0) throw new Exception("Creating HVPQ ResponseDto failed on save.");
            return Ok(0);
        }
    }
}
