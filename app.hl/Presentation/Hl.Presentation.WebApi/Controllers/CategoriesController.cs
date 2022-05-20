using Microsoft.AspNetCore.Mvc;
using Hl.Core.Application.Features.Categories.Commands;
using System.Collections.Generic;
using MediatR;
using System;
using Hl.Core.Application.Features.Calls.Queries;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Hl.Core.Application.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hl.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;
        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IEnumerable<GetCategoryDto>> Get([FromQuery] GetCategoryRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        [HttpPut]
        public async Task<GetCategoryDto> Put([FromBody] UpsertCategoryRequest request)
        {
            return await mediator.Send(request);
        }


    }
}
