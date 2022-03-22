using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hl.Core.Application.Features.Resources.Queries;
using Hl.Core.Domain.Models;

namespace Hl.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IMediator mediator;
        public ResourcesController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IEnumerable<Resource>> Get() => await mediator.Send(new GetResourcesRequest());
    }
}
