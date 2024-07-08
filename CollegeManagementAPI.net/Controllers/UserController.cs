﻿using CollegeManagementAPI.Application.DTOs;
using CollegeManagementAPI.Application.Interfaces.Repositories;
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

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = Ok(await _userService.GetUsersAsync());
            return users;
            
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDetail = new UserDetail
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                CountryId = userDto.CountryId,
                StateId = userDto.StateId,
                Gender = userDto.Gender,
                IsDeleted = false,
                Password = userDto.Password
            };

            var result = await _userService.RegisterUser(userDetail);
            if (result > 0)
            {
                return Ok("User registered successfully.");
            }

            return StatusCode(500, "An error occurred while registering the user.");
        }
    }
    

}
