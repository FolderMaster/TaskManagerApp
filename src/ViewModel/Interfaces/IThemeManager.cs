namespace ViewModel.Interfaces
{
    public interface IThemeManager
    {
        public IEnumerable<object> Themes { get; }

        public object ActualTheme { get; set; }
    }
}
