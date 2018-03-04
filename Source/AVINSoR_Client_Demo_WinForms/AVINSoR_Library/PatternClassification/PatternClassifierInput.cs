using System;
using AVINSoR_Library.Auxiliary;

namespace AVINSoR_Library.PatternClassification
{
    public class PatternClassificationInput
    {
        public PatternClassificationInput()
        {
            ClassifierUniqueId = ClassifierNewUniqueId();
            _enabled = true; // default enabled state -> true
        }

        /// <summary>
        /// Index of the input in the BayesianClassifierModule's input list
        /// </summary>
        public int ClassifierMatlabIndex { get; set; }

        private bool _enabled;
        /// <summary>
        /// Enable or Disable the object's operation (and output).
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set 
            { 
                _enabled = value;
                if (OnEnableStatusChanged != null)
                {
                    OnEnableStatusChanged(this, new EventArgs());
                }
            }
        }

        public event EventHandler OnEnableStatusChanged;

        /// <summary>
        /// The unique ID associated with this input.
        /// </summary>
        public string ClassifierUniqueId { get; private set; }

        /// <summary>
        /// The integer output of the variable/bayesclassifiermodule object.
        /// </summary>
        public virtual int ClassifierOutput { get; set; }

        /// <summary>
        /// Request that a new UniqueId be generated.
        /// </summary>
        /// <returns>The new UniqueId</returns>
        public string ClassifierNewUniqueId()
        {
            var id = KeyGenerator.GetUniqueKey(10);
            ClassifierUniqueId = id;
            return id;
        }

    }
}
