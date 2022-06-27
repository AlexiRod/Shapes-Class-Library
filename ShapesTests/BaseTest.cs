using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesClassLibrary;
using System;
using System.Collections.Generic;

namespace ShapesTests
{
    /// <summary>
    /// “естирование базовых классов библиотеки
    /// </summary>
    [TestClass]
    public class BaseTest
    {
        /// “естирование методов Equals и GetHashCode у точки
        [TestMethod]
        public void TestPointEquality()
        {
            Point p1 = new Point(1.3, 2.2);
            Point p2 = new Point(1.3, 2.2);
            Point p3 = new Point(3.7, -9.2);

            Assert.IsTrue(p1.Equals(p2));
            Assert.IsFalse(p1.Equals(p3));
            Assert.IsFalse(p1.Equals(null));
            Assert.IsFalse(p1.Equals("test"));

            Assert.AreEqual(p1.GetHashCode(), p2.GetHashCode());
            Assert.AreNotEqual(p1.GetHashCode(), p3.GetHashCode());
        }

        /// <summary>
        /// “естирование методов Equals и GetHashCode у стороны
        /// </summary>
        [TestMethod]
        public void TestSideEquality()
        {
            Point p1 = new Point(1.3, 2.2);
            Point p2 = new Point(1.3, 2.2);
            Point p3 = new Point(3.7, -9.2);
            Point p4 = new Point(-4.3, 1.1);

            Side s1 = new Side(p1, p3);
            Side s2 = new Side(p3, p2);
            Side s3 = new Side(p2, p4);
            Side s4 = new Side(p1.X, p2.Y, p3.X, p3.Y);


            Assert.IsTrue(s1.Equals(s2));
            Assert.IsTrue(s1.Equals(s4));
            Assert.IsFalse(s1.Equals(s3));
            Assert.IsFalse(s1.Equals(null));
            Assert.IsFalse(s1.Equals("test"));

            Assert.AreEqual(s1.GetHashCode(), s2.GetHashCode());
            Assert.AreEqual(s2.GetHashCode(), s4.GetHashCode());
            Assert.AreNotEqual(s1.GetHashCode(), s3.GetHashCode());

            Assert.AreEqual(s1.Length, s2.Length);
            Assert.AreEqual(s2.Length, s4.Length);
            Assert.AreNotEqual(s1.Length, s3.Length);
        }

        /// <summary>
        /// “естирование метода вычислени€ площади у разных наследников IAreable без знани€ типа фигуры
        /// </summary>
        [TestMethod]
        public void TestIAreableMethods()
        {
            IAreable t1 = new Triangle(new Point(0, 2), new Point(2, -2.5), new Point(-4.4, 1.2));
            Triangle t2 = new Triangle(new Point(0, 2), new Point(2, -2.5), new Point(-4.4, 1.2));
            IAreable c1 = new Circle(new Point(3, 1), 5.4);
            IAreable r1 = new Rectangle(new Point(3.1, 1), new Point(5.2, 4));
            List<IAreable> areables = new List<IAreable>() { t1, c1, r1 };

            Assert.AreEqual(t1.GetArea(), t2.GetArea());
            Assert.AreNotEqual(t1.GetArea(), c1.GetArea());
            Assert.IsTrue(Math.Abs(areables[0].GetArea() - 10.7) < 0.00000001);
            Assert.IsTrue(Math.Abs(areables[1].GetArea() - 5.4 * 5.4 * Math.PI) < 0.00000001);
            Assert.IsTrue(Math.Abs(areables[2].GetArea() - 2.1 * 3) < 0.00000001);
        }
    }
}