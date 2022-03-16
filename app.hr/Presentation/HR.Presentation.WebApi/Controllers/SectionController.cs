using HR.Core.Application.DTOs;
using HR.Core.Application.Features.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly IMediator mediator;

        public SectionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetSectionDto>>> Get([FromQuery] GetSectionRequest request)
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

        [HttpGet("{id}")]
        public GetSectionDto Get(int id)
        {
            return null;
        }

        [HttpPost]
        public void Post([FromBody] SetSectionDto value)
        {

        }

        [HttpPut("{id}")]
        public void Put(int id)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
