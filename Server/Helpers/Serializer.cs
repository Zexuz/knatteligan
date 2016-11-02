using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace knatteligan.Helpers
{

    public class Serializer<T>
    {

        public static IEnumerable<T> GetDataFromFile(string fileName)
        {
            var list = new List<T>();

            using (var stream = File.Open(fileName, FileMode.Open))
            {
                var xmlSerializer = new XmlSerializer(list.GetType());
                list.AddRange((IEnumerable<T>)xmlSerializer.Deserialize(stream));
            }

            return list;
        }

        public static void SaveDataToFile(List<T> list, string fileName)
        {
            var xmlSerializer = new XmlSerializer(list.GetType());

            using (var stream = File.Open(fileName, FileMode.Create))
            {
                xmlSerializer.Serialize(stream, list);
            }
        }
    }
}