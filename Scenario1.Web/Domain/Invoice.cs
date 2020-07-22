using System;
using System.Collections;
using System.Collections.Generic;

namespace Scenario1.Web.Domain
{
    public class Invoice
    {
        private readonly IList<LineItem> lineItems;
        private readonly DateTime date;
        private readonly string invoiceNumber;
        private readonly string vendorName;

        public Invoice(DateTime date, string invoiceNumber, string vendorName)
        {
            if (date > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(date));
            }

            if (string.IsNullOrWhiteSpace(invoiceNumber))
            {
                throw new ArgumentNullException(nameof(invoiceNumber));
            }

            if (string.IsNullOrWhiteSpace(vendorName))
            {
                throw new ArgumentNullException(nameof(vendorName));
            }

            lineItems = new List<LineItem>();
            this.date = date;
            this.invoiceNumber = invoiceNumber;
            this.vendorName = vendorName;
        }

        public int Size
        {
            get
            {
                return lineItems.Count;
            }

        }

        public string Number
        {
            get
            {
                return invoiceNumber;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Size == 0;
            }

        }

        public string VendorName
        {
            get
            {
                return vendorName;
            }
        }

        public decimal GrandTotal
        {
            get
            {
                return getGrandTotal();
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
        }

        private decimal getGrandTotal()
        {
            decimal total = 0m;
            //  iterate over the lineitems
            //  keep the cumulative count

            foreach (var lineItem in lineItems)
            {
                total += lineItem.LineAmount;
            }
            //  return

            return total;
        }


        public void AddItem(LineItem item)
        {
            lineItems.Add(item);
        }
    }
}