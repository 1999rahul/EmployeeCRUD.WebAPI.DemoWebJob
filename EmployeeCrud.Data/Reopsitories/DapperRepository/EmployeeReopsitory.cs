using Dapper;
using EmployeeCrud.Domain.IRepositories;
using EmployeeCrud.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Data.Reopsitories.DapperRepository
{
    public class EmployeeReopsitory: IEmployeeRepository
    {
        IDbConnection connection;
        public EmployeeReopsitory(IDbConnection Connection)
        {
            connection = Connection;
        }

        public bool DeleteEmployee(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int16);
            var res = connection.Execute("spDeleteEmployee", parameters, commandType: CommandType.StoredProcedure);
            if (res == 1)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            var res = connection.Query<Employee>("spGetAllEmployee", new DynamicParameters(), commandType: CommandType.StoredProcedure);
            return res;
        }
        public Employee GetEmployee(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int16);
            var res = connection.Query<Employee>("spGetEmployee", parameters, commandType: CommandType.StoredProcedure).ToList()[0];
            return res;
        }

        public Employee PostEmployee(Employee employee)
        {
            var parameters = new DynamicParameters();
            parameters.Add("FullName",employee.FullName, DbType.String);
            parameters.Add("Address", employee.Address, DbType.String);
            parameters.Add("DOB", employee.DOB, DbType.String);
            parameters.Add("Mobile", employee.Mobile, DbType.String);
            parameters.Add("IsActive", employee.IsActive, DbType.Boolean);
            var res = connection.Query<Employee>("spAddEmployee", parameters, commandType: CommandType.StoredProcedure).ToList()[0];
            return res;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", employee.EmployeeId, DbType.Int32);
            parameters.Add("FullName", employee.FullName, DbType.String);
            parameters.Add("Address", employee.Address, DbType.String);
            parameters.Add("DOB", employee.DOB, DbType.String);
            var res = connection.Query<Employee>("spUpdateEmployee", parameters, commandType: CommandType.StoredProcedure).ToList()[0];
            return res;
        }
    }
}
