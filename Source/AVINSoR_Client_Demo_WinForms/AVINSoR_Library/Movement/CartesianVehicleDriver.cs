using System;

namespace AVINSoR_Library.Movement
{
    [Serializable()]
    public class CartesianVehicleDriver
    {
        // cartesian inputs, dependant on grid size
        private int _minXYVal;
        private int _maxXYVal;
        // tachometeric
        private double _wheelRadiusCM;
        private double _distancePerDegreeInCM;
        private uint _noOfDegreesPerStep;

        /// <summary>
        /// 
        /// </summary>
        public NxtAbstraction.Nxt controller { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint MinMotorPower { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint MaxMotorPower { get; set; }

        private uint _absMotorPower;
        /// <summary>
        /// 
        /// </summary>
        public uint AbsMotorPower 
        { 
            get
            {
                return _absMotorPower;
            }
            set
            {
                if ((value >= MinMotorPower) & (value <= MaxMotorPower))
                {
                    _absMotorPower = value;
                }
                else
                {
                    throw new ApplicationException("Motor power beyond set minimum and maximum (safe) limits!");
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSize"></param>
        /// <param name="wheelRadius"></param>
        /// <param name="minAbsMotorPower"></param>
        /// <param name="maxAbsMotorPower"></param>
        public CartesianVehicleDriver(int gridSize, double wheelRadius)
        {
            _minXYVal = -gridSize;
            _maxXYVal = gridSize;
            _wheelRadiusCM = wheelRadius;
            _distancePerDegreeInCM = _wheelRadiusCM / 360;
            MinMotorPower = 30;
            MaxMotorPower = 120;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="calibrationMode"></param>
        /// <param name="delta"></param>
        public void Calibrate(CalibrationMode calibrationMode, object calibrationObj)
        {
            switch (calibrationMode)
            {
                case CalibrationMode.TachoCountPerStep:
                    CalibrateForTachoCountPerStep(calibrationObj);
                    break;
                case CalibrationMode.DistancePerStep:
                    CalibrateForDistancePerStep(calibrationObj);
                    break;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="calibrationObj"></param>
        private void CalibrateForTachoCountPerStep(object calibrationObj)
        {
            if (calibrationObj.GetType() != typeof(uint))
                throw new ApplicationException("Incorrect calibration object passed to Cartesian Vehicle Driver.");
            // calibrate
            _noOfDegreesPerStep = (uint)calibrationObj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="calibrationObj"></param>
        private void CalibrateForDistancePerStep(object calibrationObj)
        { 
            if (calibrationObj.GetType() != typeof(double))
                throw new ApplicationException("Incorrect calibration object passed to Cartesian Vehicle Driver.");
            // calibrate
            var distancePerStep = (double) calibrationObj;
            _noOfDegreesPerStep = Convert.ToUInt32(Math.Round(distancePerStep / _distancePerDegreeInCM));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="wheelOutputs"></param>
        public void Play(OutputToWheels wheelOutputs)
        {
            // deduce values of power
            var leftMotPower = ExtractMotorPower(AbsMotorPower, wheelOutputs.LeftMotorPolarity);
            var rightMotPower = ExtractMotorPower(AbsMotorPower, wheelOutputs.RightMotorPolarity);
            // sleep if delay
            if (wheelOutputs.DelayInSeconds > 0)
                System.Threading.Thread.Sleep(wheelOutputs.DelayInSeconds * 1000);
            // MOVE!
            controller.MoveLeftWheel(leftMotPower, wheelOutputs.LeftMotorTachocount);
            controller.MoveRightWheel(rightMotPower, wheelOutputs.RightMotorTachocount);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="wheelMovementPattern"></param>
        public void Play(WheelMovementPattern wheelMovementPattern)
        {
            foreach(var wheelOutput in wheelMovementPattern)
            {
                Play(wheelOutput);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="absPower"></param>
        /// <param name="motPolarity"></param>
        /// <returns></returns>
        private sbyte ExtractMotorPower(uint absPower, MotorPolarity motPolarity)
        {
            var forwardPwr = Convert.ToSByte(absPower);
            var reversePwr = Convert.ToSByte(-absPower);

            switch (motPolarity)
            { 
                case MotorPolarity.Forward:
                    return forwardPwr;
                case MotorPolarity.Reverse:
                    return reversePwr;
                default:
                    return 0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="inhibitPhysicalMovement"></param>
        /// <returns></returns>
        public OutputToWheels Drive(int x, int y, bool inhibitPhysicalMovement)
        {
            // outputs
            OutputToWheels output = new OutputToWheels();

            // polar radius from cartesian coordinate
            var radius = Math.Sqrt(x * x + y * y);
            // angle (radians) from cartesian coordinate
            var angle_radians = Math.Acos(Math.Abs(x) / radius);
            if (double.IsNaN(angle_radians))
                angle_radians = 0;
            // angle (degrees) from cartesian coordinate
            var angle_degrees = angle_radians * 180 / Math.PI;
            // turn coeffecient i.e. scale of deviation from 45-degrees (NE, NW, SE, SW)
            var tcoeff = (angle_degrees / 45) - 1; //-1 + (angle_degrees/ 90) * 2;
            // raw output of the turn motor
            var rawTurn = tcoeff*Math.Abs((Math.Abs(y) - Math.Abs(x)));
            rawTurn = Math.Round(rawTurn*100)/100;
            var turnMotPolarity = ExtractMotorPolarity(Convert.ToSingle(rawTurn));
            var turnMotTacho = Convert.ToUInt32((Math.Abs(rawTurn) * _distancePerDegreeInCM));
            // raw output of the distance motor
            var rawDistance = Math.Max(Math.Abs(y), Math.Abs(x));
            var distanceMotPolarity = ExtractMotorPolarity(rawDistance);
            var distanceMotTacho = Convert.ToUInt32((Math.Abs(rawDistance) * _distancePerDegreeInCM));
            //// re-map values to correct output range (from -G to +G)
            //var valLeft = ReMap(rawLeft, _minXYVal, _maxXYVal, minOutputVal, maxOutputVal);
            //var valRight = ReMap(rawRight, _minXYVal, _maxXYVal, minOutputVal, maxOutputVal);

            // if input coordinate in 1st or 3rd quadrant; left motor is Distance, right motor is Turn
            if ((x >= 0 && y >= 0) || (x < 0 && y < 0))
            {
                output.LeftMotorPolarity = distanceMotPolarity;
                output.LeftMotorTachocount = distanceMotTacho;
                output.RightMotorPolarity = turnMotPolarity;
                output.RightMotorTachocount = turnMotTacho;
            }
            else
            {
                output.RightMotorPolarity = distanceMotPolarity;
                output.RightMotorTachocount = distanceMotTacho;
                output.LeftMotorPolarity = turnMotPolarity;
                output.LeftMotorTachocount = turnMotTacho;
            }
            // Reverse polarity
            //if (y < 0)
            //{
            //    rawLeft = (float)(0.0 - rawLeft);
            //    rawRight = (float)(0.0 - rawRight);
            //}

            // play generated output (MOVE!)
            if (!inhibitPhysicalMovement)
                Play(output);

            // return generated outputs.
            return output;
        }
		
		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawValue"></param>
        /// <returns></returns>
		private MotorPolarity ExtractMotorPolarity(float rawValue)
        {
            if (rawValue < 0)
            {
                return MotorPolarity.Reverse;
            }
            else if (rawValue.Equals(0))
            {
                return MotorPolarity.Off;
            }
            else if (rawValue > 0)
            {
                return MotorPolarity.Forward;
            }
            else
            {
                throw new ApplicationException("Error extrapolating motor polarity from raw value (Cartesian Vehicle Driver).");
            }
        }
    }
}
