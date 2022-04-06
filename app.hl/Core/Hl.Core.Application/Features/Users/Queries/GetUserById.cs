using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hl.Core.Application.DTOs;
using Hl.Core.Application.Interfaces;

namespace Hl.Core.Application.Features.Users.Queries
{
    public class GetUserByIdRequest : IRequest<GetUserDto>
    {
        public int Id { get; set; }

        public GetUserByIdRequest(int id)
        {
            this.Id = id;
        }
    }

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserDto>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public GetUserByIdHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }


        public async Task<GetUserDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await unit.UserRepository.ReadAsync(request.Id);

            var mappedUser = mapper.Map<GetUserDto>(user);
            return mappedUser;
        }
    }
}
