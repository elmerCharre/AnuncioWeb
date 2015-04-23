using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ads.Dominio;
using System.Data.Entity;

namespace Ads.Repository
{
    public class BaseRepository<TEntity, TContext> : IDisposable, IRepository<TEntity>
        where TEntity : EntityBase
        where TContext : DbContext
    {

        protected TContext _context;

        public BaseRepository(TContext context)
        {
            _context = context;
        }

        public int Create(TEntity _entity)
        {
            _context.Set<TEntity>().Add(_entity);
            _context.SaveChanges();
            return _entity.Id;
        }

        public IQueryable<TEntity> Get()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public void Update(TEntity _entity)
        {
            _context.Entry(_entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context = null;
        }
    }
}
