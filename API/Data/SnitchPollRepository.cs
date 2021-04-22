using AutoMapper;

namespace API.Data
{
    public class SnitchPollRepository : BaseRepository
    {
        public SnitchPollRepository(DataContext context, UnitOfWork unitOfWork, IMapper mapper) : base(context, unitOfWork, mapper) { }
    }
}