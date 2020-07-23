using System.Collections.Generic;
using NUnit.Framework;
using Scenario1.Web.Controllers;
using Scenario1.Web.Domain;

namespace Scenario1.Tests
{
    public class InventoryControllerShould
    {
        [Test]
        public void ReturnListOfProductsForGetRequestToGetAllProducts()
        {
            //  Arrange
            var controller = new InventoryController();

            //  Act
            IEnumerable<Product> products = controller.GetAllProducts();

            //  Assert
            Assert.That(products == null, Is.EqualTo(false));
        }
    }
}
