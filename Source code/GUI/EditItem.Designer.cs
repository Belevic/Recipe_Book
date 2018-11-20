namespace RecipeBook.Gui
{
    partial class EditItem
    {
       
        private System.ComponentModel.IContainer components = null;
        
        private System.Windows.Forms.Label ItemNameLabel;
        private System.Windows.Forms.TextBox ItemNameTextBox;
        private System.Windows.Forms.Label ItemCategoryLabel;
        private System.Windows.Forms.ComboBox ItemCategoryDropDown;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        
        private void InitializeComponent()
        {
            this.ItemNameLabel = new System.Windows.Forms.Label();
            this.ItemNameTextBox = new System.Windows.Forms.TextBox();
            this.ItemCategoryLabel = new System.Windows.Forms.Label();
            this.ItemCategoryDropDown = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            this.ItemNameLabel.AutoSize = true;
            this.ItemNameLabel.Location = new System.Drawing.Point(3, 7);
            this.ItemNameLabel.Name = "nameLabel";
            this.ItemNameLabel.Size = new System.Drawing.Size(35, 13);
            this.ItemNameLabel.TabIndex = 0;
            this.ItemNameLabel.Text = "Name";
            
            this.ItemNameTextBox.Location = new System.Drawing.Point(59, 7);
            this.ItemNameTextBox.Name = "nameTextBox";
            this.ItemNameTextBox.Size = new System.Drawing.Size(232, 20);
            this.ItemNameTextBox.TabIndex = 1;
            
            this.ItemCategoryLabel.AutoSize = true;
            this.ItemCategoryLabel.Location = new System.Drawing.Point(3, 36);
            this.ItemCategoryLabel.Name = "categoryLabel";
            this.ItemCategoryLabel.Size = new System.Drawing.Size(49, 13);
            this.ItemCategoryLabel.TabIndex = 2;
            this.ItemCategoryLabel.Text = "Category";
            
            this.ItemCategoryDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ItemCategoryDropDown.FormattingEnabled = true;
            this.ItemCategoryDropDown.Location = new System.Drawing.Point(59, 36);
            this.ItemCategoryDropDown.Name = "categoryDropDown";
            this.ItemCategoryDropDown.Size = new System.Drawing.Size(232, 21);
            this.ItemCategoryDropDown.TabIndex = 3;
            
            this.saveButton.Location = new System.Drawing.Point(215, 100);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            
            this.cancelButton.Location = new System.Drawing.Point(134, 100);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.ItemCategoryDropDown);
            this.Controls.Add(this.ItemCategoryLabel);
            this.Controls.Add(this.ItemNameTextBox);
            this.Controls.Add(this.ItemNameLabel);
            this.Name = "ItemEditor";
            this.Size = new System.Drawing.Size(301, 110);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
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
