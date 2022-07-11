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
    public class CuentaController : ControllerBase
    {
        private readonly BP_CLIENTESContext _context;

        public CuentaController(BP_CLIENTESContext context) => _context = context;

        [HttpGet]
        public IEnumerable<Cuenta> Get()
             =>  _context.Cuenta.ToList();

        [HttpGet("{CuentaId}")]
        [ProducesResponseType(typeof(Cuenta), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int CuentaId)
        {
            var cuenta =  _context.Cuenta.Find(CuentaId);
            return cuenta == null ? NotFound() : Ok(cuenta);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create(DTOCuenta dtocuenta)
        {
            NegocioCuenta negocioCliente = new NegocioCuenta(_context);
            var respuesta = negocioCliente.GuardarCuenta(dtocuenta);
            return Content(respuesta.Result);
        }

        [HttpPut("{CuentaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int CuentaId, Cuenta cuenta)
        {
            if (CuentaId != cuenta.ClienteId) return BadRequest();

            _context.Entry(cuenta).State = EntityState.Modified;
             _context.SaveChanges();
            return Content("Actualizacion con Existo.");
        }

        [HttpDelete("{CuentaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int CuentaId)
        {
            var cuentaToDelete =  _context.Cuenta.Find(CuentaId);
            if (cuentaToDelete == null) return NotFound();

            _context.Cuenta.Remove(cuentaToDelete);
             _context.SaveChanges();

            return Content("Eliminacion con Exito.");
        }
    }
}
