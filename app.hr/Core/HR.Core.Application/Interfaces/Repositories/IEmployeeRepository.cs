using HR.Core.Domain.Enums;
using HR.Core.Domain.Models;
using System;
using System.Linq;

namespace HR.Core.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository 
    {
        IQueryable<Employee> Filter(
            int id,
            string privateNumber,
            string firstName,
            string lastName,
            Gender gender,
            DateTime birthDate
            );
        int Create(Employee employee);
        Employee Read(int id);
        Employee Update(Employee employee); 
        int Delete(int id); 

    }
}
