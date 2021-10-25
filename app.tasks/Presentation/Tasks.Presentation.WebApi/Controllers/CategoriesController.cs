using Microsoft.AspNetCore.Mvc;
using Tasks.Core.Application.Features.Categories.Commands;
using System.Collections.Generic;
using MediatR;
using System;
using Tasks.Core.Application.Features.Calls.Queries;
using Tasks.Core.Domain.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tasks.Core.Application.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasks.Presentation.WebApi.Controllers
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

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IEnumerable<GetCategoryDto>> Get([FromQuery] GetCategoriesRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public GetCategoryDto Post([FromBody] CreateCategoryRequest request)
        {
            var res = mediator.Send(request);
            return res.Result;
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
