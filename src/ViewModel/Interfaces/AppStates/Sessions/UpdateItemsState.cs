namespace ViewModel.Interfaces.AppStates.Sessions
{
    /// <summary>
    /// Перечисление состояний обновлений объектов.
    /// </summary>
    public enum UpdateItemsState
    {
        /// <summary>
        /// Перезагружено.
        /// </summary>
        Reset,
        /// <summary>
        /// Добавлено.
        /// </summary>
        Add,
        /// <summary>
        /// Изменено.
        /// </summary>
        Edit,
        /// <summary>
        /// Удалено.
        /// </summary>
        Remove,
        /// <summary>
        /// Перемещено.
        /// </summary>
        Move
    }
}
