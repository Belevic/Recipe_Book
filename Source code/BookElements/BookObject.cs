using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RecipeBook.Domain
{
    public abstract class BookObject
    { 
        public string Name { get; set; }
        [XmlIgnore]
        public IRecipeBook RecipeBook { get; set; }
        private Guid id = Guid.Empty;

        public override bool Equals(object obj)
        {
            var that = obj as BookObject;
            if (this.Id == that.Id)
            {
                return that != null;

            }
            return that == null;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public Guid Id
        {
            get { return this.id; }
            set
            {
                if (this.id != Guid.Empty) 
                {
                    MessageBox.Show("Id is set twice!!!");
                }

                id = value;
            }
        }
    }
}
