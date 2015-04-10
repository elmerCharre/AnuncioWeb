using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Linq;

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

        public CustomerViewModel getCustomer(string userName)
        {
            var customer = _customerRepository.Get().FirstOrDefault(x => x.email == userName);
            return new CustomerViewModel
            {
                Id = customer.Id,
                Email = customer.email,
                FullName = customer.fullname,
                Address = customer.address,
                Phone = customer.phone,
                Occupation = customer.occupation
            };
        }

        public void Dispose()
        {
            _customerRepository = null;
        }
    }
}
