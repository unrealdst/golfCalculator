using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{

    public class Returns
    {
        public Player[] standsStrokeplayNetto;
        public Player[] standsStrokeplayBrutto;
        public Player[] standsStablefordNetto;
        public Player[] standsStablefordBrutto;
    }


    public class Calculate
    {
        private const int LENGTH_OF_COURSE = 18;



        public Returns Go(Course course, List<Player> players)
        {
            PrintPlayer(players);

            CalculatePlayerPoints(course, players);

            Returns ret = new Returns();

            ret.standsStrokeplayNetto = players.OrderBy(x => x.FinalScoreStrokePlaynetto).ToArray();
            ret.standsStrokeplayBrutto = players.OrderBy(x => x.FinalScoreStrokePlayBrutto).ToArray();
            ret.standsStablefordNetto = players.OrderByDescending(x => x.finalScoreStableFordNetto).ToArray();
            ret.standsStablefordBrutto = players.OrderByDescending(x => x.finalScoreStableFordBrutto).ToArray();

            return ret;
        }

        private void CalculatePlayerPoints(Course course, List<Player> players)
        {
            foreach (Player p in players)
            {
                CalculateFinalScoreStrokePlayeBrutto(p);
                CalculateFinalScoreStrokePlayeNetto(p);
                CalculateFinalScoreStableFordNetto(p, course);
                CalculateFinalScoreStableFordBrutto(p, course);
            }
        }

        private void CalculateFinalScoreStrokePlayeNetto(Player p)
        {
            p.FinalScoreStrokePlaynetto = p.FinalScoreStrokePlayBrutto - p.handicap;
        }

        private void CalculateFinalScoreStrokePlayeBrutto(Player p)
        {
            foreach (int points in p.card.scores)
            {
                p.FinalScoreStrokePlayBrutto = p.FinalScoreStrokePlayBrutto + points;

            }
        }

        private void CalculateFinalScoreStableFordNetto(Player p, Course course)
        {
            p.CalcualteAdditionalHandicapeForStableFordNetto();
            for (int i = 0; i < LENGTH_OF_COURSE; i++)
            {
                int tempAddHandicap = 0;
                tempAddHandicap = p.additinalHandicap.additinalForAll;
                if (course.holes[i].hardness <= p.additinalHandicap.amountOfAdditianlHoles)
                {
                    tempAddHandicap = tempAddHandicap + 1;
                }

                int tempScore = AdditinalTempScore(p, i, tempAddHandicap, course);

                p.finalScoreStableFordNetto = p.finalScoreStableFordNetto + tempScore;
            }
        }

        private void CalculateFinalScoreStableFordBrutto(Player p, Course course)
        {
            p.CalcualteAdditionalHandicapeForStableFordNetto();
            for (int i = 0; i < LENGTH_OF_COURSE; i++)
            {
                int tempAddHandicap = 0;

                int tempScore = AdditinalTempScore(p, i, tempAddHandicap, course);

                p.finalScoreStableFordBrutto = p.finalScoreStableFordBrutto + tempScore;
            }
        }

        private int AdditinalTempScore(Player p, int i, int tempAddHandicap, Course course)
        {
            int tempScore = (((course.holes[i].par + tempAddHandicap) - p.card.scores[i]) + 2);

            if (tempScore < 0 || p.card.scores[i] == 0)
            {
                return 0;
            }

            return tempScore;
        }

        private void PrintPlayer(List<Player> players)
        {
            foreach (Player player in players)
            {
                System.Diagnostics.Debug.WriteLine(player.name + ": " + player.handicap);
            }
        }
    }

}
