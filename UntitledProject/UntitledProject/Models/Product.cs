using System;
using System.Collections.Generic;

namespace UntitledProject.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Offerer { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
