using System.Collections.Generic;

namespace TeamJustFour.MoveOneStep.Algorithm
{
    public class CircularList<T> : List<T>
    {
        private int m_CurrentIndex = 0;

        public T Next()
        {
            if (Count == 0)
                return default(T);

            m_CurrentIndex = (m_CurrentIndex + 1) % Count;

            return this[m_CurrentIndex];
        }

        public T Previous()
        {
            if (Count == 0)
                return default(T);

            m_CurrentIndex = (m_CurrentIndex - 1 + Count) % Count;

            return this[m_CurrentIndex];
        }

        public T Current()
        {
            if (Count == 0)
                return default(T);

            return this[m_CurrentIndex];
        }
    }
}
