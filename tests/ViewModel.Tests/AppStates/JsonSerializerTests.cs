using Newtonsoft.Json;
using System.Text;

using JsonSerializer = ViewModel.Implementations.AppStates.JsonSerializer;

namespace ViewModel.Tests.AppStates
{
    [TestFixture(Category = "Unit", TestOf = typeof(JsonSerializer),
        Description = $"Тестирование класса {nameof(JsonSerializer)}.")]
    public class JsonSerializerTests
    {
        private JsonSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new();
        }

        [Test(Description = $"Тестирование метода {nameof(JsonSerializer.Serialize)}.")]
        public void Serialize_ShouldReturnSerializedData()
        {
            var value = new { Name = "Test", Value = 123 };
            var expected = "{\"Name\":\"Test\",\"Value\":123}";

            _serializer.Settings.Formatting = Formatting.None;
            _serializer.Settings.TypeNameHandling = TypeNameHandling.None;
            var bytes = _serializer.Serialize(value);
            var result = Encoding.Default.GetString(bytes);

            Assert.That(result, Is.EqualTo(expected), "Неправильно сериализован объект!");
        }

        [Test(Description = $"Тестирование метода {nameof(JsonSerializer.Deserialize)}.")]
        public void Deserialize_ShouldReturnDeserializedObject()
        {
            var json = "{\"Name\":\"Test\",\"Value\":123}";
            var bytes = Encoding.Default.GetBytes(json);
            var expected = new { Name = "Test", Value = 123 };

            var result = _serializer.Deserialize<dynamic>(bytes);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null,
                    "Неправильно десериализован объект!");
                Assert.That((string)result.Name, Is.EqualTo(expected.Name),
                    "Неправильно десериализован объект!");
                Assert.That((int)result.Value, Is.EqualTo(expected.Value),
                    "Неправильно десериализован объект!");
            });
        }
    }
}
