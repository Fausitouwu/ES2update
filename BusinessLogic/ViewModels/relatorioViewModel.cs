using BusinessLogic.Entities;
namespace BusinessLogic.ViewModels;

public class relatorioViewModel
{
    public relatorioViewModel(Relatorio relatorio)
    {
        nota = relatorio.Nota;
        idevento = relatorio.IdEventoNavigation;
    }
    
    public string nota { get; set; }
    public Evento idevento { get; set; }
}