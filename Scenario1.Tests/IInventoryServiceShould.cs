﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using Scenario1.Web.Controllers;
using Scenario1.Web.Domain;
using Scenario1.Web.ViewModels;

namespace Scenario1.Tests
{
    public class IInventoryServiceShould
    {
        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-234)]
        public async Task ThrowsExceptionForAddingInvalidProductViewModel(int id)
        {
            IInventoryService service = Substitute.For<IInventoryService>();

            //  Configure for the id
            var productViewModelTest = new ProductAddViewModel { Id = id };
            service
                .AddProduct(Arg.Is<ProductAddViewModel>(p => p.Id <= 0))
                .Throws(new Exception());

            //  Act & Assert
            Assert.That(() => service.AddProduct(productViewModelTest), Throws.TypeOf<Exception>());
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-234)]
        public async Task ThrowsExceptionForDeletingWithNonExistentId(int id)
        {
            IInventoryService service = Substitute.For<IInventoryService>();

            //  void methods cannot use Throws chaining
            //  instead we use When and Do combination
            service
                .When(m => m.DeleteProduct(Arg.Is<int>(i => i <= 0)))
                .Do(_ => throw new Exception());

            //  Act & Assert
            Assert.That(() => service.DeleteProduct(id), Throws.TypeOf<Exception>());
        }
    }
}
