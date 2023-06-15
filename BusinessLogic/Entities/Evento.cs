using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class Evento
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public DateOnly? Data { get; set; }

    public TimeOnly? Hora { get; set; }

    public string? Local { get; set; }

    public string? Descricao { get; set; }

    public int? Capacidade { get; set; }

    public decimal? Preco { get; set; }

    public int? Idutilizador { get; set; }

    public virtual ICollection<Atividade> Atividades { get; set; } = new List<Atividade>();

    public virtual Utilizador? IdutilizadorNavigation { get; set; }

    public virtual ICollection<Relatorio> Relatorios { get; set; } = new List<Relatorio>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Categoriasevento> Categoria { get; set; } = new List<Categoriasevento>();
}
