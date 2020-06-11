using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCore.ChessExceptions
{
    public class InvalidVectorException : Exception
    {
        public override string Message => "Invalid 'Vector' values {x}, or {y}";
    }
}
