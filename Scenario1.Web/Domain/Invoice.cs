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


        public void AddItem(LineItem item)
        {
            lineItems.Add(item);
        }
    }
}