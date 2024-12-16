namespace ViewModel.Interfaces.AppStates.Sessions
{
    public class ItemsUpdatedEventArgs
    {
        public UpdateItemsState State { get; private set; }

        public IEnumerable<object> Items { get; private set; }

        public Type ItemsType { get; private set; }

        public ItemsUpdatedEventArgs(UpdateItemsState state,
            IEnumerable<object> items, Type itemsType)
        {
            State = state;
            Items = items;
            ItemsType = itemsType;
        }
    }
}
