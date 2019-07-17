using StonePayments.Business;
using StonePayments.Util.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StonePayments.Server.Repositories
{
    public class CustomerRepository : 
        AbstractCRUDRepository<CustomerModel, CustomerException, StonePaymentsEntitiesCustom>
    {
        protected override void PrepareEntityToInsert(
            CustomerModel entityModel, 
            StonePaymentsEntitiesCustom context)
        {
            var customer = new Customer
            {
                Id = entityModel.Id,
                Nome = entityModel.Name,
                CreditLimit = entityModel.CreditLimit
            };

            context.Customers.Add(customer);
        }

        protected override void PrepareEntityToUpdate(
            CustomerModel entityModel, 
            StonePaymentsEntitiesCustom context)
        {
            var customer = (from c in context.Customers
                            where c.Id == entityModel.Id
                            select c).FirstOrDefaultAsync<Customer>().GetAwaiter().GetResult();
            
            customer.Nome = entityModel.Name;
            customer.CreditLimit = entityModel.CreditLimit;
        }

        protected override void PrepareEntityToDelete(
            CustomerModel entityModel, 
            StonePaymentsEntitiesCustom context)
        {
            var customer = (from c in context.Customers
                            where c.Id == entityModel.Id
                            select c).FirstOrDefaultAsync<Customer>().GetAwaiter().GetResult();

            context.Customers.Remove(customer);
        }

        protected override async Task<List<CustomerModel>> PrepareEntityToSelect(
            CustomerModel entityModel,
            StonePaymentsEntitiesCustom context)
        {
            return (from c in
                        await (
                            from c0 in context.Customers
                            where entityModel.Id != Guid.Empty && c0.Id == entityModel.Id ||
                                    entityModel.Id == Guid.Empty
                            orderby c0.Nome
                            select c0
                            ).ToListAsync<Customer>()
                    select new CustomerModel()
                    {
                        Id = c.Id,
                        Name = c.Nome,
                        CreditLimit = c.CreditLimit

                    }).ToList<CustomerModel>();
        }
    }
}