using System.Collections;
using System.Collections.Generic;

namespace Scenario1.Web.Domain
{
    public class Invoice
    {
        private readonly IList<LineItem> lineItems;

        public Invoice()
        {
            lineItems = new List<LineItem>();
        }

        public int Size
        {
            get
            {
                return lineItems.Count;
            }

        }

        public bool IsEmpty
        {
            get
            {
                return Size == 0;
            }

        }

        public decimal GrandTotal
        {
            get
            {
                return getGrandTotal();
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