using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesClassLibrary.Exceptions
{
    public class CircleInitializationException : ArgumentException
    {
        public CircleInitializationException(string mes) : base(mes) { }
    }
}
