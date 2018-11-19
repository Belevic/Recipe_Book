namespace RecipeBook.Gui
{
    
    partial class EditRecipe
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView ingredientsTable;

        //boxes
        private System.Windows.Forms.ComboBox ComboBoxOfItemName;
        private System.Windows.Forms.ComboBox ComboBoxOfAmount;

        //labels
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label ingredients;
        private System.Windows.Forms.Label amount;
        private System.Windows.Forms.Label steps;
        private System.Windows.Forms.Label notes;

        //buttons
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button createNewItem;
        private System.Windows.Forms.Button adding;

        //text boxes for recipe info
        private System.Windows.Forms.TextBox TextBoxForName;
        private System.Windows.Forms.TextBox TextBoxForNotes;
        private System.Windows.Forms.TextBox TextBoxForSteps;

        //table columns
        private System.Windows.Forms.DataGridViewTextBoxColumn itemColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountColumn;
        private System.Windows.Forms.DataGridViewButtonColumn RemoveButtonColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkBoxColumn;
        
        private void InitializeAllComponents()
        {
            ComboBoxOfItemName = new System.Windows.Forms.ComboBox();
            ingredients = new System.Windows.Forms.Label();
            amount = new System.Windows.Forms.Label();
            ComboBoxOfAmount = new System.Windows.Forms.ComboBox();
            adding = new System.Windows.Forms.Button();
            ingredientsTable = new System.Windows.Forms.DataGridView();
            itemColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            amountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            RemoveButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            createNewItem = new System.Windows.Forms.Button();
            steps = new System.Windows.Forms.Label();
            notes = new System.Windows.Forms.Label();
            save = new System.Windows.Forms.Button();
            cancel = new System.Windows.Forms.Button();
            TextBoxForNotes = new System.Windows.Forms.TextBox();
            TextBoxForSteps = new System.Windows.Forms.TextBox();
            checkBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            name = new System.Windows.Forms.Label();
            TextBoxForName = new System.Windows.Forms.TextBox();
            ComboBoxOfItemName = new System.Windows.Forms.ComboBox();
        }

        private void Initialize()
        {
            InitializeAllComponents();
            ((System.ComponentModel.ISupportInitialize)(this.ingredientsTable)).BeginInit();
            SuspendLayout();

            name.AutoSize = true;
            name.Location = new System.Drawing.Point(4,4);
            name.Name = "nameLabel";
            name.Size = new System.Drawing.Size(35, 13);
            name.TabIndex = 0;
            name.Text = "Name";

            TextBoxForName.Location = new System.Drawing.Point(40, 4);
            TextBoxForName.Name = "nameTextBox";
            TextBoxForName.Size = new System.Drawing.Size(231, 20);
            TextBoxForName.TabIndex = 1;

            ingredients.AutoSize = true;
            ingredients.Location = new System.Drawing.Point(4, 35);
            ingredients.Name = "ingredientsLabel";
            ingredients.Size = new System.Drawing.Size(59, 13);
            ingredients.TabIndex = 5;
            ingredients.Text = "Ingredients";

            ComboBoxOfItemName.FormattingEnabled = true;
            ComboBoxOfItemName.Location = new System.Drawing.Point(4, 50);
            ComboBoxOfItemName.Name = "itemNameComboBox";
            ComboBoxOfItemName.Size = new System.Drawing.Size(158, 21);
            ComboBoxOfItemName.TabIndex = 4;
            
            amount.AutoSize = true;
            amount.Location = new System.Drawing.Point(172, 35);
            amount.Name = "quantityLabel";
            amount.Size = new System.Drawing.Size(46, 13);
            amount.TabIndex = 6;
            amount.Text = "Amount";
            
            ComboBoxOfAmount.FormattingEnabled = true;
            ComboBoxOfAmount.Location = new System.Drawing.Point(172, 50);
            ComboBoxOfAmount.Name = "quantityComboBox";
            ComboBoxOfAmount.Size = new System.Drawing.Size(57, 21);
            ComboBoxOfAmount.TabIndex = 7;
            
            adding.Location = new System.Drawing.Point(250, 50);
            adding.Name = "addButton";
            adding.Size = new System.Drawing.Size(76, 23);
            adding.TabIndex = 8;
            adding.Text = "Add";
            adding.UseVisualStyleBackColor = true;

            System.Windows.Forms.DataGridViewColumn[] dataArray = new System.Windows.Forms.DataGridViewColumn[] { itemColumn, amountColumn, RemoveButtonColumn, checkBoxColumn };
            ingredientsTable.AllowUserToAddRows = false;
            ingredientsTable.AllowUserToDeleteRows = false;
            ingredientsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ingredientsTable.Columns.AddRange(dataArray);
            ingredientsTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            ingredientsTable.Location = new System.Drawing.Point(4, 80);
            ingredientsTable.Name = "ingredientsGrid";
            ingredientsTable.Size = new System.Drawing.Size(340, 300);
            ingredientsTable.TabIndex = 9;
            
            itemColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            itemColumn.HeaderText = "Item";
            itemColumn.Name = "itemColumn";
            itemColumn.ReadOnly = true;
            
            amountColumn.HeaderText = "Amount";
            amountColumn.Name = "AmountColumn";
            amountColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            amountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            amountColumn.Width = 70;
            
            RemoveButtonColumn.HeaderText = "";
            RemoveButtonColumn.Name = "RemoveButtonColumn";
            RemoveButtonColumn.Width = 84;
            
            createNewItem.Location = new System.Drawing.Point(240, 390);
            createNewItem.Name = "createNewItemButton";
            createNewItem.Size = new System.Drawing.Size(102, 23);
            createNewItem.TabIndex = 10;
            createNewItem.Text = "Create new item";
            createNewItem.UseVisualStyleBackColor = true;
            
            steps.AutoSize = true;
            steps.Location = new System.Drawing.Point(370, 4);
            steps.Name = "stepsLabel";
            steps.Size = new System.Drawing.Size(34, 13);
            steps.TabIndex = 11;
            steps.Text = "Steps";
            
            notes.AutoSize = true;
            notes.Location = new System.Drawing.Point(370, 260);
            notes.Name = "notesLabel";
            notes.Size = new System.Drawing.Size(35, 13);
            notes.TabIndex = 12;
            notes.Text = "Notes";
            Controls.Add(this.notes);

            save.Location = new System.Drawing.Point(560, 390);
            save.Name = "saveButton";
            save.Size = new System.Drawing.Size(102, 23);
            save.TabIndex = 13;
            save.Text = "Save";
            save.UseVisualStyleBackColor = true;
            Controls.Add(this.save);

            cancel.Location = new System.Drawing.Point(450, 390);
            cancel.Name = "cancelButton";
            cancel.Size = new System.Drawing.Size(102, 23);
            cancel.TabIndex = 14;
            cancel.Text = "Cancel";
            cancel.UseVisualStyleBackColor = true;
            Controls.Add(this.cancel);

            TextBoxForNotes.Location = new System.Drawing.Point(410, 260);
            TextBoxForNotes.Multiline = true;
            TextBoxForNotes.Name = "notesTextBox";
            TextBoxForNotes.Size = new System.Drawing.Size(250, 120);
            TextBoxForNotes.TabIndex = 15;
            Controls.Add(TextBoxForNotes);

            TextBoxForSteps.Location = new System.Drawing.Point(410, 4);
            TextBoxForSteps.Multiline = true;
            TextBoxForSteps.Name = "TextBoxForSteps";
            TextBoxForSteps.Size = new System.Drawing.Size(250, 250);
            TextBoxForSteps.TabIndex = 16;
            Controls.Add(TextBoxForSteps);

            this.checkBoxColumn.HeaderText = "Add to List";
            this.checkBoxColumn.Name = "checkBoxColumn";
            this.checkBoxColumn.Visible = false;
            
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            
            Controls.Add(this.steps);
            Controls.Add(this.createNewItem);
            Controls.Add(this.ingredientsTable);
            Controls.Add(this.adding);
            Controls.Add(this.ComboBoxOfAmount);
            Controls.Add(this.amount);
            Controls.Add(this.ingredients);
            Controls.Add(this.ComboBoxOfItemName);
            Controls.Add(this.TextBoxForName);
            Controls.Add(this.name);
            Name = "EditRecipe";
            Size = new System.Drawing.Size(664, 414);
            ((System.ComponentModel.ISupportInitialize)(this.ingredientsTable)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        //disposing container of elements
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
