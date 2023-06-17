using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Context;
using BusinessLogic.Entities;

namespace Backend.Controllers
{
    [Route("api/Utilizadores")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly db_context _context;

        public AuthController(db_context context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> HandleLogin(string username, string password)
        {
            // Check if the username and password match the records in the database
            var utilizador =
                await _context.Utilizadors.FirstOrDefaultAsync(u => u.Nome == username && u.Senha == password);

            if (utilizador != null)
            {
                // Authentication successful
                return Ok("Login criado com Sucesso!");
            }
            else
            {
                // Authentication failed
                return Unauthorized("Nome e Senha inválidos!");
            }
        }

        [HttpPost("criar")]
        public async Task<IActionResult> HandleCreateUser([FromQuery] string nome, [FromQuery] string senha,
            [FromQuery] string email)
        {
            var tipo_utilizador = new TipoUtilizador
            {
                Tipo = "Utilizador"
            };

            _context.TipoUtilizadors.Add(tipo_utilizador);
            await _context.SaveChangesAsync();

            var utilizador = new Utilizador
            {
                Nome = nome,
                Senha = senha,
                Email = email,
                Idtipouser = tipo_utilizador.Id
            };

            _context.Utilizadors.Add(utilizador);
            await _context.SaveChangesAsync();

            return Ok("Utilizador criado com Sucesso!");
        }

    }
}