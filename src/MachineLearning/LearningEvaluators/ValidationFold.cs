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
        public IEnumerable<int> TrainIndices { get; set; }

        /// <summary>
        /// Возвращает и задаёт индексы данных тестирования.
        /// </summary>
        public IEnumerable<int> TestIndices { get; set; }
    }
}
