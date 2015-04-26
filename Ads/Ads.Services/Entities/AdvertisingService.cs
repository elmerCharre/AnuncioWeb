using System.Linq;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Collections.Generic;
using System;
using System.Web;

namespace Ads.Services.Entities
{
    public class AdvertisingService
    {
        private IRepository<advertising> _advertisingRepository;
        private IRepository<customer> _customerRepository;
        private IRepository<category> _categoryRepository;
        private IRepository<subtype> _subtypeRepository;
        private IRepository<extra_fields> _fieldRepository;
        private IRepository<elements> _elementRepository;
        private IRepository<fields_value> _valuesRepository;

        public AdvertisingService(IRepository<advertising> advertisingRepository, 
            IRepository<customer> customerRepository, IRepository<category> categoryRepository,
            IRepository<subtype> subtypeRepository, IRepository<extra_fields> fieldRepository,
            IRepository<elements> elementRepository, IRepository<fields_value> valuesRepository)
        {
            _advertisingRepository = advertisingRepository;
            _customerRepository = customerRepository;
            _categoryRepository = categoryRepository;
            _subtypeRepository = subtypeRepository;
            _fieldRepository = fieldRepository;
            _elementRepository = elementRepository;
            _valuesRepository = valuesRepository;
        }

        public IEnumerable<AdvertisingViewModel> GetListByUser(string userName)
        {
            var customer = _customerRepository.Get().FirstOrDefault(c => c.email == userName);
            if (customer == null) throw new InvalidOperationException(string.Format("Cliente no encontrado {0}", userName));
            var AdvertisingLists = _advertisingRepository.Get().Where(x => x.customer_id == customer.Id).ToList();
            // aqui se tiene que hacer un mapeo del dominio al viewmodel
            return AdvertisingLists.Select(AdsList => new AdvertisingViewModel(AdsList)).ToList();
        }

        public IEnumerable<AdvertisingViewModel> GetAll()
        {
            var AdvertisingLists = _advertisingRepository.Get().ToList();
            return AdvertisingLists.Select(AdsList => new AdvertisingViewModel(AdsList)).ToList();
        }
         
        public void Dispose()
        {
            _advertisingRepository = null;
            _customerRepository = null;
        }

        public int Create(AdvertisingViewModel model)
        {
            var customer = _customerRepository.Get().FirstOrDefault(x => x.Id == model.customer_id);
            if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
            var advertising = new advertising() {
                title = model.title,
                detail = model.detail,
                price = model.price,
                customer_id = customer.Id,
                category_id = model.category_id,
                subtype_id = model.subtype_id,
                resource = model.resource
            };
            return _advertisingRepository.Create(advertising);
        }

        public void Update(AdvertisingViewModel model)
        {
            var customer = _customerRepository.Get().FirstOrDefault(x => x.Id == model.customer_id);
            if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
            var advertising = new advertising()
            {
                title = model.title,
                detail = model.detail,
                price = model.price,
                customer_id = customer.Id,
                category_id = model.category_id,
                subtype_id = model.subtype_id,
                resource = model.resource
            };
            _advertisingRepository.Update(advertising);
        }

        public AdvertisingViewModel Get(int id)
        {
            var AdsList = _advertisingRepository.Get().FirstOrDefault(x => x.Id == id);
            if (AdsList == null) throw new InvalidOperationException("Anuncio no encontrado");

            var fields_values = _valuesRepository.Get().Where(v => v.ads_id == AdsList.Id).ToList();
            var values = new List<ValuesViewModel>();
            foreach (var field_value in fields_values) {
                var valuesModel = new ValuesViewModel();
                valuesModel.ads_id = AdsList.Id;
                valuesModel.field_id = field_value.field_id;
                valuesModel.field_label = _fieldRepository.Get().FirstOrDefault(f => f.Id == field_value.field_id).label;
                valuesModel.value = (_fieldRepository.Get().FirstOrDefault(f => f.Id == field_value.field_id).input == "select") ?
                    _elementRepository.Get().FirstOrDefault(e => e.Id.ToString() == field_value.value).label : field_value.value;
                values.Add(valuesModel);
            }
            
            return new AdvertisingViewModel 
            {
                id = AdsList.Id,
                category_id = AdsList.category_id,
                category_name = _categoryRepository.Get().FirstOrDefault(c => c.Id == AdsList.category_id).name,
                subtype_id = AdsList.subtype_id,
                subtype_name = _subtypeRepository.Get().FirstOrDefault(s => s.Id == AdsList.subtype_id).name,
                title = AdsList.title,
                detail = AdsList.detail,
                price = AdsList.price,
                customer_id = AdsList.customer_id,
                resource = AdsList.resource.Where(x => x.advertising_id == AdsList.Id).ToList(),
                values = values
            };
        }

        public IEnumerable<category> GetListCategory()
        {
            return _categoryRepository.Get().ToList();
        }

        public IEnumerable<subtype> GetListSubtypeByCategory(int category_id)
        {
            return _subtypeRepository.Get().Where(x => x.category_id == category_id ).ToList();
        }

        public List<subtype> GetListSubtypeByCategoryAsJson(int category_id)
        {
            var list_types = _subtypeRepository.Get().Where(x => x.category_id == category_id).ToList();
            var json = new List<subtype>();
            foreach (var obj in list_types) {
                json.Add(new subtype
                { 
                    Id = obj.Id,
                    category_id = obj.category_id,
                    name = obj.name,
                    status = obj.status
                });
            }
            return json;
        }

        public List<elements> GetElementsAsJson(int id)
        {
            var elements = _elementRepository.Get().Where(x => x.parent_element == id).ToList();
            var json = new List<elements>();
            foreach (var obj in elements)
            {
                json.Add(new elements
                {
                    Id = obj.Id,
                    label = obj.label,
                    value = obj.value,
                    status = obj.status,
                    fields_id = obj.fields_id,
                    parent_element = obj.parent_element
                });
            }
            return json;
        }

        public List<FieldsViewModel> GetListFieldsAsJson(int id)
        {
            var list = _fieldRepository.Get().Where(x => x.subtype_id == id).OrderBy(g => g.sort).ToList();
            var json = new List<FieldsViewModel>();
            foreach (var obj in list)
            {
                var element = new List<elements>();
                if (obj.parent_field == 0)
                    element = _elementRepository.Get().Where(e => e.fields_id == obj.Id).ToList();
                json.Add(new FieldsViewModel
                {
                    id = obj.Id,
                    input = obj.input,
                    name = obj.name,
                    label = obj.label,
                    sort = obj.sort,
                    subtype_id = obj.subtype_id,
                    parent_field = obj.parent_field,
                    elements = element
                });
            }
            return json;
        }
    }
}
