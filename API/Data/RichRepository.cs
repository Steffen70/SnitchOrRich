using AutoMapper;

namespace API.Data
{
    public class RichRepository : BaseRepository
    {
        public RichRepository(DataContext context, UnitOfWork unitOfWork, IMapper mapper) : base(context, unitOfWork, mapper) { }
    }
}