using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Entidades
{
    [Table("MOVIMIENTO", Schema = "BP")]
    public partial class Movimiento
    {
        [Key]
        [Column("MOVIMIENTO_ID")]
        public int MovimientoId { get; set; }
        [Column("CUENTA_ID")]
        public int CuentaId { get; set; }
        [Column("TIPO_MOVIMIENTO_ID")]
        public int TipoMovimientoId { get; set; }
        [Column("FECHA", TypeName = "datetime")]
        public DateTime Fecha { get; set; }
        [Column("VALOR", TypeName = "decimal(18, 4)")]
        public decimal Valor { get; set; }
        [Column("SALDO", TypeName = "decimal(18, 4)")]
        public decimal Saldo { get; set; }

        [ForeignKey("CuentaId")]
        [InverseProperty("Movimientos")]
        public virtual Cuenta Cuenta { get; set; }
        [ForeignKey("TipoMovimientoId")]
        [InverseProperty("Movimientos")]
        public virtual TipoMovimiento TipoMovimiento { get; set; }
    }
}