using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TrackableFeatures
{
    /// <summary>
    /// Базовый класс объекта, предоставляющий поддержку отслеживания изменений свойств и ошибок.
    /// Реализует <see cref="INotifyPropertyChanged"/> и <see cref="INotifyDataErrorInfo"/>.
    /// </summary>
    public class TrackableObject : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        /// Словарь ошибок.
        /// </summary>
        private readonly Dictionary<string, List<object>> _errors = new();

        /// <inheritdoc/>
        public bool HasErrors => _errors.Any();

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc/>
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        /// <inheritdoc/>
        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null)
            {
                return Enumerable.Empty<object>();
            }
            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }
            return Enumerable.Empty<object>();
        }

        /// <summary>
        /// Обновляет значение свойства, вызывает действие и уведомляет об изменении свойства.
        /// </summary>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <param name="field">Ссылка на поле свойства.</param>
        /// <param name="newValue">Новое значение свойства.</param>
        /// <param name="action">
        /// Дополнительное действие, выполняемое при изменении свойства.
        /// </param>
        /// <param name="propertyName">Название свойства.</param>
        protected void UpdateProperty<T>(ref T field, T newValue, Action? action = null,
            [CallerMemberName] string propertyName = "")
        {
            if (field != null && !field.Equals(newValue) ||
                newValue != null && !newValue.Equals(field))
            {
                field = newValue;
                action?.Invoke();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Обновляет значение свойства, вызывает действие и уведомляет об изменении свойства.
        /// </summary>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <param name="field">Ссылка на поле свойства.</param>
        /// <param name="newValue">Новое значение свойства.</param>
        /// <param name="action">Дополнительное действие с новым и старыми значениями,
        /// выполняемое при изменении свойства. </param>
        /// <param name="propertyName">Название свойства.</param>
        protected void UpdateProperty<T>(ref T field, T newValue, Action<T, T> action,
            [CallerMemberName] string propertyName = "")
        {
            if (field != null && !field.Equals(newValue) ||
                newValue != null && !newValue.Equals(field))
            {
                var oldValue = field;
                field = newValue;
                action?.Invoke(oldValue, newValue);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Добавляет ошибку для свойства.
        /// </summary>
        /// <param name="error">Ошибка.</param>
        /// <param name="propertyName">Название свойства.</param>
        protected void AddError(object error, [CallerMemberName] string propertyName = "")
        {
            if (!_errors.ContainsKey(propertyName))
            {
                var isUpdateHasError = !_errors.Any();
                _errors[propertyName] = new List<object>() { error };
                if (isUpdateHasError)
                {
                    OnPropertyChanged(nameof(HasErrors));
                }
            }
            else
            {
                _errors[propertyName].Add(error);
            }
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        /// Очищает все ошибки для свойства.
        /// </summary>
        /// <param name="propertyName">Название свойства.</param>
        public void ClearErrors([CallerMemberName] string propertyName = "")
        {
            if (_errors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
                if (!_errors.Any())
                {
                    OnPropertyChanged(nameof(HasErrors));
                }
            }
        }

        /// <summary>
        /// Очищает все ошибки для всех свойств.
        /// </summary>
        public void ClearAllErrors()
        {
            if (_errors.Any())
            {
                var propertyNames = _errors.Keys.ToList();
                _errors.Clear();
                foreach (var propertyName in propertyNames)
                {
                    OnErrorsChanged(propertyName);
                }
                OnPropertyChanged(nameof(HasErrors));
            }
        }

        /// <summary>
        /// Вызывает событие <see cref="PropertyChanged"/> для свойства.
        /// </summary>
        /// <param name="propertyName">Название свойства.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Вызывает событие <see cref="ErrorsChanged"/> для свойства.
        /// </summary>
        /// <param name="propertyName">Название свойства.</param>
        protected void OnErrorsChanged([CallerMemberName] string propertyName = "")
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
