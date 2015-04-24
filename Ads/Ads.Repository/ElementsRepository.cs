﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Ads.Dominio;

namespace Ads.Repository
{
    public class ElementsRepository : BaseRepository<elements, DbAdsContext>
    {
        public ElementsRepository(DbAdsContext context)
            : base(context)
        {

        }
    }
}
