using AutoMapper;
using FluentValidation;
using HR.Core.Application.DTOs;
using HR.Core.Application.Interfaces;
using HR.Core.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Sections.Commands
{
    public class UpsertSectionRequest : IRequest<GetSectionDto>
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public int ParentId { get; set; }
    }

    public class UpsertSectionHandler : IRequestHandler<UpsertSectionRequest, GetSectionDto>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        private Section section;

        public UpsertSectionHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;   
        }

        public async Task<GetSectionDto> Handle(UpsertSectionRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == default)
            {
                section = unit.SectionRepository.Create(mapper.Map<Section>(request));
            }
            else
            {
                section = unit.SectionRepository.Update(mapper.Map<Section>(request));
            }
            var result = mapper.Map<GetSectionDto>(section);
            return await Task.FromResult(result);
        }

    }

    public class UpsertSectionValidator : AbstractValidator<UpsertSectionRequest>
    {
        private readonly IUnitOfWork unit;
        public UpsertSectionValidator(IUnitOfWork unit)
        {
            //this.unit = unit;

            //RuleFor(x => x.PrivateNumber)
            //    .NotNull().WithMessage("პირადი ნომერი ცარიელია")
            //    .Length(11).WithMessage("პირადი ნომერი უნდა შედგებოდეს 11 სიმბოლოსგან")
            //    .Matches("^[0-9]*$").WithMessage("პირადი ნომერი უნდა შედგებოდეს მხოლოდ ციფრებისგან");

            //RuleFor(x => x.Resources)
            //     .NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია")
            //     .NotNull()
            //     .MustAsync(IfExistAllResources).WithMessage("{PropertyName} მითითებული რესურსები არ არსებობს");
        }
    }

}
