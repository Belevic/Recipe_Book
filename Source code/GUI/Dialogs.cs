using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecipeBook.Domain;
using System.Windows.Forms;
using RecipeBook.Data;

namespace RecipeBook.Gui
{
    public static class Dialogs
    {
        public static bool SaveWithUniqueName<T>(T bookObject, bool allowSetNewName) where T : BookObject
        {
            var sameNameObject = RecipeBooks.Current.GetAll<T>().FirstOrDefault(o => string.Compare(o.Name, bookObject.Name, ignoreCase: true) == 0 && o.Id != bookObject.Id);
            if (sameNameObject != null)
            {
                if (!allowSetNewName)
                {
                    MessageBox.Show("Item with such name is already exists!");
                    return false;
                }
                else
                {
                    RecipeBooks.Current.Remove(sameNameObject);
                }
            }

            RecipeBooks.Current.Add(bookObject);
            return true;
        }

        public static T Edit<C, T>(T bookObject = null, bool allowSetNewName = false)where C : EditorControl<C, T>, new()where T : BookObject, new()
        {
            var caption = string.Format((bookObject != null ? "Create New" : "Edit") + " {0}", typeof(T).GuiInfo().DisplayText.Capitalize());
            T ret = null;
            using (var form = new Form { Text = caption, AutoSize = true })
            using (var editor = new C())
            {
                editor.SetBookObject(bookObject);

                editor.SaveButtonClicked += ie =>
                {
                    IList<string> errorMessages;
                    if (editor.TryGetBookObject(out ret, out errorMessages))
                    {
                        if (SaveWithUniqueName(ret, allowSetNewName: allowSetNewName))
                        {
                            form.Close();
                        }
                    }
                    else
                    {
                        Utils.Alert(string.Join(Environment.NewLine, errorMessages.ToArray()));
                    }
                };

                editor.CancelButtonClicked += c => form.Close();

                form.Controls.Add(editor);
                form.ShowDialog();
            }

            return ret;
        }
    }
}
