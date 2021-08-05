using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HL.Core.Application.Commons;
using HL.Core.Application.Interfaces;
using HL.Core.Domain.Models;

namespace HL.Core.Application.Features.Users.Commands
{
    public class UpsertUserRequest : IRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Resource> Resources { get; set; }


        public User GetOrigin()
        {
            User user = new User()
            {
                Id = this.Id,
                UserName = this.UserName,
                Password = Functions.GetPasswordHash(this.UserName, this.Password),
                PrivateNumber = this.PrivateNumber,
                FirstName = this.FirstName,
                LastName = this.LastName
            };

            user.Resources = new HashSet<Resource>();

            foreach (var item in Resources)
            {
                user.Resources.Add(new Resource(item.Id, item.Name, item.Description));
            }

            return user;
        }
    }

    public class UpsertUserHandler : IRequestHandler<UpsertUserRequest>
    {
        private readonly IUnitOfWork unit;
        public UpsertUserHandler(IUnitOfWork unit) => this.unit = unit;


        public async Task<Unit> Handle(UpsertUserRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == default)
            {
                var user = request.GetOrigin();

                unit.UserRepository.Create(user);
                await unit.SaveAsync();
            }
            else
            {
                unit.UserRepository.Update(request.Id, request.GetOrigin());
                await unit.SaveAsync();
            }

            return await Task.FromResult(Unit.Value);
        }
    }

    public class UpsertUserValidator : AbstractValidator<UpsertUserRequest>
    {
        private readonly IUnitOfWork unit;

        public UpsertUserValidator(IUnitOfWork unit)
        {
            this.unit = unit;

            RuleFor(x => x.PrivateNumber)
                .NotNull().WithMessage("პირადი ნომერი ცარიელია")
                .Length(11).WithMessage("პირადი ნომერი უნდა შედგებოდეს 11 სიმბოლოსგან")
                .Matches("^[0-9]*$").WithMessage("პირადი ნომერი უნდა შედგებოდეს მხოლოდ ციფრებისგან");

            RuleFor(x => x.Resources)
                 .NotEmpty().WithMessage("{PropertyName} მითითება აუცილებელია")
                 .NotNull()
                 .MustAsync(IfExistAllResources).WithMessage("{PropertyName} მითითებული რესურსები არ არსებობს");
        }

        private async Task<bool> IfExistAllResources(ICollection<Resource> resources, CancellationToken cancellationToken)
        {
            return await unit.ResourceRepository.CheckAllAsync(resources.Select(x => x.Id).ToList());
        }
    }
}
