using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusinessLogic.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Backend.Controllers
{
    [ApiController]
    [Route("api/eventos")]
    public class EventoController : ControllerBase
    {
        private readonly ILogger<EventoController> _logger;
        private readonly db_context _context;

        public EventoController(ILogger<EventoController> logger, db_context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Evento> GetEventos()
        {
            return _context.Eventos.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Evento> GetEvento(int id)
        {
            var evento = _context.Eventos.FirstOrDefault(e => e.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return evento;
        }

        [HttpPost("criarevento")]
        public async Task<ActionResult<Evento>> CreateEvento(int Idutilizador, string nome, DateTime data,
            TimeSpan hora, string local, string descricao, int capacidade, decimal preco, string tipoingresso,
            int quantidade)
        {
            // Validação dos dados do evento
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(local) || string.IsNullOrEmpty(descricao) ||
                capacidade <= 0 || preco <= 0)
            {
                return BadRequest();
            }

            var evento = new Evento
            {
                Idutilizador = Idutilizador,
                Nome = nome,
                Data = DateOnly.FromDateTime(data),
                Hora = TimeOnly.FromTimeSpan(hora),
                Local = local,
                Descricao = descricao,
                Capacidade = capacidade,
            };

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            var ticket = new Ticket
            {
                Tipoingresso = tipoingresso,
                Preco = preco,
                Quantidade = quantidade,
                EventoId = evento.Id // Atribui o ID do evento ao EventoId do ticket
            };

            // Salvar ticket no banco de dados
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvento), new { id = evento.Id }, evento);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, string nome, DateTime data, TimeSpan hora, string local,
            string descricao, int capacidade, decimal preco)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(local) || string.IsNullOrEmpty(descricao) ||
                capacidade <= 0 || preco <= 0)
            {
                return BadRequest();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            // Atualizar os campos do evento com os novos valores
            evento.Nome = nome;
            evento.Data = DateOnly.FromDateTime(data);
            evento.Hora = TimeOnly.FromTimeSpan(hora);
            evento.Local = local;
            evento.Descricao = descricao;
            evento.Capacidade = capacidade;
            evento.Preco = preco;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets.Where(t => t.EventoId == id).ToListAsync();
            _context.Tickets.RemoveRange(tickets);

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("search")]
        public IEnumerable<Evento> SearchEvento(string search)
        {
            IQueryable<Evento> query = _context.Eventos;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(e =>
                    e.Nome.ToLower().Contains(search.ToLower()) ||
                    //e.Data.ToString().Contains(search) ||
                    e.Local.ToLower().Contains(search.ToLower())
                );
            }

            return query.ToList();
        }
        
        [HttpGet("searchCategoria")]
        public IEnumerable<Categoriasevento> SearchCategoria(string pesquisa)
        {
            IQueryable<Categoriasevento> query = _context.Categoriaseventos;

            if (!string.IsNullOrEmpty(pesquisa))
            {
                query = query.Where(e =>
                    e.Nome.ToLower().Contains(pesquisa.ToLower())
                );
            }

            return query.ToList();
        }

        [HttpGet("get")]
        public IEnumerable<Evento> GetMyEventos(int idutilizador, int idevento)
        {
            var evento = _context.Eventos.FirstOrDefault(e => e.Id == idevento);

            if (evento != null)
            {
                var utilizador = _context.Utilizadors.FirstOrDefault(u => u.Id == idutilizador);

                if (utilizador != null)
                {
                    if (evento.Idutilizador == null)
                    {
                        //evento.Idutilizador = new List<Utilizador>();
                    }

                    //evento.Idutilizador.Add(utilizador);
                    _context.SaveChanges();
                }
            }

            return _context.Eventos.ToList();
        }
        [HttpGet("eventoreport")]
        public async Task<IActionResult> GetEventoReport(int id)
        {
            var evento = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null)
                return NotFound();

            var capacidadeTotal = evento.Capacidade ?? 0;
            var precoTotal = evento.Preco ?? 0;

            return Ok(new
            {
                Preco = precoTotal,
                CapacidadeTotal = capacidadeTotal
            });
        }


    }
}

