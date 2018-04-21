using SQLite;

namespace Molfar.Models
{
    [Table("Settings")]
    public class Setting
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(24)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Value { get; set; }

    }
}
