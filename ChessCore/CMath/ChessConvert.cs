using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCore.CMath
{
    public static class ChessConvert
    {
        //Number = Y
        //Symbol = X

        public static Vector ToVector(Coordinate coord)
        {
            if (coord.Number != 0 && coord.Symbol != '\0') // TODO добавить ограничение букв
                return new Vector((byte)(coord.Symbol - 65), (byte)(8 - coord.Number));
            return new Vector(0, 0);
        }

        public static Coordinate ToCoordinate(Vector vector)
        {
            if (vector.X < 8 && vector.Y < 8)
                return new Coordinate((char)(vector.Y + 65), (byte)(8 - vector.X));
            return new Coordinate('\0', 8);
        }
    }
}
