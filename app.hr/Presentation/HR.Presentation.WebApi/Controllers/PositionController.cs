using CleanSolution.Core.Application.DTOs;
using HR.Core.Application.DTOs;
using HR.Core.Application.Features.Positions.Commands;
using HR.Core.Application.Features.Positions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IMediator mediator;
        public PositionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<PositionsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPositionDto>>> Get([FromQuery] GetPositionRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                Response.Headers.Add("PageIndex", result.PageIndex.ToString());
                Response.Headers.Add("PageSize", result.PageSize.ToString());
                Response.Headers.Add("TotalPages", result.TotalPages.ToString());
                Response.Headers.Add("TotalCount", result.TotalCount.ToString());
                Response.Headers.Add("HasPreviousPage", result.HasPreviousPage.ToString());
                Response.Headers.Add("HasNextPage", result.HasNextPage.ToString());
                return Ok(result.Items);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET api/<PositionsController>/5
        [HttpGet("{id}")]
        public GetPositionDto Get(int id)
        {
            return null;
        }

        // POST api/<PositionsController>
        [HttpPost]
        public void Post([FromBody] CreatePosition request)
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
        public void Delete(int id)
        {
            mediator.Send(new DeletePosition { Id = id});
        }
    }
}
