namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс конвертора данных в данные для предсказания и наоборот.
    /// </summary>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    /// <typeparam name="D">Тип данных.</typeparam>
    /// <typeparam name="DT">Тип входных данных.</typeparam>
    /// <typeparam name="DR">Тип выходных данных.</typeparam>
    public interface IConverter<T, R, D, DT, DR>
    {
        /// <summary>
        /// Конвертирует данные под данные для предсказания.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает конвертированные данные для предсказания.</returns>
        public LearningModelData<T, R> ConvertData(IEnumerable<D> data);

        /// <summary>
        /// Конвертирует входные данные под входные данные для предсказания.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает конвертированные входные данные для предсказания.</returns>
        public T ConvertData(DT data);

        /// <summary>
        /// Конвертирует предсказанное значение в выходное значение.
        /// </summary>
        /// <param name="predicted">Предсказанное значение.</param>
        /// <returns>Возвращает конвертированное выходное значение.</returns>
        public DR ConvertPredicted(R predicted);
    }
}
