using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string PublicId { get; set; }

        public Chore Chore { get; set; }
        public int ChoreId { get; set; }
    }
}