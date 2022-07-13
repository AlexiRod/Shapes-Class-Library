using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesClassLibrary
{
    /// <summary>
    /// Класс стороны фигуры, т.е. отрезка, соединяющего две точки
    /// </summary>
    public class Side
    {
        public Point First { get; private set; }
        public Point Second { get; private set; }

        public double Length => Math.Sqrt(Math.Pow(First.X - Second.X, 2) + Math.Pow(First.Y - Second.Y, 2));

        /// <summary>
        /// Конструктор стороны по двум точкам
        /// </summary>
        /// <param name="first">Первая точка</param>
        /// <param name="second">Вторая точка</param>
        public Side(Point first, Point second)
        {
            First = first;
            Second = second;
        }

        /// <summary>
        /// Конструктор стороны по четырем координатам
        /// </summary>
        /// <param name="x1">Координата X первой точки</param>
        /// <param name="y1">Координата Y первой точки</param>
        /// <param name="x2">Координата X второй точки</param>
        /// <param name="y2">Координата Y второй точки</param>
        public Side(double x1, double y1, double x2, double y2)
        {
            First = new Point(x1, y1);
            Second = new Point(x2, y2);
        }


        /// <summary>
        /// Равенство сторон определяется по равенству их точек, вне зависимости от того, какая из них первая, а какая вторая
        /// </summary>
        public override bool Equals(object? obj)
        {
            return obj is Side side &&
                   (EqualityComparer<Point>.Default.Equals(First, side.First) &&
                   EqualityComparer<Point>.Default.Equals(Second, side.Second) || 
                   EqualityComparer<Point>.Default.Equals(First, side.Second) &&
                   EqualityComparer<Point>.Default.Equals(Second, side.First));
        }

        /// <summary>
        /// Хеш-код стороны определяется как хеш-код ее точек. Порядок первой и второй точки здесь не важен
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(First, Second) + HashCode.Combine(Second, First);
        }
    }
}
