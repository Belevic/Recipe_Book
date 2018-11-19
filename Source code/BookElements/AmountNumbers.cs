using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeBook.BookElements
{
    public static class AmountNumbers
    {
        private static readonly IEnumerable<double> defaultNumbers= new[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0,7.0,8.0,9.0,10.0 };
        public static IEnumerable<double> DefaultNumbers
        {
            get { return defaultNumbers; }
        }
    }
}
