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
    public class GetOrderRequest : IRequest<GetPaginationDto<GetOrderDto>>
    {
        public int Id { get; set; }
        public int OrderTypeId { get; set; }
        public int EmployeeId { get; set; }
        public int StructureId { get; set; }
        public double RealSalary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetOrderHandler : IRequestHandler<GetOrderRequest, GetPaginationDto<GetOrderDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public GetOrderHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<GetPaginationDto<GetOrderDto>> Handle(GetOrderRequest request, CancellationToken cancellationToken)
        {
            var result = unit.OrderRepository.Filter(
                request.Id,
                request.OrderTypeId,
                request.EmployeeId,
                request.StructureId,
                request.RealSalary,
                request.StartDate,
                request.EndDate
            );

            var employees = await Pagination<Order>.CreateAsync(result, request.PageIndex, request.PageSize);
            return mapper.Map<GetPaginationDto<GetOrderDto>>(employees);
        }
    }

    public class GetOrderValidator : AbstractValidator<GetOrderRequest>
    {
        public GetOrderValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }

}
