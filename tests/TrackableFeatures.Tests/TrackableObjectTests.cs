﻿using System.ComponentModel;

namespace TrackableFeatures.Tests
{
    [TestFixture(Category = "Unit", TestOf = typeof(TrackableObject),
        Description = $"Тестирование класса {nameof(TrackableObject)}.")]
    public class TrackableObjectTests
    {
        private TrackableObjectPrototype _trackableObject;

        [SetUp]
        public void Setup()
        {
            _trackableObject = new();
        }

        [Test(Description = $"Тестирование события {nameof(TrackableObject.PropertyChanged)}" +
            "при изменении свойства.")]
        public void EventPropertyChanged_ChangeProperty_InvokeEventHandler()
        {
            var firstPropertyValue = "1";
            var lastPropertyValue = "2";
            var result = false;

            _trackableObject.Property = firstPropertyValue;
            _trackableObject.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(TrackableObjectPrototype.Property))
                {
                    result = true;
                }
            };
            _trackableObject.Property = lastPropertyValue;

            Assert.IsTrue(result, "Должно было отработать событие!");
        }

        [Test(Description = $"Тестирование события {nameof(TrackableObject.PropertyChanged)}" +
            "при установке свойства без изменения свойства.")]
        public void EventPropertyChanged_SetPropertyWithoutChange_NoInvokeEventHandler()
        {
            var firstPropertyValue = "1";
            var lastPropertyValue = "1";
            var result = false;

            _trackableObject.Property = firstPropertyValue;
            _trackableObject.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(TrackableObjectPrototype.Property))
                {
                    result = true;
                }
            };
            _trackableObject.Property = lastPropertyValue;

            Assert.IsFalse(result, "Не должно было отработать событие!");
        }

        [Test(Description = $"Тестирование события {nameof(TrackableObject.ErrorsChanged)}" +
            "при изменении свойства c ошибкой.")]
        public void EventErrorsChanged_ChangePropertyWithError_InvokeEventHandler()
        {
            var firstPropertyValue = "1";
            var lastPropertyValue = "";
            var result = false;

            _trackableObject.Property = firstPropertyValue;
            _trackableObject.ErrorsChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(TrackableObjectPrototype.Property))
                {
                    result = true;
                }
            };
            _trackableObject.Property = lastPropertyValue;

            Assert.IsTrue(result, "Должно было отработать событие!");
        }

        [Test(Description = $"Тестирование события {nameof(TrackableObject.ErrorsChanged)}" +
            "при изменении свойства без ошибок.")]
        public void EventErrorsChanged_ChangePropertyWithoutError_NoInvokeEventHandler()
        {
            var firstPropertyValue = "1";
            var lastPropertyValue = "2";
            var result = false;

            _trackableObject.Property = firstPropertyValue;
            _trackableObject.ErrorsChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(TrackableObjectPrototype.Property))
                {
                    result = true;
                }
            };
            _trackableObject.Property = lastPropertyValue;

            Assert.IsFalse(result, "Не должно было отработать событие!");
        }

        [Test(Description = $"Тестирование метода {nameof(TrackableObject.GetErrors)}" +
            "при изменении свойства с ошибкой.")]
        public void GetErrors_ChangePropertyWithError_ReturnError()
        {
            var propertyValue = "";
            var expected = new object[] { TrackableObjectPrototype.NullOrEmptyPropertyError };

            _trackableObject.Property = propertyValue;
            var result = _trackableObject.GetErrors(nameof(TrackableObjectPrototype.Property));

            Assert.That(result, Is.EqualTo(expected), "Должно быть 1 ошибка!");
        }

        [Test(Description = $"Тестирование метода {nameof(TrackableObject.GetErrors)}" +
            "при изменении свойства с 2 ошибками.")]
        public void GetErrors_ChangePropertyWith2Errors_Return2Errors()
        {
            var propertyValue = "TaskManager";
            var expected = new object[] { TrackableObjectPrototype.PropertyStartsWithTaskError,
                TrackableObjectPrototype.PropertyEndsWithManagerError };

            _trackableObject.Property = propertyValue;
            var result = _trackableObject.GetErrors(nameof(TrackableObjectPrototype.Property));

            Assert.That(result, Is.EqualTo(expected), "Должно быть 2 ошибки!");
        }

        [Test(Description = $"Тестирование метода {nameof(TrackableObject.GetErrors)}" +
            "при изменении свойства без ошибок.")]
        public void GetErrors_ChangePropertyWithoutErrors_ReturnZeroErrors()
        {
            var firstPropertyValue = "TaskManager";
            var lastPropertyValue = "1";
            var expected = new object[] { };

            _trackableObject.Property = firstPropertyValue;
            _trackableObject.Property = lastPropertyValue;
            var result = _trackableObject.GetErrors(nameof(TrackableObjectPrototype.Property));

            Assert.That(result, Is.EqualTo(expected), "Не должно быть никаких ошибок!");
        }

        [Test(Description = $"Тестирование свойства {nameof(TrackableObject.HasErrors)}" +
            "при изменении свойства с ошибкой.")]
        public void GetHasErrors_ChangePropertyWithError_ReturnTrue()
        {
            var propertyValue = "";

            _trackableObject.Property = propertyValue;
            var result = _trackableObject.HasErrors;

            Assert.IsTrue(result, "Должно иметь ошибку!");
        }

        [Test(Description = $"Тестирование свойства {nameof(TrackableObject.HasErrors)}" +
            "при изменении свойства без ошибок.")]
        public void GetHasErrors_ChangePropertyWithoutErrors_ReturnFalse()
        {
            var propertyValue = "1";

            _trackableObject.Property = propertyValue;
            var result = _trackableObject.HasErrors;

            Assert.IsFalse(result, "Не должно иметь никаких ошибок!");
        }
    }
}