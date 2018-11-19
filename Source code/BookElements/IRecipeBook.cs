using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeBook.BookElements
{
    public interface IRecipeBook
    {
        IEnumerable<Recipe> RecipesList { get; }
        IEnumerable<Ingredient> IngredientsList { get; }

        string SavePath { get; set; }
        
        T Get<T>(Guid id) where T : BookObject;
        IEnumerable<T> GetAll<T>() where T : BookObject;
        void Add<T>(T domainObject) where T : BookObject;
        void Remove(Guid id);        
        void Save();
    }

    public static class RecipeBookAction
    {
        public static void Remove(this IRecipeBook recipeBook, BookObject domainObject)
        {
            recipeBook.Remove(domainObject.Id);
        }
    }
}
