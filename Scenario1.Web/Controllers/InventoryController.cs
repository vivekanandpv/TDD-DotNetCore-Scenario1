using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scenario1.Web.Domain;
using Scenario1.Web.ViewModels;

namespace Scenario1.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService service;

        public InventoryController(IInventoryService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(service.GetAllProducts());
        }

        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(service.GetProductById(id));
        }

        public async Task<IActionResult> AddProduct(ProductAddViewModel vm)
        {
            try
            {
                return Ok(service.AddProduct(vm));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                service.DeleteProduct(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
