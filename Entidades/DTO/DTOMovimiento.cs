using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.DTO
{
    public class DTOMovimiento
    {
        public DTOMovimiento() { }
        public int NumeroCuenta { get; set; }
        public string Tipo { get; set; }
        //public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public decimal Movimiento { get; set; }
        //public string TipoMovimiento { get; set; }
        public DateTime Fecha { get; set; }
        //public string Cliente { get; set; }
        //public decimal SaldoDisponible { get; set; }

    }
}
