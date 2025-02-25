namespace Common.Tests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class CategoryAttribute : PropertyAttribute
    {
        public CategoryAttribute(TestCategory category) : base("Category", category.ToString()) { }
    }
}
