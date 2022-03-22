using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hl.Core.Application.DTOs;
using Hl.Core.Application.Features.Users.Commands;
using Hl.Core.Application.Features.Users.Queries;

namespace Hl.Presentation.WebApi.Controllers
{
    //[Authorize(Policy = "ViewUsersPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        public UsersController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> Get([FromQuery] GetUsersRequest request)
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

        //[HttpGet("{id}")]
        //public async Task<ActionResult<GetUserDto>> Get([FromRoute] int id) =>
        //    await mediator.Send(new GetUserByIdRequest(id));

        [HttpPut]
        //[Authorize(Policy = "EditUsersPolicy")]
        //[AllowAnonymous]
        public async Task<ActionResult<GetUserDto>> Put(UpsertUserRequest request)
        {
            var user = await mediator.Send(request);
            return Ok(user);
        }

        [HttpDelete]
        [Authorize(Policy = "DeleteUsersPolicy")]
        public async Task Delete([FromForm] int id)
        {
            await mediator.Send(new DeleteUserRequest(id));
        }
        
    }
}
