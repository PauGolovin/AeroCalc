using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace AeroCalc.ResultSaver
{
    public class TResultSaver
    {
        public static void SaveResult(string path_result, List<string> text)
        {
            // Open Excel
            Excel.Application ObjWorkExcel = new Excel.Application();   
            // Create book
            Excel.Workbook ObjWorkBook = ObjWorkExcel.Application.Workbooks.Add();  
            // Open sheet 1
            Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];  

            // fill excel file
            for (int i = 0; i < text.Count / 3; i++)
            {
                if (i == 0)
                {
                    ObjWorkSheet.Cells[i + 1, 1] = text[i * 3];
                    ObjWorkSheet.Cells[i + 1, 2] = text[i * 3 + 1];
                    ObjWorkSheet.Cells[i + 1, 3] = text[i * 3 + 2];
                }
                else
                {
                    ObjWorkSheet.Cells[i + 1, 1] = double.Parse(text[i * 3]);
                    ObjWorkSheet.Cells[i + 1, 2] = double.Parse(text[i * 3 + 1]);
                    ObjWorkSheet.Cells[i + 1, 3] = double.Parse(text[i * 3 + 2]);
                }
                
            }

            // Create Chart
            // Define chart range
            string AoARange = "A" + text.Count / 3;
            string CyRange = "B" + text.Count / 3;
            string CxRange = "C" + text.Count / 3;

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)ObjWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(200, 10, 300, 250);;
            Excel.Chart chartPage = myChart.Chart;

            SeriesCollection seriesCollection = (SeriesCollection)chartPage.SeriesCollection(Type.Missing);
            
            // Cy chart
            Series series1 = seriesCollection.NewSeries();
            series1.Name = "Lift coefficient";
            series1.XValues = ObjWorkSheet.get_Range("A2", AoARange);
            series1.Values = ObjWorkSheet.get_Range("B2", CyRange);
            chartPage.ChartType = XlChartType.xlXYScatterSmooth;

            // Cx chart
            Series series2 = seriesCollection.NewSeries();
            series2.Name = "Drag coefficient";
            series2.XValues = ObjWorkSheet.get_Range("A2", AoARange);
            series2.Values = ObjWorkSheet.get_Range("C2", CxRange);
            chartPage.ChartType = XlChartType.xlXYScatterSmooth;

            // close excel
            ObjWorkBook.SaveAs(path_result);
            ObjWorkBook.Close(false, Type.Missing, Type.Missing);
            ObjWorkExcel.Quit();
            GC.Collect();
        }
    }
}
