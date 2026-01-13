using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
