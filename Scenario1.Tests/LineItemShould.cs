using NUnit.Framework;
using Scenario1.Web.Domain;
using System;

namespace Scenario1.Tests
{
    public class LineItemShould
    {
        [Test]
        public void ThrowArgumentOutOfRangeExceptionForNegativeLineAmount()
        {
            Assert.That(() => new LineItem(itemName: "ABC", quantity: -1, price: 100.00m), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void ThrowArgumentOutOfRangeExceptionForNegativeLineAmountForPropertyLineAmount()
        {
            Assert.That(
                () => new LineItem(itemName: "ABC", quantity: 1, price: -100.00m),
                Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With
                    .Matches<ArgumentOutOfRangeException>(
                        e => e.ParamName=="lineAmount"
                        )
            );

            //  Also possible, but type unsafe because of literal strings
            //Assert.That(
            //    () => new LineItem(-1.00m), 
            //    Throws.TypeOf<ArgumentOutOfRangeException>()
            //        .With
            //        .Property("ParamName")
            //        .EqualTo("lineAmount")
            //    );
        }

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
        public void ThrowArgumentOutOfRangeExceptionForZeroAndNegativeQuantity()
        {
            Assert.That(
                () => new LineItem(itemName: "ABC", quantity: -1, price: 0m),
                Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With
                    .Matches<ArgumentOutOfRangeException>(
                        e => e.ParamName == "quantity"
                    )
            );
        }
    }
}
