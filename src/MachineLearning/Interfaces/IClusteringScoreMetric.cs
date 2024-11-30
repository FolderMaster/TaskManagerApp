using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    public interface IClusteringScoreMetric :
        IUnsupervisedScoreMetric<int, double> { }
}
