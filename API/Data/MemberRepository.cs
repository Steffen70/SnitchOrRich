using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers.Filtration;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class MemberRepository : BaseRepository
    {
        private UserRepository _userRepository;
        public override void Initialize()
        {
            base.Initialize();

            _userRepository = _unitOfWork.GetRepo<UserRepository>();
        }

        public async Task<MemberDto> GetMemberByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            return _mapper.Map<MemberDto>(user);
        }

        public async Task<MemberDto> GetMemberByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            return _mapper.Map<MemberDto>(user);
        }

        public async Task<FilteredList<MemberDto>> GetMembersAsync(FiltrationParams filtrationParams)
        {
            var userAdminDtos = _context.Users
                .OrderBy(u => u.LastActive)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider);

            return await FilteredList<MemberDto>.CreateAsync(userAdminDtos, filtrationParams, _mapper);
        }
    }
}