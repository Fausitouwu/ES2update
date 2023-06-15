using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class Relatorio
{
    public int Id { get; set; }

    public string? Nota { get; set; }

    public int? IdEvento { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }
}
