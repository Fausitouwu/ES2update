namespace Frontend.Services;

public class UserService
{
    private bool _estaAutenticado = false;
    private string _tipodoUser = "";

    public bool estaAutenticado => _estaAutenticado;
    public string TipoDoUser => _tipodoUser;
    public event Action OnChange;

    public void login(string TipoDoUser)
    {
        _estaAutenticado = true;
        _tipodoUser = TipoDoUser;
        NotifyStateChanged();
    }
    

    public void logout(string TipoDoUser)
    {
        _estaAutenticado = false;
        _tipodoUser = "";
        NotifyStateChanged();
    }
    
    private void NotifyStateChanged() => OnChange?.Invoke();
}