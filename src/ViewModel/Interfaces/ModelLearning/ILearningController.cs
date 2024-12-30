namespace ViewModel.Interfaces.ModelLearning
{
    /// <summary>
    /// Интерфейс контроллера обучения модели обучения.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IModelTeacher{D}"/>.
    /// </remarks>
    /// <typeparam name="D">Тип данных.</typeparam>
    /// <typeparam name="DT">Тип входных данных.</typeparam>
    /// <typeparam name="DR">Тип выходных данных.</typeparam>
    public interface ILearningController<D, DT, DR> : IModelTeacher<D>
    {
        /// <summary>
        /// Возвращает логическое значение, указывающее на достоверность модели обучения.
        /// </summary>
        public bool IsValidModel { get; }

        /// <summary>
        /// Выполняет предсказание на основе данных.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает предсказанное значение.</returns>
        public DR Predict(DT data);
    }
}
