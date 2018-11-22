using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecipeBook.Domain;
using RecipeBook.Data;

namespace RecipeBook.Gui
{
    public partial class EditRecipe : EditorControl<EditRecipe, Recipe>
    {
        public override void SetBookObject(Recipe bookObject)
        {
            this.SetRecipe(recipe);
            
        }

        public override bool TryGetBookObject(out Recipe bookObject, out IList<string> errorMessages)
        {
            return this.TryGetRecipe(out bookObject, out errorMessages);
            
        }

        public EditRecipe()
        {

            Initialize();
            if (RecipeBooks.Current!= null)
            {
                ComboBoxOfItemName.Items.AddRange(RecipeBooks.Current.IngredientsList.Select(itemChoosed => itemChoosed.Name).OrderBy(index => index).ToArray());
                ComboBoxOfItemName.AddAutoComplete(o => o.ToString());
            }
            ComboBoxOfItemName.SelectedIndexChanged+= (o, e) =>
            {
                if (ComboBoxOfItemName.SelectedIndex >= 0)
                {
                    var tempItem = RecipeBooks.Current.IngredientsList.FirstOrDefault(choosed => string.Compare(choosed.Name, ComboBoxOfItemName.Text) == 0);
                    if (tempItem == null)
                    {
                        return;
                        
                    }
                }
            };
            ComboBoxOfAmount.Items.AddRange(AmountNumbers.DefaultNumbers.Cast<object>().ToArray());
            ComboBoxOfAmount.SelectedIndex = AmountNumbers.DefaultNumbers.IndexWhere(o => o.Equals(1));
            ComboBoxOfAmount.Click+= (o, e) =>
            {
                var NewItemName = ComboBoxOfItemName.Text;
                var item = RecipeBooks.Current.IngredientsList.FirstOrDefault(indexStr => string.Compare(indexStr.Name, NewItemName, ignoreCase: true) == 0);
                if (item== null)
                {
                    if (string.IsNullOrWhiteSpace(NewItemName))
                    {
                        MessageBox.Show("No item selected!");
                        
                    }
                    else
                    {
                        MessageBox.Show("There is no item named '{0}'!", NewItemName);
                    }
                    return;
                }
                else if (this.ingredientsTable.Rows.Cast<DataGridViewRow>().Any(stringToCompare => string.Compare(NewItemName, stringToCompare.Cells[this.itemColumn.Index].Value.ToString(), ignoreCase: true) == 0))
                {
                    MessageBox.Show("{0} is already in the recipe!", ComboBoxOfItemName.Text);
                    return;
                }
                double quantity;
                if (!double.TryParse(ComboBoxOfAmount.Text, out quantity) || quantity <= 0)
                {
                    MessageBox.Show("'{0}' is not a valid value for amount!", ComboBoxOfAmount.Text);
                    return;
                }
                this.AddIngredient(item, quantity);
            };

            RemoveButtonColumn.CellClicked((g, e) => g.Rows.RemoveAt(e.RowIndex));

            amountColumn.CellValidating((o, e) =>
            {
                double value;
                if (!double.TryParse(e.FormattedValue.ToString(), out value) || value <= 0)
                {
                    MessageBox.Show("'{0}' is not a valid value for quantity!", e.FormattedValue.ToString());
                    e.Cancel = true;
                }
            });

            createNewItem.Click += (o, e) =>
            {
                Ingredient newItem = Dialogs.Edit<ItemEditor, Ingredient>(allowSetNewName: false);
                if (newItem != null)
                {
                    ComboBoxOfItemName.Items.Add(newItem.Name);
                    ComboBoxOfItemName.SelectedIndex = ComboBoxOfItemName.Items.Count - 1;
                    ComboBoxOfItemName.SelectedIndex = ComboBoxOfItemName.Items.Cast<double>().IndexWhere(d => d == 1);
                }
            };

            this.save.Click += (o, e) =>
            {
                if (this.ShowSaveAsEditWhenReadOnly && this.ReadOnly)
                {
                    this.ReadOnly = false;
                }
                else
                {
                    this.RaiseSaveButtonClicked(this);
                }
            };

            this.adding.Click += (o, e) =>
            {
                var NewItemName = ComboBoxOfItemName.Text;
                var item = RecipeBooks.Current.IngredientsList.FirstOrDefault(i => string.Compare(i.Name, NewItemName, ignoreCase: true) == 0);
                if (item == null)
                {
                    if (string.IsNullOrWhiteSpace(NewItemName))
                    {
                        MessageBox.Show("No item selected!");

                    }
                    else
                    {
                        MessageBox.Show("There is no item named '{0}'!", NewItemName);
                    }

                    return;
                }
                else if (ingredientsTable.Rows.Cast<DataGridViewRow>().Any(indexCellString => string.Compare(NewItemName, indexCellString.Cells[this.itemColumn.Index].Value.ToString(), ignoreCase: true) == 0))
                {
                    MessageBox.Show("{0} is already in the recipe!", ComboBoxOfItemName.Text);
                    return;
                }

                double tempAmount;
                if (!double.TryParse(ComboBoxOfAmount.Text, out tempAmount) || tempAmount <= 0)
                {
                    MessageBox.Show("'{0}' is not a valid value of amount of product!", ComboBoxOfAmount.Text);
                    return;
                }
                AddIngredient(item, tempAmount);
            };

            this.cancel.Click += (o, e) =>
            {
                if (this.ShowSaveAsEditWhenReadOnly)
                {
                    this.ReadOnly = true;
                    this.SetRecipe(this.recipe);
                }
                else
                {
                    this.RaiseCancelButtonClicked(this);
                }
            };
        }
        private Recipe recipe;
        private bool readOnly = false, checkBoxMode = false, showSaveAsEditWhenReadOnly = false;

        public bool ShowSaveAsEditWhenReadOnly
        {
            get { return this.showSaveAsEditWhenReadOnly; }
            set
            {
                this.showSaveAsEditWhenReadOnly = value;
                this.ReadOnly = this.ReadOnly;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return this.readOnly;
            }
            set
            {
                if (this.ShowSaveAsEditWhenReadOnly)
                {
                    save.Text = value ? "Edit" : "Save";
                    save.Visible = this.recipe != null;
                }
                Control[] tempControlUnit = new Control[] { ComboBoxOfItemName, ComboBoxOfAmount, this.amount };
                Controls.OfType<Button>().Where(tempButton => !ShowSaveAsEditWhenReadOnly || recipe == null || tempButton != save).Concat(tempControlUnit).ForEach(c => c.Visible = !value);
                Controls.OfType<TextBox>().ForEach(tb => tb.ReadOnly = value);
                RemoveButtonColumn.Visible = !value;
                ingredientsTable.Columns.Cast<DataGridViewColumn>().Where(c => c != this.checkBoxColumn).ForEach(c => c.ReadOnly = value);
                readOnly = value;
            }
        }

        public bool CheckBoxMode
        {
            get
            {
                return checkBoxMode;
            }
            set
            {
                ReadOnly = value;
                checkBoxColumn.Visible = value;
                if (value)
                {
                    ShowSaveAsEditWhenReadOnly = false;
                    save.Visible = true;
                    save.Text = "Submit";
                }
                checkBoxMode = value;                
            }
        }
        
        public void SetRecipe(Recipe newRecipe)
        {
            ComboBoxOfAmount.SelectedIndex = AmountNumbers.DefaultNumbers.IndexWhere(o => o.Equals(1));
            this.ingredientsTable.Rows.Clear();
            if (newRecipe == null)
            {
                TextBoxForName.Text = TextBoxForSteps.Text = TextBoxForNotes.Text = "";
            }
            else
            {
                TextBoxForName.Text = newRecipe.Name;
                TextBoxForSteps.Text = newRecipe.Steps;
                TextBoxForNotes.Text = newRecipe.Notes;
                double amountOfItem = ComboBoxOfAmount.SelectedIndex;
                newRecipe.Ingredients.ForEach(i => this.AddIngredient(newRecipe.RecipeBook.Get<Ingredient>(i.ItemId),amountOfItem));
            }
            this.recipe = newRecipe;
            if (this.ShowSaveAsEditWhenReadOnly)
            {
                this.ReadOnly = true;
            }
        }

        public bool TryGetRecipe(out Recipe recipe, out IList<string> errorMessages)
        {
            errorMessages = new List<string>();

            var name = TextBoxForName.Text;
            if (string.IsNullOrEmpty(name))
            {
                errorMessages.Add("Please enter a name");
            }

            if (errorMessages.Count > 0)
            {
                recipe = null;
                return false;
            }

            var ingredients = this.GetIngredients(onlySelected: false);

            recipe = this.recipe == null ? (this.recipe = new Recipe { Id = Guid.NewGuid() }) : this.recipe;
            recipe.Name = name;
            recipe.Steps = TextBoxForSteps.Text;
            recipe.Notes = TextBoxForNotes.Text;
            recipe.Ingredients = ingredients;

            return true;
        }


        public List<Ingredient> GetIngredients(bool onlySelected = true)
        {
            var selectedIngredients = (from current in this.ingredientsTable.Rows.Cast<DataGridViewRow>() join index in RecipeBooks.Current.IngredientsList on current.Cells[this.itemColumn.Index].Value.ToString().ToLower()
            equals index.Name.ToLower() where !onlySelected || (bool)current.Cells[this.checkBoxColumn.Index].Value
            select new Ingredient
            {
               ItemId = index.Id
            })
            .ToList();
            return selectedIngredients;
        }

        private void AddIngredient(Ingredient newItem, double itemAmount)
        {
            var row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell { Value = newItem.Name });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = itemAmount });
            row.Cells.Add(new DataGridViewButtonCell { Value = "Remove" });
            this.ingredientsTable.Rows.Add(row);
        }

        
    }
}
