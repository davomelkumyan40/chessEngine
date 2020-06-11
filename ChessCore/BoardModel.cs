using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ChessCore.CMath;
using ChessCore.Native;

namespace ChessCore
{
    public class BoardModel
    {
        internal const byte WIDTH = 8;
        internal const byte HEIGHT = 8;
        internal const byte MAXFIGURECOUNT = 36;

        #region ctors

        public BoardModel(Color startColor, bool fillBoard = false)
        {
            SquareList = new SquareModel[HEIGHT, WIDTH];
            StepList = new Stack<KeyValuePair<Vector, Vector>>();
            EatenList = new Stack<FigureModel>(MAXFIGURECOUNT);
            playColor = startColor;
            StepsPattern = new Pattern(startColor);
            if (fillBoard)
                ResetDefault();
            else
                InitializeBoard();
        }


        #endregion

        #region private fields

        private Color playColor;

        #endregion

        #region public fields

        public SquareModel[,] SquareList { get; private set; }
        public IEnumerable<SquareModel> IterableSquareList { get; private set; }
        public Color PlayColor { get => playColor; }
        public Stack<KeyValuePair<Vector, Vector>> StepList { get; private set; }
        public Stack<FigureModel> EatenList { get; private set; }
        public Pattern StepsPattern { get; private set; }
        public bool IsMate { get; private set; } // TODO finish Mate
        public bool IsCheck { get; private set; }

        #endregion

        #region Debug

        //TODO Remove in Release
        public void SwapColor()
        {
            playColor.SwapColor();
        }

        #endregion

        #region Functionality


        public void ResetDefault()
        {
            if (PlayColor != Color.None)
            {
                playColor.SwapColor();
                for (byte y = 0; y < SquareList.GetLength(0); y++)
                {
                    byte f = 1;
                    for (byte x = 0; x < SquareList.GetLength(1); x++)
                    {
                        f = x < 5 ? f : f += 2;
                        if (y == 0 || y == 7)
                            SquareList[y, x] = new SquareModel(new FigureModel(x < 5 ? (Figure)x : (Figure)(x - f), PlayColor, new Vector(x, y)));
                        else if (y == 1 || y == 6)
                            SquareList[y, x] = new SquareModel(new FigureModel(Figure.Pawn, PlayColor, new Vector(x, y)));
                        else
                            SquareList[y, x] = new SquareModel(new Vector(x, y));
                    }
                    if (y == 4)
                        playColor.SwapColor();
                }
            }
        }


        public void InitializeBoard()
        {
            for (byte i = 0; i < HEIGHT; i++)
                for (byte j = 0; j < WIDTH; j++)
                    SquareList[i, j] = new SquareModel(new Vector(j, i));
        }


        public bool Move(FigureModel figure, SquareModel to)
        {
            if (figure != null && to != null)
            {
                if (Chess.TestMode || figure.Color == PlayColor) // TODO Remove
                {
                    if (StepsPattern.CanMove(figure, to))
                    {
                        var obstacle = GetObstacleSquare(figure, to);

                        if (obstacle != null)
                            if (obstacle.Position != to.Position || obstacle.Figure.Color == figure.Color)
                                return false;
                        if (figure.Type == Figure.Pawn)
                        {
                            var x_dif = Math.Abs(figure.Position.X - to.Position.X);
                            if(obstacle == null)
                            {
                                if (x_dif == 1)
                                    return false;
                            }
                            else
                            {
                                if (x_dif == 0)
                                    return false;
                            }
                        }
                        Swap(figure, to);
                        IterableSquareList = SquareList.AsEnumerable();
                        if (IsSafeForToMove())
                        {
                            IsCheck = IsCheckForOponent();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool Move(Vector from, Vector to) => Move(SquareList[from.Y, from.X].Figure, SquareList[to.Y, to.X]);

        public bool Move(Coordinate from, Coordinate to) => Move(ChessConvert.ToVector(from), ChessConvert.ToVector(to));

        public void Swap(FigureModel figure, SquareModel to)
        {
            StepList.Push(new KeyValuePair<Vector, Vector>(figure.Position, to.Position));
            //Swaping figure;
            if (SquareList[to.Position.Y, to.Position.X].Figure != null)
                EatenList.Push(SquareList[to.Position.Y, to.Position.X].Figure); // Taking eated figure from position and puting to List of eated for displaying

            SquareList[to.Position.Y, to.Position.X].Figure = SquareList[figure.Position.Y, figure.Position.X].Figure; // no metter is there a figure or not just replace new figure with old
            SquareList[figure.Position.Y, figure.Position.X].Figure = null;
            SquareList[to.Position.Y, to.Position.X].Figure.Position = SquareList[to.Position.Y, to.Position.X].Position;
            playColor.SwapColor();
        }

        //TODO Найти алгоритм как правильно найти угловые конечные ячейки зная кординаты и длинну
        private Vector[] GetAngleEndPoints(SquareModel square)
        {
            var temp_x = square.Position.X;
            var temp_y = square.Position.Y;
            var endPoints = new Vector[4];

            //left bottom
            while (temp_x != 0)
            {
                if (temp_y == 0)
                    break;
                temp_x--;
                temp_y--;
            }

            endPoints[0] = new Vector(temp_x, temp_y);
            temp_x = square.Position.X;
            temp_y = square.Position.Y;

            while (temp_x != 7)
            {
                if (temp_y == 0)
                    break;
                temp_x++;
                temp_y--;
            }

            endPoints[1] = new Vector(temp_x, temp_y);
            temp_x = square.Position.X;
            temp_y = square.Position.Y;

            while (temp_x != 0)
            {
                if (temp_y == 7)
                    break;
                temp_x--;
                temp_y++;
            }

            endPoints[2] = new Vector(temp_x, temp_y);
            temp_x = square.Position.X;
            temp_y = square.Position.Y;

            while (temp_x != 7)
            {
                if (temp_y == 7)
                    break;
                temp_x++;
                temp_y++;
            }

            endPoints[3] = new Vector(temp_x, temp_y);
            temp_x = square.Position.X;
            temp_y = square.Position.Y;

            return endPoints;
        }

        public bool IsSafeForToMove()
        {
            playColor.SwapColor();
            var squareKingCurrent = IterableSquareList.SingleOrDefault(s => s.Figure != null && s.Figure.Type == Figure.King && s.Figure.Color == PlayColor);

            Vector[] pathsSt = // final paths for king obstacle checking
            {
                new Vector(squareKingCurrent.Position.X, 0),
                new Vector(7, squareKingCurrent.Position.Y),
                new Vector(squareKingCurrent.Position.X, 7),
                new Vector(0, squareKingCurrent.Position.Y),
            };
            Vector[] pathsAng = GetAngleEndPoints(squareKingCurrent);
            var paths = new Vector[][] { pathsSt, pathsAng };

            if (squareKingCurrent != null)
            {
                foreach (Vector v in pathsAng)
                {
                    var obstacle = GetObstacleAngle(squareKingCurrent.Position, v);
                    if (obstacle != null)
                        if (StepsPattern.CanMove(obstacle.Figure, squareKingCurrent) && obstacle.Figure.Color != PlayColor)
                        {
                            playColor.SwapColor();
                            RollBack();
                            return false;
                        }
                }

                foreach (Vector v in pathsSt)
                {
                    var obstacle = GetObstacleStraight(squareKingCurrent.Position, v);
                    if (obstacle != null)
                        if (StepsPattern.CanMove(obstacle.Figure, squareKingCurrent) && obstacle.Figure.Color != PlayColor)
                        {
                            playColor.SwapColor();
                            RollBack();
                            return false;
                        }
                }
            }
            playColor.SwapColor();
            return true;
        }

        public bool IsCheckForOponent()
        {
            var squareKingOponent = IterableSquareList.SingleOrDefault(s => s.Figure != null && s.Figure.Type == Figure.King && s.Figure.Color == PlayColor);

            Vector[] pathsSt = // final paths for king obstacle checking
            {
                new Vector(squareKingOponent.Position.X, 0),
                new Vector(7, squareKingOponent.Position.Y),
                new Vector(squareKingOponent.Position.X, 7),
                new Vector(0, squareKingOponent.Position.Y),
            };

            Vector[] pathsAng = GetAngleEndPoints(squareKingOponent);

            if (squareKingOponent != null)
            {
                foreach (Vector v in pathsAng)
                {
                    var obstacle = GetObstacleAngle(squareKingOponent.Position, v);
                    if (obstacle != null)
                        if (StepsPattern.CanMove(obstacle.Figure, squareKingOponent) && obstacle.Figure.Color != PlayColor)
                            return true;
                }

                foreach (Vector v in pathsSt)
                {
                    var obstacle = GetObstacleStraight(squareKingOponent.Position, v);
                    if (obstacle != null)
                    {
                        if (StepsPattern.CanMove(obstacle.Figure, squareKingOponent) && obstacle.Figure.Color != PlayColor)
                            if (obstacle.Figure.Type == Figure.Pawn)
                                return false;
                        return true;
                    }
                }
            }
            return false;
        }

        public void RemoveFigure(Vector from) => SquareList[from.Y, from.X].Figure = null;

        public void RemoveFigure(Coordinate from) => RemoveFigure(ChessConvert.ToVector(from));

        public void AddFigure(FigureModel figure, Vector to) => SquareList[to.Y, to.X].Figure = figure;

        public void AddFigure(FigureModel figure, Coordinate to) => AddFigure(figure, ChessConvert.ToVector(to));

        public void RollBack()
        {
            if (StepList.Count > 0)
            {
                var lastStep = StepList.Pop();

                // just rolling back last figure witch was moved
                Swap(SquareList[lastStep.Value.Y, lastStep.Value.X].Figure, SquareList[lastStep.Key.Y, lastStep.Key.X]);
                if (EatenList.Count > 0)
                    if (EatenList.Peek().Position == lastStep.Value)    // if last eaten figure position was equal to last step final position
                        SquareList[lastStep.Value.Y, lastStep.Value.X].Figure = EatenList.Pop();  // then we repairing it to previous square
            }
        }


        //Gets obstacle of figure move
        public SquareModel GetObstacleSquare(FigureModel figure, SquareModel to)
        {
            Vector toPos = to.Position;
            switch (figure.Type)
            {
                case Figure.Rook:
                    return GetObstacleStraight(figure.Position, toPos);
                case Figure.Bishop:
                    return GetObstacleAngle(figure.Position, toPos);
                case Figure.Queen:
                    return GetObstacleQueen(figure.Position, toPos);
                case Figure.Pawn:
                    return GetObstaclePawn(figure.Position, toPos);
            }
            return null;
        }

        public SquareModel GetObstaclePawn(Vector from, Vector to)
        {
            if (SquareList[to.Y, to.X].Figure != null && SquareList[from.Y, from.X].Figure != null)
                return SquareList[to.Y, to.X];
            else if (from.Y - to.Y == 2)
            {
                for (int i = 1; i < 3; i++)
                    if (SquareList[from.Y - i, from.X].Figure != null)
                        return SquareList[from.Y - i, from.X];
            }
            else if (from.Y - to.Y == -2)
            {
                for (int i = 1; i < 3; i++)
                    if (SquareList[from.Y + i, from.X].Figure != null)
                        return SquareList[from.Y + i, from.X];
            }
            return null;
        }

        //Gets obstacle in angle of figure move direction 
        public SquareModel GetObstacleAngle(Vector from, Vector to)
        {
            int dX = from.X - to.X;
            int dY = from.Y - to.Y;

            for (int i = 1; i <= Math.Max(Math.Abs(dX), Math.Abs(dY)); i++)
            {
                if (dX > 0 && dY > 0)
                {
                    if (SquareList[from.Y - i, from.X - i].Figure != null)
                        return SquareList[from.Y - i, from.X - i];

                }
                else if (dX > 0 && dY < 0)
                {
                    if (SquareList[from.Y + i, from.X - i].Figure != null)
                        return SquareList[from.Y + i, from.X - i];

                }
                else if (dX < 0 && dY > 0)
                {
                    if (SquareList[from.Y - i, from.X + i].Figure != null)
                        return SquareList[from.Y - i, from.X + i];
                }
                else if (dX < 0 && dY < 0)
                {
                    if (SquareList[from.Y + i, from.X + i].Figure != null)
                        return SquareList[from.Y + i, from.X + i];
                }
            }
            return null;
        }

        //Gets obstacle in horizontal or vertical direction of figure move
        public SquareModel GetObstacleStraight(Vector from, Vector to)
        {
            int dX = from.X - to.X;
            int dY = from.Y - to.Y;

            for (int i = 1; i <= Math.Max(Math.Abs(dX), Math.Abs(dY)); i++)
            {
                if (dX > 0)
                {
                    if (SquareList[from.Y, from.X - i].Figure != null)
                        return SquareList[from.Y, from.X - i];
                }
                else if (dX < 0)
                {
                    if (SquareList[from.Y, from.X + i].Figure != null)
                        return SquareList[from.Y, from.X + i];
                }
                else if (dY > 0)
                {
                    if (SquareList[from.Y - i, from.X].Figure != null)
                        return SquareList[from.Y - i, from.X];
                }
                else if (dY < 0)
                {
                    if (SquareList[from.Y + i, from.X].Figure != null)
                        return SquareList[from.Y + i, from.X];
                }
            }
            return null;
        }

        public SquareModel GetObstacleQueen(Vector from, Vector to)
        {
            if (Math.Abs(from.X - to.X) == Math.Abs(from.Y - to.Y))
                return GetObstacleAngle(from, to);
            else
                return GetObstacleStraight(from, to);
        }

        #endregion

    }
}
