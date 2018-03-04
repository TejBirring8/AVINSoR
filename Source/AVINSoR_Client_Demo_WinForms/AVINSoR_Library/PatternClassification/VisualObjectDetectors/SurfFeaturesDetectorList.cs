using System;
using System.Collections.Generic;
using System.Linq;
using AVINSoR_Library.PatternClassification.PatternClassifiers;
using Gajatko.Common;
using AVINSoR_Library.PatternClassification.Inputs;

namespace AVINSoR_Library.PatternClassification.VisualObjectDetectors
{
    public class SurfFeaturesDetectorList : EventyList<SurfFeaturesDetector>
    {

        //public BayesClassifierModule Parent { get; private set; }

        //public SurfFeaturesDetectorList(BayesClassifierModule parent)
        //{
        //    Parent = parent;
        //}

        new public void AddRange(IEnumerable<SurfFeaturesDetector> collection)
        {
            //var enumerable = collection as Variable[] ?? collection.ToArray();
            var enumerable = collection as SurfFeaturesDetector[] ?? collection.ToArray();
            base.AddRange(enumerable);
            foreach (var v in enumerable)
            {
                v.NewResultAvailable += v_ValueHasBeenUpdated;  
            }
        }

        void v_ValueHasBeenUpdated(object sender, EventArgs e)
        {
            var v = (Variable) sender;
            if (NewVariableValueAvailable != null)
            {
                NewVariableValueAvailable(v, new EventArgs());
            }
        }

        new public void Add(SurfFeaturesDetector v)
        {
            base.Add(v);
            v.NewResultAvailable += v_ValueHasBeenUpdated;  
        }


        public event EventHandler NewVariableValueAvailable;
    }
}
