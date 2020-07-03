using System.IO;
using System.Xml.Serialization;

namespace Torch.SharedLibrary.Utils
{
    public static class ConfigUtils
    {
        /// <summary>
        /// Generic Configuration/Data Load function to retreive data from specified file in the specified type.
        /// </summary>
        /// <typeparam name="T">The type of your configuration or data object</typeparam>
        /// <param name="plugin">Instance to the plugin</param>
        /// <param name="fileName">Name of the file to use</param>
        /// <returns></returns>
        public static T Load<T>(TorchPluginBase plugin, string fileName) where T : new()
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

        /// <summary>
        /// Saves the specified data instance to a file.
        /// </summary>
        /// <typeparam name="T">The type of the data to save.</typeparam>
        /// <param name="plugin">Instance reference to your plugin</param>
        /// <param name="data">The actual data instance</param>
        /// <param name="fileName">Name of the file to store to</param>
        /// <returns></returns>
        public static bool Save<T>(TorchPluginBase plugin, T data, string fileName) where T : new()
        {
            try
            {
                string filePath = Path.Combine(plugin.StoragePath, fileName);
                using (StreamWriter streamWriter = new StreamWriter(filePath))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(streamWriter, data);
                }
                return true;
            }
            catch (System.Exception) { }
            return false;
        }
    }
}
