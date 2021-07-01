using System;
using System.Collections.Generic;
using System.Text;

namespace ShoeStore.Products.Entities
{
    public class Catalogue
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CatalogueProduct> CatalogueProducts { get; set; }
    }
}
