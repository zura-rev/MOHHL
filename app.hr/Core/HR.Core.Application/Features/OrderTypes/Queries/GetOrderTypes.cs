using AutoMapper;
using CleanSolution.Core.Application.DTOs;
using FluentValidation;
using HR.Core.Application.Commons;
using HR.Core.Application.DTOs;
using HR.Core.Application.Interfaces;
using HR.Core.Domain.Enums;
using HR.Core.Domain.Models;
using MediatR;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Employees.Queries
{
    public class GetOrderTypeRequest : IRequest<GetOrderTypeDto>
    {
        public int Id { get; set; }
        public string OrderTypeName { get; set; }
    }

    public class GetOrderTypeHandler : IRequestHandler<GetOrderTypeRequest, GetOrderTypeDto>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public GetOrderTypeHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public Task<GetOrderTypeDto> Handle(GetOrderTypeRequest request, CancellationToken cancellationToken)
        {
            var orderTypes = unit.OrderTypeRepository.Filter(request.Id, request.OrderTypeName); 
            return Task.FromResult(mapper.Map<GetOrderTypeDto> (orderTypes));
        }

    }

}
