using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using Teste_Tecnico_Desenvolvedor_.NET.Data;

namespace Teste_Tecnico_Desenvolvedor_.NET.Repositorio
{
    public class Repositorio<TEntity> : IRepositorio<TEntity>, IDisposable where TEntity : class, new()
    {
        protected readonly TesteTecnicoDbContext  serviceContext;

        public Repositorio(TesteTecnicoDbContext _serviceContext)
        {
            serviceContext = _serviceContext;
        }

        private IQueryable<TEntity> AsNoTracking() => serviceContext.Set<TEntity>().AsNoTracking();
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate) => AsNoTracking().Where(predicate);

        public virtual void Adicionar(TEntity item)
        {
            serviceContext.Set<TEntity>().Add(item);
            Commit();
        }

        public TEntity BuscaPorId(int id)
        {
            return serviceContext.Set<TEntity>().Find(id);
        }

        public List<TEntity> BuscarTodos()
        {
            return serviceContext.Set<TEntity>().ToList();
        }
        public virtual void Editar(TEntity item)
        {
            serviceContext.Entry(item).State = EntityState.Modified;
            Commit();
        }

        public virtual void Remover(TEntity item)
        {
            serviceContext.Set<TEntity>().Remove(item);
            Commit();
        }

        public virtual void Commit()
        {
            serviceContext.SaveChanges();
        }

        public virtual void Dispose()
        {
            serviceContext.Dispose();
        }
    }
}
