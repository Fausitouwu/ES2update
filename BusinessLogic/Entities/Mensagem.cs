using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class Mensagem
{
    public int Id { get; set; }

    public string? Conteudo { get; set; }

    public virtual ICollection<Utilizador> Utilizadors { get; set; } = new List<Utilizador>();
}
