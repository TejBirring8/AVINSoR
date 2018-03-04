using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVINSoR_Library.PatternClassification;
using AVINSoR_Library.PatternClassification.Inputs;
using AVINSoR_Library.PatternClassification.Outputs;
using AVINSoR_Library.PatternClassification.PatternClassifiers;

namespace AVINSoR_Client_Demo_WinForms
{
    public partial class BayesClassifiersControl
    {
        /// <summary>
        /// The currently active/selected Input Node.
        /// </summary>
        private ClassCategory _selectedClassCategory = null;

        /// <summary>
        /// Update the the listview so all values/flag/data up-to-date with the associated class categories list of the selected BCM object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateClassCategoriesList(object sender, EventArgs e)
        {
            if (_selectedBCM == null) return;

            if (listClassCategories.InvokeRequired)
            {
                Invoke(new MethodInvoker(() => UpdateClassCategoriesList(sender, e)));
                return;
            }

            _selectedBCM.ClassificationCategories.OnAction += UpdateClassCategoriesList; // on add/remove of input nodes of selected BCM

            // clear exiting items in list
            listClassCategories.Items.Clear();

            // add input node to listview
            foreach (var classCategory in _selectedBCM.ClassificationCategories)
            {
                var name = classCategory.Name;
                var indexInMatlab = classCategory.MatlabIndex;

                var lvi = new ListViewItem(name, indexInMatlab) { Tag = classCategory };
                listClassCategories.Items.Add(lvi);
            }
        }

        /// <summary>
        /// On user selecting/deselecting listviewitem in Classification Categories listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listOfCategories_itemsSelectedOrDeselected(object sender, EventArgs e)
        {
            if (listClassCategories.SelectedItems.Count > 0)
            {
                _selectedClassCategory = (ClassCategory)listClassCategories.SelectedItems[0].Tag;
                OnNewClassCategorySelected();
            }
            else
            {
                _selectedClassCategory = null;
                OnSelectedClassCategoryCleared();
            }
        }

        /// <summary>
        /// When a new Classification Category object has been selected.
        /// </summary>
        private void OnNewClassCategorySelected()
        {
            // enable buttons
            btRemoveCategory.Enabled = true;
        }

        /// <summary>
        /// When selected Classification Category has been cleared.
        /// </summary>
        private void OnSelectedClassCategoryCleared()
        {
            // disable buttons
            btRemoveCategory.Enabled = false;
        }
    }
}
