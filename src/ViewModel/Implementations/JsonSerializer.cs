using Newtonsoft.Json;
using System.Text;
using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations
{
    public class JsonSerializer : ISerializer
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new()
        {
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };

        public T? Deserialize<T>(byte[] data)
        {
            var text = Encoding.Default.GetString(data);
            return JsonConvert.DeserializeObject<T>(text, _jsonSerializerSettings);
        }

        public byte[] Serialize(object value)
        {
            var text = JsonConvert.SerializeObject(value, _jsonSerializerSettings);
            return Encoding.Default.GetBytes(text);
        }
    }
}
