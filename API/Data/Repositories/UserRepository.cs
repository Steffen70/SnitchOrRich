using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using API.Entities;
using API.Helpers.Filtration;
using AutoMapper.QueryableExtensions;

namespace API.Data.Repositories
{
    public class UserRepository : BaseRepository
    {
        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username.ToLower());
        }

        public void AddUser(AppUser user)
        {
            _context.Users.Add(user);
        }

        public void UpdateUser(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(AppUser user)
        {
            _context.Users.Remove(user);
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.Username == username.ToLower());
        }

        public async Task<FilteredList<UserAdminDto>> GetUsersAsync(FiltrationParams filtrationParams)
        {
            var userAdminDtos = _context.Users
                .Where(u => u.Created <= filtrationParams._timeStamp)
                .OrderBy(u => u.LastActive)
                .ProjectTo<UserAdminDto>(_mapper.ConfigurationProvider);

            return await FilteredList<UserAdminDto>.CreateAsync(userAdminDtos, filtrationParams, _mapper);
        }

        public async Task<bool> AnyUsersAsync()
        => await _context.Users.AnyAsync();
    }
}