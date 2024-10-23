using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace apiencuestas.Models;

public partial class EncuestasContext : DbContext
{
    public EncuestasContext()
    {
    }

    public EncuestasContext(DbContextOptions<EncuestasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cuestionario> Cuestionarios { get; set; }

    public virtual DbSet<Opcion> Opcions { get; set; }

    public virtual DbSet<Preguntum> Pregunta { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Respuesta> Respuestas{ get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //=> optionsBuilder.UseSqlServer("Data Source=servidorencuestas.database.windows.net;Initial Catalog=encuestas;Persist Security Info=True;User ID=adminencuestas;Password=+%Y>!Tbd7%&2KhG;Encrypt=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cuestionario>(entity =>
        {
            entity.ToTable("cuestionario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Respuesta>(entity =>
        {
            entity.ToTable("respuesta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPregunta).HasColumnName("idPregunta");
            entity.Property(e => e.IdOpcion).HasColumnName("idOpcion");

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.Respuestas)
                .HasForeignKey(d => d.IdPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_respuesta_pregunta");

            entity.HasOne(d => d.IdOpcionNavigation).WithMany(p => p.Respuestas)
                .HasForeignKey(d => d.IdOpcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_respuesta_opcion");
        });


        modelBuilder.Entity<Opcion>(entity =>
        {
            entity.ToTable("opcion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Correcta).HasColumnName("correcta");
            entity.Property(e => e.IdPregunta).HasColumnName("idPregunta");
            entity.Property(e => e.Opcion1)
                .HasMaxLength(500)
                .HasColumnName("opcion");
            entity.Property(e => e.Orden).HasColumnName("orden");

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.Opcions)
                .HasForeignKey(d => d.IdPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_opcion_pregunta");
        });

        modelBuilder.Entity<Preguntum>(entity =>
        {
            entity.ToTable("pregunta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCuestionario).HasColumnName("idCuestionario");
            entity.Property(e => e.Pregunta)
                .HasMaxLength(500)
                .HasColumnName("pregunta");

            entity.HasOne(d => d.IdCuestionarioNavigation).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.IdCuestionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pregunta_cuestionario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SocialIdentity)
                .HasMaxLength(50)
                .HasColumnName("social_identity");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
