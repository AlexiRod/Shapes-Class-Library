using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesClassLibrary
{
    /// <summary>
    /// Класс квадрата, наследующего логику поведения прямоугольника
    /// </summary>
    public class Square : Rectangle
    {
        /// <summary>
        /// Конструктор квадрата по левой верхней точке и длине его стороны
        /// </summary>
        /// <param name="topLeft">Верхняя левая точка квадрата</param>
        /// <param name="side">Длина стороны квадрата</param>
        /// <exception cref="ArgumentException">Выбрасывается в случае отрицательной длины стороны</exception>
        public Square(Point topLeft, double side) : base(topLeft, new Point(topLeft.X + side, topLeft.Y + side))
        {
        }
    }
}
