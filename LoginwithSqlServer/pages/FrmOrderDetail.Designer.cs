namespace LoginwithSqlServer
{
    partial class FormOrderDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataview = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordertime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnback = new Guna.UI2.WinForms.Guna2Button();
            this.btnexport_to_pdf = new Guna.UI2.WinForms.Guna2Button();
            this.pickerdate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pickerMoths = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.txttotalamount = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataview)).BeginInit();
            this.SuspendLayout();
            // 
            // dataview
            // 
            this.dataview.AllowUserToAddRows = false;
            this.dataview.AllowUserToDeleteRows = false;
            this.dataview.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(46)))), ((int)(((byte)(47)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("!Khmer OS Siemreap", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(46)))), ((int)(((byte)(47)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataview.ColumnHeadersHeight = 28;
            this.dataview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.orderid,
            this.orderdate,
            this.ordertime,
            this.pid,
            this.pname,
            this.qty,
            this.price,
            this.amount});
            this.dataview.EnableHeadersVisualStyles = false;
            this.dataview.Location = new System.Drawing.Point(22, 135);
            this.dataview.Name = "dataview";
            this.dataview.ReadOnly = true;
            this.dataview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataview.Size = new System.Drawing.Size(871, 507);
            this.dataview.TabIndex = 30;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "#";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 50;
            // 
            // orderid
            // 
            this.orderid.HeaderText = "លេខវិកាយប័ត្រ";
            this.orderid.Name = "orderid";
            this.orderid.ReadOnly = true;
            // 
            // orderdate
            // 
            this.orderdate.HeaderText = "កាលបរិច្ជេត";
            this.orderdate.Name = "orderdate";
            this.orderdate.ReadOnly = true;
            // 
            // ordertime
            // 
            this.ordertime.HeaderText = "ពេលវេលាបញ្ជាទិញ";
            this.ordertime.Name = "ordertime";
            this.ordertime.ReadOnly = true;
            // 
            // pid
            // 
            this.pid.HeaderText = "លេខផលិតផល";
            this.pid.Name = "pid";
            this.pid.ReadOnly = true;
            // 
            // pname
            // 
            this.pname.HeaderText = "ឈ្មោះផលិតផល";
            this.pname.Name = "pname";
            this.pname.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.HeaderText = "ចំនួន";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            // 
            // price
            // 
            this.price.HeaderText = "តម្លៃ";
            this.price.Name = "price";
            this.price.ReadOnly = true;
            this.price.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.price.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // amount
            // 
            this.amount.HeaderText = "ចំនួនទឹកប្រាក់";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            // 
            // btnback
            // 
            this.btnback.BorderRadius = 5;
            this.btnback.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnback.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnback.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnback.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnback.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.btnback.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnback.ForeColor = System.Drawing.Color.White;
            this.btnback.Location = new System.Drawing.Point(779, 61);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(136, 45);
            this.btnback.TabIndex = 31;
            this.btnback.Text = "&Back";
            this.btnback.Click += new System.EventHandler(this.btnback_Click);
            // 
            // btnexport_to_pdf
            // 
            this.btnexport_to_pdf.BorderRadius = 5;
            this.btnexport_to_pdf.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnexport_to_pdf.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnexport_to_pdf.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnexport_to_pdf.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnexport_to_pdf.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(136)))), ((int)(((byte)(77)))));
            this.btnexport_to_pdf.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnexport_to_pdf.ForeColor = System.Drawing.Color.White;
            this.btnexport_to_pdf.Location = new System.Drawing.Point(474, 61);
            this.btnexport_to_pdf.Name = "btnexport_to_pdf";
            this.btnexport_to_pdf.Size = new System.Drawing.Size(180, 45);
            this.btnexport_to_pdf.TabIndex = 32;
            this.btnexport_to_pdf.Text = "&Export to pdf";
            this.btnexport_to_pdf.Click += new System.EventHandler(this.btnexport_to_pdf_Click);
            // 
            // pickerdate
            // 
            this.pickerdate.BorderRadius = 5;
            this.pickerdate.Checked = true;
            this.pickerdate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pickerdate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.pickerdate.Location = new System.Drawing.Point(22, 61);
            this.pickerdate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.pickerdate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.pickerdate.Name = "pickerdate";
            this.pickerdate.Size = new System.Drawing.Size(200, 45);
            this.pickerdate.TabIndex = 33;
            this.pickerdate.Value = new System.DateTime(2024, 7, 19, 15, 43, 7, 728);
            this.pickerdate.ValueChanged += new System.EventHandler(this.pickerdate_ValueChanged);
            this.pickerdate.TextChanged += new System.EventHandler(this.pickerdate_TextChanged);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(22, 25);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(82, 18);
            this.guna2HtmlLabel1.TabIndex = 34;
            this.guna2HtmlLabel1.Text = "Filter by Moth";
            // 
            // pickerMoths
            // 
            this.pickerMoths.Checked = true;
            this.pickerMoths.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pickerMoths.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.pickerMoths.Location = new System.Drawing.Point(241, 61);
            this.pickerMoths.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.pickerMoths.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.pickerMoths.Name = "pickerMoths";
            this.pickerMoths.Size = new System.Drawing.Size(200, 45);
            this.pickerMoths.TabIndex = 35;
            this.pickerMoths.Value = new System.DateTime(2024, 7, 19, 16, 41, 57, 607);
            this.pickerMoths.ValueChanged += new System.EventHandler(this.pickerMoths_ValueChanged);
            // 
            // txttotalamount
            // 
            this.txttotalamount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(209)))), ((int)(((byte)(142)))));
            this.txttotalamount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(209)))), ((int)(((byte)(142)))));
            this.txttotalamount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txttotalamount.DefaultText = "ចំនួនទឹកប្រាក់សរុប : ";
            this.txttotalamount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(209)))), ((int)(((byte)(142)))));
            this.txttotalamount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(209)))), ((int)(((byte)(142)))));
            this.txttotalamount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txttotalamount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(209)))), ((int)(((byte)(142)))));
            this.txttotalamount.Enabled = false;
            this.txttotalamount.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(209)))), ((int)(((byte)(142)))));
            this.txttotalamount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txttotalamount.Font = new System.Drawing.Font("!Khmer OS Siemreap", 11.25F);
            this.txttotalamount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txttotalamount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txttotalamount.Location = new System.Drawing.Point(23, 664);
            this.txttotalamount.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txttotalamount.Name = "txttotalamount";
            this.txttotalamount.PasswordChar = '\0';
            this.txttotalamount.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(209)))), ((int)(((byte)(142)))));
            this.txttotalamount.PlaceholderText = "";
            this.txttotalamount.SelectedText = "";
            this.txttotalamount.Size = new System.Drawing.Size(235, 36);
            this.txttotalamount.TabIndex = 38;
            // 
            // FormOrderDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(224)))), ((int)(((byte)(180)))));
            this.ClientSize = new System.Drawing.Size(958, 726);
            this.Controls.Add(this.txttotalamount);
            this.Controls.Add(this.pickerMoths);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.pickerdate);
            this.Controls.Add(this.btnexport_to_pdf);
            this.Controls.Add(this.btnback);
            this.Controls.Add(this.dataview);
            this.Name = "FormOrderDetail";
            this.Text = "ពត៏មានរបស់ផលិតផល";
            this.Load += new System.EventHandler(this.JoinTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataview;
        private Guna.UI2.WinForms.Guna2Button btnback;
        private Guna.UI2.WinForms.Guna2Button btnexport_to_pdf;
        private Guna.UI2.WinForms.Guna2DateTimePicker pickerdate;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2DateTimePicker pickerMoths;
        private Guna.UI2.WinForms.Guna2TextBox txttotalamount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderid;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordertime;
        private System.Windows.Forms.DataGridViewTextBoxColumn pid;
        private System.Windows.Forms.DataGridViewTextBoxColumn pname;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
    }
}