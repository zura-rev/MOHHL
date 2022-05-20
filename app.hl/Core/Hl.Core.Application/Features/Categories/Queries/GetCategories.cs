using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hl.Core.Application.Interfaces;
using Hl.Core.Application.DTOs;
using AutoMapper;
using System.Collections.Generic;

namespace Hl.Core.Application.Features.Calls.Queries
{
    public class GetCategoryRequest : IRequest<IEnumerable<GetCategoryDto>>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
        public int Status { get; set; }
    }

    public class GetCategoryHandler : IRequestHandler<GetCategoryRequest, IEnumerable<GetCategoryDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public GetCategoryHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public Task<IEnumerable<GetCategoryDto>> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            var categories = unit.CategoryRepository.Filter(
                request.Id,
                request.CategoryName,
                request.ParentId,
                request.Status
                );

            var result =  mapper.Map<IEnumerable<GetCategoryDto>>(categories);
            return Task.FromResult(result);
        }
    }

   
}
