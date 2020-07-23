﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scenario1.Web.Domain;

namespace Scenario1.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        public IEnumerable<Product> GetAllProducts()
        {
            return new List<Product>();
        }
    }
}
