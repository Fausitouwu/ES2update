using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Context;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Utilizadores")]
    public class UtilizadoresController : ControllerBase
    {
        private readonly db_context _context;

        public UtilizadoresController(db_context context)
        {
            _context = context;
        }

        [HttpGet]
        public List<BusinessLogic.Entities.Utilizador> GetUtilizadores()
        {
            return _context.Utilizadors.ToList();
        }
    }

    public class Utilizador
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public int Idtipouser { get; set; }
    }
    

}