using Newtonsoft.Json;
using System.Text;

using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.AppStates
{
    public class JsonSerializer : ISerializer
    {
        public JsonSerializerSettings Settings { get; private set; } = new()
        {
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };

        public T? Deserialize<T>(byte[] data)
        {
            var text = Encoding.Default.GetString(data);
            return JsonConvert.DeserializeObject<T>(text, Settings);
        }

        public byte[] Serialize(object value)
        {
            var text = JsonConvert.SerializeObject(value, Settings);
            return Encoding.Default.GetBytes(text);
        }
    }
}
