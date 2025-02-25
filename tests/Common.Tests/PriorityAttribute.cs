namespace Common.Tests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class PriorityAttribute : PropertyAttribute
    {
        public PriorityAttribute(PriorityLevel level) : base("Priority", level.ToString()) { }
    }
}
