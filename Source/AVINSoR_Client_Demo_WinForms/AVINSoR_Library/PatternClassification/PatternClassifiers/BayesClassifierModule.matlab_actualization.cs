using System;
using AVINSoR_Library.Auxiliary;
using AVINSoR_Library.PatternClassification.Inputs;

namespace AVINSoR_Library.PatternClassification.PatternClassifiers
{
    public partial class BayesClassifierModule
    {
        private void Actualize()
        {
            // create a corresponding BayesClassifierModule object in MATLAB
            var noOfCategories = _classificationCategories.Count;
            var cmd = ClassifierUniqueId + " = BayesClassifierModule("+noOfCategories+");";
            MatlabInterface.Execute(cmd);
            // generate a Bayesian Classifier for each 'Pattern Classification Input' object.
            foreach (var pci in InputNodes)
            {
                var minVal = 0;
                var maxVal = 0;

                if (pci.GetType() == typeof (Variable))
                {
                    var v = (Variable) pci;
                    minVal = v.Value.MinimumAllowableValue;
                    maxVal = v.Value.MaximumAllowableValue;
                }
                else if (pci.GetType() == typeof (BayesClassifierModule))
                {
                    var b = (BayesClassifierModule) pci;
                    minVal = 1;
                    maxVal = b.ClassificationCategories.Count;
                }

                MatlabInterface.Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".newInputNode("+minVal+", "+maxVal+");");
            }
            // lock BayesClassifierModule
            cmd = ClassifierUniqueId + " = " + ClassifierUniqueId + ".lock();";
            MatlabInterface.Execute(cmd);
        }
    }
}
