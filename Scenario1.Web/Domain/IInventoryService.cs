﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scenario1.Web.ViewModels;

namespace Scenario1.Web.Domain
{
    public interface IInventoryService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product AddProduct(ProductAddViewModel vm);
        void DeleteProduct(int id);
        Product UpdateProduct(int id, ProductUpdateViewModel vm);
    }
}
