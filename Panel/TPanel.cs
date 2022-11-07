// Panel Difinition
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
//***********************************************************************
namespace AeroCalc.Panel
{
    public class TPanel
    {
        /// <summary>
        /// First point of Panel
        /// </summary>
        public Vector3 PanelPoint_1 = new Vector3();
        /// <summary>
        /// Second point of Panel
        /// </summary>
        public Vector3 PanelPoint_2 = new Vector3();
        /// <summary>
        /// Third point of Panel
        /// </summary>
        public Vector3 PanelPoint_3 = new Vector3();
        /// <summary>
        /// Fourth point of Panel
        /// </summary>
        public Vector3 PanelPoint_4 = new Vector3();
        /// <summary>
        /// Panel Constructor
        /// </summary>
        public TPanel(Vector3 PanelPoint_1, Vector3 PanelPoint_2, Vector3 PanelPoint_3, Vector3 PanelPoint_4)
        {
            this.PanelPoint_1 = PanelPoint_1;
            this.PanelPoint_2 = PanelPoint_2;
            this.PanelPoint_3 = PanelPoint_3;
            this.PanelPoint_4 = PanelPoint_4;
        }
        /// <summary>
        /// Center of Panel
        /// </summary>
        public Vector3 PanelCenter
        {
            get
            {
                return (PanelPoint_1 + PanelPoint_2 + PanelPoint_3 + PanelPoint_4) / 4;
            }
        }
        /// <summary>
        /// Normal to Panel
        /// </summary>
        public Vector3 PanelNormal
        {
            get
            {
                return Vector3.Cross(PanelPoint_1 - PanelPoint_2, PanelPoint_1 - PanelPoint_3);
            }
        }
        /// <summary>
        /// Panel area
        /// </summary>
        public double Area
        {
            get
            {
                Vector3 Diagonal_1 = PanelPoint_1 - PanelPoint_4;
                Vector3 Diagonal_2 = PanelPoint_2 - PanelPoint_3;

                double Sin = Math.Sin(Math.Acos((Diagonal_1.X * Diagonal_2.X + Diagonal_1.Y * Diagonal_2.Y + Diagonal_1.Z * Diagonal_2.Z) /
                                                 Math.Sqrt(Math.Pow(Diagonal_1.X, 2) + Math.Pow(Diagonal_1.Y, 2) + Math.Pow(Diagonal_1.Z, 2)) /
                                                 Math.Sqrt(Math.Pow(Diagonal_2.X, 2) + Math.Pow(Diagonal_2.Y, 2) + Math.Pow(Diagonal_2.Z, 2))));

                return 0.5 * Diagonal_1.Length() * Diagonal_2.Length() * Sin;
            }
        }
    }
}
