using System;
using AVINSoR_Library.PatternClassification.Inputs;
using AVINSoR_Library.PatternClassification.Outputs;


namespace AVINSoR_Library.PatternClassification.PatternClassifiers
{
    public partial class BayesClassifierModule : PatternClassificationInput
    {
        /// <summary>
        /// A reference to the MATLAB server where this module should 'actualize' itself.
        /// </summary>
        public MLApp.MLApp MatlabServer { get; set; }



        /// <summary>
        /// The name of this Bayes pattern classifier module.
        /// </summary>
        public string Name { get; private set; }



        /// <summary>
        /// Indicates wether this module object is 'locked', i.e. actualized in MATLAB.
        /// </summary>
        public bool Locked { get; private set; }


        /// <summary>
        /// Categorize after receipt of new input value from a variable.
        /// </summary>
        public bool EnableLiveCategorization { get; set; }


        /// <summary>
        /// The classification result.
        /// </summary>
        public ClassCategory Result;



        /// <summary>
        /// Create a new Bayes Classifier Module object.
        /// </summary>
        public BayesClassifierModule(string name)
        {
            Name = name;
            _inputNodes = new PatternClassificationInputList(this);
            _classificationCategories = new ClassCategoryList(this);
        }



        /// <summary>
        /// Calculate a classification result (i.e. deduce which class category is most likely to be associated with current inputs).
        /// </summary>
        public void Run()
        {
            // execute appropriate MATLAB commands...
            GetDecision();
            // inform everybody
            if (NewResultAvailable != null)
            {
                NewResultAvailable(this, new EventArgs());
            }
        }
        public event EventHandler NewResultAvailable;



        /// <summary>
        /// Lock the Bayes Classifier object so that new inputs can no longer be associated with this classifier object, 
        /// and new classification categories cannot be added.
        /// </summary>
        public void Lock()
        {
            // lock both lists
            InputNodes.Lock();
            ClassificationCategories.Lock();
            // return exception if failed to lock both lists, else, set lock status and inform all parties
            if (InputNodes.Locked & ClassificationCategories.Locked)
            {
                // lock THIS object
                Locked = true;
                // actualize in MATLAB
                Actualize();
                // inform everybody
                if (HasBeenLocked != null)
                {
                    HasBeenLocked(this, new EventArgs());
                }
                // subscribe to variable value change
                foreach(var inputNode in _inputNodes)
                {
                    if (inputNode.GetType() == typeof(Variable))
                    {
                        var v = (Variable)inputNode;
                        v.ValueHasBeenUpdated += OnInputNodeValueUpdated;
                    }
                    if (inputNode.GetType() == typeof(BayesClassifierModule))
                    {
                        var bcm = (BayesClassifierModule)inputNode;
                        bcm.NewResultAvailable += OnInputNodeValueUpdated;
                    }
                }
            }
            else
            {
                throw new ApplicationException("Failed to lock Bayes Classifier Module " + Name + ".");
            }
        }

        /// <summary>
        /// Get new decision when input value updated - only if live categorization enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInputNodeValueUpdated(object sender, EventArgs e)
        {
            if(EnableLiveCategorization)
            { 
                Run();
            }
        }



        /// <summary>
        /// Raised when the Bayes Classifier Module has been locked and input variables, input bayes classifier modules  
        /// and/or classification categories can NO longer be added.
        /// </summary>
        public event EventHandler HasBeenLocked;



        /// <summary>
        /// The result, as an integer index of the category calculated to be of high-association with the inputs.
        /// </summary>
        public override int ClassifierOutput
        {
            get
            {
                var resultIndex = _classificationCategories.IndexOf(Result);
                return resultIndex;
            }
        }
    }
}
