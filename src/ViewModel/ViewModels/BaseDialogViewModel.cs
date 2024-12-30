namespace ViewModel.ViewModels
{
    /// <summary>
    /// Базовый абстрактный класс контроллера диалога.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseViewModel"/>.
    /// </remarks>
    public abstract class BaseDialogViewModel<A, R> : BaseViewModel
    {
        /// <summary>
        /// Источник для завершения задачи, который используется для возврата результата диалога.
        /// </summary>
        protected TaskCompletionSource<R>? _taskSource;

        /// <summary>
        /// Родитель.
        /// </summary>
        private BaseViewModel? _parent;

        /// <summary>
        /// Возвращает родителя.
        /// </summary>
        public BaseViewModel? Parent => _parent;

        /// <summary>
        /// Получает аргументы, переданные в диалог.
        /// </summary>
        /// <param name="args">Аргументы.</param>
        protected abstract void GetArgs(A args);

        /// <summary>
        /// Вызывает диалог.
        /// </summary>
        /// <param name="parent">Родитель.</param>
        /// <param name="args">Аргументы.</param>
        /// <returns>Возвращает задачу процесса диалога с результатом.</returns>
        /// <exception cref="InvalidOperationException">
        /// Выбрасывает, если родитель уже есть.
        /// </exception>
        public async Task<R> Invoke(BaseViewModel parent, A args)
        {
            if (Parent != null)
            {
                throw new InvalidOperationException();
            }
            _taskSource = new();
            _parent = parent;
            GetArgs(args);
            var result = await _taskSource.Task;
            _parent = null;
            return result;
        }
    }
}
