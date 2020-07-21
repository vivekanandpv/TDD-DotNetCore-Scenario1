using NUnit.Framework;
using Scenario1.Web.Domain;

namespace Scenario1.Tests
{
    public class InventoryServiceShould
    {
        [Test]
        public void ReturnTrueForSuccessfulPurchase()
        {
            //  Arrange
            var invoice = new Invoice();
            var inventoryService = new InventoryService();

            //  Act
            bool result = inventoryService.Purchase(invoice);

            //  Assert
            //Assert.AreEqual(true, result);  //classical assertion API
            Assert.That(result, Is.EqualTo(true));
        }
    }
}