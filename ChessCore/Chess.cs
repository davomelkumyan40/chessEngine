using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessCore.CMath;
using ChessCore.Native;

namespace ChessCore
{
    public class Chess
    {
        #region ctor

        public Chess(Color startColor, bool fillBoard = false)
        {
            Board = new BoardModel(startColor, fillBoard);
            StartColor = startColor;
        }

        #endregion

        #region public fields

        public static bool TestMode { get; set; }
        public BoardModel Board { get; private set; }

        public Color StartColor { get; private set; }

        public override string ToString()
        {
            return $"Start Color: {StartColor}";
        }

        #endregion
    }
}
