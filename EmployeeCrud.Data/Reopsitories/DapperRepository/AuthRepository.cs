using EmployeeCrud.Domain.IRepositories;
using EmployeeCrud.Domain.Models;
using System.Data;
using Dapper;


namespace EmployeeCrud.Data.Reopsitories.DapperRepository
{
    public class AuthRepository : IAuthRepository
    {
        IDbConnection connection;
        public AuthRepository(IDbConnection Connection)
        {
            connection = Connection;
        }
        public User CreateUser(User request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", request.FirstName, DbType.String);
            parameters.Add("@LastName", request.LastName, DbType.String);
            parameters.Add("@UserName", request.UserName, DbType.String);
            parameters.Add("@PasswordHash",request.PasswordHash, DbType.Binary);
            parameters.Add("@PasswordSalt", request.PasswordSalt, DbType.Binary);
            var res = connection.Query<User>("spCreateUser", parameters, commandType: CommandType.StoredProcedure).ToList()[0];
            return res;
        }

        public User GetUserDetails(string userName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", userName, DbType.String);
            var res=connection.Query<User>("spGetUserDetails", parameters, commandType: CommandType.StoredProcedure).ToList()[0];
            return res;
        }
        public bool ValidateUser(string userName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", userName, DbType.String);
            parameters.Add("@IsValidUser", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            connection.Execute("spValidateUser", parameters, commandType: CommandType.StoredProcedure);
            var res = parameters.Get<Boolean>("@IsValidUser");
            return res;
        } 
    }
}
