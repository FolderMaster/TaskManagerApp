namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс метрики расстояния.
    /// </summary>
    /// <typeparam name="T">Тип входных данных.</typeparam>
    public interface IDistanceMetric<T>
    {
        /// <summary>
        /// Вычисляет расстояние между двумя объектами.
        /// </summary>
        /// <param name="object1">Первый объект.</param>
        /// <param name="object2">Второй объект.</param>
        /// <returns>Возвращает расстояние.</returns>
        public double CalculateDistance(T object1, T object2);
    }
}
