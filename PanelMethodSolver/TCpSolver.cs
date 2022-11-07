// Pressure coefficient (Cp) Finder
using AeroCalc.MeshGenerator;
using AeroCalc.Panel;
using AeroCalc.WingReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//***********************************************************************
namespace AeroCalc.PanelMethodSolver
{
    public class TCpSolver
    {
        public static double[] GetCp (string path, int XMeshSize, int ZMeshSize, double Ratio, double Taper, double Sweep, double AoA)
        {
            // Read the Wing
            double[,] ReadWing = TWingReader.GetNodes(path);

            // Generate the Mesh
            List<TPanel> MeshPanels = TMeshGenerator.MeshGenerator(ReadWing, XMeshSize, ZMeshSize, Ratio, Taper, Sweep, AoA);

            // Define Cp Matrix Size
            double[] Cp = new double[MeshPanels.Count];

            // Find Dipoles
            double[] Dipoles = TMatrixBuilder.GetSolvedMatrix(MeshPanels, XMeshSize, ZMeshSize);

            Dipoles = DipolesMirror(Dipoles, XMeshSize, ZMeshSize);

            // Solve Pressure Coefficient for each panels
            int a = 0;
            for (int i = 0; i < (ZMeshSize * 2); i++)
            {
                for (int j = 0; j < XMeshSize; j++)
                {
                    int PanelNum = i * XMeshSize + j;

                    // Find Qu (Qu = Sqrt U, U - Part of Air Flow along U Axis (Projection of X Axis on Panel Plane))
                    double Qu;

                    int PostPanel;
                    int PrePanel;

                    if ((PanelNum + 2 * XMeshSize) % (2 * XMeshSize) == 0)
                    {
                        PostPanel = PanelNum + 1;
                        PrePanel = PanelNum + 2 * XMeshSize - 1;
                    }
                    else if ((PanelNum + 1) % (2 * XMeshSize) == 0)
                    {
                        PrePanel = PanelNum - 1;
                        PostPanel = PanelNum - 2 * XMeshSize + 1;
                    }
                    else
                    {
                        PrePanel = PanelNum - 1;
                        PostPanel = PanelNum + 1;
                    }

                    double LengthQu = (MeshPanels[PanelNum].PanelPoint_1 - MeshPanels[PanelNum].PanelPoint_2).Length() +
                                      (MeshPanels[PanelNum].PanelPoint_3 - MeshPanels[PanelNum].PanelPoint_4).Length();

                    if (a % 2 == 0)
                        Qu = (Dipoles[PostPanel] - Dipoles[PrePanel]) / LengthQu;
                    else
                        Qu = (Dipoles[PrePanel] - Dipoles[PostPanel]) / LengthQu;

                    // Find Qv (Qv = Sqrt V, V - Part of Air Flow along V Axis (Projection of Z Axis on Panel Plane))
                    double Qv;

                    if (PanelNum < 2 * XMeshSize)
                    {
                        PrePanel = 2 * XMeshSize - PanelNum - 1;
                        PostPanel = PanelNum + 2 * XMeshSize;
                    }
                    else if (PanelNum >= (MeshPanels.Count - 2 * XMeshSize))
                    {
                        PrePanel = PanelNum - 2 * XMeshSize;
                        PostPanel = MeshPanels.Count - (PanelNum - 2 * (ZMeshSize - 1) * XMeshSize) - 1;
                    }
                    else
                    {
                        PrePanel = PanelNum - 2 * XMeshSize;
                        PostPanel = PanelNum + 2 * XMeshSize;
                    }

                    double LengthQv = (MeshPanels[PanelNum].PanelPoint_1 - MeshPanels[PanelNum].PanelPoint_3).Length() +
                                      (MeshPanels[PanelNum].PanelPoint_2 - MeshPanels[PanelNum].PanelPoint_4).Length();

                    Qv = (Dipoles[PrePanel] - Dipoles[PostPanel]) / LengthQv;

                    // Find Qw (Qw = Sqrt W, W - Part of Air Flow along W Axis (Normal Panel Axis))
                    double Qw = MeshPanels[PanelNum].PanelNormal.X /
                                      Math.Sqrt(Math.Pow(MeshPanels[PanelNum].PanelNormal.X, 2) +
                                                Math.Pow(MeshPanels[PanelNum].PanelNormal.Y, 2) +
                                                Math.Pow(MeshPanels[PanelNum].PanelNormal.Z, 2));

                    // Find Cp
                    double U = 0;
                    U = Math.Pow(1 + Qu, 2);
                    double V = Math.Pow(Qv, 2);
                    double W = Math.Pow(-Qw, 2);

                    Cp[PanelNum] = 1 - Math.Pow(Math.Sqrt(U + V + W), 2);
                }
                a++;
            }

            // crutch
            for (int i = 0; i < Cp.GetLength(0); i++)
            {
                if ((i + 2 * XMeshSize) % (2 * XMeshSize) == 0)
                    Cp[i] = 2 * Cp[i + 1] - Cp[i + 2];
                if ((i + 1) % (2 * XMeshSize) == 0)
                    Cp[i] = 2 * Cp[i - 1] - Cp[i - 2];
            }

            return Cp;
        }
        /// <summary>
        /// Because I have a problem with Pressure Coefficient on left half of the wing I do it
        /// Here I find Pressure Coefficient on left half as mirror from right half
        /// </summary>
        /// <param name="CpFull"></param>
        /// <returns></returns>
        public static double[] DipolesMirror(double[] Dipoles, int XMeshSize, int ZMeshSize)
        {
            // Find Cp on left half of the wing
            for (int i = 0; i < ZMeshSize / 2; i++)
            {
                for (int j = 0; j < 2 * XMeshSize; j++)
                {
                    Dipoles[i * 2 * XMeshSize + j] = Dipoles[Dipoles.GetLength(0) - (i + 1) * 2 * XMeshSize + j];
                }
            }
            return Dipoles;
        }
    }
}
