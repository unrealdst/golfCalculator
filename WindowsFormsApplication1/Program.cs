using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LinqToExcel;

namespace WindowsFormsApplication1
{
    public class Returns
    {
        public Player[] standsStrokeplayNetto;
        public Player[] standsStrokeplayBrutto;
        public Player[] standsStablefordNetto;
        public Player[] standsStablefordBrutto;
    }

    public class Prog
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

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<Player> players = new List<Player>();

            string pathToExcelFile = "" + @"C:\Users\Cebulotron3000\Documents\Visual Studio 2013\Projects\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\excelFile.xlsx";
            string sheetName = "Arkusz1";
            var excelFile = new ExcelQueryFactory(pathToExcelFile);
            var playersInExcel = from a in excelFile.Worksheet(sheetName) select a;

            foreach (var a in playersInExcel)
            {
                players.Add(
                    new Player()
                    {
                        name = a["Name"],
                        handicap = Convert.ToInt32(a["handicap"]),
                        card = new Card()
                       {
                           scores = new int[] { Convert.ToInt32(a["h1"]), Convert.ToInt32(a["h2"]), Convert.ToInt32(a["h3"]), Convert.ToInt32(a["h4"]), Convert.ToInt32(a["h5"]), Convert.ToInt32(a["h6"]), Convert.ToInt32(a["h7"]), Convert.ToInt32(a["h8"]), Convert.ToInt32(a["h9"]), Convert.ToInt32(a["h10"]), Convert.ToInt32(a["h11"]), Convert.ToInt32(a["h12"]), Convert.ToInt32(a["h13"]), Convert.ToInt32(a["h14"]), Convert.ToInt32(a["h15"]), Convert.ToInt32(a["h16"]), Convert.ToInt32(a["h17"]), Convert.ToInt32(a["h18"]) }
                       }
                    }
                    );
            }

            Course course = new Course();

            Init(course, players);

            Prog prog = new Prog();
            Returns result = prog.Go(course, players);

            System.Diagnostics.Debug.WriteLine("\n\n\n\n\nstandsStablefordBrutto  standsStablefordNetto");
            for (int i = 0; i < players.Count; i++)
            {
                string show = String.Format("{0}:{1}                    {2}:{3}  ", result.standsStablefordBrutto[i].name, result.standsStablefordBrutto[i].finalScoreStableFordBrutto, result.standsStablefordNetto[i].name, result.standsStablefordNetto[i].finalScoreStableFordNetto);
                System.Diagnostics.Debug.WriteLine(show);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void Init(Course course, List<Player> players)
        {
            for (int i = 1; i < 18 + 1; i++)
            {
                course.AddHole(new Hole()
                {
                    hardness = i,
                    number = i,
                    par = 3
                });
            }

            course.PrintHoles();
        }
    }
}
