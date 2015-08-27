using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Course
    {
        public Hole[] holes { get; set; }

        public Course()
        {
            holes = new Hole[18];
        }

        public void AddHole(Hole newHole)
        {
            holes[newHole.number - 1] = newHole;
        }

        public void PrintHoles()
        {
            foreach (Hole hole in holes)
            {
                System.Diagnostics.Debug.WriteLine(hole.number + ": " + hole.hardness + " par: " + hole.par);

            }
        }


    }
}
