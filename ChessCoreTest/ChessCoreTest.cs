using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ChessCore;
using ChessCore.Native;
using ChessCore.CMath;
using System.Collections.Generic;

namespace ChessCoreTest
{
    [TestClass]
    public class ChessCoreTest
    {
        [TestMethod]
        public void Pawn_Double_Steps_Valid()
        {
            //---arrange организовать

            var path1 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('A', 2), new Coordinate('A', 4));
            var path2 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('B', 2), new Coordinate('B', 4));
            var path3 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('C', 2), new Coordinate('C', 4));
            var path4 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('D', 2), new Coordinate('D', 4));
            var path5 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('E', 2), new Coordinate('E', 4));
            var path6 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('F', 2), new Coordinate('F', 4));
            var path7 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('G', 2), new Coordinate('G', 4));
            var path8 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('H', 2), new Coordinate('H', 4));

            bool expected = true;

            //---act  действие
            Chess c = new Chess(Color.Black, true);

            var r1 = c.Board.Move(path1.Key, path1.Value);
            c.Board.SwapColor();
            var r2 = c.Board.Move(path2.Key, path2.Value);
            c.Board.SwapColor();
            var r3 = c.Board.Move(path3.Key, path3.Value);
            c.Board.SwapColor();
            var r4 = c.Board.Move(path4.Key, path4.Value);
            c.Board.SwapColor();
            var r5 = c.Board.Move(path5.Key, path5.Value);
            c.Board.SwapColor();
            var r6 = c.Board.Move(path6.Key, path6.Value);
            c.Board.SwapColor();
            var r7 = c.Board.Move(path7.Key, path7.Value);
            c.Board.SwapColor();
            var r8 = c.Board.Move(path8.Key, path8.Value);

            //---assert утверждение

            Assert.AreEqual(expected, r1);
            Assert.AreEqual(expected, r2);
            Assert.AreEqual(expected, r3);
            Assert.AreEqual(expected, r4);
            Assert.AreEqual(expected, r5);
            Assert.AreEqual(expected, r6);
            Assert.AreEqual(expected, r7);
            Assert.AreEqual(expected, r1);
        }


        [TestMethod]
        public void Pawn_One_Step_Valid()
        {
            //---arrange организовать

            var path1 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('A', 2), new Coordinate('A', 3));
            var path2 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('B', 2), new Coordinate('B', 3));
            var path3 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('C', 2), new Coordinate('C', 3));
            var path4 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('D', 2), new Coordinate('D', 3));
            var path5 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('E', 2), new Coordinate('E', 3));
            var path6 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('F', 2), new Coordinate('F', 3));
            var path7 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('G', 2), new Coordinate('G', 3));
            var path8 = new KeyValuePair<Coordinate, Coordinate>(new Coordinate('H', 2), new Coordinate('H', 3));

            var expected = true;

            //---act действие

            Chess c = new Chess(Color.White, true);

            var r1 = c.Board.Move(path1.Key, path1.Value);
            c.Board.SwapColor();
            var r2 = c.Board.Move(path2.Key, path2.Value);
            c.Board.SwapColor();
            var r3 = c.Board.Move(path3.Key, path3.Value);
            c.Board.SwapColor();
            var r4 = c.Board.Move(path4.Key, path4.Value);
            c.Board.SwapColor();
            var r5 = c.Board.Move(path5.Key, path5.Value);
            c.Board.SwapColor();
            var r6 = c.Board.Move(path6.Key, path6.Value);
            c.Board.SwapColor();
            var r7 = c.Board.Move(path7.Key, path7.Value);
            c.Board.SwapColor();
            var r8 = c.Board.Move(path8.Key, path8.Value);

            //---assert утверждение

            Assert.AreEqual(expected, r1);
            Assert.AreEqual(expected, r2);
            Assert.AreEqual(expected, r3);
            Assert.AreEqual(expected, r4);
            Assert.AreEqual(expected, r5);
            Assert.AreEqual(expected, r6);
            Assert.AreEqual(expected, r7);
            Assert.AreEqual(expected, r8);
        }

        [TestMethod]
        public void Pawn_All_Steps_Not_Valid()
        {
            //---arrange организовать

            List<KeyValuePair<Coordinate, Coordinate>> pathList = new List<KeyValuePair<Coordinate, Coordinate>>();

            for (char m = 'A'; m < 'I'; m++)
            {
                for (char f = 'A'; f < 'I'; f++)
                {
                    for (byte j = 1; j < 9; j++)
                    {
                        if (j > 1 && j < 5)
                            j = 5;
                        pathList.Add(new KeyValuePair<Coordinate, Coordinate>(new Coordinate(m, 2), new Coordinate(f, j)));
                    }
                }
            }

            var expected = false;

            //---act действие

            Chess c = new Chess(Color.White);
            var resList = new List<bool>(pathList.Count);

            foreach (var path in pathList)
            {
               resList.Add(c.Board.Move(path.Key, path.Value));
            }

            //---assert утверждение

            foreach (var res in resList)
            {
                Assert.AreEqual(expected, res);
            }
        }
    }
}
