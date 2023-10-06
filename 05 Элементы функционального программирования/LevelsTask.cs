using System;
using System.Collections.Generic;

namespace func_rocket;

public class LevelsTask
{
	static readonly Physics standardPhysics = new();
    static readonly Vector TargetPosition = new (600, 200);
    static readonly Vector InitialRocketPosition = new(200, 500);

    static Vector CreateGravity(Vector vector, int constGravity)
    {
        return vector.Normalize() * constGravity * vector.Length / (vector.Length * vector.Length + 1);
	}

    public static IEnumerable<Level> CreateLevels()
    {
        yield return new Level("Zero",
            new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
            new Vector(600, 200),
            (size, v) => Vector.Zero, standardPhysics);
        
        yield return new Level("Heavy",
            new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
            new Vector(600, 200),
            (size, v) => new Vector(0, 0.9),
            standardPhysics);

        yield return new Level("Up",
            new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
            new Vector(700, 500),
            (size, v) => new Vector(0, -300 / (size.Y - v.Y + 300.0)),
            standardPhysics);

        yield return new Level("WhiteHole",
            new Rocket(InitialRocketPosition, Vector.Zero, -0.5 * Math.PI),
            TargetPosition,
            (size, v) =>
            {
                var d1 = v - TargetPosition;
                return CreateGravity(d1, 140);
            },
            standardPhysics);

        yield return new Level("BlackHole",
            new Rocket(InitialRocketPosition, Vector.Zero, -0.5 * Math.PI),
            TargetPosition,
            (size, v) =>
            {
                var d2 = (InitialRocketPosition + TargetPosition) / 2 - v;
                return CreateGravity(d2, 300);
            },
            standardPhysics);

        yield return new Level("BlackAndWhite",
           new Rocket(InitialRocketPosition, Vector.Zero, -0.5 * Math.PI),
           TargetPosition,
           (size, v) =>
           {
               var d1 = v - TargetPosition;
               var d2 = (InitialRocketPosition + TargetPosition) / 2 - v;
               return (CreateGravity(d1, 140) + CreateGravity(d2, 300)) / 2;
           },
           standardPhysics);
    }
}
