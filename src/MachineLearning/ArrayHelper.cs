namespace MachineLearning
{
    /// <summary>
    /// Вспомогательный статичный класс для преобразования в массивы и матрицы.
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// Преобразует коллекцию значений в двухмерный массив значений.
        /// </summary>
        /// <typeparam name="T">Тип значений.</typeparam>
        /// <param name="values">Коллекция значений.</param>
        /// <returns>Возвращает двухмерный массив значений.</returns>
        public static T[][] To2dArray<T>(this IEnumerable<IEnumerable<T>> values)
        {
            if (values is T[][] array)
            {
                return array;
            }
            return values.Select(v => v.ToArray()).ToArray();
        }

        /// <summary>
        /// Преобразует коллекцию значений в двухмерную матрицу значений.
        /// </summary>
        /// <typeparam name="T">Тип значений.</typeparam>
        /// <param name="values">Коллекция значений.</param>
        /// <returns>Возвращает двухмерную матрицу значений.</returns>
        public static T[,] To2dMatrix<T>(this IEnumerable<IEnumerable<T>> values)
        {
            if (values is T[,] array)
            {
                return array;
            }
            var rows = values.Count();
            var columns = values.First().Count();

            var matrix = values.Select((row, rowIndex) => row.Select((value, columnIndex) =>
                new { RowIndex = rowIndex, ColumnIndex = columnIndex, Value = value }))
                .SelectMany(r => r)
                .Aggregate(new T[rows, columns], (array, item) =>
                {
                    array[item.RowIndex, item.ColumnIndex] = item.Value;
                    return array;
                });
            return matrix;
        }
    }
}
