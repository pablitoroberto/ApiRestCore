using Modelo.Contexto;
using Modelo.DTO;
using Modelo.Entidades;

namespace Negocio
{

    public class NegocioCliente
    {
        private readonly BP_CLIENTESContext _context;
        public NegocioCliente(BP_CLIENTESContext context) => _context = context;
        public async Task<string> GuardarCliente(DTOCliente dtocliente)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Persona = new Persona();
                cliente.Persona.Nombre = dtocliente.Nombres;
                cliente.Persona.Direccion = dtocliente.Direccion;
                cliente.Persona.Telefono = dtocliente.Telefono;
                cliente.Persona.Genero  = " ";
                cliente.Persona.Identificacion = " ";
                cliente.Contrasenia = dtocliente.Contrasenia;
                cliente.Estado = dtocliente.Estado;

                await _context.Clientes.AddAsync(cliente);
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
