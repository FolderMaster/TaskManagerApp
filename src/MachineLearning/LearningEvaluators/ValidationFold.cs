namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Структура хранения данных сегмента валидации.
    /// </summary>
    public struct ValidationFold
    {
        /// <summary>
        /// Возвращает и задаёт индексы данных обучения.
        /// </summary>
        public IEnumerable<int> TrainIndices { get; private set; }

        /// <summary>
        /// Возвращает и задаёт индексы данных тестирования.
        /// </summary>
        public IEnumerable<int> TestIndices { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ValidationFold"/>.
        /// </summary>
        /// <param name="trainIndices">Индексы данных обучения.</param>
        /// <param name="testIndices">Индексы данных тестирования.</param>
        public ValidationFold(IEnumerable<int> trainIndices, IEnumerable<int> testIndices)
        {
            TrainIndices = trainIndices;
            TestIndices = testIndices;
        }
    }
}
