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
        [TestCase(0)]
        [TestCase(-25)]
        [TestCase(-104)]
        public async Task ReturnNotFoundWhereIdIsZeroOrNegativeInteger(int id)
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);

            //  Configure for the id
            service.GetProductById(Arg.Is<int>(i => i <= 0))
                .Throws<Exception>();

            //  Act
            NotFoundResult result = await controller.GetProductById(id) as NotFoundResult;

            //  Assert
            Assert.That(result == null, Is.EqualTo(false));
        }

        [Test]
        [TestCase(1)]
        [TestCase(23)]
        [TestCase(456)]
        public async Task ReturnProductForValidProductAddViewModel(int id)
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);

            //  Configure for the id
            var productViewModelTest = new ProductAddViewModel { Id = id };
            var productTest = new Product(id);
            service
                .AddProduct(Arg.Is<ProductAddViewModel>(p => p.Id > 0))
                .Returns(productTest);

            //  Act
            OkObjectResult result = await controller.AddProduct(productViewModelTest) as OkObjectResult;
            var product = result.Value as Product;  // If result.Value cannot be cast as Product, the object will be null

            //  Assert
            Assert.That(product, Is.EqualTo(productTest));
            Assert.That(product == null, Is.EqualTo(false));
        }

        [Test]
        [TestCase(1)]
        [TestCase(34)]
        [TestCase(4589)]
        public async Task ReturnOkForValidIdDeletion(int id)
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);

            //  Configure mock
            service
                .When(m => m.DeleteProduct(Arg.Is<int>(i => i <= 0)))
                .Do(_ => throw new Exception());

            //  Act
            OkResult result = await controller.DeleteProduct(id) as OkResult;

            //  Assert
            Assert.That(result == null, Is.EqualTo(false));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-34)]
        [TestCase(-4589)]
        public async Task ReturnNotFoundForInvalidIdDeletion(int id)
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);

            //  Configure mock
            service
                .When(m => m.DeleteProduct(Arg.Is<int>(i => i <= 0)))
                .Do(_ => throw new Exception());

            //  Act
            NotFoundResult result = await controller.DeleteProduct(id) as NotFoundResult;

            //  Assert
            Assert.That(result == null, Is.EqualTo(false));
        }

        [Test]
        [TestCase(1)]
        [TestCase(23)]
        [TestCase(3421)]
        public async Task ReturnOkForValidProductAddition(int id)
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);
            var vm = new ProductAddViewModel { Id = id };

            //  Configure mock
            service
                .AddProduct(Arg.Is<ProductAddViewModel>(p => p.Id >= 0))
                .Returns(new Product(id));

            //  Act
            OkObjectResult result = await controller.AddProduct(vm) as OkObjectResult;

            //  Assert
            Assert.That(result == null, Is.EqualTo(false));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-34)]
        [TestCase(-4589)]
        public async Task ReturnNotFoundForInvalidProductAddition(int id)
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);
            var vm = new ProductAddViewModel { Id = id };

            //  Configure mock
            service
                .AddProduct(Arg.Is<ProductAddViewModel>(p => p.Id <= 0))
                .Throws(new Exception());

            //  Act
            NotFoundResult result = await controller.AddProduct(vm) as NotFoundResult;

            //  Assert
            Assert.That(result == null, Is.EqualTo(false));
        }


        [Test]
        [TestCase(1)]
        [TestCase(23)]
        [TestCase(3421)]
        public async Task ReturnOkForValidProductUpdate(int id)
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);
            var vm = new ProductUpdateViewModel { Id = id };

            //  Configure mock
            service
                .UpdateProduct(Arg.Is<int>(i=>i > 0),Arg.Is<ProductUpdateViewModel>(p => p.Id >= 0))
                .Returns(new Product(id));

            //  Act
            OkObjectResult result = await controller.UpdateProduct(id, vm) as OkObjectResult;

            //  Assert
            Assert.That(result == null, Is.EqualTo(false));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-123)]
        [TestCase(-3421)]
        public async Task ReturnBadRequestForValidProductUpdate(int id)
        {
            //  Arrange
            IInventoryService service = Substitute.For<IInventoryService>();
            var controller = new InventoryController(service);
            var vm = new ProductUpdateViewModel { Id = id };

            //  Configure mock
            service
                .UpdateProduct(Arg.Is<int>(i => i <= 0), Arg.Is<ProductUpdateViewModel>(p => p.Id <= 0))
                .Throws<Exception>();

            //  Act
            NotFoundResult result = await controller.UpdateProduct(id, vm) as NotFoundResult;

            //  Assert
            Assert.That(result == null, Is.EqualTo(false));
        }
    }
}
