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

        [Test]
        public void HaveTheGrandTotalEqualToTheAccumulatedTotalOfLineAmounts()
        {
            //  Arrange
            var lineItem1 = new LineItem(lineAmount: 100);
            var lineItem2 = new LineItem(lineAmount: 200);
            var lineItem3 = new LineItem(lineAmount: 300);

            var invoice = new Invoice();

            //  Act
            invoice.AddItem(lineItem1);
            invoice.AddItem(lineItem2);
            invoice.AddItem(lineItem3);

            //  Assert

            Assert.That(invoice.GrandTotal, Is.EqualTo(600));
        }

    }
}
