using ChessCore.CMath;
using ChessCore.Native;
using System;
using System.Text;

namespace ChessCore
{
    public class Pattern
    {
        #region private
        private Color startColor;
        #endregion

        public Pattern(Color startColor)
        {
            this.startColor = startColor;
        }

        public bool CanMove(FigureModel figure, SquareModel to)
        {
            switch (figure.Type)
            {
                case Figure.Pawn:
                    return CanMovePawn(figure, to.Position);
                case Figure.Rook:
                    return CanMoveRook(figure, to.Position);
                case Figure.Knight:
                    return CanMoveKnight(figure, to.Position);
                case Figure.Bishop:
                    return CanMoveBishop(figure, to.Position);
                case Figure.Queen:
                    return CanMoveQueen(figure, to.Position);
                case Figure.King:
                    return CanMoveKing(figure, to.Position);
                default:
                    return false;
            }
        }

        //Fixed
        private bool CanMovePawn(FigureModel figure, Vector to)
        {
            //If pawn ain't going back then
            if (startColor == figure.Color)
            {
                if (figure.Position.Y - to.Y < 0) // if start color is white and pawn going to back
                    return false;
            }
            else
            {
                if (figure.Position.Y - to.Y > 0) // if start color is not white and pawn going to back
                    return false;
            }
            //continue

            if (Math.Abs(figure.Position.Y - to.Y) == 1)
                return true;
            else if ((7 - figure.Position.Y == 1 && startColor == figure.Color) || (7 - (7 - figure.Position.Y) == 1 && startColor != figure.Color)) // 7 is last index
                if (Math.Abs(figure.Position.Y - to.Y) == 2)
                    return true;
            return false;
        }

        //Fixed
        private bool CanMoveBishop(FigureModel figure, Vector to)
        {
            for (byte i = 1; i <= Math.Abs(figure.Position.X - to.X); i++)
                if ((figure.Position.X + i == to.X && figure.Position.Y + i == to.Y) ||
                    (figure.Position.X - i == to.X && figure.Position.Y - i == to.Y) ||
                    (figure.Position.X + i == to.X && figure.Position.Y - i == to.Y) ||
                    (figure.Position.X - i == to.X && figure.Position.Y + i == to.Y))
                    return true;
            return false;
        }

        private bool CanMoveKnight(FigureModel figure, Vector to)
        {
            var x = Math.Abs(figure.Position.X - to.X);
            var y = Math.Abs(figure.Position.Y - to.Y);
            if (x == 2 && y == 1 || x == 1 && y == 2)
                return true;
            return false;
        }

        //Fixed
        private bool CanMoveRook(FigureModel figure, Vector to)
        {
            if ((figure.Position.X == to.X && figure.Position.Y != to.Y) || (figure.Position.X != to.X && figure.Position.Y == to.Y))
                return true;
            return false;
        }

        //Fixed
        private bool CanMoveQueen(FigureModel figure, Vector to)
        {
            if (Math.Abs(figure.Position.X - to.X) == Math.Abs(figure.Position.Y - to.Y))
                return CanMoveBishop(figure, to);
            else
                return CanMoveRook(figure, to);
        }

        private bool CanMoveKing(FigureModel figure, Vector to)
        {
            var x = Math.Abs(figure.Position.X - to.X);
            var y = Math.Abs(figure.Position.Y - to.Y);
            if (x == 1 || y == 1)
                return true;
            return false;
        }
    }
}
