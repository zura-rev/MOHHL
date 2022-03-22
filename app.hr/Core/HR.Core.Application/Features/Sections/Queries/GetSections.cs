using AutoMapper;
using FluentValidation;
using HR.Core.Application.DTOs;
using HR.Core.Application.Interfaces;
using HR.Core.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Employees.Queries
{
    public class GetSectionRequest : IRequest<IEnumerable<GetSectionDto>>{}

    public class GetSectionHandler : IRequestHandler<GetSectionRequest, IEnumerable<GetSectionDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public GetSectionHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public Task<IEnumerable<GetSectionDto>> Handle(GetSectionRequest request, CancellationToken cancellationToken)
        {
            var result = unit.SectionRepository.Read();
            var sections = mapper.Map<IEnumerable<GetSectionDto>>(result);
            return Task.FromResult(sections);
        }
    }

    public class GetSectionValidator : AbstractValidator<GetSectionRequest>
    {
        public GetSectionValidator(){}
    }

}
