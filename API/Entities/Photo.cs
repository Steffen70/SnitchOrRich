using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string PublicId { get; set; }

        public Rich Rich { get; set; }
        public Snitch Snitch { get; set; }
        public IChore Chore => (IChore)Rich ?? Snitch;
    }
}