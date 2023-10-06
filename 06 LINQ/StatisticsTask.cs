// Вставьте сюда финальное содержимое файла StatisticsTask.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public class StatisticsTask
{
    public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
	{
        return visits
                .GroupBy(visit => visit.UserId)
                .Select(group => group
                                    .OrderBy(x => x.DateTime)
                                    .Bigrams()
                        )

                 .Select(bigram => bigram
                            .Where(x => x.First.SlideId != x.Second.SlideId && x.First.SlideType == slideType)
                            .Select(visit => visit.Second.DateTime.Subtract(visit.First.DateTime).TotalMinutes)
                		)
                .SelectMany(x => x)
                .Where(time => time >= 1 && time <= 120)
                .ToList()
                .DefaultIfEmpty(0)
                .Median();
    }
}
