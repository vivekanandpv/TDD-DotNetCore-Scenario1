using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scenario1.Web.Domain
{
    public class Product
    {
        public Product(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
