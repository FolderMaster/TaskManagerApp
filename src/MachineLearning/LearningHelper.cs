using MachineLearning.Interfaces;
using MachineLearning.Interfaces.Generals;

namespace MachineLearning
{
    /// <summary>
    /// Вспомогательный статичный класс для обучения.
    /// </summary>
    public static class LearningHelper
    {
        /// <summary>
        /// Предсказывает значения на основе данных.
        /// </summary>
        /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
        /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
        /// <param name="learningModel">Модель обучения.</param>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает коллекцию предсказанных значений.</returns>
        public static IEnumerable<R> Predict<T, R>(this ILearningModel<T, R> learningModel,
            IEnumerable<T> data)
        {
            foreach (var value in data)
            {
                yield return learningModel.Predict(value);
            }
        }

        /// <summary>
        /// Преобразует значения.
        /// </summary>
        /// <param name="scaler">Масштабирование данных.</param>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает коллекцию нормализованных значений.</returns>
        public static IEnumerable<double> Transform(this IScaler scaler,
            IEnumerable<double> data)
        {
            foreach (var value in data)
            {
                yield return scaler.Transform(value);
            }
        }
    }
}
