using System;
using System.Threading;
using System.Windows.Forms;
using NKH.MindSqualls;

namespace AVINSoR_Library.NxtAbstraction
{
    public partial class Nxt
    {
        private Thread _sensorPollingThread; // thread use to recursively execute the sensor polling routine.
        private bool _abortPollingRequest;
        private bool _ultrasonicIsSwitchedOn; // indicates wether the ultrasonic sensor is active. Default value is 'false'.
        // sensor objects
        private NxtUltrasonicSensor _ultrasonicSensor;
        private NxtLightSensor _lightSensor;
        private NxtSoundSensor _soundSensor;
        private NxtSensorValues _values;

        /// <summary>
        /// The last set of sensor data read from the NXT Brick. Discard if '-1'.
        /// </summary>
        public NxtSensorValues Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;                
                OnSensorDataUpdated(); // inform children
                InformOfNewSensorValues(); // inform everybody else
            }
        }
        

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
            if (!EmulationMode & IsConnected)
            {
                _lightSensor.GenerateLight = _lightSensorMode != NxtLightSensorMode.Ambient;
                _soundSensor.dBA = _soundSensorMode != NxtSoundSensorMode.Db;
            }
            OnSensorModeChanged();
        }


        protected virtual void OnSensorModeChanged()
        {}
        

        /// <summary>
        /// Start polling sensors recursively (on a dedicated thread).
        /// </summary>
        public  void StartReadingSensorValues()
        {
            _abortPollingRequest = false;
            _sensorPollingThread = new Thread(RecursiveSensorReading) {IsBackground = true};
            _sensorPollingThread.Start();
        }


        public virtual NxtSensorValues EmulateGetSensorData()
        {
            var uSensor = _randomNoGenerator.Next(0, 255);
            var lIntSensor = _randomNoGenerator.Next(0, 100);
            var sIntSensor = _randomNoGenerator.Next(0, 100);
            // sleep
            Thread.Sleep(500);
            // return sensor measurements
            return new NxtSensorValues(uSensor, lIntSensor, sIntSensor);
        }



        public void StopReadingSensorValues()
        {
            //if ((_sensorPollingThread != null) && (_sensorPollingThread.IsAlive) & (_abortPollingRequest == false))
            //    _abortPollingRequest = true;
            //if (_sensorPollingThread != null)
            //{
            //
            //}
            _abortPollingRequest = true;
        }


        /// <summary>
        /// Request sensor data in a non-stop loop.
        /// ONLY FOR EXECUTION ON AN DEDICATED THREAD.
        /// </summary>
        private void RecursiveSensorReading()
        {
            while (!_abortPollingRequest)
            {
                if (IsConnected & EmulationMode)
                {
                    Values = EmulateGetSensorData();
                }
                else if (IsConnected & !EmulationMode & _brick != null)
                {
                    try
                    {
                        Values = GetSensorData();
                    }
                    catch (Exception)
                    {
                        // do nothing
                    }
                }
            }
        }

        private void InformOfNewSensorValues()
        {
            if (NewSensorValueAvailable != null)
            {
                NewSensorValueAvailable(this, new EventArgs());
            }
        }

        ///// <summary>
        ///// Request sensor data in a non-stop loop.
        ///// ONLY FOR EXECUTION ON AN DEDICATED THREAD.
        ///// </summary>
        //private void EmulateRecursiveSensorReading()
        //{
        //    while (true)
        //    {
        //        Values = EmualateGetSensorData();
        //        // inform everybody of new sensor data
        //        OnSensorDataUpdated();
        //        OnBatteryLevelUpdated();
        //        if ((NewSensorValueAvailable != null) & (NewBatteryLevelAvailable != null))
        //        {
        //            NewSensorValueAvailable(this, new EventArgs());
        //            NewBatteryLevelAvailable(this, new EventArgs());
        //        }
        //        Thread.Sleep(500); // pause for a while
        //        if (_abortPollingRequest)
        //        {
        //            _abortPollingRequest = false;
        //            _sensorPollingThread.Abort();
        //        }
        //    }
        //}


        protected virtual void OnSensorDataUpdated()
        { }

        /// <summary>
        /// Request the values from each sensor on the NXT Brick.
        /// </summary>
        /// <returns>Returns an NxtSensorValues object with the retrieved sensor data.</returns>
        public virtual NxtSensorValues GetSensorData()
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



    }
}
