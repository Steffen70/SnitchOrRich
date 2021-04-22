using AutoMapper;

namespace API.Data
{
    public class FamilyRepository : BaseRepository
    {
        public FamilyRepository(DataContext context, UnitOfWork unitOfWork, IMapper mapper) : base(context, unitOfWork, mapper) { }
    }
}