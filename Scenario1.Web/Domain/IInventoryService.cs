using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scenario1.Web.Domain
{
    public interface IInventoryService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
    }
}
