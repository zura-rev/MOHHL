using Hl.Core.Application.DTOs;
using Hl.Core.Application.Features.Calls.Commands;
using Hl.Core.Application.Features.Calls.Queries;
using Hl.Core.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hl.Core.Application.Exceptions;
using Hl.Presentation.WebApi.Extensions.Services;
using Hl.Core.Application.Interfaces.Contracts;

namespace Hl.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CallsController : ControllerBase
    {
        private readonly IMediator mediator;
        //private readonly IActiveObjectsService usersCaching;

        public CallsController(IMediator mediator)//, IActiveObjectsService usersCaching)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            //this.usersCaching = usersCaching;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCallDto>>> Get([FromQuery] GetCallRequest request)
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
        public async Task<ActionResult<Call>> Get(int id)
        {
            var request = new GetCallByIdRequest(id);
            return Ok(await mediator.Send(request));
        }

        [HttpGet("matchcalls")]
        public async Task<ActionResult<IEnumerable<GetCallDto>>> Get([FromQuery] string phone, [FromQuery] string privateNumber)
        {
            if (string.IsNullOrEmpty(phone))
                throw new BadRequestException("შეავსეთ ტელეფონი!");

            var req = new GetMatchCallRequest(phone, privateNumber);
            var res = await mediator.Send(req);
            return Ok(res);
        }

        //[HttpPost]
        //public async void Post([FromBody] CreateCallRequest request)
        //{
        //    await mediator.Send(request);
        //}

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateCallRequest request)
        {
            var res = await mediator.Send(request);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] SetCallDto value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        //[HttpGet("executable")]
        //public async Task<ActionResult<IEnumerable<GetCallDto>>> Get([FromQuery] string user)
        //{
        //    if (string.IsNullOrEmpty(user))
        //        throw new BadRequestException("მომხმარებელი ვერ მოიძებნა!");
        //    var request = new GetExecutableCallsRequest(user);
        //    return Ok(await mediator.Send(request));
        //}

    }
}
