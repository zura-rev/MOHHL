using AutoMapper;
using Tasks.Core.Application.Interfaces;
using Tasks.Core.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Tasks.Core.Application.Interfaces.Contracts;

namespace Tasks.Core.Application.Features.Calls.Commands
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
        //public int UserId { get; set; }
        public IList<string> Supervaisers { get; set; }
    }

    public class CreateCallHandler : IRequestHandler<CreateCallRequest, int>
    {
        private readonly IUnitOfWork unit;
        private readonly ICurrentUserService user;
        public CreateCallHandler(IUnitOfWork unit, IMapper mapper, ICurrentUserService user)
        {
            this.unit = unit;
            this.user = user;
        }

        public Task<int> Handle(CreateCallRequest request, CancellationToken cancellationToken)
        {
            var category = unit.CategoryRepository.Read(request.Category.Id);
            string userName = request.Supervaisers.FirstOrDefault();

            var cards = new List<Card>();

            cards.Add(new Card
            {
                User = unit.UserRepository.GetUserById(user.AccountId),
                Status = 1,
                UserType = 1
            });

            var supervisor = unit.UserRepository.GetUserByUserName(userName);

            if (request.CallType > 1 && supervisor != null)
            {
                cards.Add(new Card
                {
                    User = supervisor,
                    Status = 1,
                    UserType = 2
                });
            }

            int id = unit.CallRepository.CreateCall(new Call
            {
                Id = request.Id,
                CallAuthor = request.CallAuthor,
                Phone = request.Phone,
                Category = category,
                PrivateNumber = request.PrivateNumber,
                Note = request.Note,
                CreateDate = DateTime.Now,
                CallType = request.CallType,
                Cards = cards
            });

            return Task.FromResult(id);
        }
    }

    public class SetCallDtoValidator : AbstractValidator<CreateCallRequest>
    {
        public SetCallDtoValidator()
        {
            RuleFor(x => x.PrivateNumber).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
            RuleFor(x => x.CallAuthor).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
            RuleFor(x => x.Category.Id).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
            RuleFor(x => x.Note).NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია");
        }
    }
}
