using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using API.Helpers;
using API.DTOs;
using API.Data;

namespace API.Services
{
    public class SeedService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<ApiSettings> _apiSettings;
        public SeedService(UnitOfWork unitOfWork, IWebHostEnvironment env, IOptions<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings;
            _env = env;
            _unitOfWork = unitOfWork;
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

            if (await _unitOfWork.UserRepository.AnyUsersAsync()) return false;

            using var hmac = new HMACSHA512();
            var admin = new AppUser
            {
                Username = "admin",
                PasswordHash = hmac.ComputeHash(
                    Encoding.UTF8.GetBytes(_apiSettings.Value.AdminPassword)),
                PasswordSalt = hmac.Key,
                UserRole = "Admin"
            };

            _unitOfWork.UserRepository.AddUser(admin);

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
                _unitOfWork.UserRepository.AddUser(new AppUser
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