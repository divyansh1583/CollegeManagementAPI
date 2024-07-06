using CollegeManagementAPI.Application.DTOs;
using CollegeManagementAPI.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementAPI.net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                await _userService.AddUserAsync(userDto);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
