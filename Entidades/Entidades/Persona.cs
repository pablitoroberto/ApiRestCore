using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Entidades
{
    [Table("PERSONA", Schema = "BP")]
    public partial class Persona
    {
        public Persona()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]
        [Column("PERSONA_ID")]
        public int PersonaId { get; set; }
        [Required]
        [Column("NOMBRE")]
        [StringLength(150)]
        public string Nombre { get; set; }
        [Required]
        [Column("GENERO")]
        [StringLength(150)]
        public string Genero { get; set; }
        [Column("EDAD")]
        public int Edad { get; set; }
        [Required]
        [Column("IDENTIFICACION")]
        [StringLength(16)]
        public string Identificacion { get; set; }
        [Required]
        [Column("DIRECCION")]
        [StringLength(250)]
        public string Direccion { get; set; }
        [Required]
        [Column("TELEFONO")]
        [StringLength(10)]
        public string Telefono { get; set; }

        [InverseProperty("Persona")]
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}