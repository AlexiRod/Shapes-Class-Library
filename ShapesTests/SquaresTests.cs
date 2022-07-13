using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesClassLibrary;
using ShapesClassLibrary.Exceptions;
using System;

namespace ShapesTests
{
    [TestClass]
    public class SquaresTests
    {
        private static Point p1 = new Point(1.3, 2.2);
        private static Point p2 = new Point(2.7, -4.5);

        // Первый квадрат
        Square s1 = new Square(p1, 5);

        // Такой же как первый
        Square s2 = new Square(new Point(p1.X, p1.Y), 5);

        // Такой же как первый, но с меньшей стороной
        Square s3 = new Square(p1, 4);

        // Такой же как первый, но с другой точкой расположения
        Square s4 = new Square(p2, 5); 

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
            var ex = Assert.ThrowsException<RectangleInitializationException>(() =>
            new Square(new Point(1, 1), -1));
            Assert.AreEqual("Левая верхняя точка прямоугольника должна быть покоординатно левее и выше правой нижней.", ex.Message);
        }
    }
}
