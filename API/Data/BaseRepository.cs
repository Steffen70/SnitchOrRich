using AutoMapper;

namespace API.Data
{
    public abstract class BaseRepository
    {
        protected DataContext _context;
        protected UnitOfWork _unitOfWork;
        protected IMapper _mapper;

        public static TRepo CreateRepo<TRepo>(DataContext context, UnitOfWork unitOfWork, IMapper mapper)
            where TRepo : BaseRepository, new()
        {
            var repo = new TRepo
            {
                _mapper = mapper,
                _unitOfWork = unitOfWork,
                _context = context
            };
            
            repo.Initialize();

            return repo;
        }

        public virtual void Initialize() { }
    }
}