namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс масштабирования данных.
    /// </summary>
    public interface IScaler
    {
        /// <summary>
        /// Адаптирует преобразования под заданные данные.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает коллекцию нормализованных данных.</returns>
        public IEnumerable<double> FitTransform(IEnumerable<double> data);

        /// <summary>
        /// Преобразует значение.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Возвращает нормализованное значение.</returns>
        public double Transform(double value);
    }
}
