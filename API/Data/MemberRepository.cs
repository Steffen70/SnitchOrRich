using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers.Filtration;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class MemberRepository
    {
        private readonly DataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MemberRepository(DataContext context, UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<MemberDto> GetMemberByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);

            return _mapper.Map<MemberDto>(user);
        }

        public async Task<MemberDto> GetMemberByUsernameAsync(string username)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);

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