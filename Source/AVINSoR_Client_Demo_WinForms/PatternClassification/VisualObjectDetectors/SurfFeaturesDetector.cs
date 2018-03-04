using System;
using AVINSoR_Library.Auxiliary;

namespace AVINSoR_Library.PatternClassification.VisualObjectDetectors
{
    public class SurfFeaturesDetector : PatternClassificationInput
    {
        // MATLAB status flags
        private bool _waitingForTrainingResponse = false;


        /// <summary>
        /// A reference to the MATLAB server where this module should 'actualize' itself.
        /// </summary>
        public MLApp.MLApp MatlabServer { get; set; }


        public string Name { get; private set; }


        /// <summary>
        /// 'True' indicates object detected.
        /// </summary>
        public bool Result { get; private set; }


        public SurfFeaturesDetector(string name)
        {
            Name = name;
            
        }


        private void CaptureReferenceImage(bool cropImage)
        {
            if (cropImage)
            {
                Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".captureRefImgAndCrop();");
            }
            else
            {
                Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".captureRefImg();");
            }
        }


        public void CaptureCompare()
        {
            Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".captureCompare();");
        }


        public void CaptureCompareTrain()
        {
            Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".captureCompareTrain();");        
        }


        public void ReadStatusFlagsAndUpdateResult()
        {
            // set status flags
            if (MatlabInterface.MatlabVariableToInteger(ClassifierUniqueId + ".WaitingForTrainingResponse", false).Equals(0))
            {
                _waitingForTrainingResponse = false;
            }
            else
            {
                _waitingForTrainingResponse = true;
            }
            // update/fetch the result
            if (MatlabInterface.MatlabVariableToInteger(ClassifierUniqueId + ".ObjectFound", false).Equals(0))
            {
                Result = false;
            }
            else
            {
                Result = true;
            }
            // inform all
            if (NewResultAvailable != null)
            { 
                NewResultAvailable(this, new EventArgs());
            }
        }


        public void ProvideTrainingResponse(bool objectDetected)
        {
            if(!_waitingForTrainingResponse)
            {
                throw new ApplicationException("MATLAB SURFDetector object is not expecting a training response!");
            }
            var v = 0;
            // get int representative
            if(objectDetected)
            {
                v = 1;
            }
            Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".trainClassifier("+ v +");");
            // reset flag
            _waitingForTrainingResponse = false;
        }


        public event EventHandler NewResultAvailable;


        /// <summary>
        /// 
        /// </summary>
        private void ActualizeInMatlab()
        {
            Execute(ClassifierUniqueId + " = " + ClassifierUniqueId + ".SURFDetector(0);");
        }


        /// <summary>
        /// Execute MATLAB command, raising exception if error string returned. 
        /// ****** REQUIRES ABSTRACTION --- duplicate in BAYES CLASSIFIER MODULE
        /// </summary>
        /// <param name="command"></param>
        private void Execute(string command)
        {
            var s = MatlabServer.Execute(command);

            if (s != "")
            {
                throw new ApplicationException(s);
            }
        }
    }
}
