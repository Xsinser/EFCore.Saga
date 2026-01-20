using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestSaga.Db.Entity
{
    [Table("alpha")]
    public class AlphaEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
    }
}