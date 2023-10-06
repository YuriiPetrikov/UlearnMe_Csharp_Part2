using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace linq_slideviews;

public class ParsingTask
{
    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
    /// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
    /// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
    public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
    {
		SlideType type = SlideType.Exercise;
		int num = 0;
        return
            lines
                .Skip(1)
                .Select(line => line.Split(';'))
			    .Where(line => line.Length == 3)
				.Where(line => Enum.TryParse(line[1], true, out type))
				.Where(line => int.TryParse(line[0], out num))
     			.Select(line => (line[0], new SlideRecord(num, type, line[2])))
                .ToDictionary(w => Int32.Parse(w.Item1), w => w.Item2);
    }

    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
    /// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
    /// Такой словарь можно получить методом ParseSlideRecords</param>
    /// <returns>Список информации о посещениях</returns>
    /// <exception cref="FormatException">Если среди строк есть некорректные</exception>
    public static IEnumerable<VisitRecord> ParseVisitRecords(
		IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
	{
        try
        {

            return
            lines
            .Skip(1)
            .Select(line =>
             {
                if (line.Split(';').Length != 4)
                    throw new FormatException($"Wrong line [{line}]");
                return line.Split(';');
             })
             .Select(line =>
             {
                try
                {
                     return new VisitRecord(Int32.Parse(line[0]), Int32.Parse(line[1]),
                     DateTime.ParseExact(
                                 line[2] + ' ' + line[3],
                                 "yyyy-MM-dd HH:mm:ss",
                                 CultureInfo.InvariantCulture,
                                 DateTimeStyles.None),
                                 slides[Int32.Parse(line[1])].SlideType);
                }
                catch 
                {
                     throw new FormatException($"Wrong line [{line[0] + ';' + line[1]  + ';'+ line[2] + ';' + line[3]}]");
                }
             });
        }
        catch (Exception)
        {
            throw;
        }
    }
}
