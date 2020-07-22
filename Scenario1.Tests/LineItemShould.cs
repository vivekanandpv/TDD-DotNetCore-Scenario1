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
                () => new LineItem(itemName: "ABC", quantity: -1, price: 100.00m),
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
        public void HaveTheLineAmountEqualToTheProductOfPriceAndQuantity()
        {
            var lineItem = new LineItem(itemName: "XYZ", quantity: 2, price: 100.00m);

            Assert.That(lineItem.LineAmount, Is.EqualTo(200));
        }
    }
}
