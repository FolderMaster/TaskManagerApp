namespace ViewModel.ViewModels
{
    public partial class DialogViewModel : ViewModelBase
    {
        protected TaskCompletionSource<object?>? _taskSource;

        private ViewModelBase? _parent;

        public ViewModelBase? Parent => _parent;

        public async Task<object?> Invoke(ViewModelBase parent)
        {
            if (Parent != null)
            {
                throw new InvalidOperationException();
            }
            _taskSource = new();
            _parent = parent;
            var result = await _taskSource.Task;
            _parent = null;
            return result;
        }
    }
}
