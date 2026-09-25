using ClinicaASPNet.Data;
using ClinicaASPNet.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaASPNet.Repositories
{
    public class RepositoryProfissional : RepositoryBase<Profissional>
    {
        public RepositoryProfissional(ClinicaDbContext context) : base(context) { }
        public bool RegistroJaCadastrado(string registro, int? ignorarId = null) => DbSet.Any(x => x.RegistroProfissional == registro && x.Id != ignorarId);
        public async Task<List<Profissional>> ListarComEspecialidadeAsync() => await DbSet.Include(x => x.Especialidade).OrderBy(x => x.Nome).ToListAsync();
    }
}
