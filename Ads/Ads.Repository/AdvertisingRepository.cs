using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Ads.Dominio;

namespace Ads.Repository
{
    public class AdvertisingRepository : BaseRepository<advertising, DbAdsContext>
    {
        public AdvertisingRepository(DbAdsContext context)
            : base(context)
        {

        }
    }
}
