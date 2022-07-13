using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesClassLibrary;
using ShapesClassLibrary.Exceptions;
using System;

namespace ShapesTests
{

    /// <summary>
    /// Тестирование класса окружности
    /// </summary>
    [TestClass]
    public class CirclesTests
    {
        private static Point p1 = new Point(1.3, 2.2);
        private static Point p2 = new Point(2.7, -4.5);
        private static Point p3 = new Point(3.7, 9.2);
        
        // Первая окружность
        Circle c1 = new Circle(p1, 5);

        // Такая же, как первая окружность
        Circle c2 = new Circle(new Point(p1.X, p1.Y), 5);

        // Первая окружность с другим радиусом
        Circle c3 = new Circle(p1, 4);

        // Первая окружность с другим цетром
        Circle c4 = new Circle(p2, 5);


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
            var ex = Assert.ThrowsException<CircleInitializationException>(() =>
            new Circle(new Point(1, 1), -1));
            Assert.AreEqual("Радиус должен быть положительным числом.", ex.Message);
        }
    }
}
