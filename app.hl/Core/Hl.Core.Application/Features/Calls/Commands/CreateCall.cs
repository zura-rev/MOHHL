using Hl.Core.Application.Interfaces;
using Hl.Core.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Hl.Core.Application.Interfaces.Contracts;

namespace Hl.Core.Application.Features.Calls.Commands
{
    public class CreateCallRequest : IRequest<int>
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

    public class CreateCallHandler : IRequestHandler<CreateCallRequest, int>
    {
        private readonly IUnitOfWork unit;
        private readonly ICurrentUserService user;
        private readonly IActiveObjectsService usersCaching;

        public CreateCallHandler(IUnitOfWork unit, ICurrentUserService user, IActiveObjectsService usersCaching)
        {
            this.unit = unit;
            this.user = user;
            this.usersCaching = usersCaching;
        }

        public Task<int> Handle(CreateCallRequest request, CancellationToken cancellationToken)
        {
            var category = unit.CategoryRepository.Read(request.Category.Id);
            
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
                User = currentUser
            };

            if (request.CallType == 2)// && supervaiser != null)
            {
                var candidate = usersCaching.GetCandidate();
                //request.Supervaiser = candidate;
                var supervaiser = unit.UserRepository.GetUserByUserName(candidate);
                usersCaching.IncrementTask(candidate);

                call.Card = new Card
                {
                    User = supervaiser,
                    Status = -1,
                    UserType = 2
                };                
            }

            int id = unit.CallRepository.CreateCall(call);

            return Task.FromResult(id);
        }
    }

    public class SetCallDtoValidator : AbstractValidator<CreateCallRequest>
    {
        public SetCallDtoValidator()
        {
            //RuleFor(x => x.PrivateNumber).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
            //RuleFor(x => x.CallAuthor).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
            RuleFor(x => x.Category.Id).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
            RuleFor(x => x.Note).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
        }
    }
}
