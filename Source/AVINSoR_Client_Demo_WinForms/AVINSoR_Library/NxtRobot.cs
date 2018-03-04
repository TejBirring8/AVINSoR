using System;
using System.Threading;
using System.Windows.Forms;
using AVINSoR_Library.PatternClassification.Inputs;
using AVINSoR_Library.PatternClassification.Inputs.Value;
using Gajatko.Common;

namespace AVINSoR_Library
{
    public partial class NxtRobot : NxtAbstraction.Nxt
    {
        private readonly Variable _reflectedLight;
        private readonly Variable _ambientLight;
        private readonly Variable _soundIntensityDb;
        private readonly Variable _soundIntensityDba;
        private readonly Variable _ultrasonicCm;
        private readonly Variable _batteryLevelMillivolts;
        
        protected NxtRobot()
        { }

        /// <summary>
        /// 
        /// </summary>
        public VariablesList Variables { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        public Variable ReflectedLight
        {
            get { return _reflectedLight; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Variable AmbientLight
        {
            get { return _ambientLight; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Variable SoundIntensityDb
        {
            get { return _soundIntensityDb; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Variable SoundIntensityDba
        {
            get { return _soundIntensityDba; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Variable UltrasonicCm
        {
            get { return _ultrasonicCm; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Variable BatteryLevelMvolts
        {
            get { return _batteryLevelMillivolts; }
        }



        /// <summary>
        /// 
        /// </summary>
        public NxtRobot(bool connectNow, EventHandler onConnected = null, EventHandler onDisconnected = null, EventHandler onSensorModeChanged = null, EventHandler onBatteryValueUpdate = null, EventyList<Variable>.OnActionEventHandler onVariablesListChanged = null, string comPort = "")
        {
            // create public list for pattern classification categories
            Variables = new VariablesList();
            // create sensor-dependant pattern classification variables and add to list
            _reflectedLight = new Variable("Reflected Light", new PercentValue(), this);
            _ambientLight = new Variable("Ambient Light", new PercentValue(), this);
            _soundIntensityDb = new Variable("Sound Intensity (dB)", new PercentValue(), this);
            _soundIntensityDba = new Variable("Sound Intensity (dBA)", new PercentValue(), this);
            _ultrasonicCm = new Variable("Distance (CM)", new ByteValue(), this);
            _batteryLevelMillivolts = new Variable("Battery Level (mV)", new GenericValue(0, 15000, "Millivolts"), this);
            // init sensor variables
            Variables.AddRange(new[] { _reflectedLight, _ambientLight, _soundIntensityDb, _soundIntensityDba, _ultrasonicCm, _batteryLevelMillivolts});
            InitializeSensorVariables();
            // subscribe to important events
            Connected += onConnected;
            Disconnected += onDisconnected;
            SensorModeHasChanged += onSensorModeChanged;
            NewBatteryLevelAvailable += onBatteryValueUpdate;
            Variables.OnAction += onVariablesListChanged;
            // connect
            if (!connectNow) return;
            Connect(comPort);
            if (!IsConnected)
            {
                throw new ApplicationException("Could not connect!");
            }
        }

    

        /// <summary>
        /// Update battery level variable upon new battery level measurement being available.
        /// </summary>
        protected override void OnBatteryLevelUpdated()
        {
            if (BatteryLevelMvolts.Enabled)
            {
                BatteryLevelMvolts.Value.Value = BatteryLevelMillivolts;
            }
        }


        /// <summary>
        /// Update all sensor variables upon new sensor data being available.
        /// </summary>
        protected override void OnSensorDataUpdated()
        {
            if (ReflectedLight.Enabled)
            {
                ReflectedLight.Value.Value = Values.LightSensorPercent;
            }
            if (AmbientLight.Enabled)
            {
                AmbientLight.Value.Value = Values.LightSensorPercent;
            }
            if (SoundIntensityDb.Enabled)
            {
                SoundIntensityDb.Value.Value = Values.SoundSensorPercent;
            }
            if (SoundIntensityDba.Enabled)
            {
                SoundIntensityDba.Value.Value = Values.SoundSensorPercent;
            }
            if (UltrasonicCm.Enabled)
            {
                UltrasonicCm.Value.Value = Values.UltrasonicSensorCm;
            }
        }


        /// <summary>
        /// Enable or Disable sensor variables as appropriate upon sensor mode of the NXT robot being changed.
        /// </summary>
        protected override void OnSensorModeChanged()
        {
            InitializeSensorVariables();
        }

        private void InitializeSensorVariables()
        {
            switch (LightSensorMode)
            {
                case NxtLightSensorMode.Ambient:
                    ReflectedLight.Enabled = false;
                    AmbientLight.Enabled = true;
                    break;
                case NxtLightSensorMode.Reflected:
                    ReflectedLight.Enabled = true;
                    AmbientLight.Enabled = false;
                    break;
            }
            switch (SoundSensorMode)
            {
                case NxtSoundSensorMode.Db:
                    SoundIntensityDb.Enabled = true;
                    SoundIntensityDba.Enabled = false;
                    break;
                case NxtSoundSensorMode.Dba:
                    SoundIntensityDb.Enabled = false;
                    SoundIntensityDba.Enabled = true;
                    break;
            }            
        }

        public string GetBatteryLevelAsString()
        {
            return BatteryLevelMillivolts + " Millivolts (mV)";
        }
    }
}
