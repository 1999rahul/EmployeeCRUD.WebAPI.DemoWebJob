using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Domain.IConnection
{
    public interface IConnection
    {
        public string GetConnectionString();
    }
}
