using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesClassLibrary;
using ShapesClassLibrary.Exceptions;
using System;

namespace ShapesTests
{
    /// <summary>
    /// Тестирование класса треугольника
    /// </summary>
    [TestClass]
    public class TriangleTests
    {
        private static Point p1 = new Point(1.3, 2.2);
        private static Point p2 = new Point(2.7, -4.5);
        private static Point p3 = new Point(3.7, 9.2);

        // Первый треугольник
        Triangle t1 = new Triangle(p1, p2, p3);

        // Такой же, как и первый
        Triangle t2 = new Triangle(new Side(p1, p2), new Side(p2, p3), new Side(p3, p1));

        // Только две точки как у первого, третья отличается
        Triangle t3 = new Triangle(p1, new Point(-p2.X, -p2.Y), p3);

        // Прямоугольный треугольник t4
        Triangle t4 = new Triangle(new Point(0, 0), new Point(3, 0), new Point(0, 4));

        // Прямоугольный треугольник с площадью как у t4
        Triangle t5 = new Triangle(new Point(1, 1), new Point(4, 1), new Point(1, 5));

        // Такой же, как и первый, но с другим порядком точек
        Triangle t1Equal = new Triangle(p2, p3, p1);

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
            Assert.IsTrue(t1.Equals(t1Equal));

            Assert.AreEqual(t1.GetHashCode(), t2.GetHashCode());
            Assert.AreEqual(t1.GetHashCode(), t1Equal.GetHashCode());
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
            Assert.IsTrue(t1.GetArea() - t1Equal.GetArea() < 0.0000001);
            Assert.IsTrue(t4.IsRightTriangle());
            Assert.IsFalse(t3.IsRightTriangle());
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
            ex = Assert.ThrowsException<SidesInitializationException>(() =>
            new Triangle(new Side(bad1, bad2), new Side(bad2, bad3), new Side(bad1, bad3)));
            Assert.AreEqual("Стороны треугольника должны последовательно соединять ровно три точки.", ex.Message);

            // Неправильно заданная третья точка треугольника
            ex = Assert.ThrowsException<SidesInitializationException>(() =>
            new Triangle(new Side(bad1, bad2), new Side(bad2, bad3), new Side(bad3, bad3)));
            Assert.AreEqual("Стороны треугольника должны последовательно соединять ровно три точки.", ex.Message);

            // Несоединенный треугольник
            ex = Assert.ThrowsException<SidesInitializationException>(() =>
            new Triangle(new Side(bad1, bad2), new Side(bad2, bad3), new Side(bad3, bad4)));
            Assert.AreEqual("Стороны треугольника должны последовательно соединять ровно три точки.", ex.Message);

            // Нарушение правила треугольника
            ex = Assert.ThrowsException<TriangleRuleException>(() =>
            new Triangle(new Side(bad1, bad2), new Side(bad2, bad3), new Side(bad3, bad1)));
            Assert.AreEqual("Невозможно создать треугольник с такими сторонами. Нарушено правило треугольника.", ex.Message);
        }
    }
}
