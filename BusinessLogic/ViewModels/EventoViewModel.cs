using System.Runtime.InteropServices.JavaScript;
using BusinessLogic.Entities;
namespace BusinessLogic.ViewModels;

public class EventoViewModel
{
   public EventoViewModel(Evento evento)
   {
      nome = evento.Nome;
      data = (DateOnly)evento.Data;
      hora = (TimeOnly)evento.Hora;
      local = evento.Local;
      descricao = evento.Descricao;
      capacidade = (int)evento.Capacidade;
      preco = (decimal)evento.Preco;
      idUtilizador = evento.IdutilizadorNavigation;
   }
   
   
   public string nome { get; set; }
   public DateOnly data { get; set; }
   public TimeOnly hora { get; set; }
   public string local { get; set; }
   public string descricao { get; set; }
   public int capacidade { get; set; }
   public decimal preco { get; set; }
   public Utilizador? idUtilizador { get; set; }
}