using AVINSoR_Library.NxtAbstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AVINSoR_Library.Movement
{
    [Serializable()]
    public class WheelMovementPattern : IEnumerable<OutputToWheels>
    {
        private readonly List<OutputToWheels> _motorOutputs = new List<OutputToWheels>();
        public string Name { get; set; }

        public OutputToWheels Add(MotorPolarity leftMotorPolarity, MotorPolarity rightMotorPolarity, uint leftMotorTachocount, uint rightMotorTachocount, int delayInSeconds)
        {
            var m = new OutputToWheels(leftMotorPolarity, rightMotorPolarity, leftMotorTachocount, rightMotorTachocount, delayInSeconds);
            _motorOutputs.Add(m);
            return m;
        }

        public OutputToWheels Add(MotorPolarity leftMotorPolarity, MotorPolarity rightMotorPolarity, uint leftMotorTachocount, uint rightMotorTachocount)
        {
            var m = new OutputToWheels(leftMotorPolarity, rightMotorPolarity, leftMotorTachocount, rightMotorTachocount);
            _motorOutputs.Add(m);
            return m;
        }

        public void Add(OutputToWheels m)
        {
            _motorOutputs.Add(m);
        }

        public void Remove(OutputToWheels m)
        {
            _motorOutputs.Remove(m);
        }

        public IEnumerator<OutputToWheels> GetEnumerator()
        {
            var motorOutputArray = _motorOutputs.Cast<OutputToWheels>().ToArray();
            return motorOutputArray.TakeWhile(c => c != null).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _motorOutputs.GetEnumerator();
        }
    }
}