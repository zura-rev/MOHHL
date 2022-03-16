using HR.Core.Application.Interfaces.Repositories;
using HR.Core.Domain.Enums;
using HR.Core.Domain.Models;
using System;
using System.Linq;

namespace HR.Infrastructure.Persistence.Implementations.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly DataContext context;

        public EmployeeRepository(DataContext context)
        {
            this.context = context;
        }

        IQueryable<Employee> IEmployeeRepository.Filter(
                int id,
                string privateNumber,
                string firstName,
                string lastName,
                Gender gender,
                DateTime birthDate
            )
        {
            try
            {
                var res = context.Employees
                    .Where(x =>
                         (id == 0 || x.Id == id) &&
                         //(x.BirthDate == null && x.BirthDate == birthDate) &&
                         (string.IsNullOrWhiteSpace(privateNumber) || x.PrivateNumber == privateNumber) &&
                         (string.IsNullOrWhiteSpace(firstName) || x.FirstName.Contains(firstName) &&
                         (string.IsNullOrWhiteSpace(lastName) || x.LastName.Contains(lastName) &&
                         (gender == 0 || x.Gender == gender))))
                     .OrderByDescending(x => x.Id);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        Employee IEmployeeRepository.Read(int id)
        {
            return context.Employees.FirstOrDefault(x => x.Id == id);
        }

        int IEmployeeRepository.Create(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee.Id;
        }

        public Employee Update(Employee employee)
        {
            var result = context.Employees.FirstOrDefault(x => x.Id == employee.Id);
            if (result!=null)
            {
                result.Id = employee.Id;
                result.Gender = employee.Gender;
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.BirthDate = employee.BirthDate;
                result.PrivateNumber = employee.PrivateNumber;
                result.Image = employee.Image;
                context.Employees.Update(result);
                context.SaveChanges();
                return result;
            }
            return null;
        }

        public int Delete(int id)
        {
            var employee = context.Employees.FirstOrDefault(x => x.Id==id);
            context.Employees.Remove(employee);
            context.SaveChanges();
            return employee.Id;
        }
    }
}
