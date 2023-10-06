using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace yield;

public static class MovingMaxTask
{
	public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
	{
        LinkedList<double> deque = new LinkedList<double>();
        Queue<double> queue = new Queue<double>();
       
        foreach (var e in data)
        {
            queue.Enqueue(e.OriginalY);

            AddDeque(deque, e);
           
            if (queue.Count > windowWidth)
            {
                if (deque.First.Value == queue.Dequeue())
                    deque.RemoveFirst();
            }

            yield return e.WithMaxY(deque.First.Value);
        }
	}

    public static void AddDeque(LinkedList<double> deque, DataPoint e)
    {
        if (deque.Count == 0)
        {
            deque.AddLast(e.OriginalY);
        }
        else
        {
            while (true)
            {
                if (deque.Count >= 1 && deque.Last.Value < e.OriginalY)
                {
                    deque.RemoveLast();
                }
                else
                {
                    deque.AddLast(e.OriginalY);
                    break;
                }
            }
        }
    }
}
