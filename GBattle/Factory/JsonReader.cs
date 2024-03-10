using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GBattle.Factory
{
    internal static class JsonReader
    {

        private const string _ressources = "Ressources";
        public static NamesLibrary GetNamesLibraryFrom(string path) 
        {
            return DerserializeObject<NamesLibrary>(GetJsonContent(path));
        }

        private static T DerserializeObject<T>(string json) where T : class 
        {
            try
            {
                var v = JsonConvert.DeserializeObject<T>(json);
                if (v == null)
                {
                    throw new Exception("Fichier json vide.");
                }
                else
                {
                    return v;
                }
            }
            catch (JsonException e)
            {
                throw new Exception("Fichier json illisible :: " + e);
            }
        }

        private static string GetJsonContent(string name) 
        {
            string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, _ressources, name);

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else 
            {
                throw new Exception("Le fichier Json n'existe pas : "+path);
            }
        }
    }
}
