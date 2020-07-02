using System.IO;
using System.Xml.Serialization;
using Torch;

namespace Utils
{
    public static class ConfigUtils
    {
        public static T Load<T>(TorchPluginBase plugin, string fileName) where T : ViewModel, new()
        {
            string filePath = Path.Combine(plugin.StoragePath, fileName);
            T config = new T();
            if (File.Exists(filePath))
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    config = (T)xmlSerializer.Deserialize(streamReader);
                }
            }
            else
            {
                Save<T>(plugin, config, fileName);
            }
            return config;
        }

        public static void Save<T>(TorchPluginBase plugin, T config, string fileName) where T : ViewModel, new()
        {
            string filePath = Path.Combine(plugin.StoragePath, fileName);
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(streamWriter, config);
            }
        }
    }
}
