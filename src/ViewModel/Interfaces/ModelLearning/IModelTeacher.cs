namespace ViewModel.Interfaces.ModelLearning
{
    /// <summary>
    /// Интерфейс для обучения модели на основе данных.
    /// </summary>
    /// <typeparam name="D">Тип данных.</typeparam>
    public interface IModelTeacher<D>
    {
        /// <summary>
        /// Выполняет обучение модели на основе данных.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>
        /// Возвращает задачу процесса обучения с результатом <c>true</c>,
        /// если обучение прошло успешно, иначе <c>false</c>.
        /// </returns>
        public Task<bool> Train(IEnumerable<D> data);
    }
}
