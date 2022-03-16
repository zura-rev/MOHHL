using HR.Core.Application.DTOs;
using HR.Core.Application.Features.Employees.Commands;
using HR.Core.Application.Features.Employees.Queries;
using HR.Core.Application.Features.Positions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HR.Core.Application.Features.Employees.Commands.CreateEmployee;

namespace HR.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator mediator;

        public EmployeeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetEmployeeDto>>> Get([FromQuery] GetEmployeeRequest request)
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

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDto>> Get(int id)
        {
            var request = new GetEmployeeByIdRequest(id); 
            return Ok(await mediator.Send(request));
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<ActionResult<GetEmployeeDto>> Post([FromBody] CreateEmployeeRequest value)
        {
            var employee = await mediator.Send(value);
            return Ok(employee);
        }
        // PUT api/<EmployeesController>/5

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] SetEmployeeDto value)
        {

        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
