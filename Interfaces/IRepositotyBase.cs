namespace ClinicaASPNet.Interfaces
{
    public interface IRepositotyBase<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> ListarTodosAsync();
        Task<TEntity?> SelecionarPorIdAsync(int id);
        Task IncluirAsync(TEntity objeto);
        Task AlterarAsync(TEntity objeto);
        Task ExcluirAsync(TEntity objeto);
        IReadOnlyList<TEntity> ListarTodos();
        TEntity? SelecionarPorId(int id);
        void Incluir(TEntity objeto);
        void Alterar(TEntity objeto);
        void Excluir(TEntity objeto);
    }
}
