using ClinicaASPNet.Data;
using ClinicaASPNet.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaASPNet.Repositories
{
    public class RepositoryPaciente : RepositoryBase<Paciente>
    {
        public RepositoryPaciente(ClinicaDbContext context) : base(context) { }

        public bool CpfJaCadastrado(string cpf, int? ignorarId = null) =>
            DbSet.Any(x => x.Cpf == cpf && x.Id != ignorarId);

        public async Task<List<Paciente>> ListarAsync(string? nome = null)
        {
            var query = DbSet.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(x => x.Nome.Contains(nome));
            return await query.OrderBy(x => x.Nome).ToListAsync();
        }
    }
}
