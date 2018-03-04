using System;
using System.Runtime.Remoting.Messaging;
using System.Timers;
using System.Windows.Forms;
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
        private readonly Random _randomNoGenerator = new Random();
        private bool _emulationMode;

        public bool EmulationMode
        {
            get { return _emulationMode; }

            set
            {
                _emulationMode = value;
                if (_emulationMode)
                {
                    StopReadingSensorValues();
                    // disconnect from BT connection if already connected
                    if(IsConnected)
                        Disconnect(); 
                    // connect as emulator
                    Connect();
                    // start emulation
                    // StartReadingSensorValues();
                }
                else
                {
                    Disconnect();
                }
            }
        }
        //public bool Emulate
        //{
        //    get { return _emulate; }
        //    set
        //    {
        //        _emulate = value;
        //        if (IsConnected)
        //        {
        //            Disconnect();
        //        }
        //        if (_emulate)
        //        {
        //            StartReadingSensorValues();
        //        }
        //        else
        //        {
        //            StopReadingSensorValues();
        //        }
        //    }
        //}

        //private bool _emulate;

        /// <summary>
        /// Connect to the NXT Brick.
        /// </summary>
        /// <param name="comPort">The COM Port associated with the Bluetooth connection. 
        /// If left blank, uses the value of the 'ComPort' property.</param>
        public void Connect(string comPort = "")
        {
            if (EmulationMode)
            {
                Name = "NXT Robot Emulator";
                BatteryLevelMillivolts = _randomNoGenerator.Next(7000, 7999);
                SetBatteryLevelUpdateTimer(3);
                IsConnected = true;
                return;
            }

            SetComPort(comPort);
            // create new NxtBrick obj and connect
            _brick = new McNxtBrick(NxtCommLinkType.Bluetooth, ComPort);
            try
            {
                _brick.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting. " + ex.Message);
                return;
            }
            // if success, do necessary prep stuff here
            BatteryLevelMillivolts = _brick.BatteryLevel;
            Name = _brick.Name;
            SetBatteryLevelUpdateTimer(3);
            InitializeAllSensors();
            //StartReadingSensorValues();
            // update self and inform everybody else
            IsConnected = _brick.IsConnected;
        }


        /// <summary>
        /// Disconnect from NXT Brick.
        /// </summary>
        public void Disconnect()
        {
            StopReadingSensorValues();
            //while (_sensorPollingThread != null)
            //{
            //}
            SetBatteryLevelUpdateTimer(0);

            if (_brick != null)
            {
                _brick.Disconnect();
            }
            
            IsConnected = false;
            


        }

        protected void InformOfNewBatteryLevel()
        {
            if (NewBatteryLevelAvailable != null)
            {
                NewBatteryLevelAvailable(this, new EventArgs());
            }            
        }

        protected void InformOfConnect()
        {
            if (Connected != null)
            {
                Connected(this, new EventArgs());
            }            
        }

        protected void InformOfDisconnect()
        {
            if (Disconnected != null)
            {
                Disconnected(this, new EventArgs());
            }
        }

        /// <summary>
        /// Sets the Battery Level Update Timer to 'tick' every specified number of seconds
        /// at which the battery level property is to be updated.
        /// </summary>
        /// <param name="seconds"></param>
        private void SetBatteryLevelUpdateTimer(int seconds)
        {
            if (seconds > 0)
            {
                _batteryLevelUpdateTimer = new Timer(seconds*1000);
                _batteryLevelUpdateTimer.Elapsed += TimeToUpdateBatteryLevel;
                _batteryLevelUpdateTimer.Start();
            }
            else
            {
                _batteryLevelUpdateTimer.Stop();
            }
        }


        /// <summary>
        /// Response routine to a Battery Level Update Timer 'tick'. Periodic wake-up call
        /// to NXT Brick, and retrieval of the current battery level (in millivolts).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void TimeToUpdateBatteryLevel(object sender, ElapsedEventArgs e)
        {
            if (!IsConnected) return;

            if (_brick != null && _brick.IsConnected)
            {
                _brick.CommLink.KeepAlive(); // give the NXT a little nudge.
                BatteryLevelMillivolts = _brick.BatteryLevel;
            }
            else
            {
                    BatteryLevelMillivolts = _randomNoGenerator.Next(7000, 7999);
            }
        }


        protected virtual void OnBatteryLevelUpdated()
        {
        }

        /// <summary>
        /// Update ComPort with string ONLY IF string is not empty
        /// </summary>
        /// <param name="comPort"></param>
        public void SetComPort(string comPort)
        {
            if (comPort != "")
            {
                ComPort = byte.Parse(comPort.Remove(0, 3));
            }
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
        public bool IsConnected
        {
            get { return _isConnected; }
            protected set
            {
                _isConnected = value;
                if (value)
                {
                    InformOfConnect();
                    StartReadingSensorValues();
                }
                else
                {
                    InformOfDisconnect();
                }
            }
        }
        private bool _isConnected;

        /// <summary>
        /// The battery level of the NXT Brick, in Millivolts, from the last time the Brick
        /// was polled.
        /// </summary>
        public int BatteryLevelMillivolts
        {
            get { return _batteryLvl; }
            set
            {
                _batteryLvl = value;
                // inform all children
                OnBatteryLevelUpdated();
                // inform all external parties
                InformOfNewBatteryLevel();
            }
        }

        private int _batteryLvl;
        /// <summary>
        /// Name of NXT Brick.
        /// </summary>
        public string Name;

        /// <summary>
        /// Event raised when the object connects or disconnects with the NXT Brick.
        /// </summary>
        public event EventHandler Connected;
        /// <summary>
        /// Event raised when the object connects or disconnects with the NXT Brick.
        /// </summary>
        public event EventHandler Disconnected;
        /// <summary>
        /// Event raised when sensors have been polled and a new set of sensor values 
        /// are available.
        /// </summary>
        public event EventHandler NewSensorValueAvailable;
        /// <summary>
        /// Event raised when the battery level has been updated.
        /// </summary>
        public event EventHandler NewBatteryLevelAvailable;
    }
}
