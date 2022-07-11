using Modelo.Contexto;
using Modelo.DTO;
using Modelo.Entidades;

namespace Negocio
{

    public class NegocioCuenta
    {
        private readonly BP_CLIENTESContext _context;
        public NegocioCuenta(BP_CLIENTESContext context) => _context = context;
        public async Task<string> GuardarCuenta(DTOCuenta dtocuenta)
        {
            try
            {
                Cuenta cuenta = new Cuenta();
                
                cuenta.NumeroCuenta = dtocuenta.NumeroCuenta;
                cuenta.TipoCuentaId = _context.TipoCuenta.ToList().Where(x => x.Descripcion == dtocuenta.Tipo).FirstOrDefault().TipoCuentaId;
                cuenta.SaldoInicial = dtocuenta.SaldoInicial;
                cuenta.Estado = dtocuenta.Estado;
                cuenta.ClienteId = _context.Clientes.Join(_context.Personas,x=>x.PersonaId,y=>y.PersonaId,(x,y)=> new {x.ClienteId,y.Nombre}).ToList().Where(x => x.Nombre == dtocuenta.Cliente).FirstOrDefault().ClienteId;
                await _context.Cuenta.AddAsync(cuenta);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            

            return "Creacion con Existo.";
        }
    }
}
