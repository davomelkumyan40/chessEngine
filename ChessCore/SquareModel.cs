using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessCore.CMath;
using ChessCore.Native;

namespace ChessCore
{
    public class SquareModel
    {
        #region ctors

        public SquareModel(Vector position)
        {
            this.Position = position;
            this.Figure = null;
        }

        public SquareModel(FigureModel figure)
        {
            this.Position = figure.Position;
            this.Figure = figure;
        }

        public SquareModel(Figure type, Color color, Vector position)
        {
            this.Figure = new FigureModel(type, color, position);
            this.Position = position;
        }

        #endregion

        #region public fields

        public Vector Position { get; private set; }
        public FigureModel Figure { get; set; }

        #endregion

        #region Functionality

        public override string ToString()
        {
            return $"Pos: X = {Position.X}, Y = {Position.Y}. Figure: {Figure.Type}";
        }

        #endregion
    }
}
