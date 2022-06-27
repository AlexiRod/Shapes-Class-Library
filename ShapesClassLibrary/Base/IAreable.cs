using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesClassLibrary
{
    /// <summary>
    /// Интерфейс с методом вычисления площади фигуры. 
    /// Впоследствии может быть использован для фигур в N-мерном пространстве и дополнен иными методами
    /// </summary>
    public interface IAreable
    {
        public double GetArea();
    }
}
