using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Data.Repositories;
using API.DTOs;
using API.Extensions;
using API.Helpers.Filtration;
using AutoMapper;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserRepository _userRepository;
        public AdminController(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _userRepository = unitOfWork.GetRepo<UserRepository>();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-role")]
        public async Task<ActionResult<IEnumerable<UserAdminDto>>> GetUsers([FromQuery] FiltrationParams filtrationParams)
        {
            var users = await _userRepository.GetUsersAsync(filtrationParams);

            Response.AddFiltrationHeader(users);

            return users.Result;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("change-role/{username}")]
        public async Task<ActionResult> ChangeUserRole([FromRoute] string username, string role)
        {
            if (User.GetUserName() == username.ToLower())
                return BadRequest("You can not change your own role");

            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user is null) return NotFound($"Could not find user with username: '{username}'");

            user.UserRole = role;

            _userRepository.UpdateUser(user);

            if (!await _unitOfWork.Complete()) return BadRequest("Failed to change user role");

            return Ok(user.UserRole);
        }
    }
}