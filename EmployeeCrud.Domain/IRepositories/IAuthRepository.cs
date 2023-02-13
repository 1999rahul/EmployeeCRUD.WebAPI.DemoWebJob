using EmployeeCrud.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Domain.IRepositories
{
    public interface IAuthRepository
    {
        public User CreateUser(User request);
        public bool ValidateUser(string user);
        public User GetUserDetails(string user);
    }
}
