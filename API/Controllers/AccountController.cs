using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public AccountController(UnitOfWork unitOfWork, TokenService tokenService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _unitOfWork.UserRepository.UserExistsAsync(registerDto.Username))
                return BadRequest("Username is taken");

            using (var hmac = new HMACSHA512())
            {
                var user = _mapper.Map<AppUser>(registerDto);

                user.Username = registerDto.Username.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
                user.PasswordSalt = hmac.Key;

                _unitOfWork.UserRepository.AddUser(user);

                if (!await _unitOfWork.Complete())
                    throw new Exception("Registration failed, the user could not be created");

                var userDto = new UserDto { Token = _tokenService.CreateToken(user) };
                return _mapper.Map(user, userDto);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(loginDto.Username);

            if (user is null) return Unauthorized("Invalid username");

            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

                for (int i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            var userDto = new UserDto { Token = _tokenService.CreateToken(user) };
            return _mapper.Map(user, userDto);
        }
    }
}