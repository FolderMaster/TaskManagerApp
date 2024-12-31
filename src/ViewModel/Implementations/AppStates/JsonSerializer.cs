using Newtonsoft.Json;
using System.Text;

using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.AppStates
{
    /// <summary>
    /// Класс Json-сериализатора.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="ISerializer"/>.
    /// </remarks>
    public class JsonSerializer : ISerializer
    {
        /// <summary>
        /// Настройки.
        /// </summary>
        public JsonSerializerSettings Settings { get; private set; } = new()
        {
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };

        /// <inheritdoc/>
        public T? Deserialize<T>(byte[] data)
        {
            var text = Encoding.Default.GetString(data);
            return JsonConvert.DeserializeObject<T>(text, Settings);
        }

        /// <inheritdoc/>
        public byte[] Serialize(object value)
        {
            var text = JsonConvert.SerializeObject(value, Settings);
            return Encoding.Default.GetBytes(text);
        }
    }
}
