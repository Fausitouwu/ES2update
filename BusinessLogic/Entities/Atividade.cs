using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class Atividade
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public DateOnly? Data { get; set; }

    public TimeOnly? Hora { get; set; }

    public string? Descricao { get; set; }

    public int? Idevento { get; set; }

    public virtual Evento? IdeventoNavigation { get; set; }
}
