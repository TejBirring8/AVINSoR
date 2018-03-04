using System;
using System.Collections.Generic;
using AVINSoR_Library.PatternClassification.Inputs;
using AVINSoR_Library.PatternClassification.Outputs;
using AVINSoR_Library.Auxiliary;

namespace AVINSoR_Library.PatternClassification.PatternClassifiers
{
    public partial class BayesClassifierModule
    {
        private int[] CreateArrayOfPresentValues(bool actualizeInMatlab, string nameInMatlab = "")
        {
            // all input values to array, in chronological order (order of index) 
            var values = new List<int>();
            foreach (var inp in _inputNodes)
            {
                // get value
                int? value = null;
                if (inp.GetType() == typeof(Variable))
                {
                    var v = (Variable)inp;
                    if (v.Value.Value.HasValue)
                    {
                        value = v.Value.Value.Value + 1;
                    }
                    else
                    {
                        throw new ApplicationException("Variable's value is NULL!");
                    }
                }
                else if (inp.GetType() == typeof(BayesClassifierModule))
                {
                    var b = (BayesClassifierModule)inp;
                    if (b.Result != null)
                    {
                        value = b.Result.MatlabIndex;
                    }
                    else
                    {
                        throw new ApplicationException("Bayesian Classifier Module has not result!");
                    }
                }
                // add to list and move to next input
                if (value != null) values.Add(value.Value);
            }
            if (values.Count != _inputNodes.Count)
            {
                throw new ApplicationException("Array of input values is not the same size as the number of inputs!");
            }
            var arrayOfValues = values.ToArray();
            if (!actualizeInMatlab) return arrayOfValues;
            if (nameInMatlab != "")
            {
                MatlabInterface.ArrayToMatlabVector(arrayOfValues, nameInMatlab, true);
            }
            else
            {
                throw new ApplicationException("Must specify a name to actualize array in Matlab!");
            }
            return arrayOfValues;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ClassCategory GetDecision()
        {
            // create array of values in MATLAB
            CreateArrayOfPresentValues(true, "evaluateTheseValues");
            // execute request and return result
            var cat = GetDecisionFromMatlabObj();
            Result = cat;
            return cat;
        }


        private ClassCategory GetDecisionFromMatlabObj()
        {
            // execute request
            Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".getDecision(evaluateTheseValues);");
            Execute("Decision = " + ClassifierUniqueId + ".Decision;");
            // get result
            //var categoryNumber = Cognition.MatlabVariableToInteger(ClassifierUniqueId + ".Decision", false)
            var categoryNumber = MatlabInterface.MatlabVariableToInteger("Decision", false);
            var cat = _classificationCategories[categoryNumber - 1];
            return cat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputValues"></param>
        /// <returns></returns>
        public ClassCategory GetDecision(int[] inputValues)
        {
            MatlabInterface.ArrayToMatlabVector(inputValues, "evaluateTheseValues", true);
            // execute request
            return GetDecisionFromMatlabObj();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputVariable"></param>
        /// <param name="inputVariableValue"></param>
        /// <returns></returns>
        public ClassCategory GetDecision(PatternClassificationInput inputVariable, int inputVariableValue)
        {
            // execute request
            Execute("inputClassifierDecision = " + ClassifierUniqueId + ".InputNodeClassifiers(" + inputVariable.ClassifierMatlabIndex + ").getDecision("+inputVariableValue+");");
            // fetch value and return
            var intResult = MatlabInterface.MatlabVariableToInteger("inputClassifierDecision", true);
            var c = _classificationCategories[intResult - 1];
            return c;
        }


        /// <summary>
        /// Associate current values with a particular category.
        /// </summary>
        public void AssociateWithCategory(ClassCategory c, bool plot)
        {
            // create array of values in MATLAB
            CreateArrayOfPresentValues(true, "evaluateTheseValues");
            // execute request
            Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".associateValuesWithCategory(evaluateTheseValues, " + c.MatlabIndex + ");");
            // show us the result - comment out of not needed
            if(plot)
            Execute(ClassifierUniqueId + "plotAllLikelihoods();");
        }


        /// <summary>
        /// Associate current values with a particular category.
        /// </summary>
        public void AssociateWithCategory(int[] inputValues, ClassCategory c, bool plot)
        {
            MatlabInterface.ArrayToMatlabVector(inputValues, "evaluateTheseValues", true);
            // execute request
            Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".associateValuesWithCategory(evaluateTheseValues, " + c.MatlabIndex + ");");
            // show us the result - comment out of not needed
            if (plot)
            Execute(ClassifierUniqueId + ".plotAllLikelihoods();");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputVariable"></param>
        /// <param name="inputVariableValue"></param>
        /// <param name="c"></param>
        /// <param name="plot"></param>
        public void AssociateWithCategory(PatternClassificationInput inputVariable, int inputVariableValue,
            ClassCategory c, bool plot)
        {
            // associate input value with cateogiry, for the specified variable
            Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".InputNodeClassifiers(" + inputVariable.ClassifierMatlabIndex + ").createAssociation(" + inputVariableValue + ", " + c.MatlabIndex +");");
            // plot
            if (plot)
            Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".InputNodeClassifiers(" + inputVariable.ClassifierMatlabIndex + ").plotLikelihoods();");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputVariable"></param>
        /// <param name="priorProbabilities"></param>
        public void SetCategoryPriories(PatternClassificationInput inputVariable, double[] priorProbabilities)
        {
            var matlabArrayName = "newPrioriesForInputNo_" + inputVariable.ClassifierMatlabIndex;
            MatlabInterface.ArrayToMatlabVector(priorProbabilities, matlabArrayName, true);
            if (priorProbabilities.Length != _classificationCategories.Count)
            {
                throw new ApplicationException(
                    "Number of priories in array does not match number of classification categories!");
            }
            Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".setCategoryPrioriesForInput(" + inputVariable.ClassifierMatlabIndex +
                    ", " + matlabArrayName + ");");
            // plot resultant priories of BC
            Execute("plot(" + ClassifierUniqueId + ".InputNodeClassifiers(" + inputVariable.ClassifierMatlabIndex +
                    ").priories);");
        }
    }
}
