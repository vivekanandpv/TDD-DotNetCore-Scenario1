using NUnit.Framework;
using Scenario1.Web.Domain;
using System;

namespace Scenario1.Tests
{
    public class LineItemShould
    {
        [Test]
        [TestCase(1, -100)]
        [TestCase(-1, 200)]
        [TestCase(-5, -14.25)]
        public void ThrowArgumentOutOfRangeExceptionForNegativeLineAmount(int quantity, decimal price)
        {
            Assert.That(() => new LineItem(itemName: "ABC", quantity: quantity, price: price), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        //[Test]
        //public void ThrowArgumentOutOfRangeExceptionForNegativeLineAmountForPropertyLineAmount()
        //{
        //    Assert.That(
        //        () => new LineItem(itemName: "ABC", quantity: 1, price: -100.00m),
        //        Throws.TypeOf<ArgumentOutOfRangeException>()
        //            .With
        //            .Matches<ArgumentOutOfRangeException>(
        //                e => e.ParamName=="lineAmount"
        //                )
        //    );
        //}

        [Test]
        [TestCase(2, 100.00, ExpectedResult = 200.00)]
        [TestCase(4, 10.00, ExpectedResult = 40.00)]
        [TestCase(3, 25.25, ExpectedResult = 75.75)]
        public decimal ReturnTheLineAmountEqualToTheProductOfPriceAndQuantity(int quantity, decimal price)
        {
            var lineItem = new LineItem(itemName: "XYZ", quantity: quantity, price: price);

            return lineItem.LineAmount;
        }

        [Test]
        [TestCase(-1, 0)]
        [TestCase(-100, 10)]
        [TestCase(-52, 500)]
        public void ThrowArgumentOutOfRangeExceptionForZeroAndNegativeQuantity(int quantity, decimal price)
        {
            Assert.That(
                () => new LineItem(itemName: "ABC", quantity: quantity, price: price),
                Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With
                    .Matches<ArgumentOutOfRangeException>(
                        e => e.ParamName == "quantity"
                    )
            );
        }

        [Test]
        [TestCase(2, -741)]
        [TestCase(5, -58.47)]
        [TestCase(14, -5748.56)]
        public void ThrowArgumentOutOfRangeExceptionForNegativePrice(int quantity, decimal price)
        {
            Assert.That(
                () => new LineItem(itemName: "ABC", quantity: quantity, price: price),
                Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With
                    .Matches<ArgumentOutOfRangeException>(
                        e => e.ParamName == "price"
                    )
            );
        }
    }
}
