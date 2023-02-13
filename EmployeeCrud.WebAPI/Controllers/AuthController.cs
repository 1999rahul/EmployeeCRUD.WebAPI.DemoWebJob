using EmployeeCrud.Domain.Models;
using EmployeeCrud.Services.Iservices;
using EmployeeCrud.Services.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCrud.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public IActionResult Register(UserDtoVM request)
        {
            var res=_authService.CreateUser(request);
            return Ok(res);
        }
        [HttpPost("Login")]
        public IActionResult Login(UserLoginVM request)
        {
            var res = _authService.Login(request);
            return Ok(res);
        }

    }
}
