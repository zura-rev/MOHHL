using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hl.Core.Application.Interfaces.Repositories;
using Hl.Core.Domain.Models;

namespace Hl.Infrastructure.Persistence.Implementations.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        private IQueryable<User> Including => this.context.Users.Include(x => x.Resources);

        public async Task<IQueryable<User>> Filter(string userName, string privateNumber, string firstName, string lastName)
        {
            var users = Including.Where(x =>
                (string.IsNullOrWhiteSpace(userName) || x.UserName == userName)
                && (string.IsNullOrWhiteSpace(privateNumber) || x.PrivateNumber == privateNumber)
                && (string.IsNullOrWhiteSpace(firstName) || x.FirstName == firstName)
                && (string.IsNullOrWhiteSpace(lastName) || x.LastName == lastName));

            return await Task.FromResult(users);
        }

        public User GetUserByUserName(string userName) 
        {
            return Including.FirstOrDefault(x => x.UserName == userName && x.Resources.Select(x => x.Name).Contains("ROLE.SUPERVAISER"));
        }

        public User GetUserById(int userId) 
        {
            return Including.FirstOrDefault(x => x.Id == userId);
        }

        public override async Task<User> ReadAsync(int id)
        {
            return await Including.FirstAsync(x => x.Id == id);
        }

        User IUserRepository.CreateUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        User IUserRepository.UpdateUser(int id, User user)
        {
            try
            {
                user.Id = id;
                var existing = this.Including.First(x => x.Id == id);
                existing.Resources = user.Resources;
                // თუ პაროლში არაფერი გადმოეწოდა დარჩეს იგივე. თუ გადმოეწოდა შეიცვალოს
                user.Password = string.IsNullOrWhiteSpace(user.Password) ? existing.Password : user.Password;
                context.Entry(existing).CurrentValues.SetValues(user);
                //context.Entry(existing.Resources).CurrentValues.SetValues(user.Resources);
                context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override async Task<IEnumerable<User>> ReadAsync(Expression<Func<User, bool>> predicate)
        {
            return await this.Including.Where(predicate).ToListAsync();
        }

    }
}
