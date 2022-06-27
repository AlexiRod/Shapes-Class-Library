using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesClassLibrary;
using System;

namespace ShapesTests
{
    /// <summary>
    /// Тестирование классов фигур
    /// </summary>
    [TestClass]
    public class ShapesTest
    {
        private static Point p1 = new Point(1.3, 2.2);
        private static Point p2 = new Point(2.7, -4.5);
        private static Point p3 = new Point(3.7, 9.2);

        Triangle t1 = new Triangle(p1, p2, p3); // Первый треугольник
        Triangle t2 = new Triangle(new Side(p1, p2), new Side(p2, p3), new Side(p3, p1)); // Такой же, как и первый
        Triangle t3 = new Triangle(p1, new Point(-p2.X, -p2.Y), p3); // Только две точки как у первого
        Triangle t4 = new Triangle(new Point(0, 0), new Point(3, 0), new Point(0, 4)); // Прямоугольный треугольник
        Triangle t5 = new Triangle(new Point(1, 1), new Point(4, 1), new Point(1, 5)); // Прямоугольный треугольник с площадью как у t4

        Circle c1 = new Circle(p1, 5); // Первая окружность
        Circle c2 = new Circle(new Point(p1.X, p1.Y), 5); // Такая же, как первая окружность
        Circle c3 = new Circle(p1, 4); // Первая окружность с другим радиусом
        Circle c4 = new Circle(p2, 5); // Первая окружность с другим цетром

        Rectangle r1 = new Rectangle(p1, p3); // Первый прямоугольник
        Rectangle r2 = new Rectangle(new Side(p1.X, p1.Y, p1.X, p3.Y), new Side(p1.X, p3.Y, p3.X, p3.Y)); // Такой же как и первый
        Rectangle r3 = new Rectangle(new Side(p1.X + 1, p1.Y + 1, p1.X + 1, p3.Y + 1), 
            new Side(p1.X + 1, p3.Y + 1, p3.X + 1, p3.Y + 1)); // Такой же как и первый, но со смещением на 1 единицу вниз и вправо
        Rectangle r4 = new Rectangle(new Side(p1.X, p1.Y, p1.X, p3.Y + 1), new Side(p1.X, p3.Y + 1, p3.X, p3.Y + 1)); // Меньше чем первый

        Square s1 = new Square(p1, 5); // Первый квадрат
        Square s2 = new Square(new Point(p1.X, p1.Y), 5); // Такой же как первый
        Square s3 = new Square(p1, 4); // Такой же как первый, но с меньшей стороной
        Square s4 = new Square(p2, 5); // Такой же как первый, но с другой точкой расположения


        /// <summary>
        /// Тестирование методов Equals и GetHashCode у треугольника
        /// </summary>
        [TestMethod]
        public void TestTriangleEquality()
        {
            Assert.IsTrue(t1.Equals(t2));
            Assert.IsFalse(t1.Equals(t3));
            Assert.IsFalse(t1.Equals(null));
            Assert.IsFalse(t1.Equals("test"));
            Assert.IsFalse(t4.Equals(t5));

            Assert.AreEqual(t1.GetHashCode(), t2.GetHashCode());
            Assert.AreNotEqual(t1.GetHashCode(), t3.GetHashCode());
        }

        /// <summary>
        /// Тестирование основных методов треугольника (Вычисление площади и проверка на прямой угол)
        /// </summary>
        [TestMethod]
        public void TestTriangleMethods()
        {
            Assert.AreEqual(t1.GetArea(), t2.GetArea());
            Assert.AreNotEqual(t1.GetArea(), t3.GetArea());
            Assert.IsTrue(t4.IsRightTriangle());
        }

        /// <summary>
        /// Тестирование крайних случаев создания треугольника, вызывающих исключения
        /// </summary>
        [TestMethod]
        public void TestTriangleExceptions()
        {
            Point bad1 = new Point(1, 1);
            Point bad2 = new Point(2, 2);
            Point bad3 = new Point(3, 3);
            Point bad4 = new Point(4, 4);
            Exception ex;

            // Нарушение порядка точек в сторонах треугольника
            ex = Assert.ThrowsException<ArgumentException>(() =>
            new Triangle(new Side(bad1, bad2), new Side(bad2, bad3), new Side(bad1, bad3)));
            Assert.AreEqual("Стороны треугольника должны последовательно соединять ровно три точки.", ex.Message);

            // Неправильно заданная третья точка треугольника
            ex = Assert.ThrowsException<ArgumentException>(() =>
            new Triangle(new Side(bad1, bad2), new Side(bad2, bad3), new Side(bad3, bad3)));
            Assert.AreEqual("Стороны треугольника должны последовательно соединять ровно три точки.", ex.Message);

            // Несоединенный треугольник
            ex = Assert.ThrowsException<ArgumentException>(() =>
            new Triangle(new Side(bad1, bad2), new Side(bad2, bad3), new Side(bad3, bad4)));
            Assert.AreEqual("Стороны треугольника должны последовательно соединять ровно три точки.", ex.Message);

            // Нарушение правила треугольника
            ex = Assert.ThrowsException<ArgumentException>(() =>
            new Triangle(new Side(bad1, bad2), new Side(bad2, bad3), new Side(bad3, bad1)));
            Assert.AreEqual("Невозможно создать треугольник с такими сторонами. Нарушено правило треугольника.", ex.Message);
        }


        /// <summary>
        /// Тестирование методов Equals и GetHashCode у окружности
        /// </summary>
        [TestMethod]
        public void TestCircleEquality()
        {
            Assert.IsTrue(c1.Equals(c2));
            Assert.IsFalse(c1.Equals(c3));
            Assert.IsFalse(c1.Equals(c4));
            Assert.IsFalse(c1.Equals(null));
            Assert.IsFalse(c1.Equals("test"));

            Assert.AreEqual(c1.GetHashCode(), c2.GetHashCode());
            Assert.AreNotEqual(c1.GetHashCode(), c3.GetHashCode());
            Assert.AreNotEqual(c2.GetHashCode(), c4.GetHashCode());
        }

        /// <summary>
        /// Тестирование основных методов окружности (Вычисление площади)
        /// </summary>
        [TestMethod]
        public void TestCircleMethods()
        {
            Assert.AreEqual(c1.GetArea(), c2.GetArea());
            Assert.AreEqual(c1.GetArea(), c4.GetArea());
            Assert.AreNotEqual(c1.GetHashCode(), c4.GetHashCode());
        }

        /// <summary>
        /// Тестирование крайних случаев создания окружности, вызывающих исключения
        /// </summary>
        [TestMethod]
        public void TestCircleExceptions()
        {
            // Отрицательный радиус
            var ex = Assert.ThrowsException<ArgumentException>(() =>
            new Circle(new Point(1, 1), -1));
            Assert.AreEqual("Радиус должен быть положительным числом.", ex.Message);
        }


        /// <summary>
        /// Тестирование методов Equals и GetHashCode у прямоугольника
        /// </summary>
        [TestMethod]
        public void TestRectangleEquality()
        {
            Assert.IsTrue(r1.Equals(r2));
            Assert.IsFalse(r1.Equals(r3));
            Assert.IsFalse(r1.Equals(null));
            Assert.IsFalse(r1.Equals("test"));

            Assert.AreEqual(r1.GetHashCode(), r2.GetHashCode());
            Assert.AreNotEqual(r1.GetHashCode(), r3.GetHashCode());
        }

        /// <summary>
        /// Тестирование основных методов прямоугольника (Вычисление площади)
        /// </summary>
        [TestMethod]
        public void TestRectangleMethods()
        {
            Assert.AreEqual(r1.GetArea(), r2.GetArea());
            Assert.AreEqual(r1.GetArea(), r3.GetArea());
            Assert.AreNotEqual(r1.GetArea(), r4.GetArea());
        }

        /// <summary>
        /// Тестирование крайних случаев создания прямоугольника, вызывающих исключения
        /// </summary>
        [TestMethod]
        public void TestRectangleExceptions()
        {
            Exception ex;

            // Левая верхняя точка ниже чем правая нижняя
            ex = Assert.ThrowsException<ArgumentException>(() =>
            new Rectangle(new Point(1, 2), new Point(2, 1)));
            Assert.AreEqual("Левая верхняя точка прямоугольника должна быть покоординатно левее и выше правой нижней.", ex.Message);

            // Левая верхняя точка правее чем правая нижняя
            ex = Assert.ThrowsException<ArgumentException>(() =>
            new Rectangle(new Point(2, 1), new Point(1, 2)));
            Assert.AreEqual("Левая верхняя точка прямоугольника должна быть покоординатно левее и выше правой нижней.", ex.Message);

            // Неправильное задание точек прямоугольника через стороны (ЛН -> ЛВ, ЛВ -> ПВ)
            ex = Assert.ThrowsException<ArgumentException>(() =>
            new Rectangle(new Side(1, 2, 1, 1), new Side(1, 1, 2, 1)));
            Assert.AreEqual("Левая верхняя точка прямоугольника должна быть покоординатно левее и выше правой нижней.", ex.Message);

            // Вертикальная сторона непаралельна оси OY
            ex = Assert.ThrowsException<ArgumentException>(() =>
            new Rectangle(new Side(1, 1, 2, 2), new Side(2, 2, 3, 2)));
            Assert.AreEqual("Стороны прямоугольника должны быть параллельны соответствующим осям координат.", ex.Message);

            // Горизонтальная сторона непаралельна оси OX
            ex = Assert.ThrowsException<ArgumentException>(() =>
            new Rectangle(new Side(1, 1, 1, 2), new Side(1, 2, 3, 3)));
            Assert.AreEqual("Стороны прямоугольника должны быть параллельны соответствующим осям координат.", ex.Message);

            // Нарушение порядка точек в сторонах прямоугольника 
            ex = Assert.ThrowsException<ArgumentException>(() =>
            new Rectangle(new Side(1, 1, 1, 2), new Side(3, 2, 1, 2)));
            Assert.AreEqual("Стороны прямоугольника должны последовательно соединять три точки в следующем порядке: ЛВ -> ЛН, ЛН -> ПН.", ex.Message);
        }


        /// <summary>
        /// Тестирование методов Equals и GetHashCode у квадрата
        /// </summary>
        [TestMethod]
        public void TestSquareEquality()
        {
            Assert.IsTrue(s1.Equals(s2));
            Assert.IsFalse(s1.Equals(s3));
            Assert.IsFalse(s1.Equals(s4));
            Assert.IsFalse(s1.Equals(null));
            Assert.IsFalse(s1.Equals("test"));
            Assert.IsTrue(s1.Equals(new Rectangle(s1.TopLeftPoint, s2.BottomRightPoint)));

            Assert.AreEqual(s1.GetHashCode(), s2.GetHashCode());
            Assert.AreNotEqual(s1.GetHashCode(), s3.GetHashCode());
            Assert.AreNotEqual(s1.GetHashCode(), s4.GetHashCode());
        }

        /// <summary>
        /// Тестирование основных методов квадрата (Вычисление площади)
        /// </summary>
        [TestMethod]
        public void TestSquareMethods()
        {
            Assert.AreEqual(s1.GetArea(), s2.GetArea());
            Assert.AreEqual(s1.GetArea(), s4.GetArea());
            Assert.AreNotEqual(s1.GetHashCode(), s3.GetHashCode());
        }

        /// <summary>
        /// Тестирование крайних случаев создания треугольника, вызывающих исключения
        /// </summary>
        [TestMethod]
        public void TestSquareExceptions()
        {
            // Отрицательная длина стороны квадрата
            var ex = Assert.ThrowsException<ArgumentException>(() =>
            new Square(new Point(1, 1), -1));
            Assert.AreEqual("Левая верхняя точка прямоугольника должна быть покоординатно левее и выше правой нижней.", ex.Message);
        }

    }
}
