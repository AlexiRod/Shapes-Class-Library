using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesClassLibrary
{
    /// <summary>
    /// Класс точки в декартвой системе координат
    /// </summary>
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        /// <summary>
        /// Конструктор точки с двумя координатами
        /// </summary>
        /// <param name="x">Координата по оси OX</param>
        /// <param name="y">Координата по оси OY</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Равенство двух точек считается по равенству их координат
        /// </summary>
        public override bool Equals(object? obj)
        {
            return obj is Point point &&
                   X == point.X &&
                   Y == point.Y;
        }

        /// <summary>
        /// Хеш-код точки считается по ее координатам
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }
}
