using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Xml.Serialization;
using RecipeBook.Gui;

namespace RecipeBook.BookElements
{
    [GuiInfo(DisplayText = "recipe")]
    public class Recipe : BookObject
    {
        public List<Ingredient> Ingredients { get; set; }
        public string Steps { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }
    }
}
