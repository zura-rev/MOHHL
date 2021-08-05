using HL.Core.Application.DTOs;
using HL.Core.Application.Features.Calls.Queries;
using HL.Presentation.WebApi.Extensions.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HL.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PerformersController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly ActiveObjectsService usersCaching;


        public PerformersController(IMediator mediator, ActiveObjectsService usersCaching)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.usersCaching = usersCaching;
        }

        [HttpGet]
        public async Task<IEnumerable<GetPerformerDto>> Get([FromQuery] GetPerformerRequest request)
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
                return result.Items;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        ////GET api/<PerformersController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<PerformersController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<PerformersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<PerformersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
