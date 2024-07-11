using CollegeManagementAPI.Application.DTOs;
using CollegeManagementAPI.Application.Interfaces.Services;
using CollegeManagementAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementAPI.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)

        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsersAsync());

        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDetails loginDetails)
        {
            var result = await _userService.LoginUserAsync(loginDetails);

            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDetail userDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.RegisterUser(userDetail);
            if (result > 0)
            {
                return Ok();
            }

            return StatusCode(500, "An error occurred while registering the user.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserDetail userDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.UpdateUser(userDetail);
            if (result > 0)
            {
                return Ok("User Updated Succesfully.");
            }

            return StatusCode(500, "An error occurred while updating the user.");
        }

        [HttpDelete("delete")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result > 0)
            {
                return Ok("User Deleted Succesfully");
            }

            return StatusCode(500, "An error occurred while deleting the user.");
        }

    }
}



