using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Modelo.Entidades
{
    [Table("CLIENTE", Schema = "BP")]
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        [Key]
        [Column("CLIENTE_ID")]
        public int ClienteId { get; set; }
        [Column("PERSONA_ID")]
        public int PersonaId { get; set; }
        [Required]
        [Column("CONTRASENIA")]
        [StringLength(50)]
        public string Contrasenia { get; set; }
        [Column("ESTADO")]
        public bool Estado { get; set; }

        [ForeignKey("PersonaId")]
        [InverseProperty("Clientes")]
        public virtual Persona Persona { get; set; }
        [InverseProperty("Cliente")]
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
