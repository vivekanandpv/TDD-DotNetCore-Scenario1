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
            Assert.That(() => new LineItem(-1.00m), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void ThrowArgumentOutOfRangeExceptionForNegativeLineAmountForPropertyLineAmount()
        {
            Assert.That(
                () => new LineItem(-1.00m),
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
    }
}
