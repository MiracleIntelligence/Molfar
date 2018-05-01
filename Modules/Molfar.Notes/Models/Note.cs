using SQLite;

namespace Molfar.Notes.Models
{
    [Table("Notes")]
    public class Note
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public string Text { get; set; }

    }
}
