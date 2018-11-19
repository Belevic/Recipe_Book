using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecipeBook.Gui;

namespace RecipeBook.BookElements
{
    public enum Category : long
    {
        [GuiInfo(DisplayText = "Pasta and Rice")]
        PastaAndRice,
        Vegetables,
        Cereal,
        Fruits,
        Baking,
        Snacks,
        Beverages,
        Produce,
        Dairy,
        [GuiInfo(DisplayText = "Sauces, Spices, and Condiments")]
        SaucesSpicesAndCondiments,        
        Canned,
        [GuiInfo(DisplayText = "Milk products")]
        MilkProducts,
        [GuiInfo(DisplayText = "Bread,Other Breakfast Items")]
        BreadAndOtherBreakfast,
        Meat,
        Frozen,
        Other
    }
}
