using System.Collections;

namespace Model
{
    public interface IFullCollection<T> : IList<T>, IList
    {
        public void Replace(int index, T item);

        public void Move(int oldIndex, int newIndex);
    }
}
