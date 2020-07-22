using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scenario1.Web.Domain
{
    public class LineItem
    {
        private readonly decimal lineAmount;

        public LineItem(decimal lineAmount = 0)
        {
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
