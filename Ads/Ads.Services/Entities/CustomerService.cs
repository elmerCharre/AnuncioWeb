using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Linq;

namespace Ads.Services.Entities
{
    public class CustomerService : IDisposable
    {
        private IRepository<customers> _customerRepository;

        public CustomerService(IRepository<customers> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Create(CustomerViewModel customerViewModel)
        {
            var customer = new customers
            {
                fullname = customerViewModel.FullName,
                email = customerViewModel.Email,
                phone = customerViewModel.Phone,
                occupation = customerViewModel.Occupation,
                address = customerViewModel.Address
            };
            _customerRepository.Create(customer);
        }

        public CustomerViewModel getCustomerById(int userID)
        {
            return getCustomer(_customerRepository.Get().FirstOrDefault(x => x.Id == userID));
        }

        public CustomerViewModel getCustomerByEmail(string userEmail)
        {
            return getCustomer(_customerRepository.Get().FirstOrDefault(x => x.email == userEmail));
        }

        public CustomerViewModel getCustomer(customers customer)
        {
            if (customer == null) return null;
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
