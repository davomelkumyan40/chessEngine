using System;
using System.Collections.Generic;
using ChessCore.CMath;
using ChessCore.Native;

namespace ChessCore.Models
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
            PossibleSteps = new List<Vector>(); // TODO add capacity
        }

        #endregion

        #region public fields

        public Vector Position { get; set; }
        public Figure Type { get; private set; }
        public Figure Transform { get; set; }
        public Color Color { get; private set; }
        public List<Vector> PossibleSteps { get; private set; }

        #endregion

        public override string ToString()
        {
            return $"Type: {Type}, Color: {Color} Position: {Position}";
        }
    }
}
