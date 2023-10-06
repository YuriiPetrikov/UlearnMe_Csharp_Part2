// Вставьте сюда финальное содержимое файла LimitedSizeStack.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        LinkedList<T> limitedSizeStack = new LinkedList<T>();
        int limitList { get; set; }
        public LimitedSizeStack(int limit)
        {
            limitList = limit;
        }

        public void Push(T item)
        {
            if(limitList == 0) { }
            else if (limitedSizeStack.Count < limitList)
            {
                limitedSizeStack.AddLast(item);
            }
            else
            {
                limitedSizeStack.RemoveFirst();
                limitedSizeStack.AddLast(item);
            }
        }

        public T Pop()
        {
            T value = limitedSizeStack.Last.Value;
            limitedSizeStack.RemoveLast();
            return value;
        }

        public int Count
        {
            get
            {
                return limitedSizeStack.Count;
            }
        }
    }
}
