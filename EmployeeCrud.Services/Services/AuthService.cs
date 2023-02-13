using AutoMapper;
using EmployeeCrud.Data.UnitOfWorks;
using EmployeeCrud.Domain.IConnection;
using EmployeeCrud.Domain.Models;
using EmployeeCrud.Domain.Models.Wrapper;
using EmployeeCrud.Services.Iservices;
using EmployeeCrud.Services.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Services.Services
{
    public class AuthService : IAuthService
    {
        IMapper _mapper;
        string connString;
        IConfiguration _config;

        public AuthService(IMapper mapper, IConnection conn,IConfiguration config)
        {
            _mapper = mapper;
            connString = conn.GetConnectionString();
            _config = config;
        }
        public Result<UserVM> CreateUser(UserDtoVM request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var userVM = new UserVM()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PasswordSalt = passwordHash,
                PasswordHash = passwordSalt
            };
            using (DapUnitOfWork unitOfWorks = new DapUnitOfWork(connString))
            {
                var user = unitOfWorks.AuthRepository.CreateUser(_mapper.Map<User>(userVM));
                var res = _mapper.Map<UserVM>(userVM);
                return Result< UserVM >.Success(res);
            }
        }

        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac=new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public Result<string> Login(UserLoginVM user)
        {
            using (DapUnitOfWork unitOfWorks = new DapUnitOfWork(connString))
            {
                var IsValidUser = unitOfWorks.AuthRepository.ValidateUser(user.UserName);

                if (!IsValidUser)
                {
                    return Result<string>.Success("Invalid Credintails");
                }
                var userDetails = _mapper.Map<UserVM>(unitOfWorks.AuthRepository.GetUserDetails(user.UserName));
                var IsPasswordCorrect = VerifyPasswordHash(user.Password, userDetails.PasswordHash, userDetails.PasswordSalt);//something is wrong, PasswordHash is swapped with PasswordSalt thatswhy passing PasswordHash at the place of passwordSalt and vice versa.
                if (!IsPasswordCorrect)
                {
                    return Result<string>.Success("Invalid Credintails");
                }
                var res = new Result<string>();
                res.Data = CreateToken(userDetails.UserName);
                res.Messages = new List<string>() { "Token Received" };
                return res;
            }

        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac =new HMACSHA512(passwordHash))
            {
                var computedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordSalt);
            }
        }
        private string CreateToken(string UserName)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,UserName)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
