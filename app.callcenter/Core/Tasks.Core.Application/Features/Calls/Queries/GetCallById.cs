using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Core.Application.Commons;
using Tasks.Core.Application.Interfaces;
using Tasks.Core.Domain.Models;
using Tasks.Core.Application.Exceptions;

namespace Tasks.Core.Application.Features.Calls.Queries
{
    public class GetCallByIdRequest : IRequest<Call>
    {
        public int Id { get; set; }
        public GetCallByIdRequest(int id)
        { 
            this.Id = id;
        }
    }

    public class GetCallByIdHandler : IRequestHandler<GetCallByIdRequest, Call>
    {
        private readonly IUnitOfWork unit;
        public GetCallByIdHandler(IUnitOfWork unit)
        {
            this.unit = unit;

            if (false)
                throw new DataNotFoundException("მონაცემი ვერ მოიძებნა");
        }

        public Task<Call> Handle(GetCallByIdRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(unit.CallRepository.GetById(request.Id));
        }

    }

}
