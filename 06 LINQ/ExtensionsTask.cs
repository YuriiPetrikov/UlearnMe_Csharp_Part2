using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace linq_slideviews;

public static class ExtensionsTask
{
	/// <summary>
	/// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
	/// Медиана списка из четного количества элементов — это среднее арифметическое 
	/// двух серединных элементов списка после сортировки.
	/// </summary>
	/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
	public static double Median(this IEnumerable<double> items)
	{
		var sortItem = items.OrderBy(w => w);
        sortItem.TryGetNonEnumeratedCount(out int count);
        double item = 0;
		int i = 0;

		foreach (var vr in sortItem)
		{
			if (i == count / 2)
			{
				if (count % 2 == 0)
					return (item + vr) / 2;
				else
					return vr;
			}
            item = vr;
            i++;
        }
        throw new InvalidOperationException();
    }   
  

	/// <returns>
	/// Возвращает последовательность, состоящую из пар соседних элементов.
	/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
	/// </returns>
	public static IEnumerable<(T First, T Second)> Bigrams<T>(this IEnumerable<T> items)
	{
		var iterator = items.GetEnumerator();
        iterator.MoveNext();
        var past = iterator.Current;
  
        while (iterator.MoveNext())
		{
            yield return (past, past = iterator.Current);
        }
    }
}
