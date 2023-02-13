using EmployeeCrud.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Domain.IRepositories
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int? id);
        Employee PostEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        bool DeleteEmployee(int id);    
        IEnumerable<Employee> GetAllEmployee();
    }
}
