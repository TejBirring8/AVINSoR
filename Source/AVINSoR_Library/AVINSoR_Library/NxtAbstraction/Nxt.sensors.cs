using System;
using System.Threading;
using NKH.MindSqualls;

namespace AVINSoR_Library.NxtAbstraction
{
    public partial class Nxt
    {
        private Thread _sensorPollingThread; // thread use to recursively execute the sensor polling routine.
        private bool _ultrasonicIsSwitchedOn; // indicates wether the ultrasonic sensor is active. Default value is 'false'.
        // sensor objects
        private NxtUltrasonicSensor _ultrasonicSensor;
        private NxtLightSensor _lightSensor;
        private NxtSoundSensor _soundSensor;


        /// <summary>
        /// The last set of sensor data read from the NXT Brick. Discard if '-1'.
        /// </summary>
        public NxtSensorValues Values { get; private set; }


        /// <summary>
        /// Ensure all sensors are ready to go.
        /// </summary>
        private void InitializeAllSensors()
        {
            // Ultrasonic Sensor on Sensor Port 1
            _ultrasonicSensor = new NxtUltrasonicSensor();
            _brick.Sensor1 = _ultrasonicSensor;
            _brick.CommLink.SetInputMode(NxtSensorPort.Port1, NxtSensorType.LOWSPEED_9V, NxtSensorMode.RAWMODE); // required because NxtUltrasonicSensor class does not set this property
            // Light Sensor on Sensor Port 2
            _lightSensor = new NxtLightSensor();
            _brick.Sensor2 = _lightSensor;
            // Sound Sensor on Sensor Port 3
            _soundSensor = new NxtSoundSensor();
            _brick.Sensor3 = _soundSensor;
            // Update Sensor Settings
            UpdateSensorSettings();
        }


        /// <summary>
        /// Update sensor settings in accordance to the 'sensor mode' properties.
        /// </summary>
        private void UpdateSensorSettings()
        {
            _lightSensor.GenerateLight = _lightSensorMode != NxtLightSensorMode.Ambient;
            _soundSensor.dBA = _soundSensorMode != NxtSoundSensorMode.Db;
            OnSensorModeChanged();
        }

        protected virtual void OnSensorModeChanged()
        {}
        

        /// <summary>
        /// Start polling sensors recursively (on a dedicated thread).
        /// </summary>
        public  void StartReadingSensorValues(bool emulate)
        {
            _sensorPollingThread = !emulate ? new Thread(RecursiveSensorReading) {IsBackground = true} : new Thread(EmulateRecursiveSensorReading) { IsBackground = true };
            _sensorPollingThread.Start();
        }


        /// <summary>
        /// Request sensor data in a non-stop loop.
        /// ONLY FOR EXECUTION ON AN DEDICATED THREAD.
        /// </summary>
        private void RecursiveSensorReading()
        {
            while (true)
            {
                Values = GetSensorData();
                // inform everybody of new sensor data
                OnSensorDataUpdated();
                if (NewSensorValuesAvailable != null)
                {
                    NewSensorValuesAvailable(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Request sensor data in a non-stop loop.
        /// ONLY FOR EXECUTION ON AN DEDICATED THREAD.
        /// </summary>
        private void EmulateRecursiveSensorReading()
        {
            while (true)
            {
                Values = EmualateGetSensorData();
                // inform everybody of new sensor data
                OnSensorDataUpdated();
                OnBatteryLevelUpdated();
                if ((NewSensorValuesAvailable != null) & (NewBatteryLevelAvailable != null))
                {
                    NewSensorValuesAvailable(this, new EventArgs());
                    NewBatteryLevelAvailable(this, new EventArgs());
                }
                Thread.Sleep(500); // pause for a while
            }
        }


        protected virtual void OnSensorDataUpdated()
        { }

        /// <summary>
        /// Request the values from each sensor on the NXT Brick.
        /// </summary>
        /// <returns>Returns an NxtSensorValues object with the retrieved sensor data.</returns>
        public NxtSensorValues GetSensorData()
        {
            // setup ultrasonic sensor
            if (_ultrasonicIsSwitchedOn == false)
            {
                // set ultrasonic sensor to read continously
                _ultrasonicSensor.ContinuousMeasurementCommand();
                // dismiss first (false/null) value
                _ultrasonicSensor.Poll();
                // set flag
                _ultrasonicIsSwitchedOn = true;
            }
            // poll sensors
            _ultrasonicSensor.Poll();
            _lightSensor.Poll();
            _soundSensor.Poll();
            // read measurements extrapolated by sensor objects
            var uSensor = -1;
            if (_ultrasonicSensor.DistanceCm.HasValue)
            {
                uSensor = _ultrasonicSensor.DistanceCm.Value;
            }
            var lIntSensor = -1;
            if (_lightSensor.Intensity.HasValue)
            {
                lIntSensor = _lightSensor.Intensity.Value;
            }
            var sIntSensor = -1;
            if (_soundSensor.Intensity.HasValue)
            {
                sIntSensor = _soundSensor.Intensity.Value;
            }
            // return sensor measurements
            return new NxtSensorValues(uSensor, lIntSensor, sIntSensor);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public NxtSensorValues EmualateGetSensorData()
        {
            var r = new Random();
            var ultrasonicDistance = r.Next(0, 255);
            var soundIntensity = r.Next(0, 100);
            var lightIntensity = r.Next(0, 100);
            BatteryLevelMillivolts = r.Next(7000, 12000);
            return new NxtSensorValues(ultrasonicDistance, soundIntensity, lightIntensity);
        }
    }
}
