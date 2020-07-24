using System;
using System.Collections.Generic;
using Scenario1.Web.ViewModels;

namespace Scenario1.Web.Domain
{
    public class InventoryService : IInventoryService
    {
        public bool Purchase(Invoice invoice)
        {
            if (invoice.IsEmpty || invoice.GrandTotal == 0)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return new List<Product> {new Product(123), new Product(234)};
        }

        public Product GetProductById(int id)
        {
            if (id <= 0)
            {
                throw new Exception();
            }

            return new Product(id);
        }

        public Product AddProduct(ProductAddViewModel vm)
        {
            if (vm.Id <= 0)
            {
                throw new Exception();
            }

            return new Product(vm.Id);
        }

        public void DeleteProduct(int id)
        {
            if (id <= 0)
            {
                throw new Exception();
            }
        }

        public Product UpdateProduct(int id, ProductUpdateViewModel vm)
        {
            if (id <= 0)
            {
                throw new Exception();
            }

            return new Product(vm.Id);
        }
    }
}