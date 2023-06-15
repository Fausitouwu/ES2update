using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class Ticket
{
    public int Id { get; set; }

    public string? Tipoingresso { get; set; }

    public decimal? Preco { get; set; }

    public int? Quantidade { get; set; }

    public int? EventoId { get; set; }

    public virtual Evento? Evento { get; set; }
}
