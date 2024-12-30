namespace ViewModel.Interfaces.AppStates.Sessions
{
    /// <summary>
    /// Класс аргумента события при обновлении данных.
    /// </summary>
    public class ItemsUpdatedEventArgs
    {
        /// <summary>
        /// Возвращает состояние.
        /// </summary>
        public UpdateItemsState State { get; private set; }

        /// <summary>
        /// Возвращает объекты.
        /// </summary>
        public IEnumerable<object> Items { get; private set; }

        /// <summary>
        /// Возвращает тип объектов.
        /// </summary>
        public Type ItemsType { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ItemsUpdatedEventArgs"/>.
        /// </summary>
        /// <param name="state">Состояние.</param>
        /// <param name="items">Объекты.</param>
        /// <param name="itemsType">Тип объектов.</param>
        public ItemsUpdatedEventArgs(UpdateItemsState state,
            IEnumerable<object> items, Type itemsType)
        {
            State = state;
            Items = items;
            ItemsType = itemsType;
        }
    }
}
