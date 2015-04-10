using System.Linq;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using Ads.Services.Entities;
using System.Collections.Generic;
using System;

namespace Ads.Services.Entities
{
    public class AdvertisingService
    {
        private IRepository<advertising> _advertisingRepository;
        private IRepository<customer> _customerRepository;
        private IRepository<resource> _resourceRepository;

        public AdvertisingService(IRepository<resource> resourceRepository)
        {
            _resourceRepository = resourceRepository;
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
            _resourceRepository = null;
        }

        public void Create(AdvertisingViewModel model)
        {
            var customer = _customerRepository.Get().FirstOrDefault(x => x.Id == model.customer_id);
            if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
            var advertising = new advertising() {
                title = model.title,
                detail = model.detail,
                price = model.price,
                customer_id = customer.Id,
                resource = model.resource
            };
            _advertisingRepository.Create(advertising);

            // test
            //var playListService = new AdvertisingService(IRepository<resource>);
            //var playlist = new Playlist();
            //playlist.Id = 0;
            //playlist.Name = data.Get("Name").ToString();
            //playlist.CustomerId = 60;

            //var trackIds = data.GetValues("chk_tracklist");
            //foreach (string trackId in trackIds)
            //{
            //    _advertisingRepository.Track.Add(playListService.GetTrack(int.Parse(trackId)));
            //}
            //playListService.create(playlist);
            //
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

        public void AddResource(int advertisingId, int trackId)
        {
            //var playList = _playListRepository.Get().FirstOrDefault(x => x.Id == playListId);
            //if (playList == null) throw new InvalidOperationException("Playlist no encontrado");
            //var track = _trackRepository.Get().FirstOrDefault(x => x.Id==trackId);
            //if (playList == null) throw new InvalidOperationException("Track no encontrado");

            //playList.Track.Add(track);
            //_playListRepository.Update(playList);

        }

    }
}
