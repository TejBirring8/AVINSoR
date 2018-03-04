using System;
using System.Timers;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;
using Timer = System.Timers.Timer;

namespace AVINSoR_Library.NxtAbstraction
{
    public partial class Nxt
    {
        private McNxtBrick _brick; // Obj to use to interface with the Lego(R) NXT Brick.
        private Timer _batteryLevelUpdateTimer; // Timer to control time period at which the
                                                //  battery level property should be updated.


        /// <summary>
        /// Connect to the NXT Brick.
        /// </summary>
        /// <param name="comPort">The COM Port associated with the Bluetooth connection. 
        /// If left blank, uses the value of the 'ComPort' property.</param>
        public void Connect(string comPort = "")
        {
            // update the ComPort property
            if (comPort != "")
            {
                ComPort = byte.Parse(comPort.Remove(0, 3));
            }
            // create new NxtBrick obj and connect
            _brick = new McNxtBrick(NxtCommLinkType.Bluetooth, ComPort);
            _brick.Connect();
            // update self and inform everybody else
            IsConnected = _brick.IsConnected;
            if (ConnectionStatusHasChanged != null)
            {
                ConnectionStatusHasChanged(this, new EventArgs());
            }
            // if success, do necessary prep stuff here
            SetBatteryLevelUpdateTimer(1);
            InitializeAllSensors();
            //StartReadingSensorValues(false);
        }


        /// <summary>
        /// Sets the Battery Level Update Timer to 'tick' every specified number of seconds
        /// at which the battery level property is to be updated.
        /// </summary>
        /// <param name="seconds"></param>
        private void SetBatteryLevelUpdateTimer(int seconds)
        {
            _batteryLevelUpdateTimer = new Timer(seconds * 1000);
            _batteryLevelUpdateTimer.Elapsed += TimeToUpdateBatteryLevel;
            _batteryLevelUpdateTimer.Start();
        }


        /// <summary>
        /// Response routine to a Battery Level Update Timer 'tick'. Periodic wake-up call
        /// to NXT Brick, and retrieval of the current battery level (in millivolts).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeToUpdateBatteryLevel(object sender, ElapsedEventArgs e)
        {
            _brick.CommLink.KeepAlive();    // give the NXT a little nudge.
            BatteryLevelMillivolts = _brick.BatteryLevel;
            // inform all children
            OnBatteryLevelUpdated();
            // inform all external parties
            if (NewBatteryLevelAvailable != null)
            {
                NewBatteryLevelAvailable(this, new EventArgs());
            }
        }


        protected virtual void OnBatteryLevelUpdated()
        {
        }


        /// <summary>
        /// The COM port associated with the Bluetooth connection to the NXT (if connected). 
        /// If not connected, this property can be set so that the next time a connection is 
        /// attempted, it will try to connect to the specified COM port.
        /// </summary>
        public byte ComPort { get; set; }
        /// <summary>
        /// Indicates wether the object is currently connected to an NXT Brick.
        /// </summary>
        public bool IsConnected { get; private set; }
        /// <summary>
        /// The battery level of the NXT Brick, in Millivolts, from the last time the Brick
        /// was polled.
        /// </summary>
        public int BatteryLevelMillivolts;


        /// <summary>
        /// Event raised when the object connects or disconnects with the NXT Brick.
        /// </summary>
        public event EventHandler ConnectionStatusHasChanged;
        /// <summary>
        /// Event raised when sensors have been polled and a new set of sensor values 
        /// are available.
        /// </summary>
        public event EventHandler NewSensorValuesAvailable;
        /// <summary>
        /// Event raised when the battery level has been updated.
        /// </summary>
        public event EventHandler NewBatteryLevelAvailable;
    }
}
