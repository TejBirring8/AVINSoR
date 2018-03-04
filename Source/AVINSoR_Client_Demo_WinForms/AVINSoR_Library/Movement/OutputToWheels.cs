////    
////    Output to NXT servo motors of diffrential wheeled robot.
////    
////    

using AVINSoR_Library.NxtAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVINSoR_Library.Movement
{
    [Serializable()]
    public class OutputToWheels
    {
        /// <summary>
        /// 
        /// </summary>
        public MotorPolarity LeftMotorPolarity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MotorPolarity RightMotorPolarity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public uint LeftMotorTachocount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public uint RightMotorTachocount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DelayInSeconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OutputToWheels() { }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftMotorPower"></param>
        /// <param name="rightMotorPower"></param>
        /// <param name="leftMotorTachocount"></param>
        /// <param name="rightMotorTachocount"></param>
        /// <param name="delayInSeconds"></param>
        public OutputToWheels(MotorPolarity leftMotorPolarity, MotorPolarity rightMotorPolarity, uint leftMotorTachocount, uint rightMotorTachocount, int delayInSeconds)
        {
            DelayInSeconds = delayInSeconds;
            RightMotorTachocount = rightMotorTachocount;
            LeftMotorTachocount = leftMotorTachocount;
            LeftMotorPolarity = leftMotorPolarity;
            RightMotorPolarity = rightMotorPolarity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftMotorPower"></param>
        /// <param name="rightMotorPower"></param>
        /// <param name="leftMotorTachocount"></param>
        /// <param name="rightMotorTachocount"></param>
        public OutputToWheels(MotorPolarity leftMotorPolarity, MotorPolarity rightMotorPolarity, uint leftMotorTachocount, uint rightMotorTachocount)
        {
            DelayInSeconds = 0;
            RightMotorTachocount = rightMotorTachocount;
            LeftMotorTachocount = leftMotorTachocount;
            LeftMotorPolarity = leftMotorPolarity;
            RightMotorPolarity = rightMotorPolarity;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="controller"></param>
        //public void Play(ref Nxt controller)
        //{
        //    var leftMotorPower = LeftMotorTachocount;
        //    var rightMotorPower = RightMotorTachocount;

        //    System.Threading.Thread.Sleep(DelayInSeconds * 1000);
        //    controller.MoveLeftWheel(leftMotorPower, LeftMotorTachocount);
        //    controller.MoveRightWheel(rightMotorPower, RightMotorTachocount);
        //}
    }
}



