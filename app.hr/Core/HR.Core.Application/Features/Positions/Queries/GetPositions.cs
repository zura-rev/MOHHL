using AutoMapper;
using CleanSolution.Core.Application.DTOs;
using FluentValidation;
using HR.Core.Application.Commons;
using HR.Core.Application.DTOs;
using HR.Core.Application.Interfaces;
using HR.Core.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Positions.Queries
{
    public class GetPositionRequest : IRequest<GetPaginationDto<GetPositionDto>>
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public int SortId { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

    }

    public class GetPositionHandler : IRequestHandler<GetPositionRequest, GetPaginationDto<GetPositionDto>>
    {

        private readonly IUnitOfWork Unit;
        private readonly IMapper Mapper;

        public GetPositionHandler(IUnitOfWork unit, IMapper mapper)
        {
            Unit = unit;
            Mapper = mapper; 
        }
        public async Task<GetPaginationDto<GetPositionDto>> Handle(GetPositionRequest request, CancellationToken cancellationToken)
        {
            var positions = Unit.PositionRepository.Filter(
                 request.Id,
                 request.PositionName,
                 request.SortId
             );

            var positionList = await Pagination<Position>.CreateAsync(positions, request.pageIndex, request.pageSize);
            return Mapper.Map<GetPaginationDto<GetPositionDto>>(positionList);
        }
    }

    public class GetPositionValidator : AbstractValidator<GetPositionRequest>
    {
        public GetPositionValidator()
        {
            RuleFor(x => x.pageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }

}
