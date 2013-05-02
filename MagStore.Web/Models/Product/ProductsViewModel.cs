﻿using System.Collections.Generic;

namespace MagStore.Web.Models.Product
{
    public class ProductsViewModel
    {
        public Entities.Catalogue Catalogue { get; set; } 
        public IEnumerable<Entities.Product> Products { get; set; }
    }
}