using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RecipeBook.Data;
using RecipeBook.Gui;
using System.IO;

namespace RecipeBook
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RecipeBooks.SetCurrent();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                using (var content = new Tables())
                using (var form = new Form { Text = "Recipe Book", AutoSize = true })
                {
                    form.Controls.Add(content);
                    Application.ThreadException += (o, e) =>
                    {
                        Errors.Alert("An unhandled error has occurred in the application!");
                        Application.ExitThread();
                    };
                    Application.Run(form);
                }
            }
            finally
            {
                Errors.TryInvoke(RecipeBooks.Current.Save);
            }
        }
    }
}
