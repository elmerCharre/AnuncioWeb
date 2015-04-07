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

        public IEnumerable<AdvertisingViewModel> GetAdvertisingLists(string userName)
        {
            var customerQuery = _customerRepository.Get();
            var customer = customerQuery.FirstOrDefault(c => c.email == userName);

            if (customer == null) throw new InvalidOperationException(string.Format("Cliente no encontrado {0}", userName));
            var AdvertisingLists = _advertisingRepository.Get().Where(x => x.CustomerId == customer.Id).ToList();
            // aqui se tiene que hacer un mapeo del dominio al viewmodel
            return AdvertisingLists.Select(playList => new AdvertisingViewModel(playList)).ToList();
        }
         
        public void Dispose()
        {
            _resourceRepository = null;
        }

        public void Create(AdvertisingViewModel model)
        {
            var customer = _customerRepository.Get().FirstOrDefault(x => x.email == model.);
            if (customer == null) throw new InvalidOperationException(string.Format("Cliente no encontrado {0}", model.CustomerUserName));
            var advertising = new advertising() {
                title = model.title,
                detail = model.detail,
                price = model.price,
                CustomerId = customer.Id
            };
            _advertisingRepository.Create(advertising);
        }

        public AdvertisingViewModel Get(int playlistId)
        {
            var playList = _playListRepository.Get().FirstOrDefault(x => x.Id == playlistId);
            if (playList== null) throw new InvalidOperationException("Playlist no encontrado");
            return new PlaylistEditViewModel(){ Name = playList.Name, Id = playList.Id, CustomerId=playList.CustomerId, Tracks = playList.Track.Select(track => new TracksListViewModel(track,playList.Id)).ToList() };
        }

        public IEnumerable<TracksListViewModel> GetTracksFrom(PlaylistSearchTrackViewModel request)
        {
            var tracklist = _trackRepository.Get().Where(x=>x.Name.Contains(request.TrackName)).ToList();
            return tracklist.Select(track => new TracksListViewModel(track, request.PlayListId)).ToList();
        }

        public void AddTrack(int playListId, int trackId)
        {
            var playList = _playListRepository.Get().FirstOrDefault(x => x.Id == playListId);
            if (playList == null) throw new InvalidOperationException("Playlist no encontrado");
            var track = _trackRepository.Get().FirstOrDefault(x => x.Id==trackId);
            if (playList == null) throw new InvalidOperationException("Track no encontrado");

            playList.Track.Add(track);
            _playListRepository.Update(playList);

        }

    }
}
