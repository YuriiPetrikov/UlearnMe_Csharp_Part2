using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit;
        LimitedSizeStack<Tuple<TItem, int>> stackUndo;


        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            stackUndo = new LimitedSizeStack<Tuple<TItem, int>>(limit);
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            stackUndo.Push(Tuple.Create(item, -1));
        }

        public void RemoveItem(int index)
        {
            stackUndo.Push(Tuple.Create(Items[index], index));
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return stackUndo.Count > 0;
        }

        public void Undo()
        {
            if (CanUndo())
            {
                Tuple<TItem, int> action1 = stackUndo.Pop();
                if (action1.Item2 == -1)
                {
                    Items.Remove(action1.Item1);
                }
                else
                {
                    Items.Insert(action1.Item2, action1.Item1);
                }
            }
        }
    }
}
