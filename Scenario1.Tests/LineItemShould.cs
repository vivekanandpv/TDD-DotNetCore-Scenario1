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
    }
}
