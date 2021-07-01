using System;
using System.Collections.Generic;
using System.Text;

namespace ShoeStore.Products.Entities
{
    public class CatalogueProduct
    {
        public int CatalogueProductId { get; set; }
        public int CatalogueId { get; set; }

        public Catalogue Catalogue { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
