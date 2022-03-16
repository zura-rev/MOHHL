using HR.Core.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HR.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTypeController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderTypeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public IEnumerable<GetOrderDto> Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public GetOrderTypeDto Get(int id)
        {
            return null;
        }

      

    }
}
