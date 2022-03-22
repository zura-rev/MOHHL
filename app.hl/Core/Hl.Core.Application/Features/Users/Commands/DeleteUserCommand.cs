using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hl.Core.Application.Interfaces;

namespace Hl.Core.Application.Features.Users.Commands
{
    public class DeleteUserRequest : IRequest
    {
        public int Id { get; set; }

        public DeleteUserRequest(int id)
        {
            Id = id;
        }
    }
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest>
    {
        private readonly IUnitOfWork unit;
        public DeleteUserHandler(IUnitOfWork unit) 
        { 
            this.unit = unit; 
        }


        public async Task<Unit> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await unit.UserRepository.ReadAsync(request.Id);
            unit.UserRepository.Update(user);
            await unit.SaveAsync();

            return await Task.FromResult(Unit.Value);
        }
    }
}
