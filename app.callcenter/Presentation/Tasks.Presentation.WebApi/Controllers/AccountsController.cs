using System;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tasks.Core.Application.DTOs;
using Tasks.Core.Application.Features.Users.Commands;
using Tasks.Presentation.Extensions.Services;
using System.Collections.Generic;
using Tasks.Core.Domain.Common;
using Tasks.Core.Application.Interfaces.Contracts;

namespace Tasks.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly IActiveObjectsService usersCaching;

        public AccountsController(IMediator mediator, IActiveObjectsService usersCaching)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.usersCaching = usersCaching;
        }


        [HttpGet("activeSupervaisers")]
        public IEnumerable<Supervaiser> GetActiveSupervaisers()
        {
            return usersCaching.GetActiveRecords();
        }


        [HttpPost("logIn")]
        public async Task<ActionResult<GetUserDto>> LogIn([FromBody] LogInRequest request, 
            [FromServices] IConfiguration configuration)
        {
            try
            {
                var user = await mediator.Send(request);

                var permissions = user.Resources.Select(x => x.Name).ToArray();

                var token = JwtAuthenticationExtensions.GenerateJwtToken(
                    configuration,
                    user.Id.ToString(),
                    user.UserName,
                    user.FirstName,
                    user.LastName,
                    user.PrivateNumber,
                    permissions
                    );

                Response.Headers.Add("AccessToken", token);
                if (permissions.Contains("ROLE.SUPERVAISER"))
                {
                    usersCaching.AddOrProlong(user.UserName);
                }
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        [HttpPost("logOut")]
        public void LogOut(string userName)
        {
            usersCaching.Remove(userName);
        }

    }
}
