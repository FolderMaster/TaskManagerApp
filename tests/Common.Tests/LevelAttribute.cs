namespace Common.Tests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class LevelAttribute : PropertyAttribute
    {
        public LevelAttribute(TestLevel level) : base("Level", level.ToString()) { }
    }
}
