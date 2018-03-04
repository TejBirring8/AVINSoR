namespace AVINSoR_Client_Demo_WinForms
{
    partial class BayesClassifiersControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tblLayoutCentre = new System.Windows.Forms.TableLayoutPanel();
            this.listBCMs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tblInputNodesAndClassCategories = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btAddBcmInputNode = new System.Windows.Forms.Button();
            this.btAddVariableInputNode = new System.Windows.Forms.Button();
            this.btRemoveInputNode = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btAddCategory = new System.Windows.Forms.Button();
            this.btRemoveCategory = new System.Windows.Forms.Button();
            this.listClassCategories = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listInputNodes = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btLockAndActualize = new System.Windows.Forms.Button();
            this.btLiveCategorization = new System.Windows.Forms.Button();
            this.btEnabled = new System.Windows.Forms.Button();
            this.btAddBcm = new System.Windows.Forms.Button();
            this.btRemoveBcm = new System.Windows.Forms.Button();
            this.btLikelihoodsTable = new System.Windows.Forms.Button();
            this.btCreateAssociation = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tblLayoutCentre.SuspendLayout();
            this.tblInputNodesAndClassCategories.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tblLayoutCentre, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.190476F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1142, 656);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1136, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bayes Classifier Modules";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblLayoutCentre
            // 
            this.tblLayoutCentre.ColumnCount = 3;
            this.tblLayoutCentre.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayoutCentre.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayoutCentre.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLayoutCentre.Controls.Add(this.listBCMs, 0, 1);
            this.tblLayoutCentre.Controls.Add(this.tblInputNodesAndClassCategories, 1, 0);
            this.tblLayoutCentre.Controls.Add(this.label2, 0, 0);
            this.tblLayoutCentre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutCentre.Location = new System.Drawing.Point(3, 43);
            this.tblLayoutCentre.Name = "tblLayoutCentre";
            this.tblLayoutCentre.RowCount = 1;
            this.tblLayoutCentre.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tblLayoutCentre.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutCentre.Size = new System.Drawing.Size(1136, 546);
            this.tblLayoutCentre.TabIndex = 12;
            // 
            // listBCMs
            // 
            this.listBCMs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listBCMs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBCMs.Location = new System.Drawing.Point(4, 25);
            this.listBCMs.Margin = new System.Windows.Forms.Padding(4);
            this.listBCMs.Name = "listBCMs";
            this.listBCMs.Size = new System.Drawing.Size(560, 517);
            this.listBCMs.TabIndex = 11;
            this.listBCMs.UseCompatibleStateImageBehavior = false;
            this.listBCMs.View = System.Windows.Forms.View.Details;
            this.listBCMs.SelectedIndexChanged += new System.EventHandler(this.listOfBCMs_itemsSelectedOrDeselected);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 203;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Output (Category)";
            this.columnHeader2.Width = 129;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Locked?";
            this.columnHeader3.Width = 71;
            // 
            // tblInputNodesAndClassCategories
            // 
            this.tblInputNodesAndClassCategories.ColumnCount = 2;
            this.tblInputNodesAndClassCategories.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblInputNodesAndClassCategories.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblInputNodesAndClassCategories.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tblInputNodesAndClassCategories.Controls.Add(this.flowLayoutPanel2, 1, 2);
            this.tblInputNodesAndClassCategories.Controls.Add(this.listClassCategories, 1, 1);
            this.tblInputNodesAndClassCategories.Controls.Add(this.listInputNodes, 0, 1);
            this.tblInputNodesAndClassCategories.Controls.Add(this.label3, 0, 0);
            this.tblInputNodesAndClassCategories.Controls.Add(this.label4, 1, 0);
            this.tblInputNodesAndClassCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInputNodesAndClassCategories.Enabled = false;
            this.tblInputNodesAndClassCategories.Location = new System.Drawing.Point(571, 3);
            this.tblInputNodesAndClassCategories.Name = "tblInputNodesAndClassCategories";
            this.tblInputNodesAndClassCategories.RowCount = 3;
            this.tblLayoutCentre.SetRowSpan(this.tblInputNodesAndClassCategories, 2);
            this.tblInputNodesAndClassCategories.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblInputNodesAndClassCategories.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tblInputNodesAndClassCategories.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblInputNodesAndClassCategories.Size = new System.Drawing.Size(562, 540);
            this.tblInputNodesAndClassCategories.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btAddBcmInputNode);
            this.flowLayoutPanel1.Controls.Add(this.btAddVariableInputNode);
            this.flowLayoutPanel1.Controls.Add(this.btRemoveInputNode);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 439);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(275, 98);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btAddBcmInputNode
            // 
            this.btAddBcmInputNode.Location = new System.Drawing.Point(3, 3);
            this.btAddBcmInputNode.Name = "btAddBcmInputNode";
            this.btAddBcmInputNode.Size = new System.Drawing.Size(162, 23);
            this.btAddBcmInputNode.TabIndex = 0;
            this.btAddBcmInputNode.Text = "Add Input Node (BCM)";
            this.btAddBcmInputNode.UseVisualStyleBackColor = true;
            this.btAddBcmInputNode.Click += new System.EventHandler(this.InsertBCMInputNode);
            // 
            // btAddVariableInputNode
            // 
            this.btAddVariableInputNode.Location = new System.Drawing.Point(3, 32);
            this.btAddVariableInputNode.Name = "btAddVariableInputNode";
            this.btAddVariableInputNode.Size = new System.Drawing.Size(240, 23);
            this.btAddVariableInputNode.TabIndex = 1;
            this.btAddVariableInputNode.Text = "Add Input Node (Variable)";
            this.btAddVariableInputNode.UseVisualStyleBackColor = true;
            this.btAddVariableInputNode.Click += new System.EventHandler(this.InsertVariableInputNode);
            // 
            // btRemoveInputNode
            // 
            this.btRemoveInputNode.Enabled = false;
            this.btRemoveInputNode.Location = new System.Drawing.Point(3, 61);
            this.btRemoveInputNode.Name = "btRemoveInputNode";
            this.btRemoveInputNode.Size = new System.Drawing.Size(81, 23);
            this.btRemoveInputNode.TabIndex = 2;
            this.btRemoveInputNode.Text = "Remove";
            this.btRemoveInputNode.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btAddCategory);
            this.flowLayoutPanel2.Controls.Add(this.btRemoveCategory);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(284, 439);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(275, 98);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // btAddCategory
            // 
            this.btAddCategory.Location = new System.Drawing.Point(3, 3);
            this.btAddCategory.Name = "btAddCategory";
            this.btAddCategory.Size = new System.Drawing.Size(141, 23);
            this.btAddCategory.TabIndex = 0;
            this.btAddCategory.Text = "Add Category";
            this.btAddCategory.UseVisualStyleBackColor = true;
            this.btAddCategory.Click += new System.EventHandler(this.InsertClassCategory);
            // 
            // btRemoveCategory
            // 
            this.btRemoveCategory.Enabled = false;
            this.btRemoveCategory.Location = new System.Drawing.Point(3, 32);
            this.btRemoveCategory.Name = "btRemoveCategory";
            this.btRemoveCategory.Size = new System.Drawing.Size(141, 23);
            this.btRemoveCategory.TabIndex = 1;
            this.btRemoveCategory.Text = "Remove Category";
            this.btRemoveCategory.UseVisualStyleBackColor = true;
            // 
            // listClassCategories
            // 
            this.listClassCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.listClassCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listClassCategories.Location = new System.Drawing.Point(284, 23);
            this.listClassCategories.Name = "listClassCategories";
            this.listClassCategories.Size = new System.Drawing.Size(275, 410);
            this.listClassCategories.TabIndex = 1;
            this.listClassCategories.UseCompatibleStateImageBehavior = false;
            this.listClassCategories.View = System.Windows.Forms.View.Details;
            this.listClassCategories.SelectedIndexChanged += new System.EventHandler(this.listOfCategories_itemsSelectedOrDeselected);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Name";
            this.columnHeader7.Width = 160;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Index (Matlab)";
            this.columnHeader8.Width = 104;
            // 
            // listInputNodes
            // 
            this.listInputNodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader4});
            this.listInputNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listInputNodes.Location = new System.Drawing.Point(3, 23);
            this.listInputNodes.Name = "listInputNodes";
            this.listInputNodes.Size = new System.Drawing.Size(275, 410);
            this.listInputNodes.TabIndex = 0;
            this.listInputNodes.UseCompatibleStateImageBehavior = false;
            this.listInputNodes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Name";
            this.columnHeader5.Width = 139;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Output";
            this.columnHeader6.Width = 127;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Enabled";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(275, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Input Nodes";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(284, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(275, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Classification Categories";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(562, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "BCMs";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btLockAndActualize);
            this.flowLayoutPanel3.Controls.Add(this.btLiveCategorization);
            this.flowLayoutPanel3.Controls.Add(this.btEnabled);
            this.flowLayoutPanel3.Controls.Add(this.btAddBcm);
            this.flowLayoutPanel3.Controls.Add(this.btRemoveBcm);
            this.flowLayoutPanel3.Controls.Add(this.btLikelihoodsTable);
            this.flowLayoutPanel3.Controls.Add(this.btCreateAssociation);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 595);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(1136, 58);
            this.flowLayoutPanel3.TabIndex = 13;
            // 
            // btLockAndActualize
            // 
            this.btLockAndActualize.Enabled = false;
            this.btLockAndActualize.Location = new System.Drawing.Point(3, 3);
            this.btLockAndActualize.Name = "btLockAndActualize";
            this.btLockAndActualize.Size = new System.Drawing.Size(155, 23);
            this.btLockAndActualize.TabIndex = 0;
            this.btLockAndActualize.Text = "Lock And Actualize";
            this.btLockAndActualize.UseVisualStyleBackColor = true;
            this.btLockAndActualize.Click += new System.EventHandler(this.LockSelectedBCM);
            // 
            // btLiveCategorization
            // 
            this.btLiveCategorization.Enabled = false;
            this.btLiveCategorization.Location = new System.Drawing.Point(164, 3);
            this.btLiveCategorization.Name = "btLiveCategorization";
            this.btLiveCategorization.Size = new System.Drawing.Size(198, 23);
            this.btLiveCategorization.TabIndex = 1;
            this.btLiveCategorization.Text = "Categorize";
            this.btLiveCategorization.UseVisualStyleBackColor = true;
            this.btLiveCategorization.Click += new System.EventHandler(this.btCategorize_Click);
            // 
            // btEnabled
            // 
            this.btEnabled.Enabled = false;
            this.btEnabled.Location = new System.Drawing.Point(368, 3);
            this.btEnabled.Name = "btEnabled";
            this.btEnabled.Size = new System.Drawing.Size(75, 23);
            this.btEnabled.TabIndex = 2;
            this.btEnabled.Text = "Enable";
            this.btEnabled.UseVisualStyleBackColor = true;
            this.btEnabled.Click += new System.EventHandler(this.btEnabled_Click);
            // 
            // btAddBcm
            // 
            this.btAddBcm.Location = new System.Drawing.Point(449, 3);
            this.btAddBcm.Name = "btAddBcm";
            this.btAddBcm.Size = new System.Drawing.Size(120, 23);
            this.btAddBcm.TabIndex = 3;
            this.btAddBcm.Text = "Add BCM";
            this.btAddBcm.UseVisualStyleBackColor = true;
            this.btAddBcm.Click += new System.EventHandler(this.InsertBCM);
            // 
            // btRemoveBcm
            // 
            this.btRemoveBcm.Enabled = false;
            this.btRemoveBcm.Location = new System.Drawing.Point(575, 3);
            this.btRemoveBcm.Name = "btRemoveBcm";
            this.btRemoveBcm.Size = new System.Drawing.Size(120, 23);
            this.btRemoveBcm.TabIndex = 4;
            this.btRemoveBcm.Text = "Remove BCM";
            this.btRemoveBcm.UseVisualStyleBackColor = true;
            this.btRemoveBcm.Click += new System.EventHandler(this.RemoveBCM);
            // 
            // btLikelihoodsTable
            // 
            this.btLikelihoodsTable.Enabled = false;
            this.btLikelihoodsTable.Location = new System.Drawing.Point(701, 3);
            this.btLikelihoodsTable.Name = "btLikelihoodsTable";
            this.btLikelihoodsTable.Size = new System.Drawing.Size(147, 23);
            this.btLikelihoodsTable.TabIndex = 3;
            this.btLikelihoodsTable.Text = "Likelihoods Table";
            this.btLikelihoodsTable.UseVisualStyleBackColor = true;
            // 
            // btCreateAssociation
            // 
            this.btCreateAssociation.Enabled = false;
            this.btCreateAssociation.Location = new System.Drawing.Point(854, 3);
            this.btCreateAssociation.Name = "btCreateAssociation";
            this.btCreateAssociation.Size = new System.Drawing.Size(135, 23);
            this.btCreateAssociation.TabIndex = 4;
            this.btCreateAssociation.Text = "Create Association";
            this.btCreateAssociation.UseVisualStyleBackColor = true;
            this.btCreateAssociation.Click += new System.EventHandler(this.btCreateAssociation_Click);
            // 
            // BayesClassifiersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Enabled = false;
            this.Name = "BayesClassifiersControl";
            this.Size = new System.Drawing.Size(1142, 656);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tblLayoutCentre.ResumeLayout(false);
            this.tblLayoutCentre.PerformLayout();
            this.tblInputNodesAndClassCategories.ResumeLayout(false);
            this.tblInputNodesAndClassCategories.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tblLayoutCentre;
        private System.Windows.Forms.ListView listBCMs;
        private System.Windows.Forms.TableLayoutPanel tblInputNodesAndClassCategories;
        private System.Windows.Forms.ListView listInputNodes;
        private System.Windows.Forms.ListView listClassCategories;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btAddBcmInputNode;
        private System.Windows.Forms.Button btAddVariableInputNode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btAddCategory;
        private System.Windows.Forms.Button btRemoveCategory;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button btLockAndActualize;
        private System.Windows.Forms.Button btLiveCategorization;
        private System.Windows.Forms.Button btEnabled;
        private System.Windows.Forms.Button btAddBcm;
        private System.Windows.Forms.Button btRemoveBcm;
        private System.Windows.Forms.Button btRemoveInputNode;
        private System.Windows.Forms.Button btLikelihoodsTable;
        private System.Windows.Forms.Button btCreateAssociation;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}
