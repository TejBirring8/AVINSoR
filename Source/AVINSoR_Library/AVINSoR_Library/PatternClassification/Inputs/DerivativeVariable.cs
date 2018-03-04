using System;
using System.Collections.Generic;
using System.Linq;
using AVINSoR_Library.PatternClassification.Inputs.Value;

namespace AVINSoR_Library.PatternClassification.Inputs
{
    class DerivativeVariable : InterpolatingVariable
    {
        public DerivativeVariable(NxtRobot owner, int noOfSamples, Variable yVar, Variable xVar = null)
        {
            Enabled = true;
            // set owner
            Owner = owner;
            // set number of samples to hold
            NoOfSamples = noOfSamples;
            // set independant variable if not 'sample count'
            XVar = xVar;
            // set dependant/measured variable
            YVar = yVar;
            // initialize 'x' and 'y' stacks for sampled values
            XVarValues = new Stack<int>(NoOfSamples);
            YVarValues = new Stack<int>(NoOfSamples);
            // update name of object to describe the interpolation
            if (XVar != null)
            {
                Name = "Change in '" + YVar.Name + "' per change in '" + XVar.Name + "' over last " + NoOfSamples + " samples.";
            }
            else
            {
                Name = "Change in '" + YVar.Name + "' over last " + NoOfSamples + " samples.";
            }
            // create value obj with appropriate min and max limits
            Value = new GenericValue(-YVar.Value.MaximumAllowableValue, yVar.Value.MaximumAllowableValue, YVar.Value.Units);
        }


        protected override int CalculateValue() // convert return type to float?????
        {
            var firstVal = YVarValues.First();
            var lastVal = YVarValues.Last();
            var difference = lastVal - firstVal;
            // use the MAXIMUM of the captured x variable samples instead of MEAN
            var denominator = XVar == null ? XCount : (XVarValues.Max()-XVarValues.Min());
            // return value
            double tempResult = (difference * 100) / denominator;
            var result = Convert.ToInt32(tempResult);
          
           //MessageBox.Show("Last Val: " + lastVal + "\nFirst Val: " + firstVal + "\nNumerator: " + difference + "\nDenominator: " + denominator);
           //MessageBox.Show("Result: " + result);

            return result;
        }
    }
}
