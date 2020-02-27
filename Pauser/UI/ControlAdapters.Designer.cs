namespace Pauser.UI {
    partial class ControlAdapters {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewAdapters = new System.Windows.Forms.DataGridView();
            this.ColumnSelection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnDeviceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNetConnectionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdapters)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewAdapters, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(739, 406);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // dataGridViewAdapters
            // 
            this.dataGridViewAdapters.AllowUserToAddRows = false;
            this.dataGridViewAdapters.AllowUserToDeleteRows = false;
            this.dataGridViewAdapters.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewAdapters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewAdapters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAdapters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSelection,
            this.ColumnDeviceId,
            this.ColumnNetConnectionId,
            this.ColumnName,
            this.ColumnDescription});
            this.dataGridViewAdapters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewAdapters.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewAdapters.Name = "dataGridViewAdapters";
            this.tableLayoutPanel1.SetRowSpan(this.dataGridViewAdapters, 3);
            this.dataGridViewAdapters.Size = new System.Drawing.Size(733, 400);
            this.dataGridViewAdapters.TabIndex = 0;
            // 
            // ColumnSelection
            // 
            this.ColumnSelection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnSelection.DataPropertyName = "Selected";
            this.ColumnSelection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ColumnSelection.HeaderText = "Selected";
            this.ColumnSelection.Name = "ColumnSelection";
            this.ColumnSelection.Width = 55;
            // 
            // ColumnDeviceId
            // 
            this.ColumnDeviceId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnDeviceId.DataPropertyName = "DeviceId";
            this.ColumnDeviceId.HeaderText = "DeviceId";
            this.ColumnDeviceId.Name = "ColumnDeviceId";
            this.ColumnDeviceId.ReadOnly = true;
            this.ColumnDeviceId.Width = 75;
            // 
            // ColumnNetConnectionId
            // 
            this.ColumnNetConnectionId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnNetConnectionId.DataPropertyName = "NetConnectionId";
            this.ColumnNetConnectionId.HeaderText = "NetConnectionId";
            this.ColumnNetConnectionId.Name = "ColumnNetConnectionId";
            this.ColumnNetConnectionId.ReadOnly = true;
            this.ColumnNetConnectionId.Width = 112;
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnName.DataPropertyName = "Name";
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Width = 60;
            // 
            // ColumnDescription
            // 
            this.ColumnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnDescription.DataPropertyName = "Description";
            this.ColumnDescription.HeaderText = "Description";
            this.ColumnDescription.Name = "ColumnDescription";
            this.ColumnDescription.ReadOnly = true;
            this.ColumnDescription.Width = 85;
            // 
            // ControlAdapters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ControlAdapters";
            this.Size = new System.Drawing.Size(739, 406);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdapters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewAdapters;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSelection;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDeviceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNetConnectionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
    }
}
