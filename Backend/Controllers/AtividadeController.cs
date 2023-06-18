using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BusinessLogic.Entities;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/atividades")]
    public class AtividadeController : ControllerBase
    {
        private readonly ILogger<AtividadeController> _logger;
        private readonly db_context _context;

        public AtividadeController(ILogger<AtividadeController> logger, db_context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Atividade> GetAtividades()
        {
            return _context.Atividades.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Atividade>> GetAtividade(int id)
        {
            var atividade = await _context.Atividades.FindAsync(id);

            if (atividade == null)
            {
                return NotFound();
            }

            return atividade;
        }

        [HttpPost]
        public async Task<ActionResult<Atividade>> CreateAtividade(int Idevento,string nome, DateTime data, TimeSpan hora, string descricao)
        {
            // Validação dos dados da atividade
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(descricao))
            {
                return BadRequest();
            }

            var atividade = new Atividade
            {
                Idevento = Idevento,
                Nome = nome,
                Data = DateOnly.FromDateTime(data),
                Hora = TimeOnly.FromTimeSpan(hora),
                Descricao = descricao
            };
            
            // Salvar evento no banco de dados
            _context.Atividades.Add(atividade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAtividade), new { id = atividade.Id }, atividade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAtividade(int id, [FromBody] Atividade atividade)
        {
            if (id != atividade.Id)
            {
                return BadRequest();
            }

            _context.Entry(atividade).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAtividade(int id)
        {
            var atividade = await _context.Atividades.FindAsync(id);

            if (atividade == null)
            {
                return NotFound();
            }

            _context.Atividades.Remove(atividade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
