using Newtonsoft.Json;
using System.Text;

namespace ViewModel.Technicals
{
    public class JsonSerializer : ISerializer
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings =
            new JsonSerializerSettings()
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
