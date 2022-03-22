using AutoMapper;
using Hl.Core.Application.DTOs;
using Hl.Core.Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hl.Core.Application.Features.Calls.Queries
{
    public class GetMatchCallRequest : IRequest<IEnumerable<GetCallDto>>
    {
        public string Phone { get; set; }
        public string PrivateNumber { get; set; }
        public GetMatchCallRequest(string phone, string privateNumber)
        {
            Phone = phone;
            PrivateNumber = privateNumber;
        }
    }

    public class GetMatchCallHandler : IRequestHandler<GetMatchCallRequest, IEnumerable<GetCallDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public GetMatchCallHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public Task<IEnumerable<GetCallDto>> Handle(GetMatchCallRequest request, CancellationToken cancellationToken)
        {
            var callList = unit.CallRepository.GetMatchCalls(request.Phone, request.PrivateNumber);
            return Task.FromResult(mapper.Map<IEnumerable<GetCallDto>>(callList));
        }
    }

    //public class GetMatchCallValidator : AbstractValidator<GetMatchCallRequest>
    //{
    //    public GetMatchCallValidator()
    //    {
    //        RuleFor(x => x.Phone).NotNull();
    //        RuleFor(x => x.Phone).NotEmpty();//.WithMessage("");
    //    }
    //}

}
