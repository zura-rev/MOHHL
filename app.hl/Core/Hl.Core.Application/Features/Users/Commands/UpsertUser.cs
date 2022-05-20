using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hl.Core.Application.Commons;
using Hl.Core.Application.Interfaces;
using Hl.Core.Domain.Models;

namespace Hl.Core.Application.Features.Users.Commands
{
    public class UpsertUserRequest : IRequest<User>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Resource> Resources { get; set; }

        public User GetUser()
        {
            return new User
            {
                Id = Id,
                UserName = UserName.ToLower(),
                Password = Functions.GetPasswordHash(UserName, Password),
                PrivateNumber = PrivateNumber,
                FirstName = FirstName,
                LastName = LastName,
                Resources = Resources
            };
        }
    }

    public class UpsertUserHandler : IRequestHandler<UpsertUserRequest, User>
    {
        private readonly IUnitOfWork unit;

        public UpsertUserHandler(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public async Task<User> Handle(UpsertUserRequest request, CancellationToken cancellationToken)
        {
            var resourceIds = request.Resources.Select(x => x.Id).ToList();
            var resources = await unit.ResourceRepository.ReadAsync(x => resourceIds.Contains(x.Id));
            request.Resources = resources.ToList();
            User _user;

            if (request.Id == default)
            {
                _user = unit.UserRepository.CreateUser(request.GetUser());
            }
            else
            {
                _user = unit.UserRepository.UpdateUser(request.Id, request.GetUser());
            }

            return await Task.FromResult(_user);

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
