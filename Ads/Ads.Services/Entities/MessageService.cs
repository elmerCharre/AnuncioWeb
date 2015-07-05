using System;
using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Linq;

namespace Ads.Services.Entities
{
    public class MessageService : IDisposable
    {
        private IRepository<contacts> _messageRepository;

        public MessageService(IRepository<contacts> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public void Dispose()
        {
            _messageRepository = null;
        }
    }
}
