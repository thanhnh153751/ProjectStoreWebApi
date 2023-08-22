using BusinessObjects.Entities;
using DataAccess;
using DataAccess.ModelViewOdata;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<Customer> AuthenticationCustomer(string userName, string pass)
        {
            var model = await CustomerDAO.AuthenticationCustomer(userName,pass);
            return model;
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            var model = await CustomerDAO.GetCustomerById(customerId);
            return model;
        }

        public async Task Update(Customer model)
        {
            await CustomerDAO.Update(model);
        }
    }
}
