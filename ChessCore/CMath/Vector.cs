using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessCore.ChessExceptions;

namespace ChessCore.CMath
{
    public struct Vector
    {
        public Vector(byte x, byte y)
        {
            if (x < 8 && y < 8)
            {
                this.X = x;
                this.Y = y;
            }
            else
                throw new InvalidVectorException();
        }

        public byte X { get; set; }
        public byte Y { get; set; }


        public static bool operator ==(Vector f, Vector i) => (f.X == i.X && f.Y == i.Y);

        public static bool operator !=(Vector f, Vector i) => (f.X != i.X && f.Y != i.Y); // !(f == i)

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return (this.X + this.Y).GetHashCode();
        }
    }
}
