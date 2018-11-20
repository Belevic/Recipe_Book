using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecipeBook.Data;
using RecipeBook.Domain;
using System.IO;

namespace RecipeBook.Gui
{
    public partial class Tables : UserControl
    {
        
        public Tables()
        {
            InitializeComponent();

            this.InitializeRecipesTab();
            this.InitializeItemsTab();
        }

        private void InitializeRecipesTab()
        {
            this.deleteRecipeColumn.CellClicked((g, e) =>
            {
                var recipeId = (Guid)g.Rows[e.RowIndex].Cells[this.recipeIdColumn.Index].Value;
                var recipe = RecipeBooks.Current.RecipesList.SingleOrDefault(r => r.Id == recipeId);
                if (recipe == null)
                {
                    MessageBox.Show("Recipe was already deleted!");
                }
                else if (Errors.IsUserSure(string.Format("Delete recipe \"{0}\"?", g.Rows[e.RowIndex].Cells[this.recipeNameColumn.Index].Value)))
                {
                    RecipeBooks.Current.Remove(recipe);
                    g.Rows.RemoveAt(e.RowIndex);
                    this.itemEditor.ShowSaveAsEditWhenReadOnly = true;
                }
            });

            TypedEventHandler<DataGridView, DataGridViewCellEventArgs> displayRecipe = (g, e) =>
            {
                if (this.recipeEditor.ReadOnly)
                {
                    var recipeId = (Guid)g.Rows[e.RowIndex].Cells[this.recipeIdColumn.Index].Value;
                    var recipe = RecipeBooks.Current.RecipesList.Single(r => r.Id == recipeId);
                    this.recipeEditor.SetRecipe(recipe);
                }
            };
            this.recipeGrid.SelectedRowChanged(displayRecipe);

            this.createNewRecipeButton.Click += (o, e) =>
            {
                var newRecipe = Dialogs.Edit<EditRecipe, Recipe>(allowSetNewName: false);
                if (newRecipe != null)
                {
                    this.recipeGrid.Rows.Add(this.CreateRow(newRecipe));
                }
            };

            this.recipeEditor.SaveButtonClicked += re =>
            {
                Recipe recipe;
                IList<string> errorMessages;
                if (re.TryGetRecipe(out recipe, out errorMessages))
                {
                    if (Dialogs.SaveWithUniqueName(recipe, allowSetNewName: false))
                    {
                        re.ReadOnly = true;
                    }
                }
                else
                {
                    Errors.Alert(string.Join(Environment.NewLine, errorMessages.ToArray()));
                }
            };

            TextBoxForSearchRecipe.TextChanged += (o, e) =>
            {
                var text = TextBoxForSearchRecipe.Text.ToLower();

                var matches = (from r in RecipeBooks.Current.RecipesList
                               where r.Name.ToLower().Contains(text)
                                   || r.Source.ToLower().Contains(text)
                                   || r.Steps.ToLower().Contains(text)
                                   || r.Notes.ToLower().Contains(text)
                                   || r.Ingredients.Any(i => r.RecipeBook.Get<Ingredient>(i.ItemId).Name.ToLower().Contains(text))
                               select r.Id)
                               .ToSet();

                this.recipeGrid.Rows
                    .Cast<DataGridViewRow>()
                    .ForEach(r => r.Visible = matches.Contains((Guid)r.Cells[this.recipeIdColumn.Index].Value));
            };

            Action refreshRecipes = () =>
            {
                if (RecipeBooks.Current != null)
                {
                    this.recipeGrid.Rows.Clear();
                    this.recipeGrid.Rows.AddRange(RecipeBooks.Current.RecipesList
                        .OrderBy(r => r.Name)
                        .Select(this.CreateRow)
                        .ToArray());
                    if (this.recipeGrid.Rows.Count > 0)
                    {
                        displayRecipe(this.recipeGrid, new DataGridViewCellEventArgs(0, 0));
                    }
                }
            };

            TablesController.Selected += (o, e) =>
            {
                if (e.TabPage == this.recipesTable)
                { 
                    refreshRecipes();
                }
            };

            refreshRecipes();
        }

        private void InitializeItemsTab()
        {
            IngredientDeleteColumn.CellClicked((g, e) =>
            {
                var itemId = (Guid)g.Rows[e.RowIndex].Cells[IngredientIdColumn.Index].Value;
                var item = RecipeBooks.Current.IngredientsList.SingleOrDefault(r => r.Id == itemId);
                if (item == null)
                {
                    MessageBox.Show("Recipe was already deleted!");
                    return;
                }

                var usages = RecipeBooks.Current.RecipesList
                    .Where(r => r.Ingredients.Any(i => i.ItemId == itemId))
                    .ToArray();
                if (usages.Length > 0)
                {
                    Errors.Alert(string.Format("Cannot delete {0} since it is currently part of {1} recipe(s) ({2})",
                        item.Name, usages.Length, string.Join(", ", usages.Select(r => r.Name))));
                }
                else if (Errors.IsUserSure(string.Format("Delete item \"{0}\"?", g.Rows[e.RowIndex].Cells[IngredientIdColumn.Index].Value)))
                {
                    RecipeBooks.Current.Remove(item);
                    g.Rows.RemoveAt(e.RowIndex);
                }
            });

            TypedEventHandler<DataGridView, DataGridViewCellEventArgs> displayItem = (g, e) =>
            {
                if (this.itemEditor.ReadOnly)
                {
                    var itemId = (Guid)g.Rows[e.RowIndex].Cells[IngredientIdColumn.Index].Value;
                    var item = RecipeBooks.Current.Get<Ingredient>(itemId);
                    this.itemEditor.SetItem(item);
                }
            };
            IngredientTableCreator.SelectedRowChanged(displayItem);

            createNewItem.Click += (o, e) =>
            {
                var newItem = Dialogs.Edit<EditItem, Ingredient>(allowSetNewName: false);
                if (newItem != null)
                {
                    MessageBox.Show("Error");
                   IngredientTableCreator.Rows.Add(this.CreateRow(newItem));
                }
                
            };

            this.itemEditor.SaveButtonClicked += ie =>
            {
                Ingredient item;
                IList<string> errorMessages;
                if (ie.TryGetItem(out item, out errorMessages))
                {
                    if (Dialogs.SaveWithUniqueName(item, allowSetNewName: false))
                    {
                        ie.ReadOnly = true;
                    }
                }
                else
                {
                    Errors.Alert(string.Join(Environment.NewLine, errorMessages.ToArray()));
                }
            };

            TextBoxForSearchItem.TextChanged += (o, e) =>
            {
                string text = TextBoxForSearchItem.Text.ToLower();

                var matches = RecipeBooks.Current.IngredientsList
                    .Where(i => i.Name.ToLower().Contains(text))
                    .Select(i => i.Id)
                    .ToSet();

                IngredientTableCreator.Rows
                    .Cast<DataGridViewRow>()
                    .ForEach(r => r.Visible = matches.Contains((Guid)r.Cells[IngredientIdColumn.Index].Value));
            };

            TablesController.Selected += (o, e) =>
            {
                if (e.TabPage == TableOfItems && RecipeBooks.Current != null)
                {
                    IngredientTableCreator.Rows.Clear();
                    IngredientTableCreator.Rows.AddRange(RecipeBooks.Current.IngredientsList.OrderBy(i => i.Name).Select(this.CreateRow).ToArray());
                    if (IngredientTableCreator.Rows.Count > 0)
                    {
                        displayItem(IngredientTableCreator, new DataGridViewCellEventArgs(0, 0));
                    }
                }
            };
        }
        

        private DataGridViewRow CreateRow(Recipe recipe)
        {
            var row = new DataGridViewRow();

            row.Cells.Add(new DataGridViewTextBoxCell { Value = recipe.Name });
            row.Cells.Add(new DataGridViewButtonCell { Value = "Delete" });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = recipe.Id });

            return row;
        }

        private DataGridViewRow CreateRow(Ingredient item)
        {
            var row = new DataGridViewRow();

            row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Name });
            row.Cells.Add(new DataGridViewButtonCell { Value = "Delete" });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Id });

            return row;
        }

        
    }
}
