using System;
using System.Collections.Generic;
using BusinessLogic.Context;

namespace BusinessLogic.Entities;

public partial class Categoriasevento
{

    private readonly db_context _context;
    public int Id { get; set; }
    public string? Nome { get; set; }
    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public Categoriasevento(db_context context)
    {
        _context = context;
    }

    public void addEvento(Evento e)
    {
        _context.Eventos.Add(e);
        _context.SaveChanges();
    }
}
