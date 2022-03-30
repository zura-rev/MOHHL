using Hl.Core.Application.DTOs;
using Hl.Core.Application.Features.Calls.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hl.Core.Application.Features.Users.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hl.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CardController : ControllerBase
    {

        private readonly IMediator mediator;
        //private readonly ActiveObjectsService usersCaching;

        public CardController(IMediator mediator)//, ActiveObjectsService usersCaching)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            //this.usersCaching = usersCaching;
        }

        [HttpGet]
        public async Task<IEnumerable<GetCardDto>> Get([FromQuery] GetCardRequest request)
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
                throw ex.InnerException;
            }
        }

        [HttpPut]
        public async Task<GetCardDto> Put(UpdateCardRequest request)
        {
            var result = await mediator.Send(request);
            return result;
        }

       
    }
}
