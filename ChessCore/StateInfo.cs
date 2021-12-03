using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessCore.Models;
using ChessCore.Native;

namespace ChessCore
{
    public class StateInfo
    {
        public StateInfo(bool isCheck, bool isMate = false)
        {
            this.IsMate = isMate;
            this.IsCheck = isCheck;
            this.ThreatenFigures = this.ThreatenFigures ?? new List<FigureModel>();
        }


        public bool IsMate { get; set; }
        public bool IsCheck { get; set; }
        public List<FigureModel> ThreatenFigures { get; set; }
    }
}
