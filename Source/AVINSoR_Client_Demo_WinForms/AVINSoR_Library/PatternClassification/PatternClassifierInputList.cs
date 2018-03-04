using System;
using AVINSoR_Library.PatternClassification.PatternClassifiers;
using Gajatko.Common;

namespace AVINSoR_Library.PatternClassification
{
    public class PatternClassificationInputList : EventyList<PatternClassificationInput>
    {
        public PatternClassificationInputList(BayesClassifierModule parent)
        {
            Parent = parent;
        }

        public BayesClassifierModule Parent { get; private set; }

        public bool Locked { get; private set; }

        public void Lock()
        {
            if (Count >= 1)
            {
                Locked = true;
                Indexify();
            }
            else
            {
                throw new ApplicationException("Failed to lock Pattern Classification Inputs list of " + Parent.Name + ", because number of items < 1.");
            }
        }


        /// <summary>
        /// assign an index and unique string identifier to all input objects in the list.
        /// </summary>
        private void Indexify()
        {
            foreach (var pci in this)
            {
                pci.ClassifierMatlabIndex = IndexOf(pci) + 1;
                pci.ClassifierNewUniqueId();
            }
        }
    }
}
