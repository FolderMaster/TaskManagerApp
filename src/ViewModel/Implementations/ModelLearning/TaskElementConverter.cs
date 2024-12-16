using MachineLearning;
using MachineLearning.Interfaces;

using Model.Interfaces;

namespace ViewModel.Implementations.ModelLearning
{
    public class TaskElementConverter<R, DR> :
        IConverter<IEnumerable<double>, R, ITaskElement, ITaskElement, DR>
    {
        public TaskElementConverter()
        {
            
        }

        public LearningModelData<IEnumerable<double>, R> FitConvertData
            (IEnumerable<ITaskElement> data)
        {
            var dataResult = new List<IEnumerable<double>>();
            var targets = new List<R>();
            foreach (var item in data)
            {

            }
            return new LearningModelData<IEnumerable<double>, R>(dataResult, targets);
        }

        public IEnumerable<double> ConvertData(ITaskElement data)
        {
            throw new NotImplementedException();
        }

        public DR ConvertPredicted(R predicted)
        {
            throw new NotImplementedException();
        }
    }
}
