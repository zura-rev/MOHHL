using System;
using System.Collections.Generic;
using System.Text;
using HL.Core.Domain.Enums;

namespace HL.Core.Application.Interfaces.Contracts
{
    public interface ICurrentUserService
    {
        public int AccountId { get; }
        public AccountType AccountType { get; }
        public string IpAddress { get; }
        public int Port { get; }
        public string RequestUrl { get; }
        public string RequestMethod { get; }
    }
}
