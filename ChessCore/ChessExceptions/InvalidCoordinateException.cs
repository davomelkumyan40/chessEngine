using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCore.ChessExceptions
{
    public class InvalidCoordinateException : Exception
    {
        public override string Message => "Invalid 'Coordinate' values {Number}, or {Symbol}";
    }
}
