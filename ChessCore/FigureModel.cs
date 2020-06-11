using System;
using ChessCore.CMath;
using ChessCore.Native;

namespace ChessCore
{
    public class FigureModel
    {
        #region ctor

        public FigureModel(Figure type, Color color, Vector position)
        {
            this.Position = position;
            this.Color = color;
            this.Transform = Figure.None;
            this.Type = type;
        }

        #endregion

        #region public fields

        public Vector Position { get; set; }
        public Figure Type { get; private set; }
        public Figure Transform { get; set; }
        public Color Color { get; private set; }

        #endregion

        public override string ToString()
        {
            return $"Type: {Type}, Color: {Color} Position: {Position}";
        }
    }
}
