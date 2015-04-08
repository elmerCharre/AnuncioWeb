using System;
using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;

namespace Ads.Services.Entities
{
    public class CustomerService : IDisposable
    {
        private IRepository<customer> _customerRepository;

        public CustomerService(IRepository<customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Create(CustomerViewModel customerViewModel)
        {
            var customer = new customer
            {
                fullname = customerViewModel.FullName,
                email = customerViewModel.Email,
                phone = customerViewModel.Phone,
                occupation = customerViewModel.Occupation,
                address = customerViewModel.Address
            };
            _customerRepository.Create(customer);
        }

        public void getCustomer(CustomerViewModel customerViewModel)
        {
            var customer = new customer
            {
                fullname = customerViewModel.FullName,
                email = customerViewModel.Email,
                phone = customerViewModel.Phone,
                occupation = customerViewModel.Occupation,
                address = customerViewModel.Address
            };
            _customerRepository.Create(customer);
        }

        public void Dispose()
        {
            _customerRepository = null;
        }
    }
}
