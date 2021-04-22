using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Data.Repositories
{
    public class FamilyRepository : BaseRepository
    {
        private async Task<int> GetUserScore(AppUser user, int month)
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