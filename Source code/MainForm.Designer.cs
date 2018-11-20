namespace RecipeBook
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;


        private Gui.EditItem itemContainer;
        private Gui.EditRecipe recipeContainer;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            itemContainer = new RecipeBook.Gui.EditItem();
            recipeContainer = new RecipeBook.Gui.EditRecipe();
            SuspendLayout();
            itemContainer.Location = new System.Drawing.Point(13, 13);
            itemContainer.Name = "itemContainer";
            itemContainer.ReadOnly = false;
            itemContainer.Size = new System.Drawing.Size(306, 225);
            itemContainer.TabIndex = 0; 
            recipeContainer.Location = new System.Drawing.Point(326, 13);
            recipeContainer.Name = "recipeContainer";
            recipeContainer.Size = new System.Drawing.Size(814, 509);
            recipeContainer.TabIndex = 1;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1217, 574);
            Controls.Add(this.recipeContainer);
            Controls.Add(this.itemContainer);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
       
    }
}

