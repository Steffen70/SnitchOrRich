using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public UserRepository UserRepository => new UserRepository(_context, this, _mapper);
        public MemberRepository MemberRepository => new MemberRepository(_context, this, _mapper);
        public SnitchRepository SnitchRepository => new SnitchRepository(_context, this, _mapper);
        public SnitchPollRepository SnitchPollRepository => new SnitchPollRepository(_context, this, _mapper);
        public RichRepository ChoreRepository => new RichRepository(_context, this, _mapper);
        public FamilyRepository FamilyRepository => new FamilyRepository(_context, this, _mapper);

        public async Task<bool> Complete()
        => await _context.SaveChangesAsync() > 0;

        public bool HasChanges()
        => _context.ChangeTracker.HasChanges();

        public async Task MigrateAsync()
        => await _context.Database.MigrateAsync();
    }
}