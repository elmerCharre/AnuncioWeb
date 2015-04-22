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

        public AdvertisingService(IRepository<advertising> advertisingRepository, 
            IRepository<customer> customerRepository, IRepository<category> categoryRepository,
            IRepository<subtype> subtypeRepository)
        {
            _advertisingRepository = advertisingRepository;
            _customerRepository = customerRepository;
            _categoryRepository = categoryRepository;
            _subtypeRepository = subtypeRepository;
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
            var customer = _customerRepository.Get().FirstOrDefault(x => x.email == "elmer.nyd@gmail.com"); //Session["userID"]
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

        public AdvertisingViewModel Get(int advertisingId)
        {
            var AdsList = _advertisingRepository.Get().FirstOrDefault(x => x.Id == advertisingId);
            if (AdsList == null) throw new InvalidOperationException("Anuncio no encontrado");
            return new AdvertisingViewModel(new advertising() {
                Id = AdsList.Id,
                title = AdsList.title,
                detail = AdsList.detail,
                price = AdsList.price,
                customer_id = AdsList.customer_id,
                resource = AdsList.resource.Where(x => x.advertising_id == AdsList.Id).ToList() 
            });
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

        public void AddResource(int advertisingId, int trackId)
        {
            //var playList = _playListRepository.Get().FirstOrDefault(x => x.Id == playListId);
            //if (playList == null) throw new InvalidOperationException("Playlist no encontrado");
            //var track = _trackRepository.Get().FirstOrDefault(x => x.Id==trackId);
            //if (playList == null) throw new InvalidOperationException("Track no encontrado");

            //playList.Track.Add(track);
            //_playListRepository.Update(playList);

        }


        public string rows_customer()
        {
            var customer = _customerRepository.Get().FirstOrDefault(c => c.email == "elmer.nyd@gmail.com");
            return customer.fullname;
        }
    }
}
