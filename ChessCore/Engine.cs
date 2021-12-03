using System;
using ChessCore.CMath;
using ChessCore.Native;
using ChessCore.Models;

namespace ChessCore
{
    public class Engine
    {
        #region ctor

        public Engine(Color startColor, bool fillBoard = false)
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
