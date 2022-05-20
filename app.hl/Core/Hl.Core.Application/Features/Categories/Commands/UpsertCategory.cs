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
    public class UpsertCategoryRequest : IRequest<GetCategoryDto>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }

    public class CreateCaregoryHandler : IRequestHandler<UpsertCategoryRequest, GetCategoryDto>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public CreateCaregoryHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public Task<GetCategoryDto> Handle(UpsertCategoryRequest request, CancellationToken cancellationToken)
        {

            var category = new Category
            {
                Id = request.Id,
                CategoryName = request.CategoryName,
                ParentId = (int)request.ParentId,
                Status = request.Status,
                Note = request.Note
            };

            Category _category;

            if (request.Id == default)
            {
                _category = unit.CategoryRepository.CreateCategory(category);
            }
            else
            {
                _category = unit.CategoryRepository.UpdateCategory(request.Id, category);
            }

            return Task.FromResult(mapper.Map<GetCategoryDto>(_category));
        }
    }

    public class SetCaregoryDtoValidator : AbstractValidator<UpsertCategoryRequest>
    {
        public SetCaregoryDtoValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
            RuleFor(x => x.ParentId).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
        }
    }
}
