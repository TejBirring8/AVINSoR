using AVINSoR_Library.PatternClassification.Inputs;
using AVINSoR_Library.PatternClassification.PatternClassifiers;
using AVINSoR_Library.Auxiliary;
using System;

namespace AVINSoR_Library
{
    [Serializable()]
    public class Cognition
    {
        /// <summary>
        /// Store all Variables that are assigned to the AvinsorPatternClassification object (instead of a particular robot). 
        /// This list is a suitable location for interpolation variables that involve sensor variables inputs from different robots.
        /// </summary>
        public VariablesList Variables = new VariablesList();



        /// <summary>
        /// Store all Bayes Classifier Modules.
        /// </summary>
        public BayesClassifierModuleList BayesClassifierModules { get; private set; }



        // DO THIS LATER
        //public SurfFeaturesDetector SurfFeature


        
        /// <summary>
        /// New Avinsor Pattern Classification object to manage all pattern classification components in the project.
        /// </summary>
        public Cognition()
        {
            MatlabInterface.InitializeMatlab();
            BayesClassifierModules = new BayesClassifierModuleList(MatlabInterface.MatlabServer);
        }
    }
}
