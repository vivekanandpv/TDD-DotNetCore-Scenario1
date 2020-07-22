namespace Scenario1.Web.Domain
{
    public class InventoryService
    {
        public bool Purchase(Invoice invoice)
        {
            if (invoice.IsEmpty || invoice.GrandTotal == 0)
            {
                return false;
            }

            return true;
        }
    }
}