using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace AndresFlorez.RedSocial.Modelo.EF
{
    public partial class inalambria_redsocialContext : DbContext
    {
        public inalambria_redsocialContext()
        {
        }

        public inalambria_redsocialContext(DbContextOptions<inalambria_redsocialContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RsocialBitacoraErrore> RsocialBitacoraErrores { get; set; }
        public virtual DbSet<RsocialMensajeriaEstado> RsocialMensajeriaEstados { get; set; }
        public virtual DbSet<RsocialMensajerium> RsocialMensajeria { get; set; }
        public virtual DbSet<RsocialPublicacion> RsocialPublicacions { get; set; }
        public virtual DbSet<RsocialPublicacionArchivo> RsocialPublicacionArchivos { get; set; }
        public virtual DbSet<RsocialPublicacionEstado> RsocialPublicacionEstados { get; set; }
        public virtual DbSet<RsocialPublicacionImagen> RsocialPublicacionImagens { get; set; }
        public virtual DbSet<RsocialPublicacionVideo> RsocialPublicacionVideos { get; set; }
        public virtual DbSet<RsocialPublicidad> RsocialPublicidads { get; set; }
        public virtual DbSet<RsocialUsuario> RsocialUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                var connectionString = configuration.GetConnectionString("connectionsql");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<RsocialBitacoraErrore>(entity =>
            {
                entity.ToTable("rsocial_BitacoraErrores");

                entity.Property(e => e.ErrorInnerException)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorMessage)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorSource)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorStackTrace)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorTargetSite)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ErrorType)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RsocialMensajeriaEstado>(entity =>
            {
                entity.ToTable("rsocial_MensajeriaEstado");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RsocialMensajerium>(entity =>
            {
                entity.ToTable("rsocial_Mensajeria");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Mensaje)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.RsocialMensajeria)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MensajeriaMensajeriaEstado");

                entity.HasOne(d => d.IdUsuarioDestinoNavigation)
                    .WithMany(p => p.RsocialMensajeriumIdUsuarioDestinoNavigations)
                    .HasForeignKey(d => d.IdUsuarioDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MensajeriaUsuario_Destino");

                entity.HasOne(d => d.IdUsuarioOrigenNavigation)
                    .WithMany(p => p.RsocialMensajeriumIdUsuarioOrigenNavigations)
                    .HasForeignKey(d => d.IdUsuarioOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MensajeriaUsuario_Origen");
            });

            modelBuilder.Entity<RsocialPublicacion>(entity =>
            {
                entity.ToTable("rsocial_Publicacion");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdEstado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Texto)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.RsocialPublicacions)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicacionPublicacionEstado");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.RsocialPublicacions)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicacionUsuario");
            });

            modelBuilder.Entity<RsocialPublicacionArchivo>(entity =>
            {
                entity.ToTable("rsocial_PublicacionArchivo");

                entity.Property(e => e.ArchivoExtension)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ArchivoNombre)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ArchivoRuta)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPublicacionNavigation)
                    .WithMany(p => p.RsocialPublicacionArchivos)
                    .HasForeignKey(d => d.IdPublicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicacionArchivoPublicacion");
            });

            modelBuilder.Entity<RsocialPublicacionEstado>(entity =>
            {
                entity.ToTable("rsocial_PublicacionEstado");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RsocialPublicacionImagen>(entity =>
            {
                entity.ToTable("rsocial_PublicacionImagen");

                entity.Property(e => e.ImagenExtension)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ImagenNombre)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ImagenRuta)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPublicacionNavigation)
                    .WithMany(p => p.RsocialPublicacionImagens)
                    .HasForeignKey(d => d.IdPublicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicacionImagenPublicacion");
            });

            modelBuilder.Entity<RsocialPublicacionVideo>(entity =>
            {
                entity.ToTable("rsocial_PublicacionVideo");

                entity.Property(e => e.VideoExtension)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.VideoNombre)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.VideoRuta)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPublicacionNavigation)
                    .WithMany(p => p.RsocialPublicacionVideos)
                    .HasForeignKey(d => d.IdPublicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicacionVideoPublicacion");
            });

            modelBuilder.Entity<RsocialPublicidad>(entity =>
            {
                entity.ToTable("rsocial_Publicidad");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaFinalizacion).HasColumnType("datetime");

                entity.Property(e => e.Informacion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.VinculoComercial)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RsocialUsuario>(entity =>
            {
                entity.ToTable("rsocial_Usuario");

                entity.HasIndex(e => e.Email, "UK_UniqueUsuario")
                    .IsUnique();

                entity.Property(e => e.Apellido)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
