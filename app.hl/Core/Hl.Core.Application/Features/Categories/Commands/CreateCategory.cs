using AutoMapper;
using Hl.Core.Application.Interfaces;
using Hl.Core.Domain.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hl.Core.Application.DTOs;

namespace Hl.Core.Application.Features.Categories.Commands
{
    public class CreateCategoryRequest : IRequest<GetCategoryDto>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }
    }

    public class CreateCaregoryHandler : IRequestHandler<CreateCategoryRequest, GetCategoryDto>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public CreateCaregoryHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public Task<GetCategoryDto> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var  category = unit.CategoryRepository.CreateCategory(new Category { 
                Id = request.Id,
                CategoryName = request.CategoryName,
                ParentId = (int)request.ParentId,
            });

            var result = mapper.Map<GetCategoryDto>(category);

            return Task.FromResult(result);
        }
    }

    public class SetCaregoryDtoValidator : AbstractValidator<CreateCategoryRequest>
    {
        public SetCaregoryDtoValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
            RuleFor(x => x.ParentId).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
        }
    }
}
