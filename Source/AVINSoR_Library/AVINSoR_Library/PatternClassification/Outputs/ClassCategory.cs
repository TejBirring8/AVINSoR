using System;
using AVINSoR_Library.PatternClassification.PatternClassifiers;
using Gajatko.Common;

namespace AVINSoR_Library.PatternClassification.Outputs
{
    public class ClassCategoryList : EventyList<ClassCategory>
    {
        public BayesClassifierModule Parent { get; private set; }

        public ClassCategoryList(BayesClassifierModule parent)
        {
            Parent = parent;
        }

        public bool Locked { get; private set; }

        public void Lock()
        {
            if (Count >= 2)
            { 
                Locked = true;
                Indexify();
            }
            else
            {
                throw new ApplicationException("Failed to lock Pattern Classification Inputs list of " + Parent.Name + ", because number of items < 2.");
            }
        }

        /// <summary>
        /// Assign the index property of all items in the list.
        /// </summary>
        private void Indexify()
        {
            foreach (var cc in this)
            {
                cc.MatlabIndex = IndexOf(cc) + 1;
            }
        }
    }


    public class ClassCategory
    {
        public int MatlabIndex { get; set; }
        public string Name { get; set; }
    }
}
