namespace Common.Tests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class ReproducibilityAttribute : PropertyAttribute
    {
        public ReproducibilityAttribute(ReproducibilityType type) :
            base("Reproducibility", type.ToString()) { }
    }
}
