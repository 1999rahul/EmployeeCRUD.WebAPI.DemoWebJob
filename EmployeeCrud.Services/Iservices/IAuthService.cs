using EmployeeCrud.Domain.Models.Wrapper;
using EmployeeCrud.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Services.Iservices
{
    public interface IAuthService
    {
        Result<UserVM> CreateUser(UserDtoVM user);
        Result<string> Login(UserLoginVM user);
    }
}
