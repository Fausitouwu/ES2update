using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class Categoriasevento
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
