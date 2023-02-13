using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Services.ViewModels
{
    public class UserDtoVM
    {
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; }=string.Empty;
        public string Password { get; set; }= string.Empty;
    }
}
