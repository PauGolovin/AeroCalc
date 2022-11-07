using AeroCalc.MeshGenerator;
using AeroCalc.Panel;
using AeroCalc.WingReader;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AeroCalc.PanelMethodSolver
{
    public class TCyCxSolver
    {
        /// <summary>
        /// Here Lift Coefficient Cy and Drag Coefficient Cx will be found
        /// </summary>
        /// <param name="path"></param>
        /// <param name="XMeshSize"></param>
        /// <param name="ZMeshSize"></param>
        /// <param name="Ratio"></param>
        /// <param name="Taper"></param>
        /// <param name="Sweep"></param>
        /// <param name="AoA"></param>
        /// <returns></returns>
        public static Tuple<double, double> GetCyCx(string path, int XMeshSize, int ZMeshSize, double Ratio, double Taper, double Sweep, double AoA)
        {
            double Cy = 0;
            double Cx = 0;
            
            // Read the Wing
            double[,] ReadWing = TWingReader.GetNodes(path);

            // Generate the Mesh
            List<TPanel> MeshPanels = TMeshGenerator.MeshGenerator(ReadWing, XMeshSize, ZMeshSize, Ratio, Taper, Sweep, AoA);

            // Get Cp
            double[] Cp = TCpSolver.GetCp(path, XMeshSize, ZMeshSize, Ratio, Taper, Sweep, AoA);


            double TopSurfaceCp = 0;
            double BottomSurfaceCp = 0;

            // Find Wing Planned Projection Area
            double PlannedProjectionArea = 0;

            for (int i = 0; i < Cp.Length; i++)
            {
                TPanel PlanPanel = new TPanel(new Vector3(MeshPanels[i].PanelPoint_1.X, 0, MeshPanels[i].PanelPoint_1.Z),
                                              new Vector3(MeshPanels[i].PanelPoint_2.X, 0, MeshPanels[i].PanelPoint_2.Z),
                                              new Vector3(MeshPanels[i].PanelPoint_3.X, 0, MeshPanels[i].PanelPoint_3.Z),
                                              new Vector3(MeshPanels[i].PanelPoint_4.X, 0, MeshPanels[i].PanelPoint_4.Z));
                PlannedProjectionArea += PlanPanel.Area;
            }

            PlannedProjectionArea /= 2;

            // Find pressure coefficient for top and bottom wing surface 
            for (int i = 0; i < ZMeshSize; i++)
            {
                for (int j = 0; j < XMeshSize; j++)
                {
                    BottomSurfaceCp += Cp[i * 2 * XMeshSize + j] * MeshPanels[i * 2 * XMeshSize + j].Area / PlannedProjectionArea;
                    TopSurfaceCp += Cp[(i * 2 + 1) * XMeshSize + j] * MeshPanels[(i * 2 + 1) * XMeshSize + j].Area / PlannedProjectionArea;
                }
            }

            // Get Cy
            Cy = BottomSurfaceCp - TopSurfaceCp;
            if (Math.Abs(Cy) < 0.0001)
                Cy = 0;

            // Find full wing area
            double FullArea = 0;
            for (int i = 0; i < MeshPanels.Count; i++)
            {
                FullArea += MeshPanels[i].Area;
            }

            // Find Drag Coefficien when Y = 0 Cx0
            double Cx0 = 0.003 * FullArea / PlannedProjectionArea;

            // Find drag-due-to-lift factor A
            double A = 1 / (Math.PI * Ratio);

            // Get Cx
            Cx = Cx0 + A * Math.Pow(Cy, 2);

            Tuple<double, double> CyCx = new Tuple<double, double>(Cy, Cx);

            return CyCx;
        }

    }
}
