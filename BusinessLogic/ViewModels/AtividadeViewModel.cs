using BusinessLogic.Entities;
namespace BusinessLogic.ViewModels;

public class AtividadeViewModel
{
    public AtividadeViewModel(Atividade atividade)
    {
        nome = atividade.Nome;
        data = (DateOnly)atividade.Data;
        hora = (TimeOnly)atividade.Hora;
        descricao = atividade.Descricao;
        idvento = atividade.IdeventoNavigation;
    }
    
    public string nome { get; set; }
    public DateOnly data { get; set; }
    public TimeOnly hora { get; set; }
    public string descricao { get; set; }
    public Evento idvento { get; set; }
}