using EmployeeCrud.Data.Reopsitories.DapperRepository;
using EmployeeCrud.Domain.IRepositories;
using EmployeeCrud.Domain.IUnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Data.UnitOfWorks
{
    public class DapUnitOfWork : IUnitOfWork, IDisposable
    {
        IDbConnection connection;
        public DapUnitOfWork(string cnnString)
        {
            this.connection = new SqlConnection(cnnString);
            //this.connection.Open();
        }

        private IEmployeeRepository _employeeRepository;
        private IAuthRepository _authRepository;
        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return _employeeRepository ?? (_employeeRepository = new EmployeeReopsitory(connection));
            }
        }

        public IAuthRepository AuthRepository
        {
            get
            { 
                return _authRepository ?? (_authRepository = new AuthRepository(connection)); 
            }
        }

        public void Dispose()
        {
            if(connection != null)
            {
                connection.Close(); 
            }

        }

    }
}
