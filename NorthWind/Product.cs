namespace Northwind.Models
{
    public class Product
    {
        public bool IsDiscontinued { get; set; }
        public decimal UnitPrice { get; set; }
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string Package { get; set; }
        public string ProductName { get; set; }
    }
}
