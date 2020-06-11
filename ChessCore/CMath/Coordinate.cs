using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessCore.ChessExceptions;

namespace ChessCore.CMath
{
    public struct Coordinate
    {
        public Coordinate( char symbol, byte number)
        {
            if (symbol > 64 && symbol < 73 && number > 0 && number < 9)
            {
                this.Symbol = symbol;
                this.Number = number;
            }
            else
                throw new InvalidCoordinateException();
        }

        public char Symbol { get; private set; }

        public byte Number { get; private set; }


        public static bool operator ==(Coordinate f, Coordinate i) => (f.Number == i.Number && f.Symbol == i.Symbol);

        public static bool operator !=(Coordinate f, Coordinate i) => (f.Number != i.Number && f.Symbol != i.Symbol);

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return (((byte)this.Symbol) + this.Number).GetHashCode();
        }

        public override string ToString() => $"Symbol = {this.Symbol}, Number = {this.Number}";
    }
}
