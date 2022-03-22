using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hl.Core.Application.Commons;
using Hl.Core.Application.DTOs;
using Hl.Core.Application.Exceptions;
using Hl.Core.Application.Interfaces;
using Hl.Core.Domain.Enums;

namespace Hl.Core.Application.Features.Users.Commands
{
    public class LogInRequest : IRequest<GetUserDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }


    public class LogInHandler : IRequestHandler<LogInRequest, GetUserDto>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public LogInHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<GetUserDto> Handle(LogInRequest request, CancellationToken cancellationToken)
        {
            var passwordHash = Functions.GetPasswordHash(request.UserName, request.Password);
      
            var users = await unit.UserRepository.ReadAsync(x => x.UserName == request.UserName && x.Password == passwordHash);

            if (users.ToList().Count == 0)
                throw new UnAuthenticatedException("იუზერი ვერ მოიძებნა");
            
            var user = mapper.Map<GetUserDto>(users.First());
            return user;
        }
    }
}
