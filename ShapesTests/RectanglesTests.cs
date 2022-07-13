using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesClassLibrary;
using ShapesClassLibrary.Exceptions;
using System;

namespace ShapesTests
{
    /// <summary>
    /// Тестирование класса прямоугольника
    /// </summary>
    [TestClass]
    public class RectanglesTests
    {
        private static Point p1 = new Point(1.3, 2.2);
        private static Point p2 = new Point(3.7, 9.2);

        // Первый прямоугольник
        Rectangle r1 = new Rectangle(p1, p2);

        // Такой же как и первый
        Rectangle r2 = new Rectangle(new Side(p1.X, p1.Y, p1.X, p2.Y), new Side(p1.X, p2.Y, p2.X, p2.Y));

        // Такой же как и первый, но со смещением на 1 единицу вниз и вправо
        Rectangle r3 = new Rectangle(new Side(p1.X + 1, p1.Y + 1, p1.X + 1, p2.Y + 1),
            new Side(p1.X + 1, p2.Y + 1, p2.X + 1, p2.Y + 1));

        // Меньше чем первый на 1 по высоте
        Rectangle r4 = new Rectangle(new Side(p1.X, p1.Y, p1.X, p2.Y + 1), new Side(p1.X, p2.Y + 1, p2.X, p2.Y + 1));


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
            ex = Assert.ThrowsException<RectangleInitializationException>(() =>
            new Rectangle(new Point(1, 2), new Point(2, 1)));
            Assert.AreEqual("Левая верхняя точка прямоугольника должна быть покоординатно левее и выше правой нижней.", ex.Message);

            // Левая верхняя точка правее чем правая нижняя
            ex = Assert.ThrowsException<RectangleInitializationException>(() =>
            new Rectangle(new Point(2, 1), new Point(1, 2)));
            Assert.AreEqual("Левая верхняя точка прямоугольника должна быть покоординатно левее и выше правой нижней.", ex.Message);

            // Неправильное задание точек прямоугольника через стороны (ЛН -> ЛВ, ЛВ -> ПВ)
            ex = Assert.ThrowsException<RectangleInitializationException>(() =>
            new Rectangle(new Side(1, 2, 1, 1), new Side(1, 1, 2, 1)));
            Assert.AreEqual("Левая верхняя точка прямоугольника должна быть покоординатно левее и выше правой нижней.", ex.Message);

            // Вертикальная сторона непаралельна оси OY
            ex = Assert.ThrowsException<RectangleInitializationException>(() =>
            new Rectangle(new Side(1, 1, 2, 2), new Side(2, 2, 3, 2)));
            Assert.AreEqual("Стороны прямоугольника должны быть параллельны соответствующим осям координат.", ex.Message);

            // Горизонтальная сторона непаралельна оси OX
            ex = Assert.ThrowsException<RectangleInitializationException>(() =>
            new Rectangle(new Side(1, 1, 1, 2), new Side(1, 2, 3, 3)));
            Assert.AreEqual("Стороны прямоугольника должны быть параллельны соответствующим осям координат.", ex.Message);

            // Нарушение порядка точек в сторонах прямоугольника 
            ex = Assert.ThrowsException<RectangleInitializationException>(() =>
            new Rectangle(new Side(1, 1, 1, 2), new Side(3, 2, 1, 2)));
            Assert.AreEqual("Стороны прямоугольника должны последовательно соединять три точки в следующем порядке: ЛВ -> ЛН, ЛН -> ПН.", ex.Message);
        }

    }
}
