using API.Entities;

namespace API.Data.Repositories
{
    public class SnitchPollRepository : BaseRepository
    {
        private void OnPoleSuccessAsync(Snitch snitch, bool targetIsGuilty)
        {
            snitch.Rich.Target = targetIsGuilty ? snitch.Target : snitch.Creator;
        }
    }
}