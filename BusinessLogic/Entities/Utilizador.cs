using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities;

public partial class Utilizador
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public string? Senha { get; set; }

    public int? Idtipouser { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public virtual TipoUtilizador? IdtipouserNavigation { get; set; }

    public virtual ICollection<Mensagem> Mensagems { get; set; } = new List<Mensagem>();
}
