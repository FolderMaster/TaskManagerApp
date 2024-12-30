namespace ViewModel.Interfaces.DataManagers.Generals
{
    /// <summary>
    /// Интерфейс заменителя для изменения данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IEditorService"/> и <see cref="IProxy{T}"/>.
    /// </remarks>
    /// <typeparam name="T">Тип заменяемого данных.</typeparam>
    public interface IEditorProxy<T> : IProxy<T>, IEditorService
    {
        /// <summary>
        /// Возвращает и задаёт заменяемого объекта.
        /// </summary>
        public new T Target { get; set; }
    }
}
