using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Contexto;
using Modelo.DTO;
using Modelo.Entidades;
using Negocio;

namespace ApiRestCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly BP_CLIENTESContext _context;

        public ClienteController(BP_CLIENTESContext context) => _context = context;
        [HttpGet]
        public IEnumerable<Cliente> Get()
             =>  _context.Clientes.ToList();

        [HttpGet("{ClienteId}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int ClienteId)
        {
            var cliente =  _context.Clientes.Find(ClienteId);
            return cliente == null ? NotFound() : Ok(cliente);
        }


        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public IActionResult> Create(Cliente cliente)
        //{
        //     _context.Clientes.AddAsync(cliente);
        //     _context.SaveChangesAsync();
        //    return Content("Creacion Existosa.");
        //}
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create(DTOCliente dtoCliente)
        {

            NegocioCliente negocioCliente = new NegocioCliente(_context);
            var respuesta = negocioCliente.GuardarCliente(dtoCliente);
            return Content(respuesta.Result);
        }
        [HttpPut("{ClienteId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int ClienteId, Cliente cliente)
        {
            if (ClienteId != cliente.ClienteId) return BadRequest();

            _context.Entry(cliente).State = EntityState.Modified;
             _context.SaveChanges();
            if (cliente.Persona != null)
            {
                cliente.Persona.PersonaId= cliente.PersonaId;
                Persona persona = cliente.Persona;
                _context.Entry(persona).State = EntityState.Modified;
                 _context.SaveChanges();
            }
            return Content("Actualizacion con Existo.");
        }

        [HttpDelete("{ClienteId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int ClienteId)
        {
            var clienteToDelete =  _context.Clientes.Find(ClienteId);
            if (clienteToDelete == null) return NotFound();

            _context.Clientes.Remove(clienteToDelete);
             _context.SaveChanges();

            return Content("Eliminacion con Exito.");
        }
    }
}
