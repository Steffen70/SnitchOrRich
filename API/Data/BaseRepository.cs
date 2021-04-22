using AutoMapper;

namespace API.Data
{
    public abstract class BaseRepository
    {
        protected readonly DataContext _context;
        protected readonly UnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BaseRepository(DataContext context, UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }
    }
}