using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Context;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Entities;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/participanteregistar")]
    public class UtilizadorController : ControllerBase
    {
        private readonly db_context dbContext; // Substitua "YourDbContext" pelo nome da sua classe de contexto do banco de dados

        public UtilizadorController(db_context dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("register")]
        public IActionResult Register(Utilizador utilizador)
        {
            // Verifica se os dados do utilizador são válidos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // Salva o utilizador no banco de dados
            dbContext.Utilizadors.Add(utilizador);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("{utilizadorId}/eventos/{eventoId}/atividades/{atividadeId}")]
        public IActionResult EscolherAtividade(int utilizadorId, int eventoId, int atividadeId)
        {
            // Verifica se o utilizador existe
            Utilizador utilizador = dbContext.Utilizadors.Find(utilizadorId);
            if (utilizador == null)
            {
                return NotFound("Utilizador não encontrado");
            }

            // Verifica se o evento existe
            Evento evento = dbContext.Eventos.Find(eventoId);
            if (evento == null)
            {
                return NotFound("Evento não encontrado");
            }

            // Verifica se a atividade existe no evento
            Atividade atividade = dbContext.Atividades.Find(atividadeId);
            if (atividade == null)
            {
                return NotFound("Atividade não encontrada");
            }
            

            // Verifica se o utilizador já está registrado no evento
            if (!utilizador.Eventos.Contains(evento))
            {
                return BadRequest("O utilizador não está registrado neste evento");
            }

            // Adiciona a atividade escolhida pelo utilizador
            utilizador.Eventos.FirstOrDefault(e => e.Id == eventoId)?.Atividades.Add(atividade);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
