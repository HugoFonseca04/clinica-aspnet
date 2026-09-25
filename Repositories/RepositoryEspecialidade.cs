using ClinicaASPNet.Data;
using ClinicaASPNet.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaASPNet.Repositories
{
    public class RepositoryEspecialidade : RepositoryBase<Especialidade>
    {
        public RepositoryEspecialidade(ClinicaDbContext context) : base(context) { }
        public bool NomeJaCadastrado(string nome, int? ignorarId = null) => DbSet.Any(x => x.Nome == nome && x.Id != ignorarId);
        public async Task<List<Especialidade>> ListarComProfissionaisAsync() => await DbSet.Include(x => x.Profissionais).OrderBy(x => x.Nome).ToListAsync();
    }
}
