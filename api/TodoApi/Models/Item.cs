using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("tasks")]
    public class Item
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("details")]
        public string Details { get; set; }
    }
}
