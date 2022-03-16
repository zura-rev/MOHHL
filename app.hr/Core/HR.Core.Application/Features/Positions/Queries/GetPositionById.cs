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
    public class GetPositionByIdRequest : IRequest<GetPositionDto>
    {
        public int Id { get; set; }
        public GetPositionByIdRequest(int id)
        {
            this.Id = id;
        }

    }

    public class GetPositionByIdHandler : IRequestHandler<GetPositionByIdRequest, GetPositionDto>
    {

        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public GetPositionByIdHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper; 
        }
        public Task<GetPositionDto> Handle(GetPositionByIdRequest request, CancellationToken cancellationToken)
        {
            //var position = unit.PositionRepository.GetById(request.Id);
            var position = unit.PositionRepository.Read(request.Id);
            return Task.FromResult(mapper.Map<GetPositionDto>(position));
        }
    }

    public class GetPositionByIdValidator : AbstractValidator<GetPositionByIdRequest>
    {
        public GetPositionByIdValidator()
        {
            //RuleFor(x => x.pageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            //RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }

}
