using Frontend.Pages;

namespace Frontend.ViewModels;

public class UtilizadorViewModel
{
    public int utilizadorId { get; set; }
    public string nome { get; set; }
    public string email { get; set; }
    public string senha { get; set; }
    public string tipo { get; set; }
    public string TipoName { get; set; }
}