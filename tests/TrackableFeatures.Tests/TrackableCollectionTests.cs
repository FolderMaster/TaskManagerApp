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
    [TestFixture(TestOf = typeof(TrackableCollection<>),
        Description = $"Тестирование класса {nameof(TrackableCollection<object>)}.")]
    public class TrackableCollectionTests
    {
        private FakeTrackableCollection _trackableCollection;

        [SetUp]
        public void Setup()
        {
            _trackableCollection = new();
        }

        [Test(Description = "Тестирование события" +
            $"{nameof(TrackableCollection<object>.CollectionChanged)} при добавлении элемента.")]
        public void EventCollectionChanged_Add_InvokeEventHandler()
        {
            var item = 1;
            var index = 0;
            var action = NotifyCollectionChangedAction.Add;
            var result = false;

            _trackableCollection.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems[0].Equals(item) && args.NewStartingIndex == index &&
                    args.Action == action)
                {
                    result = true;
                }
            };
            _trackableCollection.Add(item);

            Assert.That(result, "Должно отработать событие!");
        }

        [Test(Description = "Тестирование события" +
            $"{nameof(TrackableCollection<object>.CollectionChanged)} при вставке элемента.")]
        public void EventCollectionChanged_Insert_InvokeEventHandler()
        {
            var item = 1;
            var index = 0;
            var action = NotifyCollectionChangedAction.Add;
            var result = false;

            _trackableCollection.Add(item);
            _trackableCollection.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems[0].Equals(item) && args.NewStartingIndex == index &&
                    args.Action == action)
                {
                    result = true;
                }
            };
            _trackableCollection.Insert(index, item);

            Assert.That(result, "Должно отработать событие!");
        }

        [Test(Description = "Тестирование события" +
            $"{nameof(TrackableCollection<object>.CollectionChanged)} при удалении элемента.")]
        public void EventCollectionChanged_Remove_InvokeEventHandler()
        {
            var item = 1;
            var index = 0;
            var action = NotifyCollectionChangedAction.Remove;
            var result = false;

            _trackableCollection.Add(item);
            _trackableCollection.CollectionChanged += (sender, args) =>
            {
                if (args.OldItems[0].Equals(item) && args.OldStartingIndex == index &&
                    args.Action == action)
                {
                    result = true;
                }
            };
            _trackableCollection.Remove(item);

            Assert.That(result, "Должно отработать событие!");
        }

        [Test(Description = "Тестирование события" +
            $"{nameof(TrackableCollection<object>.CollectionChanged)} " +
            "при удалении элемента в индексе.")]
        public void EventCollectionChanged_RemoveAt_InvokeEventHandler()
        {
            var item = 1;
            var index = 0;
            var action = NotifyCollectionChangedAction.Remove;
            var result = false;

            _trackableCollection.Add(item);
            _trackableCollection.CollectionChanged += (sender, args) =>
            {
                if (args.OldItems[0].Equals(item) && args.OldStartingIndex == index &&
                    args.Action == action)
                {
                    result = true;
                }
            };
            _trackableCollection.RemoveAt(index);

            Assert.That(result, "Должно отработать событие!");
        }

        [Test(Description = "Тестирование события" +
            $"{nameof(TrackableCollection<object>.CollectionChanged)} при очистке коллекции.")]
        public void EventCollectionChanged_Clear_InvokeEventHandler()
        {
            var action = NotifyCollectionChangedAction.Reset;
            var result = false;

            _trackableCollection.Add(1);
            _trackableCollection.CollectionChanged += (sender, args) =>
            {
                if (args.Action == action)
                {
                    result = true;
                }
            };
            _trackableCollection.Clear();

            Assert.That(result, "Должно отработать событие!");
        }

        [Test(Description = "Тестирование события" +
            $"{nameof(TrackableCollection<object>.CollectionChanged)} " +
            "при замещении элемента в индексе.")]
        public void EventCollectionChanged_Replace_InvokeEventHandler()
        {
            var oldItem = 1;
            var newItem = 2;
            var index = 0;
            var action = NotifyCollectionChangedAction.Replace;
            var result = false;

            _trackableCollection.Add(oldItem);
            _trackableCollection.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems[0].Equals(newItem) && args.OldItems[0].Equals(oldItem) &&
                    args.OldStartingIndex == index && args.Action == action)
                {
                    result = true;
                }
            };
            _trackableCollection.Replace(index, newItem);

            Assert.That(result, "Должно отработать событие!");
        }

        [Test(Description = "Тестирование события" +
            $"{nameof(TrackableCollection<object>.CollectionChanged)} " +
            "при замещении элемента в индексе.")]
        public void EventCollectionChanged_Move_InvokeEventHandler()
        {
            var oldItem = 1;
            var newItem = 2;
            var oldIndex = 0;
            var newIndex = 1;
            var action = NotifyCollectionChangedAction.Move;
            var result = false;

            _trackableCollection.Add(oldItem);
            _trackableCollection.Add(newItem);
            _trackableCollection.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems[0].Equals(oldItem) && args.OldItems[0].Equals(oldItem) &&
                    args.OldStartingIndex == oldIndex && args.NewStartingIndex == newIndex &&
                    args.Action == action)
                {
                    result = true;
                }
            };
            _trackableCollection.Move(oldIndex, newIndex);

            Assert.That(result, "Должно отработать событие!");
        }

        [Test(Description = $"Тестирование метода " +
            $"{nameof(TrackableCollection<object>.OnAddedItem)} при добавлении элементов.")]
        public void OnAddedItem_Add_Invoke()
        {
            var value = 1;
            var expected = 1;

            _trackableCollection.Add(value);
            var result = _trackableCollection.LastEditedItem;

            Assert.That(result, Is.EqualTo(expected), "Должен отработать метод!");
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(TrackableCollection<object>.OnRemovedItem)} при добавлении элементов.")]
        public void OnRemovedItem_Remove_Invoke()
        {
            var firstValue = 1;
            var oldValue = 2;
            var expected = 1;

            _trackableCollection.Add(firstValue);
            _trackableCollection.Add(oldValue);
            _trackableCollection.Remove(firstValue);
            var result = _trackableCollection.LastEditedItem;

            Assert.That(result, Is.EqualTo(expected), "Должен отработать метод!");
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(TrackableCollection<object>.OnAddedItem)} " +
            "при добавлении элементов в конструкторе.")]
        public void OnAddedItem_AddWithConstructor_NoInvoke()
        {
            _trackableCollection = new([1]);
            var result = _trackableCollection.LastEditedItem;

            Assert.That(result, Is.Null, "Не должен отработать метод!");
        }
    }
}