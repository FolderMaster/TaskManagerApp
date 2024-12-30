namespace ViewModel.Technicals
{
    /// <summary>
    /// Класс элемента статистики.
    /// </summary>
    public class StatisticElement
    {
        /// <summary>
        /// Возвращает значение.
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// Возвращает название.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="StatisticElement"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="name">Название.</param>
        public StatisticElement(double value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}
