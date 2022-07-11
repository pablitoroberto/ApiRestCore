using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Entidades
{
    [Table("CUENTA", Schema = "BP")]
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        [Key]
        [Column("CUENTA_ID")]
        public int CuentaId { get; set; }
        [Column("CLIENTE_ID")]
        public int ClienteId { get; set; }
        [Column("TIPO_CUENTA_ID")]
        public int TipoCuentaId { get; set; }
        [Column("NUMERO_CUENTA")]
        public long NumeroCuenta { get; set; }
        [Column("SALDO_INICIAL", TypeName = "decimal(18, 4)")]
        public decimal SaldoInicial { get; set; }
        [Column("ESTADO")]
        public bool Estado { get; set; }

        [ForeignKey("ClienteId")]
        [InverseProperty("Cuenta")]
        public virtual Cliente Cliente { get; set; }
        [ForeignKey("TipoCuentaId")]
        [InverseProperty("Cuenta")]
        public virtual TipoCuentum TipoCuenta { get; set; }
        [InverseProperty("Cuenta")]
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}