using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestSaga.Db.Entity
{
    [Table("beta")]
    public class BetaEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
    }
}