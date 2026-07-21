using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ServiceNow.ServiceNow.Application.Interfaces;

namespace ServiceNow.ServiceNow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _iauthService;
        public UserController(IAuthService authService)
        {
            _iauthService = authService;
        }

        [Authorize(Roles="Admin")]
        [HttpGet("orders")]
        public IActionResult GetOrders()
        {
            return Ok("Orders data");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _iauthService.LoginAsync(request.Email, request.Password);

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(string username, string password, string email)
        {
            var result = await _iauthService.CreateUserAsync(username, password, email);
            return Ok();
        }

    }
}
