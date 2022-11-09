// Here is Method for calculation of Left and Right Matrices from TMatrixBuilder
// Type of Matrix: A * X = B, where B's Column value is 1
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//***********************************************************************
namespace AeroCalc.PanelMethodSolver
{
    public class TMatrixSolver
    {
        /// <summary>
        /// Build an Inverse Matrix
        /// </summary>
        /// <param name="Matrix"></param>
        /// <returns></returns>
        public static double[,] GetInverseMatrix(double[,] Matrix)
        {
            // Define Row and Column Value
            int Row = Matrix.GetLength(0); 
            int Col = Matrix.GetLength(1);

            // Define Inverse Matrix
            double[,] InverseMatrix = new double[Row, Col];

            double[,] Out = new double[Row, Col * 2];
            double[,] PrOut = new double[Row, Col * 2]; // Double

            // Build Full Matrix (A * E)

            // Build A
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    PrOut[i, j] = Matrix[i, j];
                }
            }

            // Build E
            int p = 0;
            for (int j = Col; j < 2 * Col; j++)
            {
                PrOut[p, j] = 1;
                p = p + 1;
            }

            // Straight stroke
            for (int k = 0; k < Row; k++)
            {
                // Divide Main Row on Main Element
                for (int j = 0; j < 2 * Col; j++)
                {
                    Out[k, j] = PrOut[k, j] / PrOut[k, k];
                }
                // Set to zero under elements
                for (int i = k + 1; i < Row; i++)
                {
                    for (int j = 0; j < 2 * Col; j++)
                    {
                        Out[i, j] = (PrOut[i, j] - PrOut[i, k] * Out[k, j]);
                    }
                }
                // Refresh
                for (int i = 0; i < Row; i++)
                {
                    for (int j = 0; j < 2 * Col; j++)
                    {
                        PrOut[i, j] = Out[i, j];
                    }
                }
            }
            // Refverse stroke
            for (int k = 1; k < Col + 1; k++)
            {
                for (int i = k + 1; i < Row + 1; i++)
                {
                    for (int j = 0; j < 2 * Col; j++)
                    {
                        Out[Row - i, j] = PrOut[Row - i, j] - PrOut[Row - k, j] * PrOut[Row - i, Col - k];
                    }
                }

                for (int i = 0; i < Row; i++)
                {
                    for (int j = 0; j < 2 * Col; j++)
                    {
                        PrOut[i, j] = Out[i, j];
                    }
                }
            }

            InverseMatrix = new double[Row, Col];
            for (int i = 0; i < Row; i++)
            {
                for (int j = Col; j < 2 * Col; j++)
                {
                    InverseMatrix[i, j - Col] = Out[i, j];
                }
            }

            return InverseMatrix;
        }
        /// <summary>
        /// Check Matrix Solving Existence
        /// </summary>
        /// <param name="MatrixA"></param>
        /// <param name="MatrixB"></param>
        /// <returns></returns>
        public static bool MatrixChecker (double[,] MatrixA, double[] MatrixB)
        {
            if (MatrixA.GetLength(1) != MatrixB.GetLength(0))
                return false;
            return true;
        }
        /// <summary>
        /// Solve and define X (See class description)
        /// </summary>
        /// <param name="MatrixA"></param>
        /// <param name="MatrixB"></param>
        /// <returns></returns>
        public static double[] MatrixMultiply (double[,] MatrixA, double[] MatrixB)
        {
            // Define X
            double[] MatrixX = new double[MatrixA.GetLength(0)];

            // Check Matrix Solving Existence
            if (!MatrixChecker(MatrixA, MatrixB))
            {
                Console.WriteLine("Error. The matrices were not built.");
                MessageBox.Show("Error. The matrices were not built.");
                Console.ReadLine();
                Application.Exit();
                return MatrixX;
            }

            // Find A Inverse
            double[,] MatrixA_Inverse = GetInverseMatrix(MatrixA);
            
            // X finder
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                MatrixX[i] = 0;
                for (int j = 0; j < MatrixA.GetLength(1); j++)
                {
                    MatrixX[i] += MatrixA_Inverse[i, j] * MatrixB[j];
                }
            }
            return MatrixX;
        }
    }
}
