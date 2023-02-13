using EmployeeCrud.Domain.Models.Wrapper;
using EmployeeCrud.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Services.Iservices
{
    public interface IEmployeeService
    {
        Result<IEnumerable<EmployeeVM>> GetAllEmployees();
        Result<EmployeeVM> GetEmployee(int? id);
        Result<EmployeeVM> PostEmployee(EmployeeVM book);
        Result<EmployeeVM> UpdateEmployee(EmployeeVM book);
        Result<EmployeeVM> DeleteEmployee(int id);
    }
}
