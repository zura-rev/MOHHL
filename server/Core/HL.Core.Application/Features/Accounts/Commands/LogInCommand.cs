﻿using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HL.Core.Application.Commons;
using HL.Core.Application.DTOs;
using HL.Core.Application.Exceptions;
using HL.Core.Application.Interfaces;
using HL.Core.Domain.Enums;

namespace HL.Core.Application.Features.Users.Commands
{
    public class LogInRequest : IRequest<GetUserDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }


    public class LogInHandler : IRequestHandler<LogInRequest, GetUserDto>
    {
        private readonly IUnitOfWork unit;
        public LogInHandler(IUnitOfWork unit) => this.unit = unit;

        public async Task<GetUserDto> Handle(LogInRequest request, CancellationToken cancellationToken)
        {
            var passwordHash = Functions.GetPasswordHash(request.UserName, request.Password);
      
            var users = await unit.UserRepository.ReadAsync(x => x.UserName == request.UserName && x.Password == passwordHash);

            if (users.ToList().Count == 0)
                throw new UnAuthenticatedException("იუზერი ვერ მოიძებნა");

            return new GetUserDto(users.First());
        }
    }
}
