using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecipeBook.Domain;
using RecipeBook.Gui;

namespace RecipeBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            var tabs = new Tables { Dock = DockStyle.Fill };
            this.Controls.Add(tabs);
            tabs.BringToFront();
            this.Width += 200;
            this.Height += 200;
        }
    }
}
