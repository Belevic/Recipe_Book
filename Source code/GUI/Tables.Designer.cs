namespace RecipeBook.Gui
{
    partial class Tables
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl TablesController;
        private System.Windows.Forms.TabPage recipesTable;
        private System.Windows.Forms.TabPage TableOfItems;
        private System.Windows.Forms.DataGridView recipeGrid;
        private EditRecipe recipeEditor;
        private System.Windows.Forms.Label recipeSearch;
        private System.Windows.Forms.Button createNewRecipeButton;
        private System.Windows.Forms.TextBox TextBoxForSearchRecipe;
        private System.Windows.Forms.DataGridViewTextBoxColumn recipeNameColumn;
        private System.Windows.Forms.DataGridViewButtonColumn deleteRecipeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn recipeIdColumn;
        private System.Windows.Forms.Button createNewItem;
        private System.Windows.Forms.TextBox TextBoxForSearchItem;
        private System.Windows.Forms.Label IngredientSearch;
        private System.Windows.Forms.DataGridView IngredientTableCreator;
        private ItemEditor itemEditor;
        private System.Windows.Forms.DataGridViewTextBoxColumn IngredientNameColumn;
        private System.Windows.Forms.DataGridViewButtonColumn IngredientDeleteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IngredientIdColumn;
        
        private void InitializeAllComponents()
        {
            TablesController = new System.Windows.Forms.TabControl();
            recipesTable = new System.Windows.Forms.TabPage();
            createNewRecipeButton = new System.Windows.Forms.Button();
            TextBoxForSearchRecipe = new System.Windows.Forms.TextBox();
            recipeSearch = new System.Windows.Forms.Label();
            recipeGrid = new System.Windows.Forms.DataGridView();
            recipeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            deleteRecipeColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            recipeIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            recipeEditor = new RecipeBook.Gui.EditRecipe();
            TableOfItems = new System.Windows.Forms.TabPage();
            itemEditor = new RecipeBook.Gui.ItemEditor();
            createNewItem = new System.Windows.Forms.Button();
            TextBoxForSearchItem = new System.Windows.Forms.TextBox();
            IngredientSearch = new System.Windows.Forms.Label();
            IngredientTableCreator = new System.Windows.Forms.DataGridView();
            IngredientNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            IngredientDeleteColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            IngredientIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            InitializeAllComponents();

            TablesController.SuspendLayout();
            recipesTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(recipeGrid)).BeginInit();
            TableOfItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(IngredientTableCreator)).BeginInit();
            SuspendLayout();

            TablesController.Controls.Add(this.recipesTable);
            TablesController.Controls.Add(TableOfItems);
            this.TablesController.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TablesController.Location = new System.Drawing.Point(0, 0);
            this.TablesController.Name = "BookControl";
            this.TablesController.SelectedIndex = 0;
            this.TablesController.Size = new System.Drawing.Size(1301, 548);
            this.TablesController.TabIndex = 0;

            recipesTable.Controls.Add(createNewRecipeButton);
            recipesTable.Controls.Add(TextBoxForSearchRecipe);
            recipesTable.Controls.Add(recipeSearch);
            recipesTable.Controls.Add(recipeGrid);
            recipesTable.Controls.Add(recipeEditor);
            recipesTable.Location = new System.Drawing.Point(4, 22);
            recipesTable.Name = "recipesTab";
            recipesTable.Padding = new System.Windows.Forms.Padding(3);
            recipesTable.Size = new System.Drawing.Size(1293, 522);
            recipesTable.TabIndex = 0;
            recipesTable.Text = "Recipes";
            recipesTable.UseVisualStyleBackColor = true;
                    
            this.createNewRecipeButton.Location = new System.Drawing.Point(180, 4);
            this.createNewRecipeButton.Name = "createNewRecipeButton";
            this.createNewRecipeButton.Size = new System.Drawing.Size(111, 23);
            this.createNewRecipeButton.TabIndex = 4;
            this.createNewRecipeButton.Text = "Create New Recipe";
            this.createNewRecipeButton.UseVisualStyleBackColor = true;
            
            this.TextBoxForSearchRecipe.Location = new System.Drawing.Point(45, 4);
            this.TextBoxForSearchRecipe.Name = "TextBoxForSearchRecipe";
            this.TextBoxForSearchRecipe.Size = new System.Drawing.Size(128, 20);
            this.TextBoxForSearchRecipe.TabIndex = 3;
            
            recipeSearch.AutoSize = true;
            recipeSearch.Location = new System.Drawing.Point(4, 4);
            recipeSearch.Name = "recipeSearch";
            recipeSearch.Size = new System.Drawing.Size(25, 13);
            recipeSearch.TabIndex = 2;
            recipeSearch.Text = "Search";

            System.Windows.Forms.DataGridViewColumn[] dataArray = new System.Windows.Forms.DataGridViewColumn[] { recipeNameColumn, deleteRecipeColumn, recipeIdColumn };
            recipeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            recipeGrid.Columns.AddRange(dataArray);
            recipeGrid.Location = new System.Drawing.Point(4, 40);
            recipeGrid.Name = "RecipeTableForExisting";
            recipeGrid.Size = new System.Drawing.Size(293, 380);
            recipeGrid.AllowUserToAddRows = false;
            recipeGrid.AllowUserToDeleteRows = false;
            recipeGrid.TabIndex = 1;
                       
            recipeNameColumn.HeaderText = "Recipe";
            recipeNameColumn.Name = "recipeNameColumn";
            recipeNameColumn.ReadOnly = true;
            recipeNameColumn.Width = 200;
            
            deleteRecipeColumn.HeaderText = "";
            deleteRecipeColumn.Name = "deleteRecipeColumn";
            deleteRecipeColumn.Width = 50;
            
            recipeIdColumn.HeaderText = "";
            recipeIdColumn.Name = "recipeIdColumn";
            recipeIdColumn.ReadOnly = true;
            recipeIdColumn.Visible = false;
            
            recipeEditor.CheckBoxMode = false;
            recipeEditor.Location = new System.Drawing.Point(320, 7);
            recipeEditor.Name = "EditRecipe";
            recipeEditor.ReadOnly = true;
            recipeEditor.ShowSaveAsEditWhenReadOnly = true;
            recipeEditor.Size = new System.Drawing.Size(814, 509);
            recipeEditor.TabIndex = 0;
            
            TableOfItems.Controls.Add(this.itemEditor);
            TableOfItems.Controls.Add(createNewItem);
            TableOfItems.Controls.Add(TextBoxForSearchItem);
            TableOfItems.Controls.Add(IngredientSearch);
            TableOfItems.Controls.Add(IngredientTableCreator);
            TableOfItems.Location = new System.Drawing.Point(4, 22);
            TableOfItems.Name = "TableOfItems";
            TableOfItems.Padding = new System.Windows.Forms.Padding(3);
            TableOfItems.Size = new System.Drawing.Size(1293, 522);
            TableOfItems.TabIndex = 1;
            TableOfItems.Text = "Items";
            TableOfItems.UseVisualStyleBackColor = true;
            
            itemEditor.Location = new System.Drawing.Point(400, 133);
            itemEditor.Name = "itemEditor";
            itemEditor.ReadOnly = true;
            itemEditor.ShowSaveAsEditWhenReadOnly = true;
            itemEditor.Size = new System.Drawing.Size(301, 218);
            itemEditor.TabIndex = 9;
            
            createNewItem.Location = new System.Drawing.Point(180, 4);
            createNewItem.Name = "createNewItem";
            createNewItem.Size = new System.Drawing.Size(111, 23);
            createNewItem.TabIndex = 8;
            createNewItem.Text = "Create New Item";
            createNewItem.UseVisualStyleBackColor = true;
            
            TextBoxForSearchItem.Location = new System.Drawing.Point(45, 5);
            TextBoxForSearchItem.Name = "TextBoxForSearchItem";
            TextBoxForSearchItem.Size = new System.Drawing.Size(128, 20);
            TextBoxForSearchItem.TabIndex = 7;
            
            IngredientSearch.AutoSize = true;
            IngredientSearch.Location = new System.Drawing.Point(4, 5);
            IngredientSearch.Name = "IngredientSearch";
            IngredientSearch.Size = new System.Drawing.Size(41, 13);
            IngredientSearch.TabIndex = 6;
            IngredientSearch.Text = "Search";

            System.Windows.Forms.DataGridViewColumn[] dataArrayItem = new System.Windows.Forms.DataGridViewColumn[] { IngredientNameColumn, IngredientDeleteColumn, IngredientIdColumn };
            IngredientTableCreator.AllowUserToAddRows = false;
            IngredientTableCreator.AllowUserToDeleteRows = false;
            IngredientTableCreator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            IngredientTableCreator.Columns.AddRange(dataArrayItem);
            IngredientTableCreator.Location = new System.Drawing.Point(6, 41);
            IngredientTableCreator.Name = "IngredientTableCreator";
            IngredientTableCreator.Size = new System.Drawing.Size(293, 380);
            IngredientTableCreator.TabIndex = 5;
            
            IngredientNameColumn.HeaderText = "Item";
            IngredientNameColumn.Name = "IngredientNameColumn";
            IngredientNameColumn.ReadOnly = true;
            IngredientNameColumn.Width = 200;
            
            IngredientDeleteColumn.HeaderText = "";
            IngredientDeleteColumn.Name = "IngredientDeleteColumn";
            IngredientDeleteColumn.Width = 50;

            IngredientIdColumn.HeaderText = "";
            IngredientIdColumn.Name = "IngredientIdColumn";
            IngredientIdColumn.ReadOnly = true;
            IngredientIdColumn.Visible = false;

            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(this.TablesController);
            Name = "Tables";
            Size = new System.Drawing.Size(1000, 480);
            TablesController.ResumeLayout(false);
            recipesTable.ResumeLayout(false);
            recipesTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recipeGrid)).EndInit();
            TableOfItems.ResumeLayout(false);
            TableOfItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(IngredientTableCreator)).EndInit();
            ResumeLayout(false);

        }

        #endregion

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
