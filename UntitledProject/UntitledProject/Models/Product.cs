using System;
using System.Collections.Generic;

namespace UntitledProject.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public ICollection<Category> Categories { get; set; }
        public AppUser Offerer { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Condition Condition { get; set; }
        public string ImageName { get; set; }

    }
}
