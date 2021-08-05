using AutoMapper;
using FluentValidation;
using HL.Core.Application.DTOs;
using HL.Core.Application.Interfaces;
using HL.Core.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HL.Core.Application.Features.Calls.Queries
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
