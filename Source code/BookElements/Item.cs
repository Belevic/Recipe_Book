using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Xml.Serialization;
using RecipeBook.Gui;

namespace RecipeBook.BookElements
{
    [GuiInfo(DisplayText = "item")]
    public class Item : BookObject
    {
        public Category Category { get; set; }
    }
}
