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
    public class GetEmployeeRequest : IRequest<GetPaginationDto<GetEmployeeDto>>
    {
        public int Id { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetEmployeeHandler : IRequestHandler<GetEmployeeRequest, GetPaginationDto<GetEmployeeDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public GetEmployeeHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<GetPaginationDto<GetEmployeeDto>> Handle(GetEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employees = unit.EmployeeRepository.Filter(
                request.Id,
                request.PrivateNumber,
                request.FirstName,
                request.LastName,
                request.Gender,
                request.BirthDate
            );

            var employeeList = await Pagination<Employee>.CreateAsync(employees, request.PageIndex, request.PageSize);
            return mapper.Map<GetPaginationDto<GetEmployeeDto>>(employeeList);
        }
    }

    public class GetemployeeValidator : AbstractValidator<GetEmployeeRequest>
    {
        public GetemployeeValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }

}
