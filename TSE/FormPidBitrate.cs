using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InActionLibrary;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

namespace TSE
{
    public partial class FormPidBitrate : Form
    {
        //Use the list to map the PID to the each column of the grid. Int32 is to save PID. Int64 is to save maximum bitrate.
        private SortedDictionary<Int32, Int64> sortedPidList = new SortedDictionary<Int32, Int64>();

        public FormPidBitrate()
        {
            InitializeComponent();
        }

        public void AddRealtimeBitrate(KeyValuePair<DateTime, SortedDictionary<UInt16, PidBitrate>> bitrateForNow, bool isLive)
        {
            PidBitrate realtimePidBitrate = null;
            DateTime timeOfBitrate = bitrateForNow.Key;
            SortedDictionary<UInt16, PidBitrate> realtimePidBitrateList = bitrateForNow.Value;
            int rowIndex = dataGridViewRealtimeBitrate.Rows.Add();//Add a new row.
            if (rowIndex % 2 == 0)
            {
                dataGridViewRealtimeBitrate.Rows[rowIndex].DefaultCellStyle.BackColor = Color.SteelBlue;
            }
            else
            {
                dataGridViewRealtimeBitrate.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dataGridViewRealtimeBitrate.Rows[rowIndex].Cells[0].Value = rowIndex;

            if (isLive)
            {
                dataGridViewRealtimeBitrate.Rows[rowIndex].Cells[1].Value = timeOfBitrate;
            }
            else
            {
                dataGridViewRealtimeBitrate.Rows[rowIndex].Cells[1].Value = rowIndex + 1 + " second";
            }


            foreach (KeyValuePair<UInt16, PidBitrate> keyPair in realtimePidBitrateList)
            {
                //Check whether we have an existing column, if no, create the column for this PID and insert it into the right position.
                realtimePidBitrate = keyPair.Value;
                int columnIndex = FindOrCreateColumn(keyPair.Key); //Find or create a column according to the PID.
                dataGridViewRealtimeBitrate.Rows[rowIndex].Cells[columnIndex].Value = realtimePidBitrate.Bitrate;

                //To update chart.
                String pidColumnName = GetPidColumnName(keyPair.Key);
                Series newPidSeries = chartRealtimeBitrate.Series[pidColumnName];
                newPidSeries.Points.AddY(realtimePidBitrate.Bitrate);
            }
        }

        private string GetPidColumnName(Int32 pid)
        {
            return "PID " + Utility.GetValueHexString(pid, 16);
        }

        private int FindOrCreateColumn(UInt16 pid)
        {
            int index = 0;
            bool found = false;
            String pidColumnName = GetPidColumnName(pid);

            foreach (KeyValuePair<Int32, Int64> pair in sortedPidList)
            {
                if (pair.Key == pid)
                {
                    found = true;
                    break;//We have created an existing item in the list. The index will be used to access the column.
                }
                else if (pair.Key > pid)
                {
                    break;
                }

                index++;
            }

            if (!found)
            {
                //Create a new one.
                sortedPidList.Add(pid, 0);
                DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = pidColumnName;
                newColumn.Name = pidColumnName;

                dataGridViewRealtimeBitrate.Columns.Insert(index, newColumn);//Insert it into the current position, so that we are in order.
                dataGridViewRealtimeBitrate.Columns[index].Width = 60;

                Series newPidSeries = chartRealtimeBitrate.Series.Add(pidColumnName);
                newPidSeries.ChartArea = chartRealtimeBitrate.ChartAreas[0].Name;
                newPidSeries.ChartType = SeriesChartType.Line;
            }

            return index;
        }

        private void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            int i = 0;
            int j = 0;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //Export column header.
            for (i = 0; i < dataGridViewRealtimeBitrate.Columns.Count; ++i)
            {
                xlWorkSheet.Cells[1, i+1] = dataGridViewRealtimeBitrate.Columns[i].HeaderText;
            }


            for (i = 0; i < dataGridViewRealtimeBitrate.RowCount; i++)
            {
                for (j = 0; j < dataGridViewRealtimeBitrate.ColumnCount; j++)
                {
                    if (null != dataGridViewRealtimeBitrate[j, i].Value)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = dataGridViewRealtimeBitrate[j, i].Value.ToString();
                    }
                }
            }

            xlWorkBook.SaveAs(System.AppDomain.CurrentDomain.BaseDirectory + "TSE.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            //Open the file.
            Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "TSE.xls");
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public void ResetData()
        {
            dataGridViewRealtimeBitrate.Rows.Clear();
            dataGridViewRealtimeBitrate.Columns.Clear();

            chartRealtimeBitrate.Series.Clear();
            chartRealtimeBitrate.ChartAreas[0].AxisX.Title = "Time";
            chartRealtimeBitrate.ChartAreas[0].AxisY.Title = "PID bitrate in bps";

            sortedPidList.Clear();

            sortedPidList.Add(-2, 0);//Column "ID"
            sortedPidList.Add(-1, 0);//Column "Time"

            dataGridViewRealtimeBitrate.ColumnHeadersDefaultCellStyle.ForeColor = Color.BlueViolet;

            int columnIndex = dataGridViewRealtimeBitrate.Columns.Add("ID", "ID");//Column "ID"
            dataGridViewRealtimeBitrate.Columns[columnIndex].Width = 60;
            dataGridViewRealtimeBitrate.Columns[columnIndex].DefaultCellStyle.ForeColor = Color.Yellow;

            columnIndex = dataGridViewRealtimeBitrate.Columns.Add("Time", "Time");//Column "Time"
            dataGridViewRealtimeBitrate.Columns[columnIndex].ToolTipText = "This column is invalid in case the stream is a local file.";
            dataGridViewRealtimeBitrate.Columns[columnIndex].Width = 120;
            dataGridViewRealtimeBitrate.Columns[columnIndex].DefaultCellStyle.ForeColor = Color.White;
        }

        private void buttonColumnChart_Click(object sender, EventArgs e)
        {
            ChangeToChartType(SeriesChartType.Column);
        }

        private void ChangeToChartType(SeriesChartType chartType)
        {
            chartRealtimeBitrate.BeginInit();
            for (int i = 0; i < chartRealtimeBitrate.Series.Count; ++i)
            {
                chartRealtimeBitrate.Series[i].ChartType = chartType;
            }
            chartRealtimeBitrate.EndInit();
        }

        private void buttonPointChart_Click(object sender, EventArgs e)
        {
            ChangeToChartType(SeriesChartType.Point);
        }

        private void buttonLine_Click(object sender, EventArgs e)
        {
            ChangeToChartType(SeriesChartType.Line);
        }

        private void buttonStackedArea_Click(object sender, EventArgs e)
        {
            ChangeToChartType(SeriesChartType.StackedArea);
        }
    }
}
