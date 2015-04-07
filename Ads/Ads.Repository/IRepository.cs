using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ads.Dominio;

namespace Ads.Repository
{
    public interface IRepository<T> : IDisposable
        where T : EntityBase
    {
        int Create(T _entity);
        IQueryable<T> Get();
        void Update(T _entity);
    }
}
