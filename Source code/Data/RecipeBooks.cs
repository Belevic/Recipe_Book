using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecipeBook.Domain;
using System.Xml.Serialization;
using System.IO;

namespace RecipeBook.Data
{
    public class RecipeBooks
    {
        public class RecipeBookImpl : IRecipeBook
        {
            public const string DefaultSavePath = "RecipeBook.xml";

            private string savePath;

            [XmlIgnore]
            public string SavePath
            {
                get { return this.savePath ?? DefaultSavePath; }
                set { this.savePath = value; }
            }

            private IDictionary<Guid, BookObject> domainObjects = new Dictionary<Guid, BookObject>();
            private static readonly XmlSerializer serializer = new XmlSerializer(typeof(RecipeBookImpl));

            
            public void SetAll<T>(IEnumerable<T> values)
                where T : BookObject
            {
                this.GetAll<T>().ToArray().ForEach(o => this.Remove(o.Id));
                if (values != null)
                {
                    values.ForEach(o => this.Add(o));
                }
            }

            public T Get<T>(Guid id) where T : BookObject
            {
                return (T)this.domainObjects[id];
            }

            public void Add<T>(T domainObject) where T : BookObject
            {
                Utils.Assert(domainObject.Id != Guid.Empty);

                if (this.domainObjects.ContainsKey(domainObject.Id))
                {
                    this.Remove(domainObject.Id);
                }

                domainObject.RecipeBook = this;
                this.domainObjects[domainObject.Id] = domainObject;
            }

            public void Save()
            {
                var directory = Path.GetDirectoryName(this.SavePath);

                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var tempPath = Path.Combine(directory, string.Format("{0}-{1}-temp.xml", Path.GetFileNameWithoutExtension(this.SavePath), DateTime.Now.Ticks));

                using (var stream = File.OpenWrite(tempPath))
                {
                    serializer.Serialize(stream, this);
                }

                if (File.Exists(this.SavePath))
                {
                    var backupPath = string.Format("{0}.bak", this.SavePath);
                    if (File.Exists(backupPath))
                    {
                        File.Delete(backupPath);
                    }
                    File.Move(this.SavePath, backupPath);
                    File.SetAttributes(backupPath, FileAttributes.Hidden);
                }
                File.Move(tempPath, this.SavePath);
            }

            public static RecipeBookImpl Load(string path)
            {
                var savePath = path ?? DefaultSavePath;

                RecipeBookImpl recipeBook;

                if (File.Exists(savePath))
                {
                    using (var stream = File.OpenRead(savePath))
                    {
                        recipeBook = (RecipeBookImpl)serializer.Deserialize(stream);
                        recipeBook.domainObjects.ForEach(o => o.Value.RecipeBook = recipeBook);
                        
                    }
                }
                else
                {
                    recipeBook = new RecipeBookImpl();
                }

                recipeBook.SavePath = savePath;

                return recipeBook;
            }

            public Recipe[] RecipesCollection
            {
                get { return this.RecipesList.ToArray(); }
                set { this.SetAll(value); }
            }

            public Ingredient[] ItemsCollection
            {
                get { return this.IngredientsList.ToArray(); }
                set { this.SetAll(value); }
            }
            [XmlIgnore]
            public IEnumerable<Recipe> RecipesList
            {
                get { return this.GetAll<Recipe>(); }
            }

            [XmlIgnore]
            public IEnumerable<Ingredient> IngredientsList
            {
                get { return this.GetAll<Ingredient>(); }
            }
            public IEnumerable<T> GetAll<T>()
                where T : BookObject
            {
                return this.domainObjects.Values.OfType<T>();
            }


            public void Remove(Guid id)
            {
                if (this.domainObjects.ContainsKey(id))
                {
                    this.domainObjects[id].RecipeBook = null;
                    this.domainObjects.Remove(id);
                }
            }
        }

        public static IRecipeBook Current { get; private set; }

        private RecipeBooks() { }

        public static void SetCurrent(string savePath = null)
        {
            Current = RecipeBookImpl.Load(savePath);
        }
    }
}
