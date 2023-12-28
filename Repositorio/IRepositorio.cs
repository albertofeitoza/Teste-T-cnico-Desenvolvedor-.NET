using System.Linq.Expressions;

namespace Teste_Tecnico_Desenvolvedor_.NET.Repositorio
{
    public interface IRepositorio<TEntity> where TEntity : class, new()
    {
        void Adicionar(TEntity entidade);
        List<TEntity> BuscarTodos();
        TEntity BuscaPorId(int id);
        void Remover(TEntity entidade);
        void Editar(TEntity entidade);
        void Commit();
        void Dispose();

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);
    }
}
