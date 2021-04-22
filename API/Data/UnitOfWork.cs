using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Data.Repositories;
using AutoMapper;

namespace API.Data
{
    public class UnitOfWork
    {
        internal readonly DataContext _context;
        internal readonly IMapper _mapper;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        private Dictionary<Type, BaseRepository> _repositories = new Dictionary<Type, BaseRepository>();
        public TRepo GetRepo<TRepo>() where TRepo : BaseRepository, new()
        {
            var repoType = typeof(TRepo);

            if (!_repositories.ContainsKey(repoType))
                _repositories.Add(typeof(TRepo), BaseRepository.CreateRepo<TRepo>(_context, this, _mapper));

            var repo = _repositories[repoType] as TRepo;

            if (repo is null)
                _repositories.Remove(repoType);
            else
                return repo;

            return GetRepo<TRepo>();
        }

        public async Task<bool> Complete()
        => await _context.SaveChangesAsync() > 0;

        public bool HasChanges()
        => _context.ChangeTracker.HasChanges();

        public async Task MigrateAsync()
        => await _context.Database.MigrateAsync();
    }
}