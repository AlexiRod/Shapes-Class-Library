using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesClassLibrary
{
    /// <summary>
    /// Класс треугольника
    /// </summary>
    public class Triangle : Shape
    {
        // Точки, из которых состоит треугольник
        public Point FirstPoint { get; private set; }
        public Point SecondPoint { get; private set; }
        public Point ThirdPoint { get; private set; }

        // Стороны треугольника, последовательно соединяющие точки
        public Side FirstSide { get; private set; }
        public Side SecondSide { get; private set; }
        public Side ThirdSide { get; private set; }

        /// <summary>
        /// Конструктор инициализации тругольника по трем точкам.
        /// Важно: образованные стороны треугольника последовательно соединяют три его точки: 1 -> 2, 2 -> 3, 3 -> 1
        /// </summary>
        /// <param name="firstPoint">Первая точка треугольника</param>
        /// <param name="secondPoint">Вторая точка треугольника</param>
        /// <param name="thirdPoint">Третья точка треугольника</param>
        public Triangle(Point firstPoint, Point secondPoint, Point thirdPoint)
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            ThirdPoint = thirdPoint;
            FirstSide = new Side(firstPoint, secondPoint);
            SecondSide = new Side(secondPoint, thirdPoint);
            ThirdSide = new Side(thirdPoint, firstPoint);

            CheckTriangleRuleAndInitialize();
        }

        /// <summary>
        /// Конструктор инициализации тругольника по трем сторонам.
        /// Важно: стороны треугольника должны последовательно соединять ровно три точки в следующем порядке: 1 -> 2, 2 -> 3, 3 -> 1
        /// </summary>
        /// <param name="firstSide">Первая сторона треугольника, соединяющая точки 1 и 2</param>
        /// <param name="secondSide">Вторая сторона треугольника, соединяющая точки 2 и 3</param>
        /// <param name="thirdSide">Третья сторона треугольника, соединяющая точки 3 и 1</param>
        /// <exception cref="ArgumentException">Выбрасывается, в случае, если стороны не образуют треугольник с требуемым порядком точек</exception>
        public Triangle(Side firstSide, Side secondSide, Side thirdSide)
        {
            if (!firstSide.Second.Equals(secondSide.First) ||
                !secondSide.Second.Equals(thirdSide.First) ||
                !thirdSide.Second.Equals(firstSide.First))
                throw new ArgumentException("Стороны треугольника должны последовательно соединять ровно три точки.");

            FirstSide = firstSide;
            SecondSide = secondSide;
            ThirdSide = thirdSide;
            FirstPoint = firstSide.First;
            SecondPoint = secondSide.First;
            ThirdPoint = thirdSide.First;

            CheckTriangleRuleAndInitialize();
        }
        
        /// <summary>
        /// Равенство треугольников определяется по равенству их соответствующих сторон, а соответственно и точек
        /// </summary>
        public override bool Equals(object? obj)
        {
            //if (!(obj is Triangle triangle))
            //    return false;

            //HashSet<Point> set = new HashSet<Point> { FirstPoint, SecondPoint, ThirdPoint };
            //List<Point> points = new List<Point>{ triangle.FirstPoint, triangle.SecondPoint, triangle.ThirdPoint };
            //foreach (var item in points)
            //    if (!set.Remove(item))
            //        return false;
            //return set.Count == 0;
            return obj is Triangle triangle &&
                   EqualityComparer<Side>.Default.Equals(FirstSide, triangle.FirstSide) &&
                   EqualityComparer<Side>.Default.Equals(SecondSide, triangle.SecondSide) &&
                   EqualityComparer<Side>.Default.Equals(ThirdSide, triangle.ThirdSide);
        }

        /// <summary>
        /// Хеш-код треугольника определяется как хеш-код его сторон
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(FirstSide, SecondSide, ThirdSide);
        }

        /// <summary>
        /// Метод определения, является ли треугольник прямоугольным
        /// </summary>
        /// <returns>Истина, если треугольник прямоугольный и ложь, если нет</returns>
        public bool IsRightTriangle()
        {
            //if (sides.Count != 3) // Это исключение никогда не должно выкинуться
            //    throw new NotSupportedException("Стороны треугольника не определены.");

            double a = sides[0].Length, b = sides[1].Length, c = sides[2].Length;
            return Math.Abs(a * a + b * b - c * c) < double.Epsilon;
        }

        /// <summary>
        /// Метод вычисления площади треугольника по трем сторонам
        /// </summary>
        /// <returns>Площадь треугольника через три стороны (формула Герона)</returns>
        public override double GetArea()
        {
            double p = (FirstSide.Length + SecondSide.Length + ThirdSide.Length) / 2;
            return Math.Sqrt(p * (p - FirstSide.Length) * (p - SecondSide.Length) * (p - ThirdSide.Length));
        }

        /// <summary>
        /// Метод определения, можно ли создать треугольник по заданным параметром (проверка правила треугольника) 
        /// и инициализация массива точек и сторон фигуры
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается в случае нарушения правила треугольника</exception>
        private void CheckTriangleRuleAndInitialize()
        {

            if (FirstSide.Length >= SecondSide.Length + ThirdSide.Length ||
                SecondSide.Length >= FirstSide.Length + ThirdSide.Length ||
                ThirdSide.Length >= SecondSide.Length + FirstSide.Length)
                throw new ArgumentException("Невозможно создать треугольник с такими сторонами. Нарушено правило треугольника.");

            InitializePoints(FirstPoint, SecondPoint, ThirdPoint);
            InitializeSides(FirstSide, SecondSide, ThirdSide);
        }
    }
}
