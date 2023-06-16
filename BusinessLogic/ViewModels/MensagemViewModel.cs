using BusinessLogic.Entities;
namespace BusinessLogic.ViewModels;

public class MensagemViewModel
{
    public MensagemViewModel(Mensagem mensagem)
    {
        conteudo = mensagem.Conteudo;
    }
    
    public string conteudo { get; set; }
}