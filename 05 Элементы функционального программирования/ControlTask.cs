// Вставьте сюда финальное содержимое файла ControlTask.cs
using System.Linq;
using System;

namespace func_rocket;

public class ControlTask
{
    public static double TotalAngle;
    public static Turn ControlRocket(Rocket rocket, Vector target)
	{
        //Координаты вектора между целью и ракетой
        var distanceVector = new Vector(target.X - rocket.Location.X, target.Y - rocket.Location.Y);

        if (Math.Abs(distanceVector.Angle - rocket.Direction) < 0.5)
        {
            TotalAngle = (2 * distanceVector.Angle - rocket.Direction - rocket.Velocity.Angle) / 2;
        }
        else
        {
            TotalAngle = distanceVector.Angle - rocket.Direction;
        }

        if (TotalAngle < 0)
            return Turn.Left;
        return TotalAngle > 0 ? Turn.Right : Turn.None;
    }
}
