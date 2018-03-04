using System.Collections.Generic;
using System.Linq;
using Gajatko.Common;

namespace AVINSoR_Library.PatternClassification.PatternClassifiers
{
    public class BayesClassifierModuleList : EventyList<BayesClassifierModule>
    {
        private readonly MLApp.MLApp _matlabServer;

        public BayesClassifierModuleList(MLApp.MLApp matlabServer)
        {
            _matlabServer = matlabServer;
        }

        new public void Add(BayesClassifierModule bcm)
        {
            bcm.MatlabServer = _matlabServer;
            base.Add(bcm);
        }

        new public void AddRange(IEnumerable<BayesClassifierModule> collection)
        {
            var bayesClassifierModules = collection as BayesClassifierModule[] ?? collection.ToArray();
            foreach (var c in bayesClassifierModules)
            {
                c.MatlabServer = _matlabServer;
            }
            base.AddRange(bayesClassifierModules);
        }
    }
}
