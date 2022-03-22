using HR.Core.Application.DTOs;
using HR.Core.Application.Features.Employees.Queries;
using HR.Core.Application.Features.Sections.Commands;
using HR.Core.Application.Features.Sections.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<GetSectionDto>>> Get()
        {
            var result = await mediator.Send(new GetSectionRequest());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetSectionDto>> Get(int id)
        {
            var resuest = new GetSectionByIdRequest(id);
            var result = await mediator.Send(resuest);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GetSectionDto>> Post([FromBody] UpsertSectionRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<GetSectionDto>> Put([FromBody] UpsertSectionRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var result = await mediator.Send(id);
            return Ok(result);
        }

    }
}
