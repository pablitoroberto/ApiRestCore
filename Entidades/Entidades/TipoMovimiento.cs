using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Entidades
{
    [Table("TIPO_MOVIMIENTO", Schema = "BP")]
    public partial class TipoMovimiento
    {
        public TipoMovimiento()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        [Key]
        [Column("TIPO_MOVIMIENTO_ID")]
        public int TipoMovimientoId { get; set; }
        [Required]
        [Column("DESCRIPCION")]
        [StringLength(25)]
        public string Descripcion { get; set; }

        [InverseProperty("TipoMovimiento")]
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}