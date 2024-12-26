using System.Text;

using ViewModel.Implementations.AppStates;

namespace ViewModel.Tests.AppStates
{
    [TestFixture(Category = "Unit", TestOf = typeof(FileService),
        Description = $"Тестирование класса {nameof(FileService)}.")]
    public class FileServiceTests
    {
        private FileService _fileService;

        private string _tempCatalogPath;

        [SetUp]
        public void Setup()
        {
            _fileService = new();
            _tempCatalogPath = Path.Combine(Path.GetTempPath(), "Test");
            Directory.CreateDirectory(_tempCatalogPath);
        }

        [TearDown]
        public void Teardown()
        {
            if (Directory.Exists(_tempCatalogPath))
            {
                Directory.Delete(_tempCatalogPath, true);
            }
        }

        [Test(Description = $"Тестирование метода {nameof(FileService.Save)}.")]
        public async Task Save_SaveDataInFile()
        {
            var filePath = Path.Combine(_tempCatalogPath, "test.txt");
            var data = Encoding.Default.GetBytes("Test content");
            var expected = data;

            await _fileService.Save(filePath, data);
            var result = await File.ReadAllBytesAsync(filePath);

            Assert.That(result, Is.EqualTo(expected), "Неправильно сохранены данные!");
        }

        [Test(Description = $"Тестирование метода {nameof(FileService.Load)}.")]
        public async Task Load_LoadDataFromFile()
        {
            var filePath = Path.Combine(_tempCatalogPath, "test.txt");
            var data = Encoding.Default.GetBytes("Test content");
            var expected = data;

            await File.WriteAllBytesAsync(filePath, data);
            var result = await _fileService.Load(filePath);

            Assert.That(result, Is.EqualTo(expected), "Неправильно загружены данные!");
        }

        [Test(Description = $"Тестирование метода {nameof(FileService.CreateDirectory)}.")]
        public void CreateDirectory_CreateDirectory()
        {
            var directoryPath = Path.Combine(_tempCatalogPath, "Test");

            _fileService.CreateDirectory(directoryPath);
            var result = Path.Exists(directoryPath);

            Assert.That(result, "Директория не создана!");
        }

        [Test(Description = $"Тестирование свойства {nameof(FileService.PersonalDirectoryPath)}.")]
        public void GetPersonalDirectoryPath_ReturnPersonalDirectoryPath()
        {
            var expected = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.Personal), "TaskManager");

            var result = _fileService.PersonalDirectoryPath;

            Assert.That(result, Is.EqualTo(expected), "Неправильный путь!");
        }

        [Test(Description = $"Тестирование метода {nameof(FileService.CreateStream)}.")]
        public async Task CreateStream_ReturnStream()
        {
            var filePath = Path.Combine(_tempCatalogPath, "test.txt");
            var expected = "Test content";
            var data = Encoding.Default.GetBytes(expected);
            await File.WriteAllBytesAsync(filePath, data);

            var result = "";
            using (var stream = _fileService.CreateStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var reader = new StreamReader(stream);
                result = await reader.ReadToEndAsync();
            }

            Assert.That(result, Is.EqualTo(expected), "Неправильное прочтение данных!");
        }

        [Test(Description = $"Тестирование метода {nameof(FileService.GetDirectoryPath)}.")]
        public void GetDirectoryPath_ReturnDirectoryPaht()
        {
            var directoryPath = "";
            var expected = Path.GetDirectoryName(directoryPath);

            var result = _fileService.GetDirectoryPath(directoryPath);

            Assert.That(result, Is.EqualTo(expected), "Неправильный путь к директории!");
        }

        [Test(Description = $"Тестирование метода {nameof(FileService.IsPathExists)}.")]
        public void IsPathExists_ReturnTrue()
        {
            var result = _fileService.IsPathExists(_tempCatalogPath);

            Assert.That(result, "Существующий путь не найден!");
        }

        [Test(Description = $"Тестирование метода {nameof(FileService.CombinePath)}.")]
        public void CombinePath_ReturnCombinedPath()
        {
            var firstPath = "C:\\Temp";
            var secondPath = "test.txt";
            var expected = Path.Combine(firstPath, secondPath);

            var result = _fileService.CombinePath(firstPath, secondPath);

            Assert.That(result, Is.EqualTo(expected), "Неправильное объединение путей!");
        }
    }
}
