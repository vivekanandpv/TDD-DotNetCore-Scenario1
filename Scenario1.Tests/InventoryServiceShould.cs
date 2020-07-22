using NUnit.Framework;
using Scenario1.Web.Domain;

namespace Scenario1.Tests
{
    public class InventoryServiceShould
    {
        [Test]
        public void ReturnTrueForPurchaseWithInvoiceThatHasLineItemsThatHavePositiveLineAmounts()
        {
            //  Arrange
            var invoice = new Invoice();
            var inventoryService = new InventoryService();
            var lineItem = new LineItem(itemName:"ABC", quantity:10, price:10.00m);

            //  Act
            invoice.AddItem(lineItem);
            bool result = inventoryService.Purchase(invoice);

            //  Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void ReturnFalseForAnEmptyInvoice()
        {
            //  Arrange
            var invoice = new Invoice();
            var inventoryService = new InventoryService();

            //  Act
            bool result = inventoryService.Purchase(invoice);

            //  Assert
            //Assert.AreEqual(true, result);  //classical assertion API
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void ReturnFalseForInvoiceWithGrandTotalEqualToZero()
        {
            //  Arrange
            var lineItem = new LineItem(itemName: "ABC", quantity: 0, price: 10.00m);
            var invoice = new Invoice();
            invoice.AddItem(lineItem);
            var inventoryService = new InventoryService();

            //  Act
            bool result = inventoryService.Purchase(invoice);

            //  Assert
            Assert.That(result, Is.EqualTo(false));
        }
    }
}