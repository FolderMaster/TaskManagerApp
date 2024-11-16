namespace ViewModel.ViewModels
{
    public abstract class DialogViewModel<A, R> : ViewModelBase
    {
        protected TaskCompletionSource<R>? _taskSource;

        private ViewModelBase? _parent;

        public ViewModelBase? Parent => _parent;

        protected abstract void GetArgs(A args);

        public async Task<R> Invoke(ViewModelBase parent, A args)
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
