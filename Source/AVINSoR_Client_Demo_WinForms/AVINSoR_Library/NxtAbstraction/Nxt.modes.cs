using System;

namespace AVINSoR_Library.NxtAbstraction
{
    public partial class Nxt
    {
        /// <summary>
        /// 
        /// </summary>
        public enum NxtLightSensorMode
        {
            Reflected,
            Ambient
        };
        private NxtLightSensorMode _lightSensorMode = NxtLightSensorMode.Reflected;
        /// <summary>
        /// Configure the robot to detect ambient (enviromental) light or reflected (Red LED) light.
        /// </summary>
        public NxtLightSensorMode LightSensorMode
        {
            get { return _lightSensorMode; }
            set
            {
                // update property
                _lightSensorMode = value;
                UpdateSensorSettings();
                // inform everybody else that the value has been changed.
                if (SensorModeHasChanged == null) return;
                SensorModeHasChanged(this, new EventArgs());
                
            }
        }
        

        /// <summary>
        /// 
        /// </summary>
        public enum NxtSoundSensorMode
        {
            Dba,
            Db
        }
        private NxtSoundSensorMode _soundSensorMode = NxtSoundSensorMode.Dba;
        /// <summary>
        /// Configure the robot to detect sound on a dBA scale (i.e. scaled for human sensitivity to frequencies), 
        /// or dB scale (all frequencies equally).
        /// </summary>
        public NxtSoundSensorMode SoundSensorMode
        {
            get { return _soundSensorMode; }
            set
            {
                // update property
                _soundSensorMode = value;
                UpdateSensorSettings();
                // inform everybody else that the value has been changed.
                if (SensorModeHasChanged == null) return;
                SensorModeHasChanged(this, new EventArgs());
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public enum NxtMotorControlMode
        {
            DirectIndividualControl, 
            IndividualControlViaMotorControl22Rxe
        }
        private NxtMotorControlMode _motorControlMode = NxtMotorControlMode.DirectIndividualControl;
        public NxtMotorControlMode MotorControlMode
        {
            get { return _motorControlMode; }
            set
            {
                // update property
                _motorControlMode = value;
                UpdateMotorSettings();
                // inform everybody else that the value has been changed.
                if (MotorControlModeChanged == null) return;
                MotorControlModeChanged(this, new EventArgs());
            }
        }


        /// <summary>
        /// Raised when a sensor mode has been changed.
        /// </summary>
        public event EventHandler SensorModeHasChanged;
        /// <summary>
        /// Raised when the motor control mode has been changed.
        /// </summary>
        public event EventHandler MotorControlModeChanged;
    }
}
