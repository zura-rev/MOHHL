using Hl.Core.Application.Interfaces;
using Hl.Core.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Hl.Core.Application.Interfaces.Contracts;
using AutoMapper;

namespace Hl.Core.Application.Features.Calls.Commands
{
    public class UpsertCallRequest : IRequest<int>
    {
        public int Id { get; set; }
        public string CallAuthor { get; set; }
        public string PrivateNumber { get; set; }
        public string Phone { get; set; }
        public Category Category { get; set; }
        public string Note { get; set; }
        public int CallType { get; set; }
        public string Supervaiser { get; set; }
    }

    public class UpsertCallHandler : IRequestHandler<UpsertCallRequest, int>
    {
        private readonly IUnitOfWork unit;
        private readonly ICurrentUserService user;
        private readonly IActiveObjectsService usersCaching;
        private readonly IMapper mapper;

        public UpsertCallHandler(IUnitOfWork unit, ICurrentUserService user, IActiveObjectsService usersCaching, IMapper mapper)
        {
            this.unit = unit;
            this.user = user;
            this.usersCaching = usersCaching;
            this.mapper = mapper;   
        }

        public Task<int> Handle(UpsertCallRequest request, CancellationToken cancellationToken)
        {
            var category = unit.CategoryRepository.Read(request.Category.Id);
            var card = unit.CardRepository.GetByCallId(request.Id);
            var currentUser = unit.UserRepository.GetUserById(user.AccountId);

            var call = new Call
            {
                Id = request.Id,
                CallAuthor = request.CallAuthor,
                Phone = request.Phone,
                Category = category,
                PrivateNumber = request.PrivateNumber,
                Note = request.Note,
                CreateDate = DateTime.Now,
                CallType = request.CallType,
                User = currentUser,
                Card = card
            };

            if (request.CallType == 1 && card != null)
            {
                call.Card = null;
            }

            if (request.CallType == 2 && card == null)
            {
                var candidate = usersCaching.GetCandidate();
                var supervaiser = unit.UserRepository.GetUserByUserName(candidate);
                usersCaching.IncrementTask(candidate);
                call.Card = new Card
                {
                    User = supervaiser,
                    Status = -1,
                    UserType = 2,
                    CallId = request.Id,    
                };
            }
           
            if (request.Id == default)
            {
                return Task.FromResult(unit.CallRepository.CreateCall(call));
            }

            return Task.FromResult(unit.CallRepository.UpdateCall(request.Id, call));
            
        }
    }

    public class UpsertCallDtoValidator : AbstractValidator<UpsertCallRequest>
    {
        public UpsertCallDtoValidator()
        {
            RuleFor(x => x.Category).NotEmpty().WithMessage("კატეგორიის მითითება აუცილებელია!");
            RuleFor(x => x.Note).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია!");
        }
    }

}
