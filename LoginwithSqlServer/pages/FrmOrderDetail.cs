using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace LoginwithSqlServer
{
    public partial class FormOrderDetail : Form
    {
        public FormOrderDetail()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            pickerdate.Format = DateTimePickerFormat.Custom;
            pickerdate.CustomFormat = "MMMM yyyy";
            pickerdate.ShowUpDown = true;
            txttotalamount.Enabled = false;
            pickerMoths.Format = DateTimePickerFormat.Short;
            dataview.RowHeadersVisible = false;
            //dataview.ColumnHeadersVisible = false;
        }
        double totalamount = 0;
        public void LoadDate(DateTime? filterDate = null, bool filterByDay = false)
        {
            dataview.Rows.Clear();
            int i = 1;
            string sqlQuery = @"SELECT o.ordId, o.ordDate, o.ordTime, od.pid, p.name AS ProductName, od.Qty, od.Price
                                FROM tblOrder o
                                JOIN tblOrderDetail od ON o.ordId = od.orderId
                                JOIN tblProduct p ON od.pid = p.id";

            SqlCommand s = new SqlCommand(sqlQuery, DataConnection.DataCon);
            totalamount = 0;
            if (filterDate.HasValue)
            {
                if (filterByDay)
                {
                    sqlQuery += " WHERE DAY(o.ordDate) = @selectedDay AND MONTH(o.ordDate) = @selectedMonth AND YEAR(o.ordDate) = @selectedYear";
                    s.Parameters.AddWithValue("@selectedDay", filterDate.Value.Day);
                }
                else
                {
                    sqlQuery += " WHERE MONTH(o.ordDate) = @selectedMonth AND YEAR(o.ordDate) = @selectedYear";
                }
                s.Parameters.AddWithValue("@selectedMonth", filterDate.Value.Month);
                s.Parameters.AddWithValue("@selectedYear", filterDate.Value.Year);
            }

            s.CommandText = sqlQuery;

            SqlDataReader r = s.ExecuteReader();
            while (r.Read())
            {
                string orderid = r[0].ToString();
                DateTime orderDate = DateTime.Parse(r[1].ToString());
                string orderdate = orderDate.ToString("yyyy-MM-dd");

                TimeSpan orderTimeSpan = TimeSpan.Parse(r[2].ToString());
                DateTime orderTime = DateTime.Today.Add(orderTimeSpan);
                string ordertime = orderTime.ToString("hh:mm:ss tt");

                string pid = r[3].ToString();
                string pname = r[4].ToString();
                int qty = int.Parse(r[5].ToString());
                double price = double.Parse(r[6].ToString());
               
                double amount = qty * price;
                totalamount += amount;
                dataview.Rows.Add(i, orderid, orderdate, ordertime, pid, pname, qty, price.ToString("$0.00"), amount.ToString("$0.00"));
                i++;
                txttotalamount.Text = "ចំនួនទឹកប្រាក់សរុប : " + totalamount.ToString("$0.00");
            }

            r.Close();
            s.Dispose();
        }

        private void JoinTable_Load(object sender, EventArgs e)
        {
            LoadDate();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnexport_to_pdf_Click(object sender, EventArgs e)
        {
            if (dataview.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Order Detail";
                bool ErrorMessage = false;

                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("Unable to write data to disk: " + ex.Message);
                        }
                    }

                    if (!ErrorMessage)
                    {
                        try
                        {
                            Dictionary<string, List<DataGridViewRow>> groupedOrders = new Dictionary<string, List<DataGridViewRow>>();
                            foreach (DataGridViewRow row in dataview.Rows)
                            {
                                if (row.IsNewRow) continue;
                                DateTime orderDate = DateTime.Parse(row.Cells[2].Value.ToString());
                                string key = orderDate.ToString("MMMM yyyy");

                                if (!groupedOrders.ContainsKey(key))
                                {
                                    groupedOrders[key] = new List<DataGridViewRow>();
                                }

                                groupedOrders[key].Add(row);
                            }

                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 16f);
                                PdfWriter writer = PdfWriter.GetInstance(document, fileStream);
                                writer.PageEvent = new PageEventHelper(); // Set the page event

                                document.Open();

                                // Add the statement text at the top
                                Paragraph statementText = new Paragraph("Statement all months", FontFactory.GetFont("Arial", "", 12));
                                statementText.Alignment = Element.ALIGN_LEFT;
                                document.Add(statementText);
                                document.Add(new Paragraph(" ")); // Adding a blank line

                                int totalAllQty = 0;
                                decimal totalAllPrice = 0m;
                                decimal totalAllAmount = 0m;

                                foreach (var group in groupedOrders)
                                {
                                    document.Add(new Paragraph(group.Key, FontFactory.GetFont("Arial", "", 10)));
                                    document.Add(new Paragraph(" ")); // Adding a blank line

                                    PdfPTable pTable = new PdfPTable(dataview.Columns.Count);
                                    pTable.DefaultCell.Padding = 2;
                                    pTable.WidthPercentage = 100;
                                    pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                    // Adding headers with top and bottom borders
                                    foreach (DataGridViewColumn col in dataview.Columns)
                                    {
                                        PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, FontFactory.GetFont("Arial", "", 10)))
                                        {
                                            BackgroundColor = BaseColor.LIGHT_GRAY, // Set the background color
                                            Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER // Set only top and bottom borders
                                        };
                                        pTable.AddCell(pCell);
                                    }

                                    int totalQty = 0;
                                    decimal totalPrice = 0m;
                                    decimal totalAmount = 0m;

                                    bool isOddRow = true;
                                    foreach (DataGridViewRow viewRow in group.Value)
                                    {
                                        foreach (DataGridViewCell dcell in viewRow.Cells)
                                        {
                                            PdfPCell pCell = new PdfPCell(new Phrase(dcell.Value?.ToString() ?? string.Empty, FontFactory.GetFont("Arial", 10)))
                                            {
                                                BackgroundColor = isOddRow ? BaseColor.WHITE : BaseColor.LIGHT_GRAY,
                                                Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER // Set only top and bottom borders
                                            };
                                            pTable.AddCell(pCell);
                                        }

                                        isOddRow = !isOddRow; // Toggle row color

                                        int qty = 0;
                                        decimal price = 0m;

                                        if (int.TryParse(viewRow.Cells[6].Value?.ToString(), out qty))
                                        {
                                            totalQty += qty;
                                        }

                                        if (decimal.TryParse(viewRow.Cells[7].Value?.ToString().Replace("$", ""), out price))
                                        {
                                            totalPrice += price;
                                            totalAmount += qty * price; // Calculate total amount
                                        }
                                    }

                                    document.Add(pTable);
                                    document.Add(new Paragraph(" ")); // Adding a blank line
                                    document.Add(new Paragraph($"Total Quantity for {group.Key}: {totalQty}", FontFactory.GetFont("Arial", "", 12)));
                                    document.Add(new Paragraph($"Total Price for {group.Key}: {totalPrice:c2}", FontFactory.GetFont("Arial", "", 12)));
                                    document.Add(new Paragraph($"Total Amount for {group.Key}: {totalAmount:c2}", FontFactory.GetFont("Arial", "", 12)));
                                    document.Add(new Paragraph(" ")); // Adding a blank line

                                    totalAllQty += totalQty;
                                    totalAllPrice += totalPrice;
                                    totalAllAmount += totalAmount;
                                }

                                document.Add(new Paragraph("Overall Totals", FontFactory.GetFont("Arial", "", 12)));
                                document.Add(new Paragraph($"Total Quantity for all months: {totalAllQty}", FontFactory.GetFont("Arial", "", 12)));
                                document.Add(new Paragraph($"Total Price for all months: {totalAllPrice:c2}", FontFactory.GetFont("Arial", "", 12)));
                                document.Add(new Paragraph($"Total Amount for all months: {totalAllAmount:c2}", FontFactory.GetFont("Arial", "", 12)));

                                document.Close();
                                fileStream.Close();
                            }

                            MessageBox.Show("Data Exported Successfully");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while exporting data: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No record found", "Info");
            }
        }



        private void pickerdate_TextChanged(object sender, EventArgs e)
        {
            LoadDate(pickerdate.Value);
        }

        private void pickerdate_ValueChanged(object sender, EventArgs e)
        {
            LoadDate(pickerdate.Value);
        }

        private void pickerMoths_ValueChanged(object sender, EventArgs e)
        {
            LoadDate(pickerMoths.Value, filterByDay: true);
        }
    }
}

// Custom page event helper class to add page numbers
public class PageEventHelper : PdfPageEventHelper
{
    PdfContentByte cb;
    BaseFont bf = null;
    PdfTemplate template;

    public override void OnOpenDocument(PdfWriter writer, Document document)
    {
        cb = writer.DirectContent;
        bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        template = cb.CreateTemplate(50, 50);
    }

    public override void OnEndPage(PdfWriter writer, Document document)
    {
        int pageN = writer.PageNumber;
        String text = "Page " + pageN + " of ";
        float len = bf.GetWidthPoint(text, 8);

        Rectangle pageSize = document.PageSize;
        cb.SetRGBColorFill(100, 100, 100);

        cb.BeginText();
        cb.SetFontAndSize(bf, 8);
        cb.SetTextMatrix(pageSize.GetRight(50), pageSize.GetBottom(10));
        cb.ShowText(text);
        cb.EndText();

        cb.AddTemplate(template, pageSize.GetRight(50) + len, pageSize.GetBottom(10));
    }

    public override void OnCloseDocument(PdfWriter writer, Document document)
    {
        template.BeginText();
        template.SetFontAndSize(bf, 8);
        template.SetTextMatrix(0, 0);
        template.ShowText("" + (writer.PageNumber));
        template.EndText();
    }
}