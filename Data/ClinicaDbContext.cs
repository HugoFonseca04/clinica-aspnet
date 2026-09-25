using ClinicaASPNet.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaASPNet.Data
{
    public class ClinicaDbContext : DbContext
    {
        public ClinicaDbContext(DbContextOptions<ClinicaDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<Profissional> Profissionais => Set<Profissional>();
        public DbSet<Especialidade> Especialidades => Set<Especialidade>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.ToTable("Pacientes");
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Cpf).IsUnique();
                entity.Property(x => x.Nome).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Cpf).HasMaxLength(11).IsRequired();
                entity.Property(x => x.Telefone).HasMaxLength(30).IsRequired();
                entity.Property(x => x.Email).HasMaxLength(150).IsRequired();
                entity.Property(x => x.Endereco).HasMaxLength(200);
            });

            modelBuilder.Entity<Especialidade>(entity =>
            {
                entity.ToTable("Especialidades");
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Nome).IsUnique();
                entity.Property(x => x.Nome).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Descricao).HasMaxLength(300).IsRequired();
                entity.Property(x => x.AreaAtuacao).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Profissional>(entity =>
            {
                entity.ToTable("Profissionais");
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.RegistroProfissional).IsUnique();
                entity.Property(x => x.Nome).HasMaxLength(100).IsRequired();
                entity.Property(x => x.RegistroProfissional).HasMaxLength(30).IsRequired();
                entity.Property(x => x.Telefone).HasMaxLength(30).IsRequired();
                entity.Property(x => x.Email).HasMaxLength(150).IsRequired();

                entity.HasOne(x => x.Especialidade)
                    .WithMany(x => x.Profissionais)
                    .HasForeignKey(x => x.EspecialidadeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
