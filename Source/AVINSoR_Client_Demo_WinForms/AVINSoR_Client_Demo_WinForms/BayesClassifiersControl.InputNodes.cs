using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVINSoR_Library.PatternClassification;
using AVINSoR_Library.PatternClassification.Inputs;
using AVINSoR_Library.PatternClassification.PatternClassifiers;

namespace AVINSoR_Client_Demo_WinForms
{
    public partial class BayesClassifiersControl
    {
        /// <summary>
        /// The currently active/selected Input Node.
        /// </summary>
        private PatternClassificationInput _selectedInputNode = null;

        /// <summary>
        /// Update the the listview so all values/flag/data up-to-date with the associated InputNodes list of the selected BCM object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateInputNodesList(object sender, EventArgs e)
        {
            _selectedBCM.InputNodes.OnAction += UpdateInputNodesList; // on add/remove of input nodes of selected BCM

            // clear exiting items in list
            listInputNodes.Items.Clear();

            // add input node to listview
            foreach (var input in _selectedBCM.InputNodes)
            {
                var name = "";
                var result = "";

                if (input.GetType() == typeof (BayesClassifierModule))
                {
                    var inputBcm = (BayesClassifierModule) input;
                    name = inputBcm.Name + " (BCM)";
                    if (inputBcm.Result != null)
                    {
                        result = inputBcm.Result.Name;
                    }
                    inputBcm.NewResultAvailable += UpdateInputNodeInList;
                }
                else if (input.GetType() == typeof (Variable))
                {
                    var inputVar = (Variable) input;
                    name = inputVar.Name + " (Variable)";
                    result = inputVar.Value.Value + " " + inputVar.Value.Units;
                    inputVar.NewResultAvailable += UpdateInputNodeInList;
                }

                var lvi = new ListViewItem(new[] {name, result, input.Enabled.ToString()}) {Tag = input};
                listInputNodes.Items.Add(lvi);
            }
        }


        /// <summary>
        /// Update the particular listviewitem associated with the input node  (upon change - i.e. use this method as response to an event).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateInputNodeInList(object sender, EventArgs e)
        {
            if (listInputNodes.InvokeRequired)
            {
                listInputNodes.Invoke(new MethodInvoker(() => UpdateInputNodeInList(sender, e)));
                return;
            }

            var input = (PatternClassificationInput) sender;
            foreach (var lvi in listInputNodes.Items.Cast<ListViewItem>().Where(lvi => lvi.Tag == input))
            {
                var name = "";
                var result = "";
                var enabled = input.Enabled;

                if (input.GetType() == typeof(BayesClassifierModule))
                {
                    var inputBcm = (BayesClassifierModule)input;
                    name = inputBcm.Name + " (BCM)";
                    result = inputBcm.Result.Name;
                }
                else if (input.GetType() == typeof(Variable))
                {
                    var inputVar = (Variable)input;
                    name = inputVar.Name + " (Variable)";
                    result = inputVar.Value.Value + " " + inputVar.Value.Units;
                }

                // update current categorization output
                lvi.SubItems[0].Text = name;
                // update 'locked' status
                lvi.SubItems[1].Text = result;
                // update 'enabled' status
                lvi.SubItems[2].Text = enabled.ToString();
            }
        }

        /// <summary>
        /// On user selecting/deselecting listviewitem in Input Nodes listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listOfInputNodes_itemsSelectedOrDeselected(object sender, EventArgs e)
        {
            if (listInputNodes.SelectedItems.Count > 0)
            {
                _selectedInputNode = (PatternClassificationInput)listInputNodes.SelectedItems[0].Tag;
                OnNewInputNodeSelected();
            }
            else
            {
                _selectedInputNode = null;
                OnSelectedInputNodeCleared();
            }
        }

        /// <summary>
        /// When a new Input Node object has been selected.
        /// </summary>
        private void OnNewInputNodeSelected()
        {
            // enable buttons
            btRemoveInputNode.Enabled = true;
        }

        /// <summary>
        /// When selected Input Node has been cleared.
        /// </summary>
        private void OnSelectedInputNodeCleared()
        {
            // disable buttons
            btRemoveInputNode.Enabled = false;
        }
    }
}
