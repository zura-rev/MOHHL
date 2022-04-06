using AutoMapper;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hl.Core.Application.Commons;
using Hl.Core.Application.DTOs;
using Hl.Core.Application.Interfaces;
using Hl.Core.Domain.Models;

namespace Hl.Core.Application.Features.Users.Queries
{
    public class GetOperatorsRequest : IRequest<IEnumerable<GetOperatorDto>>
    {
    }

    public class GetOperatorsHandler : IRequestHandler<GetOperatorsRequest, IEnumerable<GetOperatorDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public GetOperatorsHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }


        public async Task<IEnumerable<GetOperatorDto>> Handle(GetOperatorsRequest request, CancellationToken cancellationToken)
        {
            var opertators = await unit.UserRepository.GetOperators();
            return mapper.Map<IEnumerable<GetOperatorDto>>(opertators);
        }
    }
  
}
