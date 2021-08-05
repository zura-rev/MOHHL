using AutoMapper;
using HL.Core.Application.Interfaces;
using HL.Core.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HL.Core.Application.Features.Positions.Commands
{
    public class CreatePositionRequest : IRequest
    {
        public string Name { get; set; }
        public double Salary { get; set; }       
    }

    public class CreatePositionHandler : IRequestHandler<CreatePositionRequest>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public CreatePositionHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }   
        public Task<Unit> Handle(CreatePositionRequest request, CancellationToken cancellationToken)
        {
            var poxition = mapper.Map<Position>(request);
            throw new NotImplementedException();
        }
    }

    public class SetPositionDtoValidator : AbstractValidator<CreatePositionRequest>
    {
        public SetPositionDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
            RuleFor(x => x.Salary).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
        }
    }

}
