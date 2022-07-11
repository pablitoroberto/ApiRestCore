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
    public class ReporteController : ControllerBase
    {
        private readonly BP_CLIENTESContext _context;

        public ReporteController(BP_CLIENTESContext context) => _context = context;

        [HttpGet]
        public IEnumerable<DTOReporte> Get()
        {
            NegocioReporte negocioReporte = new NegocioReporte(_context);
            return negocioReporte.listaReportes();
        }
             

        [HttpGet("{CuentaId}")]
        [ProducesResponseType(typeof(Cuenta), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Consulta(string FechaInicio, string FechaFin, string Cliente)
        {
            NegocioReporte negocioReporte = new NegocioReporte(_context);
            List<DTOReporte> listaReporte = new List<DTOReporte>();
            negocioReporte.GenerarReporte(FechaInicio, FechaFin, Cliente, out listaReporte);
            return listaReporte == null ? NotFound() : Ok(listaReporte.ToArray());
        }
    }
}
