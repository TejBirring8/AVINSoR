using System;
using System.Collections.Generic;
using System.Linq;
using Gajatko.Common;

namespace AVINSoR_Library.PatternClassification.Inputs
{
    public class VariablesList : EventyList<Variable>
    {
        new public void AddRange(IEnumerable<Variable> collection)
        {
            //var enumerable = collection as Variable[] ?? collection.ToArray();
            var enumerable = collection as Variable[] ?? collection.ToArray();
            base.AddRange(enumerable);
            foreach (var v in enumerable)
            {
                v.ValueHasBeenUpdated += v_ValueHasBeenUpdated;  
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

        new public void Add(Variable v)
        {
            base.Add(v);
            v.ValueHasBeenUpdated += v_ValueHasBeenUpdated;  
        }


        public event EventHandler NewVariableValueAvailable;
    }
}
