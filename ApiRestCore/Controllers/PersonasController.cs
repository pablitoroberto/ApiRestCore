using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Contexto;
using Modelo.Entidades;

namespace ApiRestCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly BP_CLIENTESContext _context;

        public PersonasController(BP_CLIENTESContext context) => _context = context;

        [HttpGet]
        public IEnumerable<Persona> Get()
            =>  _context.Personas.ToList();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create(Persona persona)
        {
             _context.Personas.Add(persona);
             _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = persona.PersonaId }, persona);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Persona), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var persona =  _context.Personas.Find(id);
            return persona == null ? NotFound() : Ok(persona);
        }
    }
}
