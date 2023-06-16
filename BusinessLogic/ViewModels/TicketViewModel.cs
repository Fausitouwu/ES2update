using BusinessLogic.Entities;
namespace BusinessLogic.ViewModels;

public class TicketViewModel
{
    public TicketViewModel(Ticket ticket)
    {
        tipoingresso = ticket.Tipoingresso;
        preco = (decimal)ticket.Preco;
        quantidade = (int)ticket.Quantidade;
        evento_id = ticket.IdEventoNavigation;
    }
    
    public string tipoingresso { get; set; }
    public decimal preco { get; set; }
    public int quantidade { get; set; }
    public Evento evento_id { get; set; }
}