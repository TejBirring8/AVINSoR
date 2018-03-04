using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using AVINSoR_Library;
using AVINSoR_Library.PatternClassification.Inputs;
using AVINSoR_Library.PatternClassification.Outputs;
using AVINSoR_Library.PatternClassification.PatternClassifiers;
using Gajatko.Common;

namespace AVINSoR_Client_Demo_WinForms
{
    public partial class MainForm : Form
    {
        private Variable _selectedVariable = null;

        public MainForm()
        {
            InitializeComponent();
        }


        private void FindConnectionOptions(object sender, EventArgs e)
        {
            // find a list of available COM ports to connect to.
            string[] ports = SerialPort.GetPortNames();
            menuConnect.DropDownItems.Clear();
            if (ports.Length < 1) return;
            // if options available, generate drowndown items for each.
            foreach (var s in ports)
            {
                menuConnect.DropDownItems.Add(s, null, TryConnectToNxt);
            }
            // do not make connections available if application already associated with existing NXT Brick connection/controller.
            if (Global.TheRobot == null)
                menuConnect.Enabled = true;
        }


        private void TryConnectToNxt(object sender, EventArgs e)
        {
            // connect and create robot obj
            var menuitem = (ToolStripItem)sender;
            var comPortStr = menuitem.Text;
            // initialize application
            var robot = new AVINSoR_Library.NxtRobot(true, OnRobotConnectionStatusChanged, OnRobotConnectionStatusChanged, null, OnRobotNewBatteryLevel, null, comPortStr);
            Global.InitializeApp(ref robot, 100, 10);
            if (Global.Ready)
            {
                lstRobotVariables.List = Global.TheRobot.Variables;
                lstPatternClassVariables.List = Global.TheCognition.Variables;
                bayesClassifiersControl1.BcmList = Global.TheCognition.BayesClassifierModules;
                // start polling
                Global.TheRobot.StartReadingSensorValues();
                // cognition
                var bcm = new BayesClassifierModule("BCM1");
                bcm.ClassificationCategories.Add(new ClassCategory(){Name = "Close By"});
                bcm.ClassificationCategories.Add(new ClassCategory() { Name = "Far Away" });
                Global.TheCognition.BayesClassifierModules.Add(bcm);
            }
        }


        private void OnRobotNewBatteryLevel(object sender, EventArgs e)
        {
            var robot = (NxtRobot)sender;
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(() => OnRobotNewBatteryLevel(sender, e)));
            }
            // update battery value label
            lblBatteryValue.Text = robot.GetBatteryLevelAsString();
            // show battery value if hidden
            if(lblBatteryValue.Visible == false)
                lblBatteryValue.Visible = true;
        }


        private void OnRobotConnectionStatusChanged (object sender, EventArgs e)
        {
            var robot = (NxtRobot)sender;
            if (robot.IsConnected)
            {
                // * do this if robot connected:
                menuConnect.Enabled = false;
                menuDisconnect.Enabled = true;
                lblConnectedTo.Text = @"Connected to " + robot.Name + @" on COM" + robot.ComPort;
                lblConnectedTo.Visible = true;
            }
            else
            {
                // * do this if robot disconnected:
                menuConnect.Enabled = true;
                menuDisconnect.Enabled = false;
                lblConnectedTo.Visible = false;
                lblBatteryValue.Visible = false;
                // 
                Global.DeinitializeApp();
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuDisconnect_Click(object sender, EventArgs e)
        {
            Global.TheRobot.Disconnect();
        }

        private void startPollingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.TheRobot.StartReadingSensorValues();
        }

        private void stopPollingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.TheRobot.StopReadingSensorValues();
        }

        private void SelectedVariableChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                var variable = (Variable) sender;
                _selectedVariable = variable;
                bayesClassifiersControl1.SelectedVariable = _selectedVariable;
            }
        }




    }
}
