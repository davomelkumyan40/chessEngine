using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessCore.CMath;
using ChessCore.Native;

namespace ChessCore.Models
{
    //TODO finish color auto generation
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
