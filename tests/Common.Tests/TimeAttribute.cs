namespace Common.Tests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class TimeAttribute : PropertyAttribute
    {
        public TimeAttribute(TestTime time) : base("Time", time.ToString()) { }
    }
}
