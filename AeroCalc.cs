using AeroCalc.PanelMethodSolver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AeroCalc.ResultSaver;

namespace AeroCalc
{
    public partial class AeroCalc : Form
    {
        public AeroCalc()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            fileTextBox.Text = path;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            string path = fileTextBox.Text;
            string Ratio = ratioTextBox.Text;
            string Taper = taperTextBox.Text;
            string Sweep = sweepTextBox.Text;
            string FirstAoA = firstAoATextBox.Text;
            string LastAoA = lastAoATextBox.Text;
            string StepAoA = stepTextBox.Text;
            string XMeshSize = XMeshSizeTextBox.Text;
            string ZMeshSize = ZMeshSizeTextFile.Text;

            double ratio = 0;
            if (!double.TryParse(Ratio, out ratio))
            {
                Console.WriteLine("Error. Error in the input data");
                MessageBox.Show("Error. Error in the input data");
                Console.ReadLine();
                Application.Exit();
            }
            ratio = Math.Abs(ratio);
            if (ratio == 0)
            {
                MessageBox.Show("Error. Ratio must be more than zero!\nTry again)");
                Application.Exit();
            }

            double taper = 0;
            if (!double.TryParse(Taper, out taper))
            {
                Console.WriteLine("Error. Error in the input data");
                MessageBox.Show("Error. Error in the input data");
                Console.ReadLine();
                Application.Exit();
            }
            taper = Math.Abs(taper);
            if (taper == 0)
            {
                MessageBox.Show("Error. Taper must be more than zero!\nTry again)");
                Application.Exit();
            }

            double sweep = 0;
            if (!double.TryParse(Sweep, out sweep))
            {
                Console.WriteLine("Error. Error in the input data");
                MessageBox.Show("Error. Error in the input data");
                Console.ReadLine();
                Application.Exit();
            }

            double firstAoA = 0;
            if (!double.TryParse(FirstAoA, out firstAoA))
            {
                Console.WriteLine("Error. Error in the input data");
                MessageBox.Show("Error. Error in the input data");
                Console.ReadLine();
                Application.Exit();
            }

            double lastAoA = 0;
            if (!double.TryParse(LastAoA, out lastAoA))
            {
                Console.WriteLine("Error. Error in the input data");
                MessageBox.Show("Error. Error in the input data");
                Console.ReadLine();
                Application.Exit();
            }

            double stepAoA = 0;
            if (!double.TryParse(StepAoA, out stepAoA))
            {
                Console.WriteLine("Error. Error in the input data");
                MessageBox.Show("Error. Error in the input data");
                Console.ReadLine();
                Application.Exit();
            }
            stepAoA = Math.Abs(stepAoA);
            if (stepAoA == 0)
            {
                stepAoA = 1;
            }

            if (lastAoA < firstAoA)
            {
                double swapAoA = firstAoA;
                firstAoA = lastAoA;
                lastAoA = swapAoA;
            }

            int xMeshSize = 0;
            if(!int.TryParse(XMeshSize, out xMeshSize))
            {
                Console.WriteLine("Error. Error in the input data");
                MessageBox.Show("Error. Error in the input data");
                Console.ReadLine();
                Application.Exit();
            }
            xMeshSize = Math.Abs(xMeshSize);
            if (xMeshSize == 0)
            {
                MessageBox.Show("Error. Mesh size along X axis must be more than zero!\nTry again)");
                Application.Exit();
            }

            int zMeshSize = 0;
            if (!int.TryParse(ZMeshSize, out zMeshSize))
            {
                Console.WriteLine("Error. Error in the input data");
                MessageBox.Show("Error. Error in the input data");
                Console.ReadLine();
                Application.Exit();
            }
            zMeshSize = Math.Abs(zMeshSize);
            zMeshSize = Math.Abs(xMeshSize);
            if (zMeshSize == 0)
            {
                MessageBox.Show("Error. Mesh size along Z axis must be more than zero!\nTry again)");
                Application.Exit();
            }

            //resultTextBox.Text = "AoA" + "\t" + "Cy" + "\t" + "Cx" + Environment.NewLine;
            string result = "AoA" + "\t" + "Cy" + "\t" + "Cx" + Environment.NewLine;
            resultTextBox.Text = result;

            for (double i = firstAoA; i <= lastAoA; i += stepAoA)
            {
                (double Cy, double Cx) = TCyCxSolver.GetCyCx(path, xMeshSize, zMeshSize, ratio, taper, sweep, i);
                //resultTextBox.Text = resultTextBox.Text + Math.Round(i, 5) + "\t" + Math.Round(Cy, 5) + "\t" + Math.Round(Cx, 5) + Environment.NewLine;
                result += Math.Round(i, 5) + "\t" + Math.Round(Cy, 5) + "\t" + Math.Round(Cx, 5) + Environment.NewLine;
                resultTextBox.Text = result;
            }
            result += Environment.NewLine + Environment.NewLine + "Succes!";
            resultTextBox.Text = result;
        }

        private void ratioTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            ratioTextBox.Clear();
        }

        private void fileTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            fileTextBox.Clear();
        }

        private void taperTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            taperTextBox.Clear();
        }

        private void sweepTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            sweepTextBox.Clear();
        }

        private void firstAoATextBox_MouseClick(object sender, MouseEventArgs e)
        {
            firstAoATextBox.Clear();
        }

        private void lastAoATextBox_MouseClick(object sender, MouseEventArgs e)
        {
            lastAoATextBox.Clear();
        }

        private void stepTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            stepTextBox.Clear();
        }

        private void XMeshSizeTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            XMeshSizeTextBox.Clear();
        }

        private void ZMeshSizeTextFile_MouseClick(object sender, MouseEventArgs e)
        {
            ZMeshSizeTextFile.Clear();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            if (resultTextBox.Text == null)
            {
                MessageBox.Show("The calculation was not carried out. Make a calculation.");
            }
            else
            {
                int ResultLines = resultTextBox.Lines.Length - 3;

                string path = fileTextBox.Text;

                string path_result;

                if (Path.GetExtension(path) == ".xlsx")
                {
                    path_result = path.Substring(0, path.Length - 5) + "_result.xlsx";
                }
                else
                {
                    path_result = path.Substring(0, path.Length - 4) + "_result.xlsx";
                }

                List<string> result_strings = new List<string>();

                for (int i = 0; i < ResultLines; i++)
                {
                    string[] line = resultTextBox.Lines[i].Split("\t");
                    result_strings.Add(line[0]);
                    result_strings.Add(line[1]);
                    result_strings.Add(line[2]);
                }

                TResultSaver.SaveResult(path_result, result_strings);
                MessageBox.Show("File " + path_result + " Successfully created!!!");
            }
        }
    }
}
