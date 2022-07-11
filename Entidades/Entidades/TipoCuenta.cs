using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Entidades
{
    [Table("TIPO_CUENTA", Schema = "BP")]
    public partial class TipoCuentum
    {
        public TipoCuentum()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        [Key]
        [Column("TIPO_CUENTA_ID")]
        public int TipoCuentaId { get; set; }
        [Required]
        [Column("DESCRIPCION")]
        [StringLength(25)]
        public string Descripcion { get; set; }

        [InverseProperty("TipoCuenta")]
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}