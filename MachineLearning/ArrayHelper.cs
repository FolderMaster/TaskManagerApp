namespace MachineLearning
{
    public static class ArrayHelper
    {
        public static T[][] To2dArray<T>(this IEnumerable<IEnumerable<T>> values)
        {
            if (values is T[][] array)
            {
                return array;
            }
            return values.Select(v => v.ToArray()).ToArray();
        }

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
