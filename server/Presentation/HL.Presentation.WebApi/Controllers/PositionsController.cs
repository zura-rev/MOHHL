﻿using HL.Core.Application.DTOs;
using HL.Core.Application.Features.Positions.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace HL.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IMediator mediator;
        public PositionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<PositionsController>
        [HttpGet]
        public IEnumerable<GetPositionDto> Get()
        {
            return null;
        }

        // GET api/<PositionsController>/5
        [HttpGet("{id}")]
        public GetPositionDto Get(int id)
        {
            return null;
        }

        // POST api/<PositionsController>
        [HttpPost]
        public void Post([FromBody] CreatePositionRequest request)
        {
            mediator.Send(request);    
        }

        // PUT api/<PositionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] SetPositionDto value)
        {
        }

        // DELETE api/<PositionsController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            mediator.Send(new DeletePositionRequest { Id = id});
        }
    }
}
