using AutoMapper;

namespace API.Data
{
    public class SnitchRepository : BaseRepository
    {
        public SnitchRepository(DataContext context, UnitOfWork unitOfWork, IMapper mapper) : base(context, unitOfWork, mapper) { }
    }
}