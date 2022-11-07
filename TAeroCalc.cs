//Aerodynamic Calculation by Panel Method
using AeroCalc.MeshGenerator;
using AeroCalc.Panel;
using AeroCalc.PanelMethodSolver;
using AeroCalc.WingReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//***********************************************************************************************
namespace AeroCalc
{
    class TAeroCalc
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AeroCalc());

            //using (StreamWriter sw = new StreamWriter(@"D:\C Sharp\Panel_Solver\AeroCalc\Validation\Mynk-12_Cy_Cx.txt"))
            //{
            //    for (double i = -3; i <= 21; i += 1.5)
            //    {
            //        (double Cy, double Cx) = TCyCxSolver.GetCyCx("D:\\C Sharp\\Panel_Solver\\AeroCalc\\Validation\\Wing_Mynk-12.xlsx", 10, 10, 5, 1, 0, i);
            //        sw.WriteLine(Cy + "\t" + Cx);
            //    }
            //}
        }
    }
}