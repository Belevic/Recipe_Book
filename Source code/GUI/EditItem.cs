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
    public partial class EditItem : EditorControl<EditItem, Ingredient>
    {
        private Ingredient item;
        private bool readOnly, showSaveAsEditWhenReadOnly = false;

        public override void SetBookObject(Ingredient bookObject)
        {
            this.SetItem(bookObject);
        }

        public override bool TryGetBookObject(out Ingredient bookObject, out IList<string> errorMessages)
        {
            return this.TryGetItem(out bookObject, out errorMessages);

        }

        //method for creating new item
        public void SetItem(Ingredient creatingItem)
        {
            if (creatingItem == null)
            {
                ItemNameTextBox.Text = string.Empty;
                ItemCategoryDropDown.SelectedIndex = -1;
                
            }
            else
            {
                ItemNameTextBox.Text = creatingItem.Name;
                ItemCategoryDropDown.SelectedIndex = Enum.GetValues(typeof(Category)).Cast<Category>().IndexWhere(c => c == creatingItem.Category);
            }
            this.item = creatingItem;
            if (this.ShowSaveAsEditWhenReadOnly)
            {
                this.ReadOnly=true;
                
            }
        }

        public bool TryGetItem(out Ingredient item, out IList<string> errorMessages)
        {
            errorMessages = new List<string>();
            var name = this.ItemNameTextBox.Text;
            if (string.IsNullOrEmpty(name))
            {
                errorMessages.Add("Please enter valid item name!");
                
            }
            var category = this.ItemCategoryDropDown.SelectedIndex < 0 ? null
                : (Category?)((DisplayPointer<Category>)this.ItemCategoryDropDown.SelectedItem).Item;
            if (category == null)
            {
                errorMessages.Add("Please select category from list!");
            }
            if (errorMessages.Count > 0)
            {
                item = null;
                return false;
            }
            item= this.item==null ? (this.item = new Ingredient() { Id = Guid.NewGuid() }) : this.item;
            item.Name = name;
            item.Category= category.Value;
            return true;
        }

        //ItemEditor class constructor
        public EditItem()
        {
            InitializeComponent();
            var categoryList = Enum.GetValues(typeof(Category)).Cast<Category>().ToArray();
            this.ItemCategoryDropDown.Items.AddRange(Enum.GetValues(typeof(Category)).Cast<Category>().Select(c => c.DisplayPointer(c.GuiInfo().DisplayText)).ToArray());
            this.saveButton.Click += (o, e) =>
            {
                if (this.ShowSaveAsEditWhenReadOnly && this.ReadOnly)
                {

                    this.SetItem(this.item);
                    this.ReadOnly = false;
                }
                else
                {
                    this.RaiseSaveButtonClicked(this);
                    
                }
            };
            this.cancelButton.Click+= (o, e) =>
            {
                if (this.ShowSaveAsEditWhenReadOnly)
                {
                    this.ReadOnly =true;
                    this.SetItem(this.item);
                }
                else
                {
                    this.RaiseCancelButtonClicked(this);
                }
            };
        }

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
            get { return this.readOnly; }
            set 
            {
                if (this.ShowSaveAsEditWhenReadOnly)
                {
                    this.saveButton.Text = value ? "Edit" : "Save";
                    this.saveButton.Visible = this.cancelButton.Visible = this.item != null;
                }
                else
                {
                    this.saveButton.Visible = this.cancelButton.Visible = !value;
                }
                
                this.Controls
                    .Cast<Control>()
                    .Where(c => !(c is Button))
                    .ForEach(c => c.SetReadOnly(value));

                this.readOnly = value;
            }
        }
    }
}
