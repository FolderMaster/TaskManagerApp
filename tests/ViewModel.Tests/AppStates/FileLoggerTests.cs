﻿using Common.Tests;
using System.Text;

using ViewModel.Implementations.AppStates;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace ViewModel.Tests.AppStates
{
    [Level(TestLevel.Integration)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Minor)]
    [Priority(PriorityLevel.Medium)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Fast)]
    [TestFixture(TestOf = typeof(FileLogger),
        Description = $"Тестирование класса {nameof(FileLogger)}.")]
    public class FileLoggerTests
    {
        private FileLogger _fileLogger;

        private string _logFilePath = "log.txt";

        [SetUp]
        public void Setup()
        {
            _fileLogger = new(new FileService());
            _fileLogger.FilePath = _logFilePath;
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_logFilePath);
        }

        [Test(Description = $"Тестирование метода {nameof(FileLogger.Log)}.")]
        public async Task Log_LogMessage()
        {
            var message = "Error!";
            var expected = message;

            _fileLogger.Log(message);
            var bytes = await File.ReadAllBytesAsync(_logFilePath);
            var result = Encoding.Default.GetString(bytes);

            Assert.That(result, Does.Match(".*\\d{2}/\\d{2}/\\d{4} \\d{2}:\\d{2}:\\d{2}: " +
                message + ".*"), "Неправильно залогировано сообщение!");
        }
    }
}
