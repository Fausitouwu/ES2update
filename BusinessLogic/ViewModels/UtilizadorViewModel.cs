using BusinessLogic.Entities;
namespace BusinessLogic.ViewModels;

public class UtilizadorViewModel
{
    public UtilizadorViewModel(Utilizador utilizador)
    {
        nome = utilizador.Nome;
        email = utilizador.Email;
        senha = utilizador.Senha;
        tipo = utilizador.IdtipouserNavigation.Tipo;
    }
    
    
    public string nome { get; set; }
    public string email { get; set; }
    public string senha { get; set; }
    public string tipo { get; set; }
    
}