using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using BusinessLogic.Context;

namespace Backend.Controllers
{
    [Route("api/editarparticipante")]
    [ApiController]
    
    public class EditarParticipanteController : ControllerBase
    {
        private readonly db_context _context;

        public EditarParticipanteController(db_context context)
        {
            _context = context;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipante(int id, string nome, string senha, string email )
        {
            if (string.IsNullOrEmpty(nome) )
            {
                return BadRequest();
            }

            var participante = await _context.Utilizadors.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }

            // Atualizar os campos do evento com os novos valores
            participante.Id = id;
            participante.Nome = nome;
            participante.Senha = senha;
            participante.Email = email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

      
    }
}
