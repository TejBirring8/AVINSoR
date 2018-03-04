using System;

namespace AVINSoR_Library.PatternClassification.Inputs.Value
{
    public class GenericValue
    {
        private int? _value;
        /// <summary>
        /// 
        /// </summary>
        public int? Value 
        {
            get
            {
                return _value;
            }
            set
            {
                if ((value >= MinimumAllowableValue) & (value <= MaximumAllowableValue))
                {
                    // update all values
                    _value = value;
                    CurrentMin = value;
                    CurrentMax = value;
                    // inform everybody
                    if (ValueHasBeenUpdated != null)
                    {
                        ValueHasBeenUpdated(this, new EventArgs());
                    }
                }
                else
                {
                    throw new ApplicationException("Inacceptable variable data.");
                }
            }
        }


        private int? _currentMin;
        /// <summary>
        /// 
        /// </summary>
        public int? CurrentMin
        {
            get { return _currentMin; }
            set
            {
                if (value < _currentMin)
                {
                    _currentMin = value;
                }
            }
        }


        private int? _currentMax;
        /// <summary>
        /// 
        /// </summary>
        public int? CurrentMax
        {
            get { return _currentMax; }
            set
            {
                if (value > _currentMax)
                {
                    _currentMax = value;
                }
            }
        }


        public int MinimumAllowableValue { get; protected set; }
        public int MaximumAllowableValue { get; protected set; }
        public string Units { get; protected set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimumAllowableValue"></param>
        /// <param name="maximumAllowableValue"></param>
        /// <param name="units"></param>
        public GenericValue(int minimumAllowableValue, int maximumAllowableValue, string units)
        {
            Units = units;
            MaximumAllowableValue = maximumAllowableValue;
            MinimumAllowableValue = minimumAllowableValue;
        }


        /// <summary>
        /// 
        /// </summary>
        protected GenericValue()
        { }


        public event EventHandler ValueHasBeenUpdated;
    }
}
