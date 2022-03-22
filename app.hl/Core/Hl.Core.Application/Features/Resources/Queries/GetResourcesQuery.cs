using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hl.Core.Application.Interfaces;
using Hl.Core.Domain.Models;

namespace Hl.Core.Application.Features.Resources.Queries
{
    public class GetResourcesRequest : IRequest<IEnumerable<Resource>> { }

    public class GetResourcesHandler : IRequestHandler<GetResourcesRequest, IEnumerable<Resource>>
    {
        private readonly IUnitOfWork unit;
        public GetResourcesHandler(IUnitOfWork unit) => this.unit = unit;

        public Task<IEnumerable<Resource>> Handle(GetResourcesRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(unit.ResourceRepository.Read());
        }
    }
}
