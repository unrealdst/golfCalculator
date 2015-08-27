using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    [DebuggerDisplay("{name}")]
    public class Player
    {
        public string name;
        public int handicap;
        public Card card;
        public int FinalScoreStrokePlayBrutto;
        public int FinalScoreStrokePlaynetto;

        public int finalScoreStableFordNetto;
        public int finalScoreStableFordBrutto;

        public AdditinalHandicap additinalHandicap;
        
        public Player()
        {
            additinalHandicap = new AdditinalHandicap();
        }

        public void CalcualteAdditionalHandicapeForStableFordNetto()
        {
            additinalHandicap.additinalForAll = (int)Math.Floor((double)(handicap / card.scores.Length));
            additinalHandicap.amountOfAdditianlHoles = (handicap % 18);

        }
    }
}
