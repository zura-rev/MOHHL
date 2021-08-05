using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HL.Core.Application.DTOs;
using HL.Core.Application.Interfaces;

namespace HL.Core.Application.Features.Users.Queries
{
    public class GetUserByIdRequest : IRequest<GetUserDto>
    {
        public int Id { get; set; }

        public GetUserByIdRequest(int id) => this.Id = id;
    }


    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserDto>
    {
        private readonly IUnitOfWork unit;
        public GetUserByIdHandler(IUnitOfWork unit) => this.unit = unit;


        public async Task<GetUserDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await unit.UserRepository.ReadAsync(request.Id);

            return new GetUserDto(user);
        }
    }
}
