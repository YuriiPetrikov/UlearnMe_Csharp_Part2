// Вставьте сюда финальное содержимое файла GhostsTask.cs
using System;
using System.Reflection.Metadata;
using System.Text;

namespace hashes;

public class GhostsTask : 
	IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
	IMagic
{
    readonly Cat cat  = new("Кошка1", "Ангорская", new DateTime(1, 1, 1));
    readonly Vector vector = new(10, 10);
    readonly Robot robot = new("11", 123.456);

    static readonly Encoding unicode = Encoding.Unicode;
    byte[] content = unicode.GetBytes("dfaewifhwef");
    Document document;


    Segment segment;// = new(vector, new Vector(3, 4));
    public Cat Create()
    {
		return cat;
    }

    public void DoMagic()
	{
		cat.Rename("Кошка2");
		vector.Add(new Vector(1, 2));
		Robot.BatteryCapacity++;
        content[0] = 12;
    }

	// Чтобы класс одновременно реализовывал интерфейсы IFactory<A> и IFactory<B> 
	// придется воспользоваться так называемой явной реализацией интерфейса.
	// Чтобы отличать методы создания A и B у каждого метода Create нужно явно указать, к какому интерфейсу он относится.
	// На самом деле такое вы уже видели, когда реализовывали IEnumerable<T>.

	Vector IFactory<Vector>.Create()
	{
		return vector;
	}

	Segment IFactory<Segment>.Create()
	{
        segment = new(vector, new Vector(3, 4));
        return segment;
    }

    Document IFactory<Document>.Create()
    {
        document = new("Заголовок", unicode, content);
        return document;
    }

    Robot IFactory<Robot>.Create()
    {
		return robot;
    }

    // И так даллее по аналогии...
}
