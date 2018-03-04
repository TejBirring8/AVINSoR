using System;
using System.Collections.Generic;

namespace AVINSoR_Library.PatternClassification.Inputs
{
    class InterpolatingVariable : Variable
    {
        private Variable _xVar;
        /// <summary>
        /// The independant variable 'x'. If NULL, this means that the discrete time
        /// (number of samples) is used as the independant variable.
        /// i.e. 
        /// </summary>
        public Variable XVar
        {
            get
            {
                return _xVar;
            }

            protected set
            {
                // update property
                _xVar = value;
                if (value != null)
                {
                    // subscribe to value change event 
                    XVar.ValueHasBeenUpdated += XVarValueHasBeenUpdated;
                }
                else
                {
                    // if independant variable is # of samples, then set count appropriately.
                    XCount = NoOfSamples;
                }
            }
        }


        private Variable _yVar;
        /// <summary>
        /// The dependant ("measured") variable, i.e. f(x), to evaluate with respect to change in 'x'.
        /// </summary>
        public Variable YVar
        {
            get
            {
                return _yVar;
            }
            protected set
            {
                _yVar = value;
                // subscribe to value change event 
                YVar.ValueHasBeenUpdated += YVarValueHasBeenUpdated;
            }
        }


        /// <summary>
        /// The number of values to hold in memory so that the final average (mean) can  
        /// be deduced from which the interpolation is calculated.
        /// </summary>
        public int NoOfSamples { get; protected set; }


        // stack to hold the 'MeanCount' number of 'x' values.
        protected Stack<int> XVarValues;
        // number of new 'x' values in stack (since last value was calculated).
        protected int XCount = 0;
        // stack to hold the 'MeanCount' number of 'y' values.
        protected Stack<int> YVarValues;
        // number of 'y' values in stack (sincce last value was calculated).
        protected int YCount = 0;


        protected InterpolatingVariable() { }


        void XVarValueHasBeenUpdated(object sender, EventArgs e)
        {
            // push X value in stack
            if (XVar.Value.Value != null)
            {
                var v = (int)XVar.Value.Value;
                XVarValues.Push(v);
            }
            else
            {
                throw new ApplicationException("Interpolating variable cannot accept or evaluate a NULL value from X.");
            }
            // update X count
            if (XCount < NoOfSamples)
            {
                XCount++;
            }
            else
            {
                EvaluateXandY();
            }
        }



        void YVarValueHasBeenUpdated(object sender, EventArgs e)
        {
            // push Y value in stack
            if (YVar.Value.Value != null)
            {
                var v = (int)YVar.Value.Value;
                YVarValues.Push(v);
            }
            else
            {
                throw new ApplicationException("Interpolating variable cannot accept or evaluate a NULL value from Y.");
            }
            // update Y count
            if (YCount < NoOfSamples)
            {
                YCount++;
            }
            else
            {
                EvaluateXandY();
            }
        }



        void EvaluateXandY()
        {
            if ((XCount == NoOfSamples) & (YCount == NoOfSamples))
            {
                Value.Value = CalculateValue();
            }
            InformOfValueChange();
            // reset x and y count
            XCount = 0;
            YCount = 0;
            // reset values
            XVarValues.Clear();
            YVarValues.Clear();
        }



        protected virtual int CalculateValue()
        {
            return 0;
        }
    }
}
