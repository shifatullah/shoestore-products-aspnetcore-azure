using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Products.Entities
{
    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int Rank { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Cost { get; set; }

        public int Stock { get; set; }

        public Category Category { get; set; }

        public Brand Brand { get; set; }

        public string Size { get; set; }

        public Gender Gender { get; set; }

        public AgeGroup AgeGroup { get; set; }

        public Colour Colour { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public List<CatalogueProduct> CatalogueProducts { get; set; }
    }
}
