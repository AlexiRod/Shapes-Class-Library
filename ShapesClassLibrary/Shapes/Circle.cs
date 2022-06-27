using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesClassLibrary
{
    /// <summary>
    /// Класс окружности
    /// </summary>
    public class Circle : Shape
    {
        public Point CenterPoint { get; private set; }
        public double Radius { get; private set; }

        /// <summary>
        /// Конструктор инициализации окружности по точке центра и радиусу
        /// </summary>
        /// <param name="centerPoint">Центр окружности</param>
        /// <param name="radius">Радиус окружности</param>
        /// <exception cref="ArgumentException">Выбрасывается в случае отрицательного радиуса</exception>
        public Circle(Point centerPoint, double radius)
        {
            if (radius < 0)
                throw new ArgumentException("Радиус должен быть положительным числом.");
            
            CenterPoint = centerPoint;
            Radius = radius;

            InitializePoints(CenterPoint);
        }

        /// <summary>
        /// Равенство окружностей определяется по равенству их центров и радиусов
        /// </summary>
        public override bool Equals(object? obj)
        {
            return obj is Circle circle &&
                   EqualityComparer<Point>.Default.Equals(CenterPoint, circle.CenterPoint) &&
                   Radius == circle.Radius;
        }

        /// <summary>
        /// Хеш-код окружности определяется по ее центру и радиусу
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(CenterPoint, Radius);
        }


        /// <summary>
        /// Метод вычисления площади окружности по радиусу
        /// </summary>
        /// <returns>Площадь окружности по формуле πR²</returns>
        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }
}
