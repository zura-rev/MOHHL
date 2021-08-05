using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HL.Core.Application.Interfaces.Contracts;
using HL.Core.Domain.Enums;

namespace HL.Presentation.Extensions.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        /// <summary>
        /// იუზერის მონაცემების და მოთხოვნის ინფორმაციის ამოღება
        /// </summary>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor) : this(httpContextAccessor.HttpContext) { }
        public CurrentUserService(HttpContext context)
        {
            if (context == null) return;

            this.AccountId = int.TryParse(context.User?.FindFirstValue(ClaimTypes.NameIdentifier), out int result) ? result : 0;
            this.IpAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
            this.Port = context.Connection?.RemotePort ?? 0;

            this.RequestUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
            this.RequestMethod = context.Request.Method;
        }

        public int AccountId { get; }
        public AccountType AccountType { get; } = AccountType.Client;
        public string IpAddress { get; }
        public int Port { get; }

        public string RequestUrl { get; }
        public string RequestMethod { get; }
    }
}
