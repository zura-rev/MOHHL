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
    public class GetCategoriesRequest : IRequest<IEnumerable<GetCategoryDto>>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
    }

    public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, IEnumerable<GetCategoryDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public GetCategoriesHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public Task<IEnumerable<GetCategoryDto>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
        {
            var categories = unit.CategoryRepository.Filter(
                request.Id,
                request.CategoryName,
                request.ParentId
                );

            var result =  mapper.Map<IEnumerable<GetCategoryDto>>(categories);
            return Task.FromResult(result);
        }
    }

    public class GetCategoriesValidator : AbstractValidator<GetCategoriesRequest>
    {
        public GetCategoriesValidator()
        {
            //RuleFor(x => x.pageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            //RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }
}
