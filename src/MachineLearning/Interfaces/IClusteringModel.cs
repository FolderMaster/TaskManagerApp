﻿using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс модели обучения кластеризации.
    /// Наследует <see cref="IUnsupervisedLearningEvaluator{IEnumerable{double}, int}"/>.
    /// </summary>
    public interface IClusteringModel : IUnsupervisedLearningModel<IEnumerable<double>, int> { }
}
