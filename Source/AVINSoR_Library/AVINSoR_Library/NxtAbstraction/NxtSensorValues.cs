namespace AVINSoR_Library.NxtAbstraction
{
    public class NxtSensorValues
    {
        private int _ultrasonicSensorCm = -1;
        /// <summary>
        /// Distance retrieved from Ultrasonic Sensor in Centimeters.
        /// </summary>
        public int UltrasonicSensorCm 
        {
            get { return _ultrasonicSensorCm; }
            set { _ultrasonicSensorCm = value; }
        }

        private int _lightSensorPercent = -1;
        /// <summary>
        /// Light intensity retrieved from Light Sensor as Percent.
        /// </summary>
        public int LightSensorPercent
        {
            get { return _lightSensorPercent; }
            set { _lightSensorPercent = value; }
        }

        private int _soundSensorPercent = -1;
        /// <summary>
        /// Sound intensity retrieved from Sound Sensor as Percent.
        /// </summary>
        public int SoundSensorPercent
        {
            get { return _soundSensorPercent; } 
            set { _soundSensorPercent = value; }
            
        }


        /// <summary>
        /// Create a new NxtSensorValues object to hold sensor data.
        /// </summary>
        public NxtSensorValues(int uSensor, int lSensor, int sSensor)
        {
            UltrasonicSensorCm = uSensor;
            LightSensorPercent = lSensor;
            SoundSensorPercent = sSensor;
        }
    }
}
