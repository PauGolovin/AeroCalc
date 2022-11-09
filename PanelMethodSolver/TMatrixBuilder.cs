// Build Left and Right Matrix for the Equation
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroCalc.Panel;
using System.Numerics;
using static System.Windows.Forms.DataFormats;
using System.Drawing.Drawing2D;
//***********************************************************************
namespace AeroCalc.PanelMethodSolver
{
    public class TMatrixBuilder
    {
        /// <summary>
        /// Move panel that panel center will be in the coordinate system origin and Panel Normal will be along Y Axis
        /// </summary>
        /// <param name="Panel"></param>
        /// <returns></returns>
        public static Tuple<TPanel, Vector3> TurnAndMove (TPanel Panel, Vector3 CollocationPoint)
        {
            // Define Moved Panel (Center of Panel must be in the Coordinate System Origin)
            Vector3 Point1 = Vector3.Subtract(Panel.PanelPoint_1, Panel.PanelCenter);
            Vector3 Point2 = Vector3.Subtract(Panel.PanelPoint_2, Panel.PanelCenter);
            Vector3 Point3 = Vector3.Subtract(Panel.PanelPoint_3, Panel.PanelCenter);
            Vector3 Point4 = Vector3.Subtract(Panel.PanelPoint_4, Panel.PanelCenter);

            Vector3 ColPoint = Vector3.Subtract(CollocationPoint, Panel.PanelCenter);

            TPanel MovedPanel = new TPanel(Point1, Point2, Point3, Point4);

            if (MovedPanel.PanelNormal.X == 0 && MovedPanel.PanelNormal.Z == 0)
                return new Tuple<TPanel, Vector3> (MovedPanel, ColPoint);

            // Define Turned Panel (Normal of Panel must be along Y axis)

            // Define Alpha Angle for a first turn (Angle between Panel Normal Proection on XZ Plane and X axis)

            double Alpha = Math.Acos(MovedPanel.PanelNormal.X /
                                     Math.Sqrt(Math.Pow(MovedPanel.PanelNormal.X, 2) + 
                                               Math.Pow(MovedPanel.PanelNormal.Z, 2)));

            if (MovedPanel.PanelNormal.Z < 0)
                Alpha = -Alpha;

            // Define Beta Angle for a second turn (Angle between Panel Normal and Y axis)

            double Beta = Math.Acos(MovedPanel.PanelNormal.Y /
                                     Math.Sqrt(Math.Pow(MovedPanel.PanelNormal.X, 2) +
                                               Math.Pow(MovedPanel.PanelNormal.Y, 2) +
                                               Math.Pow(MovedPanel.PanelNormal.Z, 2)));

            if (Beta > Math.PI / 2)
                Beta += Math.PI;

            // Create Rotation Matrices

            var YRotation = Matrix4x4.CreateRotationY((float)Alpha);
            var ZRotation = Matrix4x4.CreateRotationZ((float)Beta);

            // First turn
            Point1 = Vector3.Transform(Point1, YRotation);
            Point2 = Vector3.Transform(Point2, YRotation);
            Point3 = Vector3.Transform(Point3, YRotation);
            Point4 = Vector3.Transform(Point4, YRotation);

            ColPoint = Vector3.Transform(ColPoint, YRotation);

            // Second turn
            Point1 = Vector3.Transform(Point1, ZRotation);
            Point2 = Vector3.Transform(Point2, ZRotation);
            Point3 = Vector3.Transform(Point3, ZRotation);
            Point4 = Vector3.Transform(Point4, ZRotation);

            ColPoint = Vector3.Transform(ColPoint, ZRotation);

            TPanel Panel_Turned_And_Moved = new TPanel(Point1, Point2, Point3, Point4);
            
            Tuple<TPanel, Vector3> TurnedAndMove = new Tuple<TPanel, Vector3> (Panel_Turned_And_Moved, ColPoint); 

            return TurnedAndMove;
        }
        /// <summary>
        /// Create Left Matrix
        /// </summary>
        /// <param name="Panels"></param>
        /// <param name="XMeshSize"></param>
        /// <param name="ZMeshSize"></param>
        /// <returns></returns>
        public static double[,] LeftMatrix (List<TPanel> Panels, int XMeshSize, int ZMeshSize)
        {
            // Define Left Matrix Size
            double[,] LeftMatrix = new double[Panels.Count, Panels.Count];

            // Define new List of panels with Wake panels
            List<TPanel> Panels_Wake = new List<TPanel>();
            List<TPanel> Wake = new List<TPanel> ();

            // Define Length of Wake
            float WakeLength = 3 * Math.Abs(Panels[Panels.Count - 1].PanelPoint_3.Z - Panels[0].PanelPoint_1.Z);

            // Build Wake Panels
            for (int i = 0; i < ZMeshSize; i++)
            {
                Vector3 Point1 = Panels[i * 2 * XMeshSize].PanelPoint_2;
                Vector3 Point2 = new Vector3(Point1.X + WakeLength, Point1.Y, Point1.Z);
                Vector3 Point3 = Panels[i * 2 * XMeshSize].PanelPoint_4;
                Vector3 Point4 = new Vector3(Point3.X + WakeLength, Point3.Y, Point3.Z);

                Wake.Add(new TPanel(Point1, Point2, Point3, Point4));
            }

            // Make new panels list with Wake panels
            Panels_Wake.AddRange(Panels);
            Panels_Wake.AddRange(Wake);

            // Create Big Left Matrix
            double[,] LeftMatrixBig = new double[Panels_Wake.Count, Panels_Wake.Count];
            for (int i = 0; i < Panels_Wake.Count; i++)
            {
                for (int j = 0; j < Panels_Wake.Count; j++)
                {
                    (TPanel panel, Vector3 pk) = TurnAndMove(Panels_Wake[j], Panels_Wake[i].PanelCenter);

                    // Create an Matrix Element
                    Vector3 p0 = (panel.PanelPoint_1 + panel.PanelPoint_3 + panel.PanelPoint_2 + panel.PanelPoint_4) / 4;
                    Vector3 p1 = (panel.PanelPoint_1 + panel.PanelPoint_3 - panel.PanelPoint_2 - panel.PanelPoint_4) / 4;
                    Vector3 p2 = (panel.PanelPoint_1 - panel.PanelPoint_3 + panel.PanelPoint_2 - panel.PanelPoint_4) / 4;
                    Vector3 p3 = (panel.PanelPoint_1 - panel.PanelPoint_3 - panel.PanelPoint_2 + panel.PanelPoint_4) / 4;

                    double PD = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        int Y = 1;
                        int X = 1;
                        if (k % 2 == 0)
                            Y = -1;
                        if (k > 1)
                            X = -1;

                        Vector3 R = p0 - pk + X * p1 + Y * p2 + X * Y * p3;
                        Vector3 A1 = p1 + p3 * Y;
                        Vector3 A2 = p2 + p3 * X;
                        Vector3 RA1 = Vector3.Cross(R, A1);
                        Vector3 RA2 = Vector3.Cross(R, A2);
                        Vector3 A12 = Vector3.Cross(A1, A2);

                        double AM = R.Length();
                        double SA = R.X * A12.X +
                                    R.Y * A12.Y +
                                    R.Z * A12.Z;
                        double B1 = RA1.X * RA2.X +
                                    RA1.Y * RA2.Y +
                                    RA1.Z * RA2.Z;
                        double SAM = SA * AM;
                        double AID = Math.Atan(B1 / SAM);
                        PD += X * Y * AID;
                    }
                    LeftMatrixBig[i, j] = PD / (4 * Math.PI);

                    // Define Matrix Diagonal Elements
                    if (i == j)
                        LeftMatrixBig[i, j] = -0.5;
                }
                for (int n = 0; n < ZMeshSize; n++)
                {
                    // Influence Wake panels on tales panels
                    LeftMatrixBig[i, n * 2 * XMeshSize] += LeftMatrixBig[i, Panels.Count + n];
                    LeftMatrixBig[i, (n + 1) * 2 * XMeshSize - 1] -= LeftMatrixBig[i, Panels.Count + n];
                }
            }

            for (int i = 0; i < Panels.Count; i++)
                for (int j = 0; j < Panels.Count; j++)
                    LeftMatrix[i, j] = LeftMatrixBig[i, j];

            return LeftMatrix;
        }
        /// <summary>
        /// Create Rigth Square Matrix
        /// </summary>
        /// <param name="Panels"></param>
        /// <returns></returns>
        public static double[,] RightMatrix_Square (List<TPanel> Panels)
        {
            // Define Right Square Matrix Size
            double[,] RightMatrix_Square = new double[Panels.Count, Panels.Count];

            // Build Right Square Matrix Elaments
            for (int i = 0; i < Panels.Count; i++)
            {
                for (int j = 0; j < Panels.Count; j++)
                {
                    (TPanel panel, Vector3 pk) = TurnAndMove(Panels[j], Panels[i].PanelCenter);

                    // Create an Matrix Element
                    Vector3 p0 = (panel.PanelPoint_1 + panel.PanelPoint_3 + panel.PanelPoint_2 + panel.PanelPoint_4) / 4;
                    Vector3 p1 = (panel.PanelPoint_1 + panel.PanelPoint_3 - panel.PanelPoint_2 - panel.PanelPoint_4) / 4;
                    Vector3 p2 = (panel.PanelPoint_1 - panel.PanelPoint_3 + panel.PanelPoint_2 - panel.PanelPoint_4) / 4;
                    Vector3 p3 = (panel.PanelPoint_1 - panel.PanelPoint_3 - panel.PanelPoint_2 + panel.PanelPoint_4) / 4;

                    double PS = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        int Y = 1;
                        int X = 1;
                        if (k % 2 == 0)
                            Y = -1;
                        if (k > 1)
                            X = -1;

                        Vector3 R = p0 - pk + X * p1 + Y * p2 + X * Y * p3;
                        Vector3 A1 = p1 + p3 * Y;
                        Vector3 A2 = p2 + p3 * X;
                        Vector3 RA1 = Vector3.Cross(R, A1);
                        Vector3 RA2 = Vector3.Cross(R, A2);
                        Vector3 A12 = Vector3.Cross(A1, A2);

                        double AM = R.Length();
                        double SA = R.X * A12.X +
                                    R.Y * A12.Y +
                                    R.Z * A12.Z;
                        double B1 = RA1.X * RA2.X +
                                    RA1.Y * RA2.Y +
                                    RA1.Z * RA2.Z;
                        double SAM = SA * AM;
                        double AID = Math.Atan(B1 / SAM);
                        double A12M = A12.Length();
                        Vector3 BN = Vector3.Divide(A12, (float)A12M);
                        if (float.IsNaN(BN.X))
                            BN = new Vector3(0, 1, 0);
                        double SCR1N = RA1.X * BN.X +
                                       RA1.Y * BN.Y +
                                       RA1.Z * BN.Z;
                        double SCR2N = RA2.X * BN.X +
                                       RA2.Y * BN.Y +
                                       RA2.Z * BN.Z;
                        double A1M = A1.Length();
                        double A2M = A2.Length();
                        double RBN = R.X * BN.X +
                                      R.Y * BN.Y +
                                      R.Z * BN.Z;
                        double SRA1 = R.X * A1.X +
                                      R.Y * A1.Y +
                                      R.Z * A1.Z;
                        double SRA2 = R.X * A2.X +
                                      R.Y * A2.Y +
                                      R.Z * A2.Z;
                        double AIS1 = -SCR1N / A1M * Math.Log(Math.Abs(AM * A1M + SRA1));
                        double AIS = AIS1 + SCR2N / A2M * Math.Log(Math.Abs(AM * A2M + SRA2)) + RBN * AID;
                        PS += X * Y * AIS;
                    }
                    RightMatrix_Square[i, j] = PS / (4 * Math.PI);
                }
            }
            return RightMatrix_Square;
        }
        /// <summary>
        /// Create Source Matrix (For each Panels need to find proection of flow velocity on Panel Normal)
        /// Flow velocity is 1 and it's along X Axis
        /// </summary>
        /// <param name="Panels"></param>
        /// <returns></returns>
        public static double[] RightMatrix_Column(List<TPanel> Panels)
        {
            // Define Source Matrix Size
            double[] SourceMatrix = new double[Panels.Count];

            // Build Source Matrix Elaments
            for (int i = 0; i < Panels.Count; i++)
            {
                SourceMatrix[i] = Panels[i].PanelNormal.X /
                                  Math.Sqrt(Math.Pow(Panels[i].PanelNormal.X, 2) +
                                            Math.Pow(Panels[i].PanelNormal.Y, 2) +
                                            Math.Pow(Panels[i].PanelNormal.Z, 2));      
            }
            return SourceMatrix;
        }
        /// <summary>
        /// Build Right Matrix as multiply of RightMatrix_Square and RightMatrix_Column
        /// </summary>
        /// <param name="Panels"></param>
        /// <returns></returns>
        public static double[] RightMatrix(List<TPanel> Panels)
        {
            // Define Factors of matrix Multiply
            double[,] Matrix = RightMatrix_Square(Panels);
            double[] Column = RightMatrix_Column(Panels);
            double[] RightMatrix = new double[Column.Length];

            // Multiply Matrices
            for (int i = 0; i < Column.Length; i++)
            {
                RightMatrix[i] = 0;
                for (int j = 0; j < Column.Length; j++)
                {
                    RightMatrix[i] += Matrix[i, j] * Column[j];
                }
            }

            return RightMatrix;
        }

        public static double[] GetSolvedMatrix (List<TPanel> Panels, int XMeshSize, int ZMeshSize)
        {
            double[,] LeftM = LeftMatrix(Panels, XMeshSize, ZMeshSize);
            double[] RightM = RightMatrix(Panels);

            return TMatrixSolver.MatrixMultiply(LeftM, RightM);
        }
    }
}
