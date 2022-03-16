using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Core.Application.Commons;
using Tasks.Core.Application.Interfaces;
using Tasks.Core.Domain.Models;
using System;
using Tasks.Core.Application.DTOs;
using AutoMapper;
using CleanSolution.Core.Application.DTOs;
using System.Linq;
using System.Collections.Generic;
using Tasks.Core.Application.Interfaces.Contracts;

namespace Tasks.Core.Application.Features.Calls.Queries
{
    public class GetCardRequest : IRequest<GetPaginationDto<GetCardDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Id { get; set; }
        public int CallId { get; set; }
        public int UserId { get; set; }
        public int UserType { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public DateTime PerformDate { get; set; }
        public int? CategoryId { get; set; }

    }

    public class GetCardHandler : IRequestHandler<GetCardRequest, GetPaginationDto<GetCardDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        private readonly ICurrentUserService user;

        public GetCardHandler(IUnitOfWork unit, IMapper mapper, ICurrentUserService user)
        {
            this.unit = unit;
            this.mapper = mapper;
            this.user = user;
        }

        public async Task<GetPaginationDto<GetCardDto>> Handle(GetCardRequest request, CancellationToken cancellationToken)
        {

            var userInfo = unit.UserRepository.GetUserById(user.AccountId);
            var has = userInfo.Resources.Any(x=>x.Name == "ROLE.ADMIN");

            var cards = unit.CardRepository.Filter(
                request.Id,
                request.CallId,
                has? 0: user.AccountId,
                request.UserType,
                request.Status,
                request.Note,
                request.PerformDate,
                request.CategoryId
            );

            var performerList = await Pagination<Card>.CreateAsync(cards, request.PageIndex, request.PageSize);
            var result = mapper.Map<GetPaginationDto<GetCardDto>>(performerList);
            return result;
        }
    }

    public class GetCardValidator : AbstractValidator<GetCardRequest>
    {
        public GetCardValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }
}
