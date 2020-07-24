using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Scenario1.Web.Controllers;
using Scenario1.Web.Domain;

namespace Scenario1.Tests
{
    public class InventoryControllerShould
    {
        [Test]
        public async Task ReturnListOfProductsForGetRequestToGetAllProducts()
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);

            //  Act
            OkObjectResult result = await controller.GetAllProducts() as OkObjectResult;
            var products = result.Value as IEnumerable<Product>;

            //  Assert
            Assert.That(products == null, Is.EqualTo(false));
        }

        [Test]
        public async Task GetTheListOfProductsFromTheDependency()
        {
            //  Arrange
            // Test double: Dummy, Fake, Mock, Spy
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);

            //  Act and assert
            //If you call controller.GetAllProducts() this should call service.GetAllProducts()

            var _ = await controller.GetAllProducts();

            //  Assert!
            service.Received().GetAllProducts();
        }
    }
}
