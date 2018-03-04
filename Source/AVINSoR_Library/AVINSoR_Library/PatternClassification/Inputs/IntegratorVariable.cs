using System.Collections.Generic;
using System.Linq;
using AVINSoR_Library.PatternClassification.Inputs.Value;

namespace AVINSoR_Library.PatternClassification.Inputs
{
    class IntegratorVariable : InterpolatingVariable
    {
        public IntegratorVariable(NxtRobot owner, int noOfSamples, Variable yVar, Variable xVar = null)
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
                Name = "Distance of '" + YVar.Name + "' over distance of  '" + XVar.Name + "' over last " + NoOfSamples + " samples.";
            }
            else
            {
                Name = "Distance of '" + YVar.Name + "' over last " + NoOfSamples + " samples.";
            }
            // create value obj with appropriate min and max limits
            Value = new GenericValue(YVar.Value.MinimumAllowableValue, yVar.Value.MaximumAllowableValue * NoOfSamples, YVar.Value.Units);
        }


        protected override int CalculateValue() // convert return type to float?????
        {
            // The Trapezoid Rule (improve to Simpson's rule at some point when updating).
            var firstYVal = YVarValues.First();
            var lastYVal = YVarValues.Last();
            int firstXVal;
            int lastXVal;
            if (XVar == null)
            {
                firstXVal = 0;
                lastXVal = NoOfSamples;
            }
            else
            {
                firstXVal = XVarValues.First();
                lastXVal = XVarValues.Last();
            }
            var result = (lastXVal - firstXVal)*((firstYVal + lastYVal)/2);
            //MessageBox.Show("First X Val: " + firstXVal + "\nLast X Val: " + lastXVal);
            //MessageBox.Show("Result: " + result);
            return result;
        }
    }
}
