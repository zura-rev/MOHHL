using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HL.Core.Application.Commons;
using HL.Core.Application.Interfaces;
using HL.Core.Domain.Models;
using System;
using HL.Core.Application.DTOs;
using AutoMapper;
using CleanSolution.Core.Application.DTOs;
using System.Linq;
using System.Collections.Generic;

namespace HL.Core.Application.Features.Calls.Queries
{
    public class GetCallRequest : IRequest<GetPaginationDto<GetCallDto>>
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

        public int Id { get; set; }
        public string CallAuthor { get; set; }
        public string PrivateNumber { get; set; }
        public string Phone { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public int CallStatus { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public DateTime FromDate { get; set; } = DateTime.Now.AddDays(-360);
        public DateTime ToDate { get; set; } = DateTime.Now;
    }

    public class GetCallsHandler : IRequestHandler<GetCallRequest, GetPaginationDto<GetCallDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public GetCallsHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<GetPaginationDto<GetCallDto>> Handle(GetCallRequest request, CancellationToken cancellationToken)
        {
            var calls = unit.CallRepository.Filter(
                request.Id,
                request.CallAuthor,
                request.PrivateNumber,
                request.Phone,
                request.CategoryId,
                request.Category,
                request.Note,
                request.CreateDate,
                request.CallStatus,
                request.UserId,
                request.FromDate,
                request.ToDate
                );

            var callList = await Pagination<Call>.CreateAsync(calls, request.pageIndex, request.pageSize);
            var result = mapper.Map<GetPaginationDto<GetCallDto>>(callList);
            return result;
        }
    }

    public class GetCallsValidator : AbstractValidator<GetCallRequest>
    {
        public GetCallsValidator()
        {
            RuleFor(x => x.pageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }
}
