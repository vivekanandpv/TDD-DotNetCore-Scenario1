using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using Scenario1.Web.Controllers;
using Scenario1.Web.Domain;
using Scenario1.Web.ViewModels;

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

        [Test]
        [TestCase(14)]
        [TestCase(25)]
        [TestCase(104)]
        public async Task ReturnTheProductForGetProductByIdWhereIdIsNonZeroPositiveInteger(int id)
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);

            //  Configure for the id
            var productTest = new Product(id);
            service.GetProductById(Arg.Is<int>(i => i > 0)).Returns(productTest);

            //  Act
            //  Act
            OkObjectResult result = await controller.GetProductById(id) as OkObjectResult;
            var product = result.Value as Product;

            //  Assert
            Assert.That(product, Is.InstanceOf<Product>());
            Assert.That(product.Id, Is.EqualTo(productTest.Id));
        }

        [Test]
        [TestCase(1)]
        [TestCase(23)]
        [TestCase(456)]
        public async Task ReturnProductForValidProductViewModel(int id)
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);

            //  Configure for the id
            var productViewModelTest = new ProductAddViewModel {Id = id};
            service
                .AddProduct(Arg.Is<ProductAddViewModel>(p => p.Id > 0))
                .Returns(new Product(id));

            //  Act
            OkObjectResult result = await controller.AddProduct(productViewModelTest) as OkObjectResult;
            var product = result.Value as Product;

            //  Assert
            Assert.That(product.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-234)]
        public async Task ThrowsExceptionForInvalidProductViewModel(int id)
        {
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);

            //  Configure for the id
            var productViewModelTest = new ProductAddViewModel { Id = id };
            service
                .AddProduct(Arg.Is<ProductAddViewModel>(p => p.Id <= 0))
                .Throws(new Exception());

            //  Act & Assert
            Assert.That(() => service.AddProduct(productViewModelTest), Throws.TypeOf<Exception>());
        }
    }
}
