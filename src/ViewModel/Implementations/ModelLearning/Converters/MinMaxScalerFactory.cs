using MachineLearning.Interfaces;
using MachineLearning.Scalers;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public class MinMaxScalerFactory : IFactory<IScaler>
    {
        public IScaler Create() => new MinMaxScaler();
    }
}
