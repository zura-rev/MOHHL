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
using Hl.Core.Application.Interfaces.Contracts;
using System.IO;
using System.Linq;

namespace Hl.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CallsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IActiveObjectsService usersCaching;

        public CallsController(IMediator mediator, IActiveObjectsService usersCaching)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.usersCaching = usersCaching;
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
        public async Task<ActionResult<IEnumerable<GetCallDto>>> Get([FromQuery] string phone, [FromQuery] string privateNumber, [FromQuery] int topValue = 10)
        {
            if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(privateNumber))
                throw new BadRequestException("შეავსეთ ტელეფონი ან პირადი ნომერი!");

            var req = new GetMatchCallRequest(phone, privateNumber, topValue);
            var res = await mediator.Send(req);
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateCallRequest request)
        {
            if (request.CallType==2 && usersCaching.GetActiveRecords().Count == 0)
            {
                throw new BadRequestException("ამ მომენტისთვის არცერთი სუპერვაიზერი არ არის სისტემაში შემოსული, სამწუხაროდ ვერ შეძლებთ ბარათის რეგისტრაციას! ");
            }
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

        [HttpPost("excel")]
        [Authorize(Policy = "ExportExcelGermanyPolicy")]
        public async Task<IActionResult> Export([FromBody] GetCallRequest request, [FromServices] IExcelManager excelManager)
        {
            var result = await mediator.Send(request);

            (string mimeType, MemoryStream stream) = excelManager.GenerateExcel(result.Items.ToList());

            return File(stream, mimeType, string.Format("hl_app({0:yyyy_MM_dd_HH_mm_ss}).xlsx", DateTime.Now));
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
