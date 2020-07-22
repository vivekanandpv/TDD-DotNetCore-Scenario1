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
            var lineItem = new LineItem(itemName:"ABC", quantity:1, price:100.00m);

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
            var lineItem = new LineItem(itemName: "ABC", quantity: 1, price: 100.00m);

            //  Act
            invoice.AddItem(lineItem);

            //  Assert
            Assert.That(invoice.IsEmpty, Is.EqualTo(false));
        }

        [Test]
        public void HaveTheGrandTotalEqualToTheAccumulatedTotalOfLineAmounts()
        {
            //  Arrange
            var lineItem1 = new LineItem(itemName: "ABC", quantity: 1, price: 100.00m);
            var lineItem2 = new LineItem(itemName: "DEF", quantity: 2, price: 100.00m);
            var lineItem3 = new LineItem(itemName: "GHI", quantity: 3, price: 100.00m);

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
