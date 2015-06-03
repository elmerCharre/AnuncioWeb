using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Ads.Dominio;

namespace Ads.Repository
{
    public class ArticleRepository : BaseRepository<article, DbAdsContext>
    {
        public ArticleRepository(DbAdsContext context)
            : base(context)
        {

        }
    }

    public class motoRepository : BaseRepository<moto, DbAdsContext>
    {
        public motoRepository(DbAdsContext context)
            : base(context)
        {

        }
    }

    public class autoRepository : BaseRepository<auto, DbAdsContext>
    {
        public autoRepository(DbAdsContext context)
            : base(context)
        {

        }
    }
}
