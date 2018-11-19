using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Xml.Serialization;

namespace RecipeBook.Domain
{
    public class Ingredient : BookObject
    {
        public Category Category { get; set; }
        public Guid ItemId { get; set; }
    }
}
