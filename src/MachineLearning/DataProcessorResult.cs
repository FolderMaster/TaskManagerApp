namespace MachineLearning
{
    /// <summary>
    /// Структура хранения данных результата преобразования.
    /// </summary>
    /// <typeparam name="T">Тип данных.</typeparam>
    public struct DataProcessorResult<T>
    {
        /// <summary>
        /// Возвращает и задаёт результат преобразования.
        /// </summary>
        public IEnumerable<T> Result { get; private set; }

        /// <summary>
        /// Возвращает и задаёт индексы удалённых столбцов.
        /// </summary>
        public IEnumerable<int> RemovedColumnsIndices { get; private set; }

        /// <summary>
        /// Возвращает и задаёт индексы удалённых строк.
        /// </summary>
        public IEnumerable<int> RemovedRowsIndices { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="DataProcessorResult{T}"/>.
        /// </summary>
        /// <param name="result">Результат преобразования.</param>
        /// <param name="removedColumnsIndices">Индексы удалённых столбцов.</param>
        /// <param name="removedRowsIndices">Индексы удалённых строк.</param>
        public DataProcessorResult(IEnumerable<T> result,
            IEnumerable<int>? removedColumnsIndices = null,
            IEnumerable<int>? removedRowsIndices = null)
        {
            Result = result;
            RemovedColumnsIndices = removedColumnsIndices ?? Enumerable.Empty<int>();
            RemovedRowsIndices = removedRowsIndices ?? Enumerable.Empty<int>();
        }
    }
}
