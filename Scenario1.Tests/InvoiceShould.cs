using System;
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
            var date = DateTime.Now;
            var invoice = new Invoice(date, "A1234", "Vendor");
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
            var date = DateTime.Now;
            var invoice = new Invoice(date, "A1234", "Vendor");
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

            var date = DateTime.Now;
            var invoice = new Invoice(date, "A1234", "Vendor");

            //  Act
            invoice.AddItem(lineItem1);
            invoice.AddItem(lineItem2);
            invoice.AddItem(lineItem3);

            //  Assert

            Assert.That(invoice.GrandTotal, Is.EqualTo(600));
        }

        [Test]
        public void HaveNonFutureDateByDefault()
        {
            var date = DateTime.Now;
            var invoice = new Invoice(date, "A1234", "Vendor");
            var now = DateTime.Now;

            var timeSpan = (invoice.Date - now).TotalMilliseconds;

            Assert.That(timeSpan, Is.LessThanOrEqualTo(0));
        }

        [Test]
        public void ThrowArgumentOutOfRangeExceptionForFutureDate()
        {
            var futureDate = DateTime.Now.AddDays(2);
            Assert.That(
                () => new Invoice(futureDate, "A1234", "Vendor"), 
                Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With
                    .Matches<ArgumentOutOfRangeException>(
                        e => e.ParamName == "date"
                    )
            );
        }

        [Test]
        [TestCase("A1234")]
        [TestCase("B5678")]
        [TestCase("E1")]
        [TestCase("234")]
        public void HaveNonEmptyNonNullNonWhiteSpaceAlphaNumericInvoiceNumber(string invoiceNumber)
        {
            var invoice = new Invoice(DateTime.Now, invoiceNumber, "Vendor");

            Assert.That(string.IsNullOrWhiteSpace(invoice.Number), Is.EqualTo(false));
        }

        [Test]
        [TestCase("")]                  //  empty
        [TestCase(null)]    //  null
        [TestCase(" ")]                 //  space
        [TestCase("\t")]                //  tab
        [TestCase("\n")]                //  UNIX new line
        [TestCase("\r\n")]              //  Windows new line
        public void ThrowArgumentNullExceptionWhereInvoiceNumberIsEmptyNullWhiteSpace(string invoiceNumber)
        {
            Assert.That(
                () => new Invoice(DateTime.Now, invoiceNumber, "Vendor"),
                Throws.TypeOf<ArgumentNullException>()
                    .With
                    .Matches<ArgumentNullException>(
                        e => e.ParamName == "invoiceNumber"
                    )
            );
        }

        [Test]
        [TestCase("ABC Traders")]
        [TestCase("My Store")]
        [TestCase("Easy Supermarket")]
        [TestCase("Bengaluru Stores")]
        public void HaveNonEmptyNonNullNonWhiteSpaceVendorName(string vendorName)
        {
            var invoice = new Invoice(DateTime.Now, "A1234", vendorName);

            Assert.That(string.IsNullOrWhiteSpace(invoice.VendorName), Is.EqualTo(false));
        }

        [Test]
        [TestCase("")]                  //  empty
        [TestCase(null)]       //  null
        [TestCase(" ")]                 //  space
        [TestCase("\t")]                //  tab
        [TestCase("\n")]                //  UNIX new line
        [TestCase("\r\n")]              //  Windows new line
        public void ThrowArgumentNullExceptionWhereVendorNameIsEmptyNullWhiteSpace(string vendorName)
        {
            Assert.That(
                () => new Invoice(DateTime.Now, "ABC123", vendorName),
                Throws.TypeOf<ArgumentNullException>()
                    .With
                    .Matches<ArgumentNullException>(
                        e => e.ParamName == "vendorName"
                    )
            );
        }
    }
}
