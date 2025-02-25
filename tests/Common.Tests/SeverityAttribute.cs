namespace Common.Tests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class SeverityAttribute : PropertyAttribute
    {
        public SeverityAttribute(SeverityLevel level) : base("Severity", level.ToString()) { }
    }
}
