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
        public async Task CallGetAllProductsOnTheServiceIfGetAllProductsIsCalled()
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);

            //  Act
            var _ = await controller.GetAllProducts();

            //  Assert!
            service.Received().GetAllProducts();
        }

        [Test]
        public async Task ReturnTheListOfProductsFromTheService()
        {
            //  Arrange
            IEnumerable<Product> productsFromTest = new List<Product>();
            IInventoryService service = Substitute.For<IInventoryService>();

            // configure the mock so that it returns the value we say
            service.GetAllProducts().Returns(productsFromTest);

            var controller = new InventoryController(service);

            //  Act
            OkObjectResult result = await controller.GetAllProducts() as OkObjectResult;
            var productsFromController = result.Value as IEnumerable<Product>;

            //  Assert
            Assert.That(productsFromController, Is.EqualTo(productsFromTest));
        }
    }
}
