using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesClassLibrary
{
    /// <summary>
    /// Класс прямоугольника
    /// </summary>
    public class Rectangle : Shape
    {
        // Левая верхняя и правая нижняя точки прямоугольника
        public Point TopLeftPoint { get; private set; }
        public Point BottomRightPoint { get; private set; }

        // Вертикальная и горзонтальная стороны треугольника
        public Side VerticalSide { get; private set; }
        public Side HorizontalSide { get; private set; }

        /// <summary>
        /// Конструктор прямоугольника по двум точкам
        /// Важно: стороны прямоугольника инициализируются следующим образом: ЛВ -> ЛН, ЛН -> ПН
        /// где Л - левая, Н - нижняя, П - правая, В - верхняя
        /// </summary>
        /// <param name="topLeft">Левая верхняя точка</param>
        /// <param name="bottomRight">Правая нижняя точка</param>
        /// <exception cref="ArgumentException">Выбрасывается, если нарушена корректность точек и сторон прямоугольника</exception>
        public Rectangle(Point topLeft, Point bottomRight)
        {
            
            TopLeftPoint = topLeft;
            BottomRightPoint = bottomRight;
            HorizontalSide = new Side(topLeft, new Point(topLeft.X, bottomRight.Y));
            VerticalSide = new Side(new Point(topLeft.X, bottomRight.Y), bottomRight);

            CheckRectangleRulesAndInitialize();
        }

        /// <summary>
        /// Конструктор прямоугольника по двум сторонам
        /// Важно: стороны прямоугольника должны последовательно соединять три точки прямоугольника следующим образом: ЛВ -> ЛН, ЛН -> ПН
        /// где Л - левая, Н - нижняя, П - правая, В - верхняя
        /// </summary>
        /// <param name="horizontal">Горизонтальная сторона, соединяющая левую верхнюю и левую нижнюю сторону прямоугольника</param>
        /// <param name="vertical">Вертикальная сторона, соединяющая левую нижнюю и правую нижнюю сторону прямоугольника</param>
        /// <exception cref="ArgumentException">Выбрасывается, если нарушена корректность точек и сторон прямоугольника</exception>
        public Rectangle(Side horizontal, Side vertical)
        {
            HorizontalSide = horizontal;
            VerticalSide = vertical;
            TopLeftPoint = horizontal.First;
            BottomRightPoint = vertical.Second;

            CheckRectangleRulesAndInitialize();
        }

        /// <summary>
        /// Равенство прямоугольников определяется по равенству их левых верхних и правых нижних точек
        /// </summary>
        public override bool Equals(object? obj)
        {
            return obj is Rectangle rectangle &&
                   EqualityComparer<Point>.Default.Equals(TopLeftPoint, rectangle.TopLeftPoint) &&
                   EqualityComparer<Point>.Default.Equals(BottomRightPoint, rectangle.BottomRightPoint);
        }
        /// <summary>
        /// Хеш-код прямоугольника определяется по его левой вехрней и правой нижней точке
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(TopLeftPoint, BottomRightPoint);
        }

        /// <summary>
        /// Метод вычисления площади прямоугольника по двум сторонам
        /// </summary>
        /// <returns>Площадь прямоугольника через произведение длин сторон</returns>
        public override double GetArea()
        {
            return HorizontalSide.Length * VerticalSide.Length;
        }

        /// <summary>
        /// Метод определения корректности прямоугольника по заданным параметрам и инициализация
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если:
        /// 1) Левая верхняя точка правее или ниже чем нижняя правая
        /// 2) Стороны не образуют прямоугольник с требуемым порядком точек ЛВ -> ЛН, ЛН -> ПН
        /// 3) Стороны не параллельны осям координат
        /// </exception>
        private void CheckRectangleRulesAndInitialize()
        {
            if (TopLeftPoint.X > BottomRightPoint.X || TopLeftPoint.Y > BottomRightPoint.Y)
                throw new ArgumentException("Левая верхняя точка прямоугольника должна быть покоординатно левее и выше правой нижней.");

            if (HorizontalSide.First.X != HorizontalSide.Second.X || VerticalSide.First.Y != VerticalSide.Second.Y)
                throw new ArgumentException("Стороны прямоугольника должны быть параллельны соответствующим осям координат.");

            if (!HorizontalSide.Second.Equals(VerticalSide.First))
                throw new ArgumentException("Стороны прямоугольника должны последовательно соединять три точки в следующем порядке: ЛВ -> ЛН, ЛН -> ПН.");

            InitializePoints(TopLeftPoint, BottomRightPoint);
            InitializeSides(VerticalSide, HorizontalSide);
        }
    }
}
