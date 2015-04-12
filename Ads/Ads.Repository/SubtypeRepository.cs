using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Ads.Dominio;

namespace Ads.Repository
{
    public class SubtypeRepository : BaseRepository<subtype, DbAdsContext>
    {
        public SubtypeRepository(DbAdsContext context)
            : base(context)
        {

        }
    }
}
