using AutoMapper;
using FluentValidation;
using HR.Core.Application.DTOs;
using HR.Core.Application.Interfaces;
using HR.Core.Application.Interfaces.Contracts;
using HR.Core.Domain.Enums;
using HR.Core.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Employees.Commands
{
    public class CreateEmployee
    {
        public class CreateEmployeeRequest : IRequest<GetEmployeeDto>
        {
            public int Id { get; set; }
            public string PrivateNumber { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Gender Gender { get; set; }
            public DateTime BirthDate { get; set; }
            public string Image { get; set; }
        }

        public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeRequest, GetEmployeeDto>
        {
            private readonly IUnitOfWork unit;
            private readonly IMapper mapper;
            private readonly ICurrentUserService user;

            public CreateEmployeeHandler(IUnitOfWork unit, IMapper mapper, ICurrentUserService user)
            {
                this.unit = unit;
                this.mapper = mapper;   
                this.user = user;
            }

            public Task<GetEmployeeDto> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
            {
                //var category = unit.CategoryRepository.Read(request.Category.Id);

                //var currentUser = unit.UserRepository.GetUserById(user.AccountId);

                var employee = new Employee
                {
                    Id = request.Id,
                    PrivateNumber = request.PrivateNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Gender = request.Gender,
                    BirthDate = request.BirthDate,
                    Image = request.Image
                    //UserId = currentUser
                };

                unit.EmployeeRepository.Create(employee);
                unit.Save();

               //return Task.FromResult(id);
               var result = mapper.Map<GetEmployeeDto>(employee);  
                return Task.FromResult(result);  
            }

        }

        public class SetEmployeeValidator : AbstractValidator<CreateEmployeeRequest>
        {
            public SetEmployeeValidator()
            {
                RuleFor(x => x.PrivateNumber).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
                RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
                RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
                RuleFor(x => x.Gender).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
                RuleFor(x => x.BirthDate).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
            }
        }

    }
}
