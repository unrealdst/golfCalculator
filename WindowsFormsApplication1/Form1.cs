using LinqToExcel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Returns result;

        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> stands = new List<string>();

            switch (comboBox1.Text)
            {
                case ("Stableford Brutto"):
                    for (int i = 0; i < result.standsStablefordBrutto.Count(); i++)
                    {
                        Player tempPLayer = result.standsStablefordBrutto[i];
                        stands.Add(String.Format(@"{1} :({2} {0}", tempPLayer.name, tempPLayer.finalScoreStableFordBrutto, (tempPLayer.handicap + (tempPLayer.handicap > 9 ? ")" : ")  "))));
                    }
                    break;

                case ("Stableford Netto"):
                    for (int i = 0; i < result.standsStablefordNetto.Count(); i++)
                    {
                        Player tempPLayer = result.standsStablefordNetto[i];
                        stands.Add(String.Format(@"{1} :({2} {0}", tempPLayer.name, tempPLayer.finalScoreStableFordBrutto, (tempPLayer.handicap + (tempPLayer.handicap > 9 ? ")" : ")  "))));
                    }
                    break;

                case ("Strokeplay Brutto"):
                    for (int i = 0; i < result.standsStrokeplayBrutto.Count(); i++)
                    {
                        Player tempPLayer = result.standsStrokeplayBrutto[i];
                        stands.Add(String.Format(@"{1} :({2} {0}", tempPLayer.name, tempPLayer.finalScoreStableFordBrutto, (tempPLayer.handicap + (tempPLayer.handicap > 9 ? ")" : ")  "))));
                    }
                    break;

                case ("Strokeplay Netto"):
                    for (int i = 0; i < result.standsStrokeplayNetto.Count(); i++)
                    {
                        Player tempPLayer = result.standsStrokeplayNetto[i];
                        stands.Add(String.Format(@"{1} :({2} {0}", tempPLayer.name, tempPLayer.finalScoreStableFordBrutto, (tempPLayer.handicap + (tempPLayer.handicap > 9 ? ")" : ")  "))));
                    }
                    break;
            }

            listBox1.DataSource = stands;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            string path = "";
            DialogResult fileToOpenDialog = openFileDialog1.ShowDialog();
            if (fileToOpenDialog == DialogResult.OK) // Test result.
            {
                path = openFileDialog1.FileName;
            }

            textBox1.Text = path;

            List<Player> players = new List<Player>();

            string pathToExcelFile = textBox1.Text;
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

            Init(course);

            var prog = new Calculate();
            result = prog.Go(course, players);
        }

        private static void Init(Course course)
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
