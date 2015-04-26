using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Linq;

namespace Ads.Services.Entities
{
    public class ValuesService : IDisposable
    {
        private IRepository<fields_value> _valuesRepository;

        public ValuesService(IRepository<fields_value> valuesRepository)
        {
            _valuesRepository = valuesRepository;
        }

        public int Create(ValuesViewModel valuesRepository)
        {
            var res = new fields_value
            {
                ads_id = valuesRepository.ads_id,
                field_id = valuesRepository.field_id,
                value = valuesRepository.value
            };
            return _valuesRepository.Create(res);
        }

        public void Dispose()
        {
            _valuesRepository = null;
        }
    }
}
