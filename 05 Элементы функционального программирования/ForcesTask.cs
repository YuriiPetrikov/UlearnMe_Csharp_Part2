// Вставьте сюда финальное содержимое файла ForcesTask.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace func_rocket;

public class ForcesTask
{
    /// <summary>
    /// Создает делегат, возвращающий по ракете вектор силы тяги двигателей этой ракеты.
    /// Сила тяги направлена вдоль ракеты и равна по модулю forceValue.
    /// </summary>
    public static RocketForce GetThrustForce(double forceValue) => r => 
    new Vector(forceValue * Math.Cos(r.Direction), forceValue * Math.Sin(r.Direction));
    
    /// <summary>
    /// Преобразует делегат силы гравитации, в делегат силы, действующей на ракету
    /// </summary>
	public static RocketForce ConvertGravityToForce(Gravity gravity, Vector spaceSize) => r => 
	gravity(spaceSize, new Vector(r.Location.X, r.Location.Y));
    
    /// <summary>
    /// Суммирует все переданные силы, действующие на ракету, и возвращает суммарную силу.
    /// </summary>
    public static RocketForce Sum(params RocketForce[] forces)
    {
        return r =>
        {
            Vector sum = Vector.Zero;
            foreach (var force in forces)
                sum += force(r);
            return sum;
        };
    }
}
