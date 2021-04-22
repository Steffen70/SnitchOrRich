using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Data.Repositories
{
    public class SnitchRepository : BaseRepository
    {
        private void CompleteRich(Snitch snitch, AppUser user)
        {
            snitch.Rich.Completed = DateTime.UtcNow;
            snitch.Rich.Creator = user;
        }

        private async Task SetRichPoints(Snitch snitch)
        {
            snitch.Rich.Points = (int)Math.Round(await snitch.SnitchPoll.Votes
                .AsQueryable()
                .AverageAsync(v => v.Points), 0);
        }
    }
}