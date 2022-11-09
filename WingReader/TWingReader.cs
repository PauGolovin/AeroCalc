// Get Nodes of Wing Profile from File
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Globalization;
//***********************************************************************
namespace AeroCalc.WingReader
{
    public class TWingReader
    {
        /// <summary>
        /// File Existence Check
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FileExistanceCheck(string path)
        {
            bool FileExist = File.Exists(path);
            if (!FileExist)
                return false;
            else
                return true;
        }
        /// <summary>
        /// Get Nodes from Excel
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static double[,] GetFromExcel (string path)
        {           
            Excel.Application ObjWorkExcel = new Excel.Application();   // Open Excel
            Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, 
                                                                           Type.Missing, Type.Missing, Type.Missing, 
                                                                           Type.Missing, Type.Missing, Type.Missing, 
                                                                           Type.Missing, Type.Missing, Type.Missing, 
                                                                           Type.Missing, Type.Missing); // Open File
            Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];  // Get first sheet
            var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);    // Get last cell coordinates
                        
            // Check format of first Row in input data
            int StartRow;
            double CheckRow;
            if (double.TryParse(ObjWorkSheet.Cells[1, 1].Text.ToString(), out CheckRow))
                StartRow = 0;
            else 
                StartRow = 1;

            // Check the correctness of input data
            if (lastCell.Column != 3)
            {
                MessageBox.Show("Error. File cannot be read. Check the correctness of input data.");
                Application.Exit();
            }
            if ((lastCell.Row - StartRow) == 0 || (lastCell.Row - StartRow) == 1)
            {
                MessageBox.Show("Error. File cannot be read. Check the correctness of input data.");
                Application.Exit();
            }

            string[,] StringNodes = new string[lastCell.Row - StartRow, lastCell.Column];   // Array size is equal to Excel size
            for (int i = 0; i < lastCell.Row - StartRow; i++)   // Through rows
                for (int j = 0; j < lastCell.Column; j++)   // Through columns
                    StringNodes[i, j] = ObjWorkSheet.Cells[i + 1 + StartRow, j + 1].Text.ToString();   // String from Excel to Array
            ObjWorkBook.Close(false, Type.Missing, Type.Missing);   // Close without saving
            ObjWorkExcel.Quit();    // Quit Excel
            GC.Collect();   // Clean up after yourself

            // TryParse from String to Double and return
            double[,] WingNodes = new double[StringNodes.GetLength(0), StringNodes.GetLength(1)];
            for (int i = 0; i < StringNodes.GetLength(0); i++)
                for (int j = 0; j < StringNodes.GetLength(1); j++)
                {
                    if (double.TryParse(StringNodes[i, j], out WingNodes[i, j]))
                        continue;
                    else
                    {
                        MessageBox.Show("Error. File cannot be read. Check values.");
                        Application.Exit();
                    }
                }
            return WingNodes;
        }
        /// <summary>
        /// Get Nodes from .txt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static double[,] GetFromTXT (string path)
        {
            // Read all Lines from TXT as a string Array
            StreamReader ReadTXT = new StreamReader(path);
                string[] LinesFromTXT = ReadTXT.ReadToEnd().Split("\n");

            // Make strings from lines
            string[,] StringFromLines = new string[LinesFromTXT.Length, 3];
            for (int i = 0; i < LinesFromTXT.Length; i++)
            {
                string[] LineSplit = LinesFromTXT[i].Split("\t");  // Make string from one line

                // Check the correctness of input data
                if (LineSplit.Length != 3)
                {
                    MessageBox.Show("Error. File cannot be read. Check the correctness of input data.");
                    Application.Exit();
                }

                // Make string from lines
                StringFromLines[i, 0] = LineSplit[0];
                StringFromLines[i, 1] = LineSplit[1];
                StringFromLines[i, 2] = LineSplit[2];
            }

            // Check format of first Row in input data
                        int StartRow;
            double CheckRow;
            if (double.TryParse(StringFromLines[0, 0].ToString(), out CheckRow))
                StartRow = 0;
            else
                StartRow = 1;

            // Check the correctness of input data
            if ((LinesFromTXT.Length - StartRow) == 0 || (LinesFromTXT.Length - StartRow) == 1)
            {
                MessageBox.Show("Error. File cannot be read. Check the correctness of input data.");
                Application.Exit();
            }

            // Make double Array from strings
            double[,] WingNodes = new double[LinesFromTXT.Length - StartRow, 3];
            for (int i = 0; i < LinesFromTXT.Length - StartRow; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (double.TryParse(StringFromLines[i + StartRow, j], out WingNodes[i, j]))
                        continue;
                    else
                    {
                        MessageBox.Show("Error. File cannot be read. Check values.");
                        Application.Exit();
                    }
                }
            return WingNodes;
        }
        /// <summary>
        /// Get Nodes
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static double[,] GetNodes (string path)
        {
            // File Existence Check
            bool FileExistance = FileExistanceCheck(path);
            if (!FileExistance)
            {
                MessageBox.Show("Error. No file " + path); ;
                Application.Exit();
                return null;
            }

            // File Extension Checker 
            switch (Path.GetExtension(path))
            {
                case ".xlsx":
                    return GetFromExcel(path);
                case ".txt":
                    return GetFromTXT(path);
                default:
                    MessageBox.Show("File cannot be read. Check that extension of your file is .xlsx or .txt");
                    Application.Exit();
                    return null;
            }
        }
    }
}
