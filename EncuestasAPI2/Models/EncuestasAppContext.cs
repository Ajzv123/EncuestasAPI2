using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EncuestasAPI2.DTO;

namespace EncuestasAPI2.Models;

public partial class EncuestasAppContext : DbContext
{
    public EncuestasAppContext()
    {
    }

    public EncuestasAppContext(DbContextOptions<EncuestasAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaPreguntum> CategoriaPregunta { get; set; }

    public virtual DbSet<Encuestum> Encuesta { get; set; }

    public virtual DbSet<Preguntum> Pregunta { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Respuestum> Respuesta { get; set; }

    public virtual DbSet<TipoPreguntum> TipoPregunta { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EncuestasApp;Integrated Security=True; Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaPreguntum>(entity =>
        {
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Encuestum>(entity =>
        {
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaExpiracion).HasColumnType("date");
            entity.Property(e => e.NombreEncuesta)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.IdRegionNavigation).WithMany(p => p.Encuesta)
                .HasForeignKey(d => d.IdRegion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Encuesta_Region");
        });

        modelBuilder.Entity<Preguntum>(entity =>
        {
            entity.Property(e => e.Pregunta)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pregunta_CategoriaPregunta");

            entity.HasOne(d => d.IdEncuestasNavigation).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.IdEncuestas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pregunta_Encuesta1");

            entity.HasOne(d => d.IdTipoPreguntaNavigation).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.IdTipoPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pregunta_TipoPregunta");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IdRegion");

            entity.ToTable("Region");

            entity.Property(e => e.NombreRegion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Respuestum>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdPregunta).HasColumnName("idPregunta");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Respuesta)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.IdPregunta)
                .HasConstraintName("FK_Respuesta_Pregunta");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Respuesta_Usuario");
        });

        modelBuilder.Entity<TipoPreguntum>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TipoPregunta)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.ToTable("TipoUsuario");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nss)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NSS");
            entity.Property(e => e.NumeroTelefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRegionNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRegion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Region1");

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTipoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_TipoUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<EncuestasAPI2.DTO.UsuarioDTO> UsuarioDTO { get; set; } = default!;
}
