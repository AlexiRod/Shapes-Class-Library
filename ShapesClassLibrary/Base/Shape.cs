namespace ShapesClassLibrary
{
    /// <summary>
    /// Абстрактный класс фигуры в двумерной плоскости
    /// </summary>
    public abstract class Shape : IAreable
    {
        protected List<Point> points = new List<Point>();
        protected List<Side> sides = new List<Side>();

        /// <summary>
        /// Метод инициализации точек, из которых состоит фигура
        /// </summary>
        protected void InitializePoints(params Point[] points)
        { 
            points.ToList().ForEach(p => this.points.Add(p));
        }

        /// <summary>
        /// Метод инициализации сторон, из которых складывается фигура. Стороны сортируются в порядке возрастания длин
        /// </summary>
        protected void InitializeSides(params Side[] sides)
        {
            sides.ToList().ForEach(s => this.sides.Add(s));
            this.sides.Sort((x, y) => x.Length.CompareTo(y.Length));
        }

        /// <summary>
        /// Метод вычисления площади фигуры
        /// </summary>
        public abstract double GetArea();
    }
}