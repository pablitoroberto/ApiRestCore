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
    public class MovimientoController : ControllerBase
    {
        private readonly BP_CLIENTESContext _context;

        public MovimientoController(BP_CLIENTESContext context) => _context = context;

        [HttpGet]
        public  IEnumerable<Movimiento> Get()
             =>  _context.Movimientos.ToList();

        [HttpGet("{MovimientoId}")]
        [ProducesResponseType(typeof(Movimiento), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int MovimientoId)
        {
            var movimiento =  _context.Movimientos.Find(MovimientoId);
            return movimiento == null ? NotFound() : Ok(movimiento);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create(DTOMovimiento dtomovimiento)
        {
            NegocioMovimiento negociomovimiento = new NegocioMovimiento(_context);
            var respuesta = negociomovimiento.GuardarMovimiento(dtomovimiento);
            return Content(respuesta);
        }

        [HttpPut("{MovimientoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int MovimientoId, Movimiento movimiento)
        {
            if (MovimientoId != movimiento.MovimientoId) return BadRequest();

            _context.Entry(movimiento).State = EntityState.Modified;
            _context.SaveChanges();
            return Content("Actualizacion con Existo.");
        }

        [HttpDelete("{MovimientoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int MovimientoId)
        {
            var movimientoToDelete =  _context.Movimientos.Find(MovimientoId);
            if (movimientoToDelete == null) return NotFound();

            _context.Movimientos.Remove(movimientoToDelete);
             _context.SaveChanges();

            return Content("Eliminacion con Exito.");
        }
    }
}
