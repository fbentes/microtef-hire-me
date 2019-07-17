using StonePayments.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;


namespace StonePayments.Server.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task Insert(CustomerModel customerModel)
        {
            using (var context = new StonePaymentsEntitiesCustom())
            {
                var trans = context.Database.BeginTransaction();

                try
                {
                    var customer = new Customer
                    {
                        Id = Guid.NewGuid(),
                        Nome = customerModel.Name,
                        CreditLimit = customerModel.CreditLimit
                    };

                    context.Customers.Add(customer);

                    await context.SaveChangesAsync();

                    trans.Commit();
                }
                catch (Exception E)
                {
                    trans.Rollback();

                    throw new CustomerException(E.Message);
                }
            }
        }

        public async Task Update(CustomerModel customerModel)
        {
            using (var context = new StonePaymentsEntitiesCustom())
            {
                var trans = context.Database.BeginTransaction();

                try
                {
                    var customer = await (from c in context.Customers
                                          where c.Id == customerModel.Id
                                          select c).FirstOrDefaultAsync<Customer>();

                    customer.Nome = customerModel.Name;
                    customer.CreditLimit = customerModel.CreditLimit;

                    await context.SaveChangesAsync();

                    trans.Commit();
                }
                catch (Exception E)
                {
                    trans.Rollback();

                    throw new CustomerException(E.Message);
                }
            }
        }

        public async Task Delete(string id)
        {
            using (var context = new StonePaymentsEntitiesCustom())
            {
                var trans = context.Database.BeginTransaction();

                try
                {
                    var customer = await (from c in context.Customers
                                      where c.Id.ToString() == id
                                      select c).FirstOrDefaultAsync<Customer>();

                    context.Customers.Remove(customer);

                    await context.SaveChangesAsync();

                    trans.Commit();
                }
                catch (Exception E)
                {
                    trans.Rollback();

                    throw new CardException(E.Message);
                }
            }
        }

        public async Task<List<CustomerModel>> GetCustomers(string id = "")
        {
            using (var context = new StonePaymentsEntitiesCustom())
            {
                var result = (from c in
                                    await (
                                       from c0 in context.Customers
                                       where id != "" && c0.Id.ToString() == id ||
                                             id == null
                                       orderby c0.Nome
                                       select c0
                                     ).ToListAsync<Customer>()
                              select new CustomerModel()
                              {
                                  Id = c.Id,
                                  Name = c.Nome,
                                  CreditLimit = c.CreditLimit

                              }).ToList<CustomerModel>();

                return result;
            }
        }
    }
}
