using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class SnitchOrRichService
    {
        private readonly UnitOfWork _unitOfWork;
        public SnitchOrRichService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnPoleSuccessAsync(Snitch snitch, bool targetIsGuilty)
        => snitch.Rich.Target = targetIsGuilty ? snitch.Target : snitch.Creator;

        public void CompleteRichForSnitch(Snitch snitch, AppUser user)
        {
            snitch.Rich.Completed = DateTime.UtcNow;
            snitch.Rich.Creator = user;
        }

        public async Task SetRichPoints(Snitch snitch)
        => snitch.Rich.Points = (int)Math.Round(await snitch.SnitchPoll.Votes
            .AsQueryable()
            .AverageAsync(v => v.Points), 0);

        public async Task<int> GetPointsForUser(AppUser user, int month)
        {
            var minusPoints = await _unitOfWork._context.Snitches
                .Where(s => s.Created.Month == month)
                .Where(s => s.Creator == user || s.Target == user)
                .Select(s => s.Rich)
                .Where(r => (r.IsIncomplete || r.Target == user) && r.IsExpired)
                .SumAsync(r => r.Points * r.Multiplier);

            var plusPoints = await _unitOfWork._context.RichEntries
                .Where(s => s.Created.Month == month)
                .Where(r => r.Creator == user)
                .Where(r => r.Target != user)
                .Where(r => !r.IsExpired)
                .SumAsync(r => r.Points);

            return plusPoints - minusPoints;
        }
    }
}