namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс конвертора данных в данные для предсказания с учителем и наоборот.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ILearningConverter{T, R, DT, DR}"/>.
    /// </remarks>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    /// <typeparam name="D">Тип данных.</typeparam>
    /// <typeparam name="DT">Тип входных данных.</typeparam>
    /// <typeparam name="DR">Тип выходных данных.</typeparam>
    public interface ISupervisedLearningConverter<T, R, D, DT, DR> :
        ILearningConverter<T, R, DT, DR>
    {
        /// <summary>
        /// Адаптирует конвертацию данных под данные для предсказания.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает конвертированные данные для предсказания.</returns>
        public LearningModelData<T, R> FitConvertData(IEnumerable<D> data);
    }
}
