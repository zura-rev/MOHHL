using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Core.Application.Commons;
using Tasks.Core.Application.DTOs;
using Tasks.Core.Application.Interfaces;
using Tasks.Core.Domain.Models;

namespace Tasks.Core.Application.Features.Users.Commands
{
    public class UpdateCardRequest : IRequest<GetCardDto>
    {
        public int Id { get; set; }
        public string Note { get; set; }
    }

    public class UpdateCardHandler : IRequestHandler<UpdateCardRequest, GetCardDto>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public UpdateCardHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<GetCardDto> Handle(UpdateCardRequest request, CancellationToken cancellationToken)
        {
            var card  = unit.CardRepository.UpdateCard(request.Id, request.Note);
            await unit.SaveAsync();
            var result = mapper.Map<GetCardDto>(card);
            return await Task.FromResult(result);
        }
    }

    //public class UpdateCardValidator : AbstractValidator<UpdateCardRequest>
    //{
    //    private readonly IUnitOfWork unit;
    //    public UpdateCardValidator(IUnitOfWork unit)
    //    {
    //        this.unit = unit;
    //    }
    //}
}
