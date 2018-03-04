namespace AVINSoR_Client_Demo_WinForms
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConnections = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.movementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPollingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopPollingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblBatteryValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblConnectedTo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lstRobotVariables = new AVINSoR_Client_Demo_WinForms.VariablesControl();
            this.lstPatternClassVariables = new AVINSoR_Client_Demo_WinForms.VariablesControl();
            this.bayesClassifiersControl1 = new AVINSoR_Client_Demo_WinForms.BayesClassifiersControl();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Gray;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.menuConnections,
            this.movementToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1914, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.menuExit});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.newToolStripMenuItem.Text = "New";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(111, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(114, 24);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuConnections
            // 
            this.menuConnections.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConnect,
            this.menuDisconnect});
            this.menuConnections.ForeColor = System.Drawing.Color.White;
            this.menuConnections.Name = "menuConnections";
            this.menuConnections.Size = new System.Drawing.Size(96, 24);
            this.menuConnections.Text = "Connection";
            this.menuConnections.Click += new System.EventHandler(this.FindConnectionOptions);
            // 
            // menuConnect
            // 
            this.menuConnect.Name = "menuConnect";
            this.menuConnect.Size = new System.Drawing.Size(151, 24);
            this.menuConnect.Text = "Connect";
            // 
            // menuDisconnect
            // 
            this.menuDisconnect.Enabled = false;
            this.menuDisconnect.Name = "menuDisconnect";
            this.menuDisconnect.Size = new System.Drawing.Size(151, 24);
            this.menuDisconnect.Text = "Disconnect";
            this.menuDisconnect.Click += new System.EventHandler(this.menuDisconnect_Click);
            // 
            // movementToolStripMenuItem
            // 
            this.movementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startPollingToolStripMenuItem,
            this.stopPollingToolStripMenuItem});
            this.movementToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.movementToolStripMenuItem.Name = "movementToolStripMenuItem";
            this.movementToolStripMenuItem.Size = new System.Drawing.Size(94, 24);
            this.movementToolStripMenuItem.Text = "NXT Robot";
            // 
            // startPollingToolStripMenuItem
            // 
            this.startPollingToolStripMenuItem.Name = "startPollingToolStripMenuItem";
            this.startPollingToolStripMenuItem.Size = new System.Drawing.Size(159, 24);
            this.startPollingToolStripMenuItem.Text = "Start Polling";
            this.startPollingToolStripMenuItem.Click += new System.EventHandler(this.startPollingToolStripMenuItem_Click);
            // 
            // stopPollingToolStripMenuItem
            // 
            this.stopPollingToolStripMenuItem.Name = "stopPollingToolStripMenuItem";
            this.stopPollingToolStripMenuItem.Size = new System.Drawing.Size(159, 24);
            this.stopPollingToolStripMenuItem.Text = "Stop Polling";
            this.stopPollingToolStripMenuItem.Click += new System.EventHandler(this.stopPollingToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Gray;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblBatteryValue,
            this.lblConnectedTo,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 658);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1914, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblBatteryValue
            // 
            this.lblBatteryValue.Name = "lblBatteryValue";
            this.lblBatteryValue.Size = new System.Drawing.Size(97, 24);
            this.lblBatteryValue.Text = "Battery Value";
            this.lblBatteryValue.Visible = false;
            // 
            // lblConnectedTo
            // 
            this.lblConnectedTo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblConnectedTo.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.lblConnectedTo.Name = "lblConnectedTo";
            this.lblConnectedTo.Size = new System.Drawing.Size(85, 24);
            this.lblConnectedTo.Text = "NXT Name";
            this.lblConnectedTo.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblStatus.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(4, 17);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.lstRobotVariables, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstPatternClassVariables, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.bayesClassifiersControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1914, 630);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // lstRobotVariables
            // 
            this.lstRobotVariables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstRobotVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRobotVariables.List = null;
            this.lstRobotVariables.Location = new System.Drawing.Point(3, 3);
            this.lstRobotVariables.Name = "lstRobotVariables";
            this.lstRobotVariables.Size = new System.Drawing.Size(632, 309);
            this.lstRobotVariables.TabIndex = 2;
            this.lstRobotVariables.TopText = "Robot Level : Variables";
            this.lstRobotVariables.SelectedVariableChanged += new System.EventHandler(this.SelectedVariableChanged);
            // 
            // lstPatternClassVariables
            // 
            this.lstPatternClassVariables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPatternClassVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPatternClassVariables.List = null;
            this.lstPatternClassVariables.Location = new System.Drawing.Point(641, 3);
            this.lstPatternClassVariables.Name = "lstPatternClassVariables";
            this.lstPatternClassVariables.Size = new System.Drawing.Size(632, 309);
            this.lstPatternClassVariables.TabIndex = 3;
            this.lstPatternClassVariables.TopText = "Cognition Level : Variables";
            this.lstPatternClassVariables.SelectedVariableChanged += new System.EventHandler(this.SelectedVariableChanged);
            // 
            // bayesClassifiersControl1
            // 
            this.bayesClassifiersControl1.BcmList = null;
            this.bayesClassifiersControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.bayesClassifiersControl1, 3);
            this.bayesClassifiersControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bayesClassifiersControl1.Enabled = false;
            this.bayesClassifiersControl1.Location = new System.Drawing.Point(3, 318);
            this.bayesClassifiersControl1.Name = "bayesClassifiersControl1";
            this.bayesClassifiersControl1.SelectedVariable = null;
            this.bayesClassifiersControl1.Size = new System.Drawing.Size(1908, 309);
            this.bayesClassifiersControl1.TabIndex = 4;
            this.bayesClassifiersControl1.TopText = "Cognition Level : Pattern Classification I/O";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1914, 680);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.Black;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "AVINSoR Client Demo";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuConnections;
        private System.Windows.Forms.ToolStripMenuItem menuConnect;
        private System.Windows.Forms.ToolStripMenuItem menuDisconnect;
        private System.Windows.Forms.ToolStripMenuItem movementToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblBatteryValue;
        private System.Windows.Forms.ToolStripStatusLabel lblConnectedTo;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private VariablesControl lstRobotVariables;
        private System.Windows.Forms.ToolStripMenuItem startPollingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopPollingToolStripMenuItem;
        private BayesClassifiersControl bayesClassifiersControl1;
        private VariablesControl lstPatternClassVariables;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

