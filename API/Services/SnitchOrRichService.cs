using System.Threading.Tasks;
using API.Data;
using API.Entities;

namespace API.Services
{
    public class SnitchOrRichService
    {
        private readonly UnitOfWork _unitOfWork;
        public SnitchOrRichService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // function createSnitch(target, minusPoints) => new instance of snitch + new instance of rich with set snitch

        // function onPoleSuccess(this snitch snitch, bool targetSchuldig) => snitch.rich.target = targetSchulding ? snitch.target : snitch.creator
        public async Task OnPoleSuccessAsync()
        {

        }

        // function setPoints(this snitch snitch, listmoderatorpoints) => snitch.rich.RichReview.Points = listmoderatorpoints.average()
        public async Task SetPointsAsync(Snitch snitch)
        {

        }

        // function completeRichForSnitch(this snitch snitch, person me) => snitch.Rich.Timestamp = now; snitch.rich.creator = me
        public async Task CompleteRichForSnitch(Snitch snitch, AppUser user)
        {

        }
    }
}