using NUnit.Framework;
using Scenario1.Web.Domain;

namespace Scenario1.Tests
{
    public class InvoiceShould
    {
        [Test]
        public void AddTheLineItemToTheInternalList()
        {
            //  Arrange
            var invoice = new Invoice();
            var lineItem = new LineItem();

            //  Act
            invoice.AddItem(lineItem);

            //  Assert
            Assert.That(invoice.Size, Is.EqualTo(1));
        }

        [Test]
        public void NotBeEmptyForMinimumOneLineItem()
        {
            //  Arrange
            var invoice = new Invoice();
            var lineItem = new LineItem();

            //  Act
            invoice.AddItem(lineItem);

            //  Assert
            Assert.That(invoice.IsEmpty, Is.EqualTo(false));
        }

    }
}
