using System;
using System.IO.Ports;
using System.Windows.Forms;
using AVINSoR_Library.NxtAbstraction;


namespace AVINSoR_Library.DTControls
{
    public partial class NxtRobot : UserControl
    {
        //public NxtRobot Robot { get; private set; }
        private AVINSoR_Library.NxtRobot _robot;
        public bool Connected { get { return _connected; }
            private set
            {
                _connected = value;
                if (value)
                {
                    if (HasConnected != null) HasConnected(this, new EventArgs());
                }
                else
                {
                    if (HasDisconnected != null) HasDisconnected(this, new EventArgs());
                }
            } 
        }
        private bool _connected;
        public event EventHandler HasConnected;
        public event EventHandler HasDisconnected;

        public NxtRobot()
        {
            _robot = new AVINSoR_Library.NxtRobot(false, OnConnected, OnDisconnect, OnSensorModeChanged, OnNewBatteryLevel);
            InitializeComponent();
            SetSensorModeComboLists();
            Connected = false;
        }

        private void UpdateControl(object sender, EventArgs e)
        {
            // define listbox
            var lbox = lstAvailableConnections;

            // find a list of available COM ports to connect to.
            string[] ports = SerialPort.GetPortNames();
            lbox.Items.Clear();
            if (ports.Length < 1) return;
            // if options available, generate drowndown items for each.
            foreach (var s in ports)
            {
                lbox.Items.Add(s);
            }
        }

        private void SetSensorModeComboLists()
        {
            cmboLightSensorMode.Items.Clear();
            cmboSoundSensorMode.Items.Clear();
            cmboLightSensorMode.Items.Add(Nxt.NxtLightSensorMode.Ambient);
            cmboLightSensorMode.Items.Add(Nxt.NxtLightSensorMode.Reflected);
            cmboSoundSensorMode.Items.Add(Nxt.NxtSoundSensorMode.Db);
            cmboSoundSensorMode.Items.Add(Nxt.NxtSoundSensorMode.Dba);
        }

        private void ConnectToRobot(object sender, EventArgs e)
        {
            if (lstAvailableConnections.SelectedItems.Count > 0)
            {
                var comPort = (string) lstAvailableConnections.SelectedItems[0];
                // initialize robot obj
                _robot.SetComPort(comPort);
                _robot.Connect();
            }
            else
            {
                MessageBox.Show("COM Port not selected.");
            }
        }

        private void OnConnected(object sender, EventArgs e)
        {
            var robot = (AVINSoR_Library.NxtRobot) sender;
            // connect/disconnect buttons
            btDisconnect.Enabled = true;
            btConnect.Enabled = false;
            // robot name/com-port
            txtName.Text = robot.Name;
            //txtComPort.Text = "COM" + robot.ComPort;
            UpdateBatteryLevel(robot.GetBatteryLevelAsString());
            OnSensorModeChanged(robot, new EventArgs()); // to update sensor combo boxes
            // update flag and inform
            Connected = true;
            
        }

        private void OnDisconnect(object sender, EventArgs e)
        {
            //_robot = null;
            // connect/disconnect buttons
            btDisconnect.Enabled = false;
            btConnect.Enabled = true;
            chkEmulate.Checked = false;
            // update flag and inform
            Connected = false;
        }

        private void OnNewBatteryLevel(object sender, EventArgs e)
        {
            var robot = (AVINSoR_Library.NxtRobot) sender;
            UpdateBatteryLevel(robot.GetBatteryLevelAsString());
        }

        private void UpdateBatteryLevel(string voltageWithUnits)
        {
            if (txtBatteryLevel.InvokeRequired)
            {
                txtBatteryLevel.Invoke(new MethodInvoker(() => UpdateBatteryLevel(voltageWithUnits)));
            }
            txtBatteryLevel.Text = voltageWithUnits;
        }

        private void OnSensorModeChanged(object sender, EventArgs e)
        {
            var robot = (AVINSoR_Library.NxtRobot) sender;
            cmboLightSensorMode.SelectedItem = robot.LightSensorMode;
            cmboSoundSensorMode.SelectedItem = robot.SoundSensorMode;
        }

        public PatternClassification.Inputs.Variable AmbientLightIntensity
        {
            get { return _robot.AmbientLight; }
        }

        public PatternClassification.Inputs.Variable ReflectedLightIntensity
        {
            get { return _robot.ReflectedLight; }
        }

        public PatternClassification.Inputs.Variable SoundIntensity_dB
        {
            get { return _robot.AmbientLight; }
        }

        public PatternClassification.Inputs.Variable SoundIntensity_dBA
        {
            get { return _robot.AmbientLight; }
        }

        public PatternClassification.Inputs.Variable UltrasonicDistance
        {
            get { return _robot.UltrasonicCm; }
        }

        public PatternClassification.Inputs.Variable BatteryLevel
        {
            get { return _robot.BatteryLevelMvolts; }
        }

        public PatternClassification.Inputs.VariablesList Variables
        {
            get { return _robot.Variables; }
        }

        public event EventHandler ModesReconfigured;

        private void SetRobotObjModes(object sender, EventArgs e)
        {
            if (_robot == null)
                return;

            if (!(cmboLightSensorMode.SelectedItem != null & cmboSoundSensorMode.SelectedItem != null)) return;
            var lightSensorMode = (Nxt.NxtLightSensorMode) cmboLightSensorMode.SelectedItem;
            var soundSensorMode = (Nxt.NxtSoundSensorMode) cmboSoundSensorMode.SelectedItem;

            _robot.LightSensorMode = lightSensorMode;
            _robot.SoundSensorMode = soundSensorMode;
            if (ModesReconfigured != null) ModesReconfigured(this, new EventArgs());
        }

        private void chkEmulate_CheckedChanged(object sender, EventArgs e)
        {
            _robot.EmulationMode = chkEmulate.Checked;
        }

        private void DisconnectFromRobot(object sender, EventArgs e)
        {
            
            _robot.Disconnect();
        }
    }
}
