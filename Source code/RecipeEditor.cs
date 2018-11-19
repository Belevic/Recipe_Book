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
    public partial class RecipeEditor : EditorControl<RecipeEditor, Recipe>
    {
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
                    this.saveButton.Text = value ? "Edit" : "Save";
                    this.saveButton.Visible = this.recipe != null;
                }
                this.Controls.OfType<Button>().Where(b => !this.ShowSaveAsEditWhenReadOnly || this.recipe == null || b != this.saveButton).Concat(new Control[] { this.itemNameComboBox, this.quantityComboBox, this.quantityLabel }).ForEach(c => c.Visible = !value);
                this.Controls.OfType<TextBox>().ForEach(tb => tb.ReadOnly = value);
                this.RemoveButtonColumn.Visible = !value;
                this.ingredientsTable.Columns.Cast<DataGridViewColumn>().Where(c => c != this.checkBoxColumn).ForEach(c => c.ReadOnly = value);
                this.readOnly = value;
            }
        }

        public bool CheckBoxMode
        {
            get
            {
                return this.checkBoxMode;
            }
            set
            {
                this.ReadOnly = value;
                this.checkBoxColumn.Visible = value;
                if (value)
                {
                    this.ShowSaveAsEditWhenReadOnly = false;
                    this.saveButton.Visible = true;
                    this.saveButton.Text = "Submit";
                }

                this.checkBoxMode = value;                
            }
        }

        public RecipeEditor()
        {
            InitializeComponent();
            if (RecipeBooks.Current != null)
            {
                this.itemNameComboBox.Items.AddRange(RecipeBooks.Current.Items.Select(i => i.Name).OrderBy(n => n).ToArray());
                this.itemNameComboBox.AddAutoComplete(o => o.ToString());
            }
            this.itemNameComboBox.SelectedIndexChanged += (o, e) =>
            {
                if (this.itemNameComboBox.SelectedIndex >= 0)
                {
                    var item = RecipeBooks.Current.Items.FirstOrDefault(i => string.Compare(i.Name, this.itemNameComboBox.Text) == 0);
                    if (item == null)
                    {
                        return;
                    }
                }
            };
            this.quantityComboBox.Items.AddRange(Constants.DefaultQuantities.Cast<object>().ToArray());
            this.quantityComboBox.SelectedIndex = Constants.DefaultQuantities.IndexWhere(o => o.Equals(1));
            this.addButton.Click += (o, e) =>
            {
                var itemName = itemNameComboBox.Text;
                var item = RecipeBooks.Current.Items.FirstOrDefault(i => string.Compare(i.Name, itemName, ignoreCase: true) == 0);
                if (item == null)
                {
                    Utils.Alert(string.IsNullOrWhiteSpace(itemName) ? "No item selected!" : string.Format("There is no item named '{0}'!", itemName));
                    this.itemNameComboBox.SelectAndFocus();
                    return;
                }
                else if (this.ingredientsTable.Rows.Cast<DataGridViewRow>().Any(r => string.Compare(itemName, r.Cells[this.itemColumn.Index].Value.ToString(), ignoreCase: true) == 0))
                {
                    Utils.Alert(string.Format("{0} is already in the recipe!", this.itemNameComboBox.Text));
                    this.itemNameComboBox.SelectAndFocus();
                    return;
                }
                double quantity;
                if (!double.TryParse(this.quantityComboBox.Text, out quantity) || quantity <= 0)
                {
                    Utils.Alert(string.Format("'{0}' is not a valid value for quantity!", this.quantityComboBox.Text));
                    this.quantityComboBox.SelectAndFocus();
                    return;
                }
                this.AddIngredient(item, quantity);
                this.itemNameComboBox.SelectAndFocus();
            };

            this.RemoveButtonColumn.CellClicked((g, e) => g.Rows.RemoveAt(e.RowIndex));
            this.QuantityColumn.CellValidating((o, e) =>
            {
                double value;
                if (!double.TryParse(e.FormattedValue.ToString(), out value) || value <= 0)
                {
                    Utils.Alert(string.Format("'{0}' is not a valid value for quantity!", e.FormattedValue));
                    e.Cancel = true;
                }
            });

            this.createNewItemButton.Click += (o, e) =>
            {
                Item newItem = Dialogs.Edit<ItemEditor, Item>(allowOverwrite: false);
                if (newItem != null)
                {
                    this.itemNameComboBox.Items.Add(newItem.Name);
                    this.itemNameComboBox.SelectedIndex = this.itemNameComboBox.Items.Count - 1;
                    this.quantityComboBox.SelectedIndex = this.quantityComboBox.Items.Cast<double>().IndexWhere(d => d == 1);
                }
            };

            this.saveButton.Click += (o, e) =>
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

            this.cancelButton.Click += (o, e) =>
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

        public void SetRecipe(Recipe recipe)
        {
            this.quantityComboBox.SelectedIndex = Constants.DefaultQuantities.IndexWhere(o => o.Equals(1));
            this.ingredientsTable.Rows.Clear();

            if (recipe == null)
            {
                this.nameTextBox.Text = this.stepsTextBox.Text = this.notesTextBox.Text = string.Empty;
            }
            else
            {
                this.nameTextBox.Text = recipe.Name;
                this.stepsTextBox.Text = recipe.Steps;
                this.notesTextBox.Text = recipe.Notes;
                recipe.Ingredients.ForEach(i => this.AddIngredient(recipe.RecipeBook.Get<Item>(i.ItemId), i.Quantity));
            }

            this.recipe = recipe;

            if (this.ShowSaveAsEditWhenReadOnly)
            {
                this.ReadOnly = true;
            }
        }

        public bool TryGetRecipe(out Recipe recipe, out IList<string> errorMessages)
        {
            errorMessages = new List<string>();

            var name = this.nameTextBox.Text;
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
            recipe.Steps = this.stepsTextBox.Text;
            recipe.Notes = this.notesTextBox.Text;
            recipe.Ingredients = ingredients;

            return true;
        }

        public List<Ingredient> GetIngredients(bool onlySelected = true)
        {
            var selectedIngredients = (from r in this.ingredientsTable.Rows.Cast<DataGridViewRow>()
                                      join i in RecipeBooks.Current.Items on
                                        r.Cells[this.itemColumn.Index].Value.ToString().ToLower()
                                        equals
                                        i.Name.ToLower()
                                      where !onlySelected || (bool)r.Cells[this.checkBoxColumn.Index].Value
                                      select new Ingredient
                                      {
                                          ItemId = i.Id,
                                          Quantity = double.Parse(r.Cells[this.QuantityColumn.Index].Value.ToString())
                                      })
                                      .ToList();

            return selectedIngredients;
        }

        private void AddIngredient(Item item, double quantity)
        {
            var row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Name });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = quantity });
            row.Cells.Add(new DataGridViewButtonCell { Value = "Remove" });
            row.Cells.Add(new DataGridViewCheckBoxCell { FalseValue = false, TrueValue = true, Value = !item.AssumeIsInStock });
            this.ingredientsTable.Rows.Add(row);
        }

        public override void SetDomainObject(Recipe domainObject)
        {
            this.SetRecipe(recipe);
        }

        public override bool TryGetDomainObject(out Recipe domainObject, out IList<string> errorMessages)
        {
            return this.TryGetRecipe(out domainObject, out errorMessages);
        }
    }
}
