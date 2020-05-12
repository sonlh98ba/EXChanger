using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace EXChanger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // Tạo file mở
            OpenFileDialog fOpen = new OpenFileDialog();
            fOpen.Filter = "(Tất cả các tệp)|*.*|(Các tệp excel)|*.xlsx";
            fOpen.ShowDialog();

            // Xử lý
            if (fOpen.FileName!="")
            {
                lblPath.Text = fOpen.FileName;

                // Tạo đối tượng

                Excel.Application app = new Excel.Application();

                // Mở tệp

                Excel.Workbook wb = app.Workbooks.Open(fOpen.FileName);

                try
                {
                    // Mở sheet
                    Excel._Worksheet sheet = wb.Sheets[1];
                    Excel.Range range = sheet.UsedRange;
                    // Đọc dữ liệu

                    int rows = range.Rows.Count;
                    int columns = range.Columns.Count;

                    // Bắt đầu listview

                    // Tiêu đề
                    for (int i=1;i<=columns;i++)
                    {
                        string columnName = range.Cells[1, i].Value.ToString();
                        ColumnHeader col = new ColumnHeader();
                        col.Text = columnName;
                        lstCustomer.Columns.Add(col);
                    }

                    // Dữ liệu


                    // Nhập
                    for (int i = 2;i <= rows;i++)
                    {
                        ListViewItem item = new ListViewItem();
                        for (int j = 1;j <= columns;j++)
                        {
                            if (j == 1)
                            {
                                if (range.Cells[i, j].Value == null)
                                    item.Text = "";
                                else
                                    item.Text = range.Cells[i, j].Value.ToString();
                            }
                            else
                            {
                                if (range.Cells[i, j].Value == null)
                                    item.SubItems.Add("");
                                else
                                    item.SubItems.Add(range.Cells[i, j].Value.ToString());
                            }
                        }

                        lstCustomer.Items.Add(item);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Chọn lại tập tin!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Tạo file lưu
            SaveFileDialog fSave = new SaveFileDialog();
            fSave.Filter = "(Tất cả các tệp)|*.*|(Các tệp excel)|*.xlsx";
            fSave.ShowDialog();

            // Xử lý
            if (fSave.FileName != "")
            {
                Excel.Application app = new Excel.Application();

                Excel.Workbook wb = app.Workbooks.Add(Type.Missing);

                Excel._Worksheet sheet = null;

                try 
                { 
                    // Đọc dữ liệu từ listview

                    sheet = wb.ActiveSheet;
                    sheet.Name = "Dữ liệu xuất";

                    for (int i=1;i<10;i++)
                    {

                    }
                
                }
                catch (Exception ex)
                { 
                   MessageBox.Show(ex.Message);
                }
                finally
                {
                    app.Quit();
                    wb = null;
                }

            }
            else
            {
                MessageBox.Show("Chọn lại tập tin!");
            }
        }
    }
}
