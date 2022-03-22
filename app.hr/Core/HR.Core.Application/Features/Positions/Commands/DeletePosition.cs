using AutoMapper;
using HR.Core.Application.Interfaces;
using HR.Core.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Positions.Commands
{
    public class DeletePosition : IRequest
    {
        public int Id { get; set; }
    }

    public class DeletePositionHandler : IRequestHandler<DeletePosition>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public DeletePositionHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        public Task<Unit> Handle(DeletePosition request, CancellationToken cancellationToken)
        {
            //unit.PositionRepository.Delete(request.Id);
            throw new NotImplementedException();
        }
    }
}
