using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AVINSoR_Library.PatternClassification.Inputs;
using AVINSoR_Library.PatternClassification.Outputs;
using AVINSoR_Library.PatternClassification.PatternClassifiers;

namespace AVINSoR_Client_Demo_WinForms
{
    public partial class BayesClassifiersControl
    {
        public Variable SelectedVariable { get; set; }

        private void InsertBCMInputNode(object sender, EventArgs e)
        {
            // generate the form
            var tbl = GenerateTable(1, 2);
            tblLayoutCentre.Controls.Add(tbl,2,1);
            tbl.Dock = DockStyle.Fill;
            var lstOfAvailableBCMs = new ListView();
            var btnsTbl = GenerateTable(2, 1);
            foreach (var bcm in BcmList)
            {
                var lvi = new ListViewItem(bcm.Name) {Tag = bcm};
                lstOfAvailableBCMs.Items.Add(lvi);
            }
            tbl.Controls.Add(lstOfAvailableBCMs, 0, 0);


            var bt1 = new Button() { Text = @"Add BCM", Tag = lstOfAvailableBCMs };
            bt1.Click += AddBCMAsInputNode;
            var bt2 = new Button() { Text = @"Cancel", Tag = tbl };
            bt2.Click += ClearLayoutTable;
            btnsTbl.Controls.Add(bt1, 0, 0);
            btnsTbl.Controls.Add(bt2, 0, 1);

            tbl.Controls.Add(btnsTbl, 0, 1);
            btnsTbl.Dock = DockStyle.Fill;
        }

        void AddBCMAsInputNode(object sender, EventArgs e)
        {
            var bt = (Button) sender;
            var lstView = (ListView) bt.Tag;
            BayesClassifierModule bcm = null;
            // check if BCM selected and get selected BCM
            if (lstView.SelectedItems.Count > 0)
            {
                bcm = (BayesClassifierModule) lstView.SelectedItems[0].Tag;
            }
            // return error if null
            if (bcm != null)
            {
                _selectedBCM.InputNodes.Add(bcm);
                MessageBox.Show(_selectedBCM.InputNodes.Count.ToString());
            }
            else
            {
                throw new ApplicationException("No BCM selected.");
            }
        }

        private void InsertVariableInputNode(object sender, EventArgs e)
        {
            // generate the form
            var tbl = GenerateTable(2, 1);
            tblLayoutCentre.Controls.Add(tbl, 2, 1);
            var bt1 = new Button() { Text = @"Add Selected Variable", Tag = SelectedVariable };
            bt1.Click += AddVariableAsInputNode;
            var bt2 = new Button() { Text = @"Cancel", Tag = tbl };
            bt2.Click += ClearLayoutTable;
            tbl.Controls.Add(bt1, 0, 0);
            tbl.Controls.Add(bt2, 0, 1);            
        }

        void AddVariableAsInputNode(object sender, EventArgs e)
        {
            var bt = (Button) sender;
            var variable = (Variable) bt.Tag;
            _selectedBCM.InputNodes.Add(variable);
        }

        private void InsertClassCategory(object sender, EventArgs e)
        {
            // generate the form
            var tbl = GenerateTable(2, 2);
            tblLayoutCentre.Controls.Add(tbl, 2, 1);
            var lbl = new Label() { Text = @"Name: " };
            var txtbox = new TextBox();
            var bt1 = new Button() { Text = @"Add", Tag = txtbox};
            bt1.Click += AddNewCategory;
            var bt2 = new Button() { Text = @"Cancel", Tag = tbl };
            bt2.Click += ClearLayoutTable;
            tbl.Controls.Add(lbl, 0, 0);
            tbl.Controls.Add(txtbox, 1, 0);
            tbl.Controls.Add(bt1, 0, 1);
            tbl.Controls.Add(bt2, 1, 1);
        }

        void AddNewCategory(object sender, EventArgs e)
        {
            var bt = (Button) sender;
            var tbox = (TextBox) bt.Tag;
            var cat = new ClassCategory(){Name=tbox.Text};
            _selectedBCM.ClassificationCategories.Add(cat);
        }

        private void InsertBCM(object sender, EventArgs e)
        {
            // generate the form
            var tbl = GenerateTable(2, 2);
            tblLayoutCentre.Controls.Add(tbl, 2, 1);
            var lbl = new Label() {Text = @"Name: "};
            var txtbox = new TextBox();
            var bt1 = new Button() { Text = @"Add", Tag= txtbox};
            bt1.Click += AddNewBCM;
            var bt2 = new Button() { Text = @"Cancel", Tag=tbl};
            bt2.Click += ClearLayoutTable;
            tbl.Controls.Add(lbl,0,0);
            tbl.Controls.Add(txtbox, 1, 0);
            tbl.Controls.Add(bt1, 0, 1);
            tbl.Controls.Add(bt2, 1, 1);
        }

        void ClearLayoutTable(object sender, EventArgs e)
        {
            var bt = (Button) sender;
            var tbl = (TableLayoutPanel) bt.Tag;
            tblLayoutCentre.Controls.Remove(tbl);
        }

        void AddNewBCM(object sender, EventArgs e)
        {
            var bt = (Button) sender;
            var txtbox = (TextBox) bt.Tag;
            BcmList.Add(new BayesClassifierModule(txtbox.Text));
        }

        private TableLayoutPanel GenerateTable(int columnCount, int rowCount, Control parent = null)
        {
            //Create control
            var tbl = new TableLayoutPanel();
            if(parent != null)
            { 
                tbl.Parent = parent;
                tbl.Dock = DockStyle.Fill;
            }
            //Clear out the existing controls, we are generating a new table layout
            tbl.Controls.Clear();

            //Clear out the existing row and column styles
            tbl.ColumnStyles.Clear();
            tbl.RowStyles.Clear();

            //Now we will generate the table, setting up the row and column counts first
            tbl.ColumnCount = columnCount;
            tbl.RowCount = rowCount;

            for (var x = 0; x < columnCount; x++)
            {
                //First add a column
                tbl.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                for (var y = 0; y < rowCount; y++)
                {
                    //Next, add a row.  Only do this when once, when creating the first column
                    if (x == 0)
                    {
                        tbl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    }
                }
            }

            return tbl;
        }
    }
}
