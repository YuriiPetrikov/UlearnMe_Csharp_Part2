// Вставьте сюда финальное содержимое файла MovingAverageTask.cs
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace yield;

public static class MovingAverageTask
{
	public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
	{
        Queue<double> queue = new Queue<double>();
        int bufferLength = windowWidth;
        double sum = 0;
        foreach (var e in data)
        {
            queue.Enqueue(e.OriginalY);
            sum += e.OriginalY;
            if (queue.Count > bufferLength)
            {
                sum -= queue.Dequeue();
            }
            yield return e.WithAvgSmoothedY(sum / queue.Count);
        }
    }
}
