using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using IMLApp = MLApp.MLApp;


namespace AVINSoR_Library.Auxiliary
{
    public static class MatlabInterface
    {
        /// <summary>
        /// A reference to the MatlabServer used by the application.
        /// </summary>
        public static IMLApp MatlabServer { get; set; }

               /// <summary>
        /// Execute MATLAB command, raising exception if error string returned.
        /// </summary>
        /// <param name="command"></param>
        public static void Execute(string command)
        {
            var s = MatlabServer.Execute(command);

            if (s != "")
            {
                 throw new ApplicationException(s);
            }
        }

        /// <summary>
        /// Initialize the COM Automation Server.
        /// </summary>
        public static void InitializeMatlab()
        {
            Type matlabType = Type.GetTypeFromProgID("Matlab.Desktop.Application");
            MatlabServer = (IMLApp)Activator.CreateInstance(matlabType);
            // set directory
            var dir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\MATLAB";
            MatlabInterface.Execute(@"cd('" + dir + @"');");
            // delete all variables in memory and clear screen
            MatlabInterface.Execute(@"clear;clc;");
        }


        /// <summary>
        /// Fetch a variable from MATLAB into an 32-bit integer.
        /// </summary>
        /// <param name="variableName">Exact case-sensitive name in MATLAB.</param>
        /// <param name="clearAfterRetrieving">Indicates wether the variable should 
        /// be deleted after the data has been retrieved.</param>
        /// <returns>32-bit integer</returns>
        public static int MatlabVariableToInteger(string variableName, bool clearAfterRetrieving)
        {
            object o = MatlabServer.GetVariable(variableName, "base");
            var r = Convert.ToInt32(o);
            if (clearAfterRetrieving)
            {
                MatlabInterface.Execute("clear " + variableName);
            }
            return r;
        }


        /// <summary>
        /// Transfer an array of strings (holding data as integer, float, or double) to MATLAB - creating a vector.
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="nameInMatlab"></param>
        /// <param name="executeNow"></param>
        /// <returns></returns>
        public static string ArrayToMatlabVector(string[] strArray, string nameInMatlab, bool executeNow)
        {
            var command = new StringBuilder();
            command.Append(nameInMatlab + " = [");
            foreach (var str in strArray)
            {
                command.Append(str);
                command.Append(str != strArray.Last() ? " " : "];");
            }
            if (executeNow)
                MatlabInterface.Execute(command.ToString());
            return command.ToString();
        }


        /// <summary>
        /// Transfer an array of integers to MATLAB - creating a vector.
        /// </summary>
        /// <param name="intArray"></param>
        /// <param name="nameInMatlab"></param>
        /// <param name="executeNow"></param>
        /// <returns></returns>
        public static string ArrayToMatlabVector(int[] intArray, string nameInMatlab, bool executeNow)
        {
            var command = new StringBuilder();
            command.Append(nameInMatlab + " = [");
            foreach (var i in intArray)
            {
                command.Append(i);
                command.Append(i != intArray.Last() ? " " : "];");
            }
            if (executeNow)
                MatlabInterface.Execute(command.ToString());
            return command.ToString();
        }


        /// <summary>
        /// Transfer an array of double-precision values into MATLAB.
        /// </summary>
        /// <param name="dblArray"></param>
        /// <param name="nameInMatlab"></param>
        /// <param name="executeNow"></param>
        /// <returns></returns>
        public static string ArrayToMatlabVector(double[] dblArray, string nameInMatlab, bool executeNow)
        {
            var command = new StringBuilder();
            command.Append(nameInMatlab + " = [");
            foreach (var i in dblArray)
            {
                var g = Math.Round(i, 2);
                var gStr = g.ToString("F");
                command.Append(gStr);
                command.Append(i != dblArray.Last() ? " " : "];");
            }
            if (executeNow)
                MatlabInterface.Execute(command.ToString());
            return command.ToString();
        }
    }
}
