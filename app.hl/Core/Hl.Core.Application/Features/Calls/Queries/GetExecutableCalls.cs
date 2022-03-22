using AutoMapper;
using Hl.Core.Application.DTOs;
using Hl.Core.Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hl.Core.Application.Features.Calls.Queries
{
    public class GetExecutableCallsRequest : IRequest<IEnumerable<GetCallDto>>
    {
        public string User { get; set; }
        public GetExecutableCallsRequest(string user)
        {
            User = user;
        }
    }

    public class GetExecutableCallsHandler : IRequestHandler<GetExecutableCallsRequest, IEnumerable<GetCallDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public GetExecutableCallsHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public Task<IEnumerable<GetCallDto>> Handle(GetExecutableCallsRequest request, CancellationToken cancellationToken)
        {
            //var callList = unit.CallRepository.GetExecutableCalls(request.User);
            return null;//Task.FromResult(mapper.Map<IEnumerable<GetCallDto>>(callList));
        }
    }

 

}
