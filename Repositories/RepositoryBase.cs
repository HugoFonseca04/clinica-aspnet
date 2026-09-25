using ClinicaASPNet.Data;
using ClinicaASPNet.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaASPNet.Repositories
{
    public class RepositoryBase<TEntity> : IRepositotyBase<TEntity> where TEntity : class
    {
        protected readonly ClinicaDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(ClinicaDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<IReadOnlyList<TEntity>> ListarTodosAsync() => await DbSet.ToListAsync();
        public async Task<TEntity?> SelecionarPorIdAsync(int id) => await DbSet.FindAsync(id);
        public async Task IncluirAsync(TEntity objeto) { await DbSet.AddAsync(objeto); await Context.SaveChangesAsync(); }
        public async Task AlterarAsync(TEntity objeto) { DbSet.Update(objeto); await Context.SaveChangesAsync(); }
        public async Task ExcluirAsync(TEntity objeto) { DbSet.Remove(objeto); await Context.SaveChangesAsync(); }

        public IReadOnlyList<TEntity> ListarTodos() => DbSet.ToList();
        public TEntity? SelecionarPorId(int id) => DbSet.Find(id);
        public void Incluir(TEntity objeto) { DbSet.Add(objeto); Context.SaveChanges(); }
        public void Alterar(TEntity objeto) { DbSet.Update(objeto); Context.SaveChanges(); }
        public void Excluir(TEntity objeto) { DbSet.Remove(objeto); Context.SaveChanges(); }
    }
}
