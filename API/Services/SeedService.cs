using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using API.Data;
using API.Data.Repositories;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Services
{
    public class SeedService
    {
        private readonly IOptions<ApiSettings> _apiSettings;
        private readonly IWebHostEnvironment _env;
        private readonly UnitOfWork _unitOfWork;
        private readonly UserRepository _userRepository;
        public SeedService(UnitOfWork unitOfWork, IWebHostEnvironment env, IOptions<ApiSettings> apiSettings)
        {
            _userRepository = unitOfWork.GetRepo<UserRepository>();
            _unitOfWork = unitOfWork;
            _env = env;
            _apiSettings = apiSettings;
        }

        public async Task SeedData()
        {
            if (await CreateDatabaseAsync())
            {
                if (_env.IsDevelopment())
                    await SeedUsersAsync();


                if (await _unitOfWork.Complete())
                    return;

                throw new Exception("Database seeding operation failed");
            }
        }

        private async Task<bool> CreateDatabaseAsync()
        {
            await _unitOfWork.MigrateAsync();

            if (await _userRepository.AnyUsersAsync()) return false;

            using var hmac = new HMACSHA512();
            var admin = new AppUser
            {
                Username = "admin",
                PasswordHash = hmac.ComputeHash(
                    Encoding.UTF8.GetBytes(_apiSettings.Value.AdminPassword)),
                PasswordSalt = hmac.Key,
                UserRole = "Admin"
            };

            _userRepository.AddUser(admin);

            return true;
        }

        private async Task SeedUsersAsync()
        {
            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
            var registerDtos = JsonSerializer.Deserialize<List<RegisterDto>>(userData);

            if (registerDtos == null) return;

            var demoPassword = Encoding.UTF8.GetBytes(_apiSettings.Value.DemoPassword);
            foreach (var r in registerDtos)
            {
                using var hmac = new HMACSHA512();
                _userRepository.AddUser(new AppUser
                {
                    Username = r.Username.ToLower(),
                    PasswordHash = hmac.ComputeHash(string.IsNullOrWhiteSpace(r.Password)
                        ? demoPassword
                        : Encoding.UTF8.GetBytes(r.Password)),
                    PasswordSalt = hmac.Key
                });
            }
        }
    }
}