using System.Collections.Generic;
using System.Linq;

namespace Dungeon;

public class BfsTask
{
	public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
	{
        var ways = new Dictionary<Point, SinglyLinkedList<Point>>();
        var queue = new Queue<Point>();
		var visited = new HashSet<Point> 
		{ 
			start
		};
		
		queue.Enqueue(start);
        ways.Add(start, new SinglyLinkedList<Point>(start));

        while (queue.Count != 0) 
        {
            var point = queue.Dequeue();

			if (point.X < 0 || point.X >= map.Dungeon.GetLength(0) 	
			 || point.Y < 0 || point.Y >= map.Dungeon.GetLength(1)) continue;
            if (map.Dungeon[point.X, point.Y] != MapCell.Empty) continue;

            foreach(var direction in Walker.PossibleDirections)
            {
                var nextPoint = new Point { X = point.X + direction.X, Y = point.Y + direction.Y };
                if (visited.Contains(nextPoint))
                    continue;
                visited.Add(nextPoint);
                queue.Enqueue(nextPoint);
                ways.Add(nextPoint, new SinglyLinkedList<Point>(nextPoint, ways[point]));
            }
        }

		foreach (var chest in chests)
		{
			if (ways.ContainsKey(chest)) yield return ways[chest];
		}	
	}
}
