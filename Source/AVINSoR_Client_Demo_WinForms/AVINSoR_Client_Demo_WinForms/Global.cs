using System;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Runtime.Serialization.Formatters.Binary;
using AVINSoR_Library;
using AVINSoR_Library.Movement;
using AVINSoR_Library.PatternClassification.Inputs;
using AVINSoR_Library.PatternClassification.PatternClassifiers;
using Gajatko.Common;
namespace AVINSoR_Client_Demo_WinForms
{
    public static class Global
    {
        //public static SerialPort TheSerialPort;
        public static NxtRobot TheRobot;
        public static Cognition TheCognition;
        public static CartesianVehicleDriver TheDriver;
        //public static WheelMovementPatternManager MovementPatternsManager;
        public static WheelMovementPatternList MovementPatterns = new WheelMovementPatternList();

        public static bool Ready { get; private set; }


        public static void InitializeApp(ref NxtRobot robotObj, int cartesianGridAbsMax, double robotWheelRadius)
        {
            TheCognition = new Cognition();
            TheDriver = new CartesianVehicleDriver(cartesianGridAbsMax, robotWheelRadius);
            TheRobot = robotObj;

            //connect & set status
            Ready = true;
        }


        public static void DeinitializeApp()
        {
            TheRobot = null;
            TheCognition = null;
            TheDriver = null;
            //MovementPatternsManager = null;
            Ready = false;
        }

        private static object LoadObj(string filename)
        {
            // Open the file containing the data that you want to deserialize.
            var fs = new FileStream(filename, FileMode.Open);
            var formatter = new BinaryFormatter();
            var output = formatter.Deserialize(fs);
            fs.Close();
            return output;           
        }

        private static void SaveCurrentState()
        {
            SaveObj(TheCognition, "cognition");
            SaveObj(TheDriver, "driver");
            SaveObj(MovementPatterns, "movement_patterns");
        }

        private static void SaveObj(object graph, string filename)
        {
            // To serialize the hashtable and its key/value pairs,   
            // you must first open a stream for writing.  
            // In this case, use a file stream.
            var fs = new FileStream(filename, FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            var formatter = new BinaryFormatter();
            formatter.Serialize(fs, graph);
            fs.Close();
        }
    }
}
