using System;

namespace Scenario1.Web.Domain
{
    public class LineItem
    {
        private readonly decimal lineAmount;
        private readonly string itemName;
        private readonly int quantity;
        private readonly decimal price;

        public LineItem(string itemName, int quantity, decimal price)
        {
            this.itemName = itemName;
            this.quantity = quantity;
            this.price = price;

            lineAmount = this.quantity * this.price;

            if (lineAmount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lineAmount));
            }
        }

        public decimal LineAmount
        {
            get
            {
                return lineAmount;
            }
        }
    }
}
