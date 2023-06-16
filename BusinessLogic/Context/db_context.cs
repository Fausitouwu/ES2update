using System;
using System.Collections.Generic;
using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Context;

public partial class db_context : DbContext
{

    public db_context(DbContextOptions<db_context> options)
        : base(options)
    {
    }

    public virtual DbSet<Atividade> Atividades { get; set; }

    public virtual DbSet<Categoriasevento> Categoriaseventos { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Mensagem> Mensagems { get; set; }

    public virtual DbSet<Relatorio> Relatorios { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TipoUtilizador> TipoUtilizadors { get; set; }

    public virtual DbSet<Utilizador> Utilizadors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=es2;Username=es2;Password=es2;SearchPath=public;Port=15432");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("postgis")
            .HasPostgresExtension("uuid-ossp")
            .HasPostgresExtension("topology", "postgis_topology");

        modelBuilder.Entity<Atividade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("atividades_pkey");

            entity.ToTable("atividades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.Idevento).HasColumnName("idevento");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");

            entity.HasOne(d => d.IdeventoNavigation).WithMany(p => p.Atividades)
                .HasForeignKey(d => d.Idevento)
                .HasConstraintName("atividades_idevento_fkey");
        });

        modelBuilder.Entity<Categoriasevento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categoriaseventos_pkey");

            entity.ToTable("categoriaseventos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eventos_pkey");

            entity.ToTable("eventos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacidade).HasColumnName("capacidade");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.Idutilizador).HasColumnName("idutilizador");
            entity.Property(e => e.Local)
                .HasMaxLength(255)
                .HasColumnName("local");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Preco)
                .HasPrecision(10, 2)
                .HasColumnName("preco");

            entity.HasOne(d => d.IdutilizadorNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.Idutilizador)
                .HasConstraintName("eventos_idutilizador_fkey");

            entity.HasMany(d => d.Categoria).WithMany(p => p.Eventos)
                .UsingEntity<Dictionary<string, object>>(
                    "Eventocategorium",
                    r => r.HasOne<Categoriasevento>().WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("eventocategoria_categoria_id_fkey"),
                    l => l.HasOne<Evento>().WithMany()
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("eventocategoria_evento_id_fkey"),
                    j =>
                    {
                        j.HasKey("EventoId", "CategoriaId").HasName("eventocategoria_pkey");
                        j.ToTable("eventocategoria");
                        j.IndexerProperty<int>("EventoId").HasColumnName("evento_id");
                        j.IndexerProperty<int>("CategoriaId").HasColumnName("categoria_id");
                    });
        });

        modelBuilder.Entity<Mensagem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mensagem_pkey");

            entity.ToTable("mensagem");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Conteudo).HasColumnName("conteudo");

            entity.HasMany(d => d.Utilizadors).WithMany(p => p.Mensagems)
                .UsingEntity<Dictionary<string, object>>(
                    "Mensagemutilizador",
                    r => r.HasOne<Utilizador>().WithMany()
                        .HasForeignKey("UtilizadorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("mensagemutilizador_utilizador_id_fkey"),
                    l => l.HasOne<Mensagem>().WithMany()
                        .HasForeignKey("MensagemId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("mensagemutilizador_mensagem_id_fkey"),
                    j =>
                    {
                        j.HasKey("MensagemId", "UtilizadorId").HasName("mensagemutilizador_pkey");
                        j.ToTable("mensagemutilizador");
                        j.IndexerProperty<int>("MensagemId").HasColumnName("mensagem_id");
                        j.IndexerProperty<int>("UtilizadorId").HasColumnName("utilizador_id");
                    });
        });

        modelBuilder.Entity<Relatorio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("relatorio_pkey");

            entity.ToTable("relatorio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Nota)
                .HasMaxLength(255)
                .HasColumnName("nota");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Relatorios)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("relatorio_id_evento_fkey");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_pkey");

            entity.ToTable("ticket");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EventoId).HasColumnName("evento_id");
            entity.Property(e => e.Preco)
                .HasPrecision(10, 2)
                .HasColumnName("preco");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");
            entity.Property(e => e.Tipoingresso)
                .HasMaxLength(255)
                .HasColumnName("tipoingresso");

            entity.HasOne(d => d.Evento).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.EventoId)
                .HasConstraintName("ticket_evento_id_fkey");
        });

        modelBuilder.Entity<TipoUtilizador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_utilizador_pkey");

            entity.ToTable("tipo_utilizador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Utilizador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("utilizador_pkey");

            entity.ToTable("utilizador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Idtipouser).HasColumnName("idtipouser");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .HasColumnName("senha");

            entity.HasOne(d => d.IdtipouserNavigation).WithMany(p => p.Utilizadors)
                .HasForeignKey(d => d.Idtipouser)
                .HasConstraintName("utilizador_idtipouser_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
