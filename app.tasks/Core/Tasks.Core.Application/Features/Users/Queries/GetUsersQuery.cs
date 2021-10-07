using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Core.Application.Commons;
using Tasks.Core.Application.DTOs;
using Tasks.Core.Application.Interfaces;
using Tasks.Core.Domain.Models;

namespace Tasks.Core.Application.Features.Users.Queries
{
    public class GetUsersRequest : IRequest<Pagination<GetUserDto>>
    {
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int pageIndex { get; set; }
        public int pageSize { get; set; }

        public DateTime fromDate { get; set; } = DateTime.Now.AddDays(-10);
        public DateTime toDate { get; set; } = DateTime.Now;

    }


    public class GetUsersHandler : IRequestHandler<GetUsersRequest, Pagination<GetUserDto>>
    {
        private readonly IUnitOfWork unit;
        public GetUsersHandler(IUnitOfWork unit) => this.unit = unit;


        public async Task<Pagination<GetUserDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await unit.UserRepository.Filter(request.PrivateNumber, request.FirstName, request.LastName);

            var userData = await Pagination<User>.CreateAsync(users, request.pageIndex, request.pageSize);


            var userDto = new List<GetUserDto>();
            foreach (var item in userData.Items)
            {
                userDto.Add(new GetUserDto(item));
            }

            return new Pagination<GetUserDto>(userDto, userData.TotalCount, userData.PageIndex, userData.PageSize);
        }
    }

    public class GetUsersValidator : AbstractValidator<GetUsersRequest>
    {
        public GetUsersValidator()
        {
            RuleFor(x => x.pageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }
}
