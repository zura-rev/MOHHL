using AutoMapper;
using HR.Core.Application.Interfaces;
using HR.Core.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Positions.Commands
{
    public class CreatePosition : IRequest
    {
        public string PositionName { get; set; }
        public int SortId { get; set; }       
    }

    public class CreatePositionHandler : IRequestHandler<CreatePosition>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public CreatePositionHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        
        public Task<Unit> Handle(CreatePosition request, CancellationToken cancellationToken)
        {
            var position = mapper.Map<Position>(request);
            throw new NotImplementedException();
        }
    }

    public class SetPositionDtoValidator : AbstractValidator<CreatePosition>
    {
        public SetPositionDtoValidator()
        {
            RuleFor(x => x.PositionName).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
        }
    }

}
