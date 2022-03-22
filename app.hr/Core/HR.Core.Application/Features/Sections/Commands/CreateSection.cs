using AutoMapper;
using FluentValidation;
using HR.Core.Application.Interfaces;
using HR.Core.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Sections.Commands
{
    public class CreateSection : IRequest<Section>
    {
        //public int Id { get; set; }
        public string SectionName { get; set; }
        public int ParentId { get; set; }
    }

    public class CreateSectionHandler : IRequestHandler<CreateSection, Section>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public CreateSectionHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public Task<Section> Handle(CreateSection request, CancellationToken cancellationToken)
        {
            var section = new Section
            {
                //Id = request.Id,
                SectionName = request.SectionName,
                ParentId = request.ParentId
            };
            return Task.FromResult(unit.SectionRepository.Create(section));
        }
    }

    public class SetSectionDtoValidator : AbstractValidator<CreateSection>
    {
        public SetSectionDtoValidator()
        {
            RuleFor(x => x.SectionName).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
            RuleFor(x => x.ParentId).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
        }
    }
}
