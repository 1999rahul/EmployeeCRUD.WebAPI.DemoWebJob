using EmployeeCrud.Domain.IRepositories;
using EmployeeCrud.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Data.Reopsitories.EFCoreRepository
{
    public class EFCoreEmployeeRepository : IEmployeeRepository
    {
        private EmployeeDBContext _dbContext;
        public EFCoreEmployeeRepository(EmployeeDBContext dbContext)
        {
            _dbContext= dbContext;
        }   
        public bool DeleteEmployee(int id)
        {
            
                bool result = false;
                var employee = _dbContext.employee.Find(id);
                if (employee != null)
                {
                         _dbContext.Entry(employee).State = EntityState.Deleted;
                         _dbContext.SaveChanges();
                          result = true;
                }
                else
                {
                    result = false;
                }
                return result;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _dbContext.employee.ToList();
        }

        public Employee GetEmployee(int? id)
        {
            return _dbContext.employee.Find(id);
        }

        public Employee PostEmployee(Employee employee)
        {
            _dbContext.employee.Add(employee);  
            _dbContext.SaveChanges();
            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            _dbContext.employee.Update(employee);
            _dbContext.SaveChanges();
            return employee;
        }
    }
}
