using Modelo.Contexto;
using Modelo.DTO;
using Modelo.Entidades;

namespace Negocio
{

    public class NegocioMovimiento
    {

        private  readonly BP_CLIENTESContext _context;
        public NegocioMovimiento(BP_CLIENTESContext context) => _context = context;
        
        public string GuardarMovimiento(DTOMovimiento dtomovimiento)
        {
            try
            {
                Movimiento movimiento = new Movimiento();
                var FechaActual = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                movimiento.Fecha = FechaActual;
                movimiento.CuentaId = _context.Cuenta.ToList().Where(x => x.NumeroCuenta == dtomovimiento.NumeroCuenta).FirstOrDefault().CuentaId;
                
                if (dtomovimiento.Movimiento > 0)
                {
                    movimiento.TipoMovimientoId = (int)EnumTipoMovimento.Deposito;
                }
                else {
                    movimiento.TipoMovimientoId = (int)EnumTipoMovimento.Retiro;
                }
                movimiento.Valor = dtomovimiento.Movimiento;
                decimal saldoInicial = 0;
                decimal limiteRetiro = 1000;
                decimal valorRetiroDiario = 0;
                if (movimiento.TipoMovimientoId == (int)EnumTipoMovimento.Retiro)
                {
                    
                    if (_context.Movimientos.Where(x=>x.CuentaId==movimiento.CuentaId).Count() > 0)
                    {
                        saldoInicial = _context.Movimientos.Where(x=>x.CuentaId == movimiento.CuentaId).OrderByDescending(x => x.Fecha ).FirstOrDefault().Saldo;
                    }
                    else
                    {
                        saldoInicial = _context.Cuenta.ToList().Where(x => x.NumeroCuenta == dtomovimiento.NumeroCuenta).FirstOrDefault().SaldoInicial;
                    }

                    if (saldoInicial == 0)
                    {
                        return "Saldo no disponible";
                    }
                    else
                    {
                        if (_context.Movimientos.Where(x => x.Fecha == FechaActual && x.CuentaId==movimiento.CuentaId).Any())
                        {
                            valorRetiroDiario = _context.Movimientos.Where(x => x.Fecha == FechaActual && x.CuentaId == movimiento.CuentaId).Sum(x => x.Valor);
                            if ((valorRetiroDiario + dtomovimiento.Movimiento)*-1 > limiteRetiro)
                            {
                                return "Cupo diario Excedido";
                            }
                        }
                        else
                        {
                            movimiento.Saldo = saldoInicial + dtomovimiento.Movimiento;
                            if (movimiento.Saldo < 0)
                            {
                                return "Saldo Insuficiente.";
                            }
                        }
                    }

                }
                else
                {
                    if (_context.Movimientos.Count() > 0)
                    {
                        saldoInicial = _context.Movimientos.Where(x=>x.CuentaId==movimiento.CuentaId).OrderByDescending(x => x.Fecha ).FirstOrDefault().Saldo;
                    }
                    else
                    {
                        saldoInicial = _context.Cuenta.ToList().Where(x => x.NumeroCuenta == dtomovimiento.NumeroCuenta).FirstOrDefault().SaldoInicial;
                    }

                    movimiento.Saldo = saldoInicial + dtomovimiento.Movimiento;
                }
                
                 _context.Movimientos.Add(movimiento);
                 _context.SaveChanges();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            

            return "Creacion con Existo.";
        }

        public enum EnumTipoMovimento
        {
            Retiro=1,
            Deposito=2

        }
    }
}
