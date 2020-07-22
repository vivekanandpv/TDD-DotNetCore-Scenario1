using System;

namespace Scenario1.Web.Domain
{
    public class LineItem
    {
        private readonly decimal lineAmount;

        public LineItem(decimal lineAmount = 0)
        {
            if (lineAmount < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.lineAmount = lineAmount;
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
