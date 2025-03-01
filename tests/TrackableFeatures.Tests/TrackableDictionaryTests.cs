using Common.Tests;
using System.Collections.Specialized;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace TrackableFeatures.Tests
{
    [Level(TestLevel.Unit)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Minor)]
    [Priority(PriorityLevel.Low)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Instant)]
    [TestFixture(TestOf = typeof(TrackableDictionary<,>),
        Description = $"Тестирование класса {nameof(TrackableDictionary<object, object>)}.")]
    public class TrackableDictionaryTests
    {
        private FakeTrackableDictionary _trackableDictionary;

        [SetUp]
        public void Setup()
        {
            _trackableDictionary = new();
        }

        [Test(Description = "Тестирование события " +
            $"{nameof(TrackableDictionary<object, object>.CollectionChanged)} " +
            "при добавлении элемента.")]
        public void EventCollectionChanged_Add_InvokeEventHandler()
        {
            var key = 0;
            var value = 1;
            var item = new KeyValuePair<object, object>(key, value);
            var action = NotifyCollectionChangedAction.Add;
            var result = false;

            _trackableDictionary.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems[0].Equals(item) && args.Action == action)
                {
                    result = true;
                }
            };
            _trackableDictionary.Add(key, value);

            Assert.That(result, "Должно отработать событие!");
        }

        [Test(Description = "Тестирование события" +
            $"{nameof(TrackableDictionary<object, object>.CollectionChanged)} " +
            "при удалении элемента.")]
        public void EventCollectionChanged_Remove_InvokeEventHandler()
        {
            var key = 0;
            var value = 1;
            var item = new KeyValuePair<object, object>(key, value);
            var action = NotifyCollectionChangedAction.Remove;
            var result = false;

            _trackableDictionary.Add(key, value);
            _trackableDictionary.CollectionChanged += (sender, args) =>
            {
                if (args.OldItems[0].Equals(item) && args.Action == action)
                {
                    result = true;
                }
            };
            _trackableDictionary.Remove(key);

            Assert.That(result, "Должно отработать событие!");
        }

        [Test(Description = "Тестирование события" +
            $"{nameof(TrackableDictionary<object, object>.CollectionChanged)} " +
            "при очистке коллекции.")]
        public void EventCollectionChanged_Clear_InvokeEventHandler()
        {
            var action = NotifyCollectionChangedAction.Reset;
            var result = false;

            _trackableDictionary.Add(0, 1);
            _trackableDictionary.CollectionChanged += (sender, args) =>
            {
                if (args.Action == action)
                {
                    result = true;
                }
            };
            _trackableDictionary.Clear();

            Assert.That(result, "Должно отработать событие!");
        }

        [Test(Description = "Тестирование события" +
            $"{nameof(TrackableDictionary<object, object>.CollectionChanged)} " +
            "при замещении элемента в индексе.")]
        public void EventCollectionChanged_Replace_InvokeEventHandler()
        {
            var key = 0;
            var oldValue = 1;
            var newValue = 2;
            var oldItem = new KeyValuePair<object, object>(key, oldValue);
            var newItem = new KeyValuePair<object, object>(key, newValue);
            var action = NotifyCollectionChangedAction.Replace;
            var result = false;

            _trackableDictionary.Add(key, oldValue);
            _trackableDictionary.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems[0].Equals(newItem) &&
                    args.OldItems[0].Equals(oldItem) && args.Action == action)
                {
                    result = true;
                }
            };
            _trackableDictionary[key] = newValue;

            Assert.That(result, "Должно отработать событие!");
        }

        [Test(Description = $"Тестирование метода " +
            $"{nameof(TrackableDictionary<object, object>.OnAddedItem)} " +
            "при добавлении элементов.")]
        public void OnAddedItem_Add_Invoke()
        {
            var key = 0;
            var value = 1;
            var expected = new KeyValuePair<object, object>(key, value);

            _trackableDictionary.Add(key, value);
            var result = _trackableDictionary.LastEditedItem;

            Assert.That(result, Is.EqualTo(expected), "Должен отработать метод!");
        }

        [Test(Description = $"Тестирование метода " +
            $"{nameof(TrackableDictionary<object, object>.OnRemovedItem)} " +
            "при удалении элементов.")]
        public void OnRemovedItem_Remove_Invoke()
        {
            var firstKey = 1;
            var firstValue = 1;
            var oldKey = 0;
            var oldValue = 2;
            var expected = new KeyValuePair<object, object>(firstKey, firstValue);

            _trackableDictionary.Add(firstKey, firstValue);
            _trackableDictionary.Add(oldKey, oldValue);
            _trackableDictionary.Remove(firstValue);
            var result = _trackableDictionary.LastEditedItem;

            Assert.That(result, Is.EqualTo(expected), "Должен отработать метод!");
        }

        [Test(Description = $"Тестирование метода " +
            $"{nameof(FakeTrackableDictionary.OnAddedItem)}" +
            "при добавлении элементов в конструкторе.")]
        public void OnAddedItem_AddWithConstructor_NoInvoke()
        {
            _trackableDictionary = new(new Dictionary<object, object>() { [0] = 1 });
            var result = _trackableDictionary.LastEditedItem;

            Assert.That(result, Is.Null, "Не должен отработать метод!");
        }
    }
}
