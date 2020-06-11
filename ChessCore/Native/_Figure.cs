using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCore.Native
{
    public enum Figure :  uint
    {
        Rook = 0,
        Knight,
        Bishop,
        Queen,
        King,
        Pawn,
        None,
    }
}
