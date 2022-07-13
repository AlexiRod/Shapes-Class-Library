using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesClassLibrary.Exceptions
{
    public class TriangleRuleException : ArgumentException
    {
        public TriangleRuleException(string mes) : base(mes) { }
    }
}
