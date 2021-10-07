using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tasks.Core.Application.Interfaces.Repositories;
using Tasks.Core.Domain.Models;

namespace Tasks.Infrastructure.Persistence.Implementations.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }


        private IQueryable<User> Including =>
            this.context.Users.Include(x => x.Resources);

        public async Task<IQueryable<User>> Filter(string privateNumber, string firstName, string lastName)
        {
            var users = Including.Where(x =>
                (string.IsNullOrWhiteSpace(privateNumber) || x.PrivateNumber == privateNumber)
                && (string.IsNullOrWhiteSpace(firstName) || x.FirstName == firstName)
                && (string.IsNullOrWhiteSpace(lastName) || x.LastName == lastName));

            return await Task.FromResult(users);
        }

        public User GetUserByUserName(string userName) {
            return Including.FirstOrDefault(x => x.UserName == userName && x.Resources.Select(x => x.Name).Contains("ROLE.SUPERVAISER"));
        }

        public User GetUserById(int userId) {
            return Including.FirstOrDefault(x => x.Id == userId);
        }

        public override async Task<User> ReadAsync(int id)
        {
            var res = await Including.FirstAsync(x => x.Id == id);
            return res;
        }

        public override void Update(int id, User user)
        {
            user.Id = id;

            var existing = this.Including.First(x => x.Id == id);
            // თუ პაროლში არაფერი გადმოეწოდა დარჩეს იგივე. თუ გადმოეწოდა შეიცვალოს
            user.Password = string.IsNullOrWhiteSpace(user.Password) ? existing.Password : user.Password;

            context.Entry(existing).CurrentValues.SetValues(user);
        }

        public override async Task<IEnumerable<User>> ReadAsync(Expression<Func<User, bool>> predicate)
        {
            return await this.Including.Where(predicate).ToListAsync();
        }

        public override void Create(User user)
        {
            var resources = user.Resources;
            user.Resources.Clear();
            foreach (var item in resources)
            {
               user.Resources.Add(context.Resources.First(x => x.Id == item.Id));
            }
            context.Users.Add(user);
        }


    }
}
