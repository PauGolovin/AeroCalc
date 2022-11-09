// Mesh Nodes Definitoin and Build of Panel of Mesh
using AeroCalc.Panel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
//***********************************************************************
namespace AeroCalc.MeshGenerator
{
    public class TMeshGenerator
    {
        /// <summary>
        /// Check that Top Y and Bottom Y of Start and End Points are equal
        /// </summary>
        /// <param name="InputNodes"></param>
        /// <returns></returns>
        public static double[,] CheckStartAndEndPoints (double[,] InputNodes)
        {
            if (InputNodes[0, 1] == InputNodes[0, 2] && InputNodes[InputNodes.GetLength(0) - 1, 1] == InputNodes[InputNodes.GetLength(0) - 1, 2])
                return InputNodes;
            else
            {
                InputNodes[0, 1] = InputNodes[0, 2];
                InputNodes[InputNodes.GetLength(0) - 1, 1] = InputNodes[InputNodes.GetLength(0) - 1, 2];
                return InputNodes;
            }
        }
        /// <summary>
        /// Definition of wing root chord nodes by Lagrange Interpolation Method
        /// </summary>
        /// <param name="InputNodes"></param>
        /// <param name="XMeshSize"></param>
        /// <returns></returns>
        public static double[,] RootChordNodes(double[,] InputNodes, int XMeshSize)
        {
            // XMeshSize Check
            if (XMeshSize < 4)
            {
                Console.WriteLine("Error. Upsampling X mesh size");
                MessageBox.Show("Error. Upsampling X mesh size");
                Console.ReadLine();
                Application.Exit();
            }

            // Define of Length of Root Chord
            double RootChordLength = InputNodes[InputNodes.GetLength(0) - 1, 0] - InputNodes[0, 0];

            // Array of Root Chord Nodes
            double[,] RootChordNodes = new double[XMeshSize + 1, 3];

            // Define of X coordinates of Root Chord Nodes
            for (int i = 0; i < RootChordNodes.GetLength(0); i++)
                RootChordNodes[i, 0] = (double)i * (double)(RootChordLength / XMeshSize);

            // Define of Y Coordinates of Start and End Nodes
            RootChordNodes[0, 1] = InputNodes[0, 1];
            RootChordNodes[0, 2] = InputNodes[0, 2];

            RootChordNodes[RootChordNodes.GetLength(0) - 1, 1] = InputNodes[InputNodes.GetLength(0) - 1, 1];
            RootChordNodes[RootChordNodes.GetLength(0) - 1, 2] = InputNodes[InputNodes.GetLength(0) - 1, 2];

            // Use Lagrange Interpolation Method for Define of Y coordinates for Root Chord Nodes
            for (int i = 1; i < RootChordNodes.GetLength(0) - 1; i++)       // Go through all points (Except Start and End points)
            {
                RootChordNodes[i, 1] = 0;
                RootChordNodes[i, 2] = 0;

                //Define points for terms in Lagrange Method
                int StartMethodPoint = 0;
                int EndMethodPoint = 0;
                for (int j = 0; j < InputNodes.GetLength(0); j++)
                {
                    if (InputNodes[j, 0] < RootChordNodes[i, 0])
                        continue;
                    else
                    {
                        // Check Start Point for Lagrange Method
                        if (j < 3)
                            StartMethodPoint = 0;
                        else
                            StartMethodPoint = j - 2;

                        // Check End Point for Lagrange Method
                        if (j > InputNodes.GetLength(0) - 4)
                            EndMethodPoint = InputNodes.GetLength(0) - 1;
                        else 
                            EndMethodPoint = j + 1;

                        break;
                    }
                }

                for (int j = StartMethodPoint; j <= EndMethodPoint; j++)       // Make a terms
                {
                    double TopLagrangeTerm = InputNodes[j, 1];
                    double BottomLagrangeTerm = InputNodes[j, 2];
                    for (int k = StartMethodPoint; k <= EndMethodPoint; k++)   // Make a factors
                    {
                        double LagrangeFactor = 0;

                        if (k == j)
                            continue;
                        else
                            LagrangeFactor = (RootChordNodes[i, 0] - InputNodes[k, 0]) / (InputNodes[j, 0] - InputNodes[k, 0]);

                        TopLagrangeTerm *= LagrangeFactor;
                        BottomLagrangeTerm *= LagrangeFactor;
                    }
                    RootChordNodes[i, 1] += TopLagrangeTerm;
                    RootChordNodes[i, 2] += BottomLagrangeTerm;
                }
            }
            return RootChordNodes;
        }
        /// <summary>
        /// Define size and nodes of chord
        /// </summary>
        /// <param name="RootChordNodes"></param>
        /// <param name="Factor"></param>
        /// <returns></returns>
        public static double[,] ChordNodes (double[,] RootChordNodes, double Factor)
        {
            // Return Nodes of Chord
            double[,] ChordNodes = new double[RootChordNodes.GetLength(0), 3];
            for (int i = 0; i < RootChordNodes.GetLength(0); i++)
            {
                ChordNodes[i, 0] = Factor * RootChordNodes[i, 0];
                ChordNodes[i, 1] = Factor * RootChordNodes[i, 1];
                ChordNodes[i, 2] = Factor * RootChordNodes[i, 2];
            }
            return ChordNodes;
        }
        /// <summary>
        /// Move the chord along X axis
        /// </summary>
        /// <param name="ChordNodes"></param>
        /// <param name="Distance"></param>
        /// <returns></returns>
        public static double[,] MoveChord(double[,] ChordNodes, double Distance)
        {
            double[,] MovedChord = new double[ChordNodes.GetLength(0), 3];

            MovedChord[0, 0] = Distance;
            double MovingDistance = MovedChord[0, 0] - ChordNodes[0, 0];
            for (int i = 0; i < ChordNodes.GetLength(0); i++)
            {
                MovedChord[i, 0] = ChordNodes[i, 0] + MovingDistance;
                MovedChord[i, 1] = ChordNodes[i, 1];
                MovedChord[i, 2] = ChordNodes[i, 2];
            }
            return MovedChord;
        }
        /// <summary>
        /// Build of Panels of Mesh
        /// </summary>
        /// <param name="RootChordNodes"></param>
        /// <param name="Ratio"></param>
        /// <param name="Taper"></param>
        /// <param name="Sweep"></param>
        /// <param name="ZMeshSize"></param>
        /// <returns></returns>
        public static List<TPanel> BuildPanels(double[,] RootChordNodes, double Ratio, double Taper, double Sweep, int ZMeshSize)
        {
            List<TPanel> WingPanels = new List<TPanel>();

            // Move Root Chord Nose to the Coordinate System Origin
            double NoseDist = RootChordNodes[0, 0];
            for (int i = 0; i < RootChordNodes.GetLength(0); i++)
            {
                RootChordNodes[i, 0] -= NoseDist;
            }

            // Define of Wing Parameters
            double RootChordLength = RootChordNodes[RootChordNodes.GetLength(0) - 1, 0] - RootChordNodes[0, 0];
            double TipChordLength = RootChordLength / Taper;
            double WingSpan = Ratio * (RootChordLength + TipChordLength) / 2;

            // Define of Span of Panel (Wide, along Z Axis) 
            int HalfOfZMeshSize = (ZMeshSize + 1) / 2;
            double PanelSpan = WingSpan / (HalfOfZMeshSize * 2);

            // Build of Panels of Left Half of Wing (go from the left to the right)
            for (int i = 0; i < HalfOfZMeshSize; i++)
            {
                // Distance from Root Chord to Left and Right Chord
                double DistanceToLeftChord = (double)(HalfOfZMeshSize - i) * PanelSpan;
                double DistanceToRightChord = (double)(HalfOfZMeshSize - i - 1) * PanelSpan;

                // Length of Left and Right Chord (Along X Axis)
                double LeftChordLength = (RootChordLength * i + TipChordLength * (HalfOfZMeshSize - i)) / HalfOfZMeshSize;
                double RightChordLength = (RootChordLength * (i + 1) + TipChordLength * (HalfOfZMeshSize - (i + 1))) / HalfOfZMeshSize;

                // Left and Right Chord Nose X Coordinates
                double LeftNoseX = DistanceToLeftChord * Math.Tan(Sweep * Math.PI / 180) + RootChordNodes[0, 0];
                double RightNoseX = DistanceToRightChord * Math.Tan(Sweep * Math.PI / 180) + RootChordNodes[0, 0];

                // Define of Nodes of Chords
                double[,] LeftChord = ChordNodes(RootChordNodes, LeftChordLength / RootChordLength);
                double[,] RightChord = ChordNodes(RootChordNodes, RightChordLength / RootChordLength);

                // Move Chords
                LeftChord = MoveChord(LeftChord, LeftNoseX);
                RightChord = MoveChord(RightChord, RightNoseX);

                // Build Bottom Panels
                for (int j = LeftChord.GetLength(0) - 1; j > 0; j--)
                {
                    Vector3 Point1 = new Vector3((float)LeftChord[j - 1, 0], (float)LeftChord[j - 1, 2], -(float)DistanceToLeftChord);
                    Vector3 Point2 = new Vector3((float)LeftChord[j, 0], (float)LeftChord[j, 2], -(float)DistanceToLeftChord);
                    Vector3 Point3 = new Vector3((float)RightChord[j - 1, 0], (float)RightChord[j - 1, 2], -(float)DistanceToRightChord);
                    Vector3 Point4 = new Vector3((float)RightChord[j, 0], (float)RightChord[j, 2], -(float)DistanceToRightChord);

                    TPanel Panel = new TPanel(Point1, Point2, Point3, Point4);
                    WingPanels.Add(Panel);
                }

                // Build Top Panels
                for (int j = 0; j < LeftChord.GetLength(0) - 1; j++)
                {
                    
                    Vector3 Point1 = new Vector3((float)LeftChord[j + 1, 0], (float)LeftChord[j + 1, 1], -(float)DistanceToLeftChord);
                    Vector3 Point2 = new Vector3((float)LeftChord[j, 0], (float)LeftChord[j, 1], -(float)DistanceToLeftChord);
                    Vector3 Point3 = new Vector3((float)RightChord[j + 1, 0], (float)RightChord[j + 1, 1], -(float)DistanceToRightChord);
                    Vector3 Point4 = new Vector3((float)RightChord[j, 0], (float)RightChord[j, 1], -(float)DistanceToRightChord);

                    TPanel Panel = new TPanel(Point1, Point2, Point3, Point4);
                    WingPanels.Add(Panel);
                }
            }

            // Build Panel of Right Haf of the Wing
            int PanelsCount = WingPanels.Count;
            for (int i = 0; i < HalfOfZMeshSize; i++)
            {
                for (int j = 0; j < (RootChordNodes.GetLength(0) - 1) * 2; j++)
                {
                    Vector3 Point1 = new Vector3(WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_3.X,
                                                 WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_3.Y,
                                                -WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_3.Z);
                    Vector3 Point2 = new Vector3(WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_4.X,
                                                 WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_4.Y,
                                                -WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_4.Z);
                    Vector3 Point3 = new Vector3(WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_1.X,
                                                 WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_1.Y,
                                                -WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_1.Z);
                    Vector3 Point4 = new Vector3(WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_2.X,
                                                 WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_2.Y,
                                                -WingPanels[PanelsCount - (i + 1) * (RootChordNodes.GetLength(0) - 1) * 2 + j].PanelPoint_2.Z);

                    TPanel Panel = new TPanel(Point1, Point2, Point3, Point4);
                    WingPanels.Add(Panel);
                }
            }

            return WingPanels;
        }
        /// <summary>
        /// Rotate Model by Angle of Attack
        /// </summary>
        /// <param name="Panels"></param>
        /// <param name="AngleOfAttack"></param>
        /// <returns></returns>
        public static List<TPanel> RotateModel (List<TPanel> Panels, double AngleOfAttack)
        {
            // Make Radians from Degrees
            AngleOfAttack = -AngleOfAttack * Math.PI / 180;

            // Create Rotation Matrix
            var RotationMatrix = Matrix4x4.CreateRotationZ((float)AngleOfAttack);

            // Create Model Rotation
            foreach (var panel in Panels)
            {
                panel.PanelPoint_1 = Vector3.Transform(panel.PanelPoint_1, RotationMatrix);
                panel.PanelPoint_2 = Vector3.Transform(panel.PanelPoint_2, RotationMatrix);
                panel.PanelPoint_3 = Vector3.Transform(panel.PanelPoint_3, RotationMatrix);
                panel.PanelPoint_4 = Vector3.Transform(panel.PanelPoint_4, RotationMatrix);
            }

            return Panels;
        }
        /// <summary>
        /// Mesh Generator
        /// </summary>
        /// <param name="InputNodes"></param>
        /// <param name="XMeshSize"></param>
        /// <param name="ZMeshSize"></param>
        /// <param name="Ratio"></param>
        /// <param name="Taper"></param>
        /// <param name="Sweep"></param>
        /// <param name="AoA"></param>
        /// <returns></returns>
        public static List<TPanel> MeshGenerator (double[,] InputNodes, int XMeshSize, int ZMeshSize, double Ratio, double Taper, double Sweep, double AoA)
        {
            double[,] CheckedNodes = CheckStartAndEndPoints(InputNodes);
            double[,] rootChordNodes = RootChordNodes(CheckedNodes, XMeshSize);
            List<TPanel> WingPanels = BuildPanels(rootChordNodes, Ratio, Taper, Sweep, ZMeshSize);
            WingPanels = RotateModel(WingPanels, AoA);

            return WingPanels;
        }
    }
}
