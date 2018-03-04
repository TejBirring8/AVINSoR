using System;
using System.Windows.Forms;
using AVINSoR_Library.PatternClassification.Inputs;

namespace AVINSoR_Client_Demo_WinForms
{
    public partial class VariablesControl : UserControl
    {
        private string text;
        public Variable SelectedVariable { get; private set; }

        public string TopText
        {
            get { return text; }
            set
            {
                text = value;
                label1.Text = text;
            }
        }
        private VariablesList _list;
        public VariablesList List 
        {
            get { return _list; }
            set
            {
                _list = value;
                UpdateList();
            }
        }

        public VariablesControl()
        {
            SelectedVariable = null;
            InitializeComponent();
        }

        public void UpdateList()
        {
            if (_list == null)
                return;

            listVariables.Items.Clear();

            foreach (var v in _list)
            {
                //if (!v.Enabled) continue;
                var value = "";
                if (v.Enabled)
                {
                    value = v.Value.Value.ToString();
                }

                var lvi = new ListViewItem(new string[] {v.Name, value, v.Value.Units, v.Enabled.ToString()}) {Tag = v};
                listVariables.Items.Add(lvi);
                v.NewResultAvailable += VariableValueUpdated;
            }


            listVariables.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listVariables.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void VariableValueUpdated(object sender, EventArgs e)
        {
            var v = (Variable) sender;

            // if invoke required
            if (listVariables.InvokeRequired)
            {
                listVariables.Invoke(new MethodInvoker(() => VariableValueUpdated(sender, e)));
                return;
            }

            // else update value with latest
            foreach (ListViewItem lvi in listVariables.Items)
            {
                if (lvi.Tag == v)
                {
                    lvi.SubItems[1].Text = v.Value.Value.ToString();
                }
            }
        }

        private void listVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listVariables.SelectedItems.Count > 0)
            {
                SelectedVariable = (Variable) listVariables.SelectedItems[0].Tag;
            }
            else
            {
                SelectedVariable = null;
            }
            // inform 
            if (SelectedVariableChanged != null)
            {
                SelectedVariableChanged(SelectedVariable, new EventArgs());
            }
        }

        public event EventHandler SelectedVariableChanged;

        //listRobotVariables.Update();
    }
}
