using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;

namespace AVINSoR_Library.NxtAbstraction
{
    public partial class Nxt
    {
        private object _leftWheel; 
        private object _rightWheel;
        //private McNxtMotorSync _bothWheels = null;  // current unused


        /// <summary>
        /// 
        /// </summary>
        private void UpdateMotorSettings()
        {
            switch (_motorControlMode)
            {
                case NxtMotorControlMode.DirectIndividualControl:
                    _brick.MotorA = new NxtMotor();
                    _leftWheel = _brick.MotorA;
                    _brick.MotorC = new NxtMotor();
                    _rightWheel = _brick.MotorC;
                    break;
                case NxtMotorControlMode.IndividualControlViaMotorControl22Rxe:
                    _brick.MotorA = new McNxtMotor();
                    _leftWheel = _brick.MotorA;
                    _brick.MotorC = new McNxtMotor();
                    _rightWheel = _brick.MotorC;
                    //_bothWheels = new McNxtMotorSync((McNxtMotor)_leftWheel, (McNxtMotor)_rightWheel);
                    _brick.CommLink.StartProgram("MotorControl22.rxe");
                    break;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="wheel"></param>
        /// <param name="power"></param>
        /// <param name="noOfRotations"></param>
        private void MoveWheel(object wheel, sbyte power, uint noOfRotations)
        {
            switch (_motorControlMode)
            {
                case NxtMotorControlMode.DirectIndividualControl:
                    var m = (NxtMotor)wheel;
                    m.Run(power, noOfRotations);
                    break;
                case NxtMotorControlMode.IndividualControlViaMotorControl22Rxe:
                    var n = (McNxtMotor)wheel;
                    n.Run(power, noOfRotations);
                    n.ResetMotorPosition(true);
                    n.ResetMotorPosition(false);
                    break;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="power"></param>
        /// <param name="noOfRotations"></param>
        public void MoveLeftWheel(sbyte power, uint noOfRotations)
        {
            MoveWheel(_leftWheel, power, noOfRotations);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="power"></param>
        /// <param name="noOfRotations"></param>
        public void MoveRightWheel(sbyte power, uint noOfRotations)
        {
            MoveWheel(_rightWheel, power, noOfRotations);
        }
    }
}
