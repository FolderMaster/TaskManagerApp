using System.Diagnostics.CodeAnalysis;

using MachineLearning.LearningEvaluators;

namespace MachineLearning.Tests.LearningEvaluators
{
    public class ValidationFoldComparer : IEqualityComparer<ValidationFold>
    {
        public bool Equals(ValidationFold x, ValidationFold y) =>
            x.TrainIndices.SequenceEqual(y.TrainIndices) &&
            x.TestIndices.SequenceEqual(y.TestIndices);

        public int GetHashCode([DisallowNull] ValidationFold obj) =>
            obj.TrainIndices.GetHashCode() ^ obj.TestIndices.GetHashCode();
    }
}
