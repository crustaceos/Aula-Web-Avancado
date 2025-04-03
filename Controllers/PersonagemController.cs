using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Xablau.Data;
using Xablau.Models;

namespace Xablau.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonagemController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PersonagemController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]

        public async Task<IActionResult> AddPersonagem(Personagem personagem)
        {
            if(personagem == null){
                return BadRequest("Dados Invalidos");
            }

            _appDbContext.XablauDB.Add(personagem);
            await _appDbContext.SaveChangesAsync();

            return StatusCode(201,personagem);
    
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Personagem>>> GetPersonagem()
        {
            var personagens = await _appDbContext.XablauDB.ToListAsync();

            return StatusCode(201,personagens);
    
        }

        [HttpGet("{id}")]

        ///ENUMERABLE PARA FZER GET 
        public async Task<ActionResult<IEnumerable<Personagem>>> GetPersonagemId(int id)
        {
            var buscarPersonagem = await _appDbContext.XablauDB.FindAsync(id);
            
            if(buscarPersonagem == null){
                return NotFound("Personagem não encontrado");
            }

            return StatusCode(201,buscarPersonagem);
    
        }

        ///PARA FAZER PUT

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdatePersonagemId(int id, [FromBody] Personagem personagemAtualizado)
        {
            var buscarPersonagem = await _appDbContext.XablauDB.FindAsync(id);
            
            if(buscarPersonagem == null){
                return NotFound("Personagem não encontrado");
            }

            ///ENTRY SERVE PARA DIZER QUAL OBJETO EU VOU QUERER ALTERAR DA TABELA, DEPOIS DIGO O VALOR QUE ELE VAI PASSAR A TER
            _appDbContext.Entry(buscarPersonagem).CurrentValues.SetValues(personagemAtualizado);

            await _appDbContext.SaveChangesAsync();

            return Ok("Atualizado com sucesso");
    
        }


    }
}