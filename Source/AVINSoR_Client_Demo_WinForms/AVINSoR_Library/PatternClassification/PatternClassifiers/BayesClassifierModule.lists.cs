using AVINSoR_Library.PatternClassification.Outputs;
using AVINSoR_Library.PatternClassification.PatternClassifiers;
using AVINSoR_Library.PatternClassification.VisualObjectDetectors;


namespace AVINSoR_Library.PatternClassification.PatternClassifiers
{
    public partial class BayesClassifierModule
    {
        /// <summary>
        /// The module should categorize the variables from the inputs into one of the categories in this list. 
        /// The list must be LOCKED before this BayesClassifierModule can be implemented in MATLAB.
        /// </summary>
        public ClassCategoryList ClassificationCategories
        {
            get { return _classificationCategories; }
        }
        private readonly ClassCategoryList _classificationCategories;


        /// <summary>
        /// The inputs into this BayesClassifierModule.
        /// </summary>
        public PatternClassificationInputList InputNodes
        {
            get { return _inputNodes; }
        }
        private readonly PatternClassificationInputList _inputNodes;

    }




}
