using AutoMapper;
using HL.Core.Application.Interfaces;
using HL.Core.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HL.Core.Application.Features.Positions.Commands
{
    public class DeletePositionRequest:IRequest
    {
        public Guid Id { get; set; }


        public class DeletePositionHandler : IRequestHandler<DeletePositionRequest>
        {
            private readonly IUnitOfWork unit;
            private readonly IMapper mapper;

            public DeletePositionHandler(IUnitOfWork unit, IMapper mapper)
            {
                this.unit = unit;
                this.mapper = mapper;
            }
            public Task<Unit> Handle(DeletePositionRequest request, CancellationToken cancellationToken)
            {
                //unit.PositionRepository.Delete(request.Id);
                throw new NotImplementedException();
            }
        }

    }
}
