using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesClassLibrary.Exceptions
{
    public class SidesInitializationException : ArgumentException
    {
        public SidesInitializationException(string mes) : base(mes) { }
    }
}
