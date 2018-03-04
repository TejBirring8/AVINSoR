using System;
using System.Linq;
using System.Windows.Forms;
using AVINSoR_Library.PatternClassification;
using AVINSoR_Library.PatternClassification.Inputs;
using AVINSoR_Library.PatternClassification.Outputs;
using AVINSoR_Library.PatternClassification.PatternClassifiers;
using Microsoft.VisualBasic;

namespace AVINSoR_Client_Demo_WinForms
{
    public partial class BayesClassifiersControl : UserControl
    {
        /// <summary>
        /// The currently active/selected Bayes Classifier Module.
        /// </summary>
        private BayesClassifierModule _selectedBCM = null;

        /// <summary>
        /// Control text / title.
        /// </summary>
        public string TopText {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        /// <summary>
        /// The BayesClassifierModuleList object associated with this control.
        /// </summary>
        private BayesClassifierModuleList _bcmList;
        /// <summary>
        /// Get or set the BayesClassifierModuleList object associated with this control.
        /// </summary>
        public BayesClassifierModuleList BcmList
        {
            get { return _bcmList; }
            set
            {
                _bcmList = value;
                UpdateBCMList(null, null);
                this.Enabled = true;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public BayesClassifiersControl()
        {
            InitializeComponent();
            label1.Text = Text;
        }

        /// <summary>
        /// Update the the listview so all values/flag/data up-to-date with the associated BayesClassifierList object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBCMList(object sender, EventArgs e)
        {
            // return if no BCM list object
            if (_bcmList == null)
                return;

            BcmList.OnAction += UpdateBCMList; // On add/remove of BCM

            // clear the existing/old listview items
            listBCMs.Items.Clear();
            
            // add BCM items in list to listview
            foreach (var bcm in _bcmList)
            {
                // create new listview item and add to listview -- bcm.ClassificationCategories[bcm.ClassifierOutput].Name
                var lvi = new ListViewItem(new[] { bcm.Name, "", bcm.Locked.ToString() }) { Tag = bcm };
                listBCMs.Items.Add(lvi);
                // subscribe to events that SHOULD cause the LISTVIEWITEM to update itself in accordance to BCM list
                bcm.NewResultAvailable += UpdateBCMInListForResult;// On new result
                bcm.OnEnableStatusChanged += UpdateBCMInList; // On BCM enabled/disabled
                bcm.HasBeenLocked += UpdateBCMInList; // On BCM locked/actualized
            }
            // auto-size the columns
            listBCMs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listBCMs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// Update the listviewitem of a BCM (upon change - i.e. use this method as response to an event).
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void UpdateBCMInList(object sender, EventArgs e)
        {
            var bcm = (BayesClassifierModule) sender;
            if (listBCMs.InvokeRequired)
            {
                listBCMs.Invoke(new MethodInvoker(() => UpdateBCMInList(sender, e)));
                return;
            }

            foreach (var lvi in listBCMs.Items.Cast<ListViewItem>().Where(lvi => lvi.Tag == bcm))
            {
                // update current categorization output
                if(bcm.ClassifierOutput > -1)
                    lvi.SubItems[1].Text = bcm.ClassificationCategories[bcm.ClassifierOutput].Name;
                // update 'locked' status
                lvi.SubItems[2].Text = bcm.Locked.ToString();
            }
        }

        private void UpdateBCMInListForResult(object sender, EventArgs e)
        {
            var bcm = (BayesClassifierModule)sender;
            if (listBCMs.InvokeRequired)
            {
                listBCMs.Invoke(new MethodInvoker(() => UpdateBCMInListForResult(sender, e)));
                return;
            }

            foreach (var lvi in listBCMs.Items.Cast<ListViewItem>().Where(lvi => lvi.Tag == bcm))
            {
                    lvi.SubItems[1].Text = bcm.ClassificationCategories[bcm.ClassifierOutput].Name;
            }
        }

        /// <summary>
        /// On user selecting/deselecting listviewitem in BCM listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listOfBCMs_itemsSelectedOrDeselected(object sender, EventArgs e)
        {
            if (listBCMs.SelectedItems.Count > 0)
            {
                _selectedBCM = (BayesClassifierModule) listBCMs.SelectedItems[0].Tag;
                OnNewBCMSelected();    
            }
            else
            {
                _selectedBCM = null;
                OnSelectedBCMCleared();
            }
        }

        /// <summary>
        /// When a new BCM object has been selected.
        /// </summary>
        private void OnNewBCMSelected()
        {
            btRemoveBcm.Enabled = true;
            tblInputNodesAndClassCategories.Enabled = true;
            if (!_selectedBCM.Locked)
            {
                btLockAndActualize.Enabled = true;
                UpdateInputNodesList(null, null);
                UpdateClassCategoriesList(null, null);
            }
            else
            {
                btEnabled.Enabled = true;
                btLockAndActualize.Enabled = false;
                btLiveCategorization.Enabled = true;
                btLikelihoodsTable.Enabled = true;
                btCreateAssociation.Enabled = true;
            }
        }

        /// <summary>
        /// When selected BCM has been cleared.
        /// </summary>
        private void OnSelectedBCMCleared()
        {
            btRemoveBcm.Enabled = false;
            btLockAndActualize.Enabled = false;
            btEnabled.Enabled = false;
            btLiveCategorization.Enabled = false;
            btLikelihoodsTable.Enabled = false;
            btCreateAssociation.Enabled = false;
            tblInputNodesAndClassCategories.Enabled = false;
        }

        private void LockSelectedBCM(object sender, EventArgs e)
        {
            _selectedBCM.Lock();
        }

        private void RemoveBCM(object sender, EventArgs e)
        {
            BcmList.Remove(_selectedBCM);
        }

        private void btEnabled_Click(object sender, EventArgs e)
        {
            _selectedBCM.Enabled = true;
        }

        private void btCategorize_Click(object sender, EventArgs e)
        {
            _selectedBCM.Run();
        }

        private void btCreateAssociation_Click(object sender, EventArgs e)
        {
            _selectedBCM.AssociateWithCategory(_selectedClassCategory,true);
        }







    }
}
