using Modelo.Contexto;
using Modelo.DTO;
using Modelo.Entidades;

namespace Negocio
{

    public class NegocioReporte
    {

        private readonly BP_CLIENTESContext _context;
        public NegocioReporte(BP_CLIENTESContext context) => _context = context;

        public List<DTOReporte> listaReportes()
        {
           var  lista = new List<DTOReporte>();
            var reporteConsulta = _context.Movimientos.Join(_context.Cuenta, x => x.CuentaId, y => y.CuentaId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, y.SaldoInicial, y.Estado, y.ClienteId, y.NumeroCuenta, y.TipoCuentaId })
                      .Join(_context.TipoCuenta, x => x.TipoCuentaId, y => y.TipoCuentaId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, x.SaldoInicial, x.Estado, x.NumeroCuenta, x.ClienteId, y.Descripcion })
                      .Join(_context.Clientes, x => x.ClienteId, y => y.ClienteId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, x.SaldoInicial, x.Estado, x.NumeroCuenta, x.Descripcion, y.PersonaId })
                      .Join(_context.Personas, x => x.PersonaId, y => y.PersonaId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, x.SaldoInicial, x.Estado, x.NumeroCuenta, x.Descripcion, y.Nombre }).ToArray();

            foreach (var rep in reporteConsulta)
            {
                lista.Add(new DTOReporte
                {
                    Fecha = rep.Fecha,
                    Cliente = rep.Nombre,
                    Estado = rep.Estado,
                    Movimiento = rep.Valor,
                    NumeroCuenta = rep.NumeroCuenta,
                    SaldoDisponible = rep.Saldo,
                    SaldoInicial = rep.Valor,

                }); ;
            }
            return lista;
        }

        public string GenerarReporte(string FechaInicio, string FechaFin, string Cliente,out List<DTOReporte> listaReporte)
        {
            try
            {
                listaReporte = new List<DTOReporte>();
                if (String.IsNullOrEmpty(FechaInicio) || String.IsNullOrEmpty(FechaInicio) || String.IsNullOrEmpty(Cliente))
                {
                    return "Datos Incorrectos.";
                }
                var FechaInicioReporte = Convert.ToDateTime(FechaInicio);
                var FechaFinReporte = Convert.ToDateTime(FechaFin);
                DTOReporte reporte = new DTOReporte();
                if (_context.Movimientos.Join(_context.Cuenta, x => x.CuentaId, y => y.CuentaId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, y.SaldoInicial, y.Estado, y.ClienteId, y.NumeroCuenta, y.TipoCuentaId })
                    .Join(_context.TipoCuenta, x => x.TipoCuentaId, y => y.TipoCuentaId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, x.SaldoInicial, x.Estado, x.NumeroCuenta, x.ClienteId, y.Descripcion })
                    .Join(_context.Clientes, x => x.ClienteId, y => y.ClienteId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, x.SaldoInicial, x.Estado, x.NumeroCuenta, x.Descripcion, y.PersonaId })
                    .Join(_context.Personas, x => x.PersonaId, y => y.PersonaId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, x.SaldoInicial, x.Estado, x.NumeroCuenta, x.Descripcion, y.Nombre })
                    .Where(x => x.Fecha >= FechaInicioReporte && x.Fecha <= FechaFinReporte && x.Nombre == Cliente).Any())
                {
                    var reporteConsulta = _context.Movimientos.Join(_context.Cuenta, x => x.CuentaId, y => y.CuentaId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, y.SaldoInicial, y.Estado, y.ClienteId, y.NumeroCuenta, y.TipoCuentaId })
                      .Join(_context.TipoCuenta, x => x.TipoCuentaId, y => y.TipoCuentaId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, x.SaldoInicial, x.Estado, x.NumeroCuenta, x.ClienteId, y.Descripcion })
                      .Join(_context.Clientes, x => x.ClienteId, y => y.ClienteId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, x.SaldoInicial, x.Estado, x.NumeroCuenta, x.Descripcion, y.PersonaId })
                      .Join(_context.Personas, x => x.PersonaId, y => y.PersonaId, (x, y) => new { x.Fecha, x.Valor, x.Saldo, x.SaldoInicial, x.Estado, x.NumeroCuenta, x.Descripcion, y.Nombre })
                      .Where(x => x.Fecha >= FechaInicioReporte && x.Fecha <= FechaFinReporte && x.Nombre == Cliente).ToList();

                    foreach (var rep in reporteConsulta)
                    {
                        listaReporte.Add(new DTOReporte {
                        Fecha=rep.Fecha,
                        Cliente=rep.Nombre,
                        Estado=rep.Estado,
                        Movimiento=rep.Valor,
                        NumeroCuenta=rep.NumeroCuenta,
                        SaldoDisponible=rep.Saldo,
                        SaldoInicial=rep.Valor,
                        
                        });;
                    }
                }


            }
            catch (Exception ex)
            {
                listaReporte = new List<DTOReporte>();
                return ex.Message;
            }


            return "Consulta con Existo.";
        }

        public enum EnumTipoMovimento
        {
            Retiro = 1,
            Deposito = 2

        }
    }
}
