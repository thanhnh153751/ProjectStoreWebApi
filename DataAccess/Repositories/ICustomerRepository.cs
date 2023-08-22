
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;

namespace DataAccess
{
    public interface ICustomerRepository
    {
        public Task<Customer> AuthenticationCustomer(string userName, string pass);
        public Task<Customer> GetCustomerById(int customerId);
        public Task Update(Customer model);
    }
}
