using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AVINSoR_Library.Auxiliary
{
    public static class MatlabInterface
    {
        /// <summary>
        /// A reference to the MatlabServer used by the application.
        /// </summary>
        public static MLApp.MLApp MatlabServer { get; set; }


        /// <summary>
        /// Initialize the COM Automation Server.
        /// </summary>
        public static void InitializeMatlab()
        {
            MatlabServer = new MLApp.MLApp();
            // set directory
            var dir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\MATLAB";
            MatlabServer.Execute(@"cd('" + dir + @"');");
            // delete all variables in memory and clear screen
            MatlabServer.Execute(@"clear;clc;");
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
                MatlabServer.Execute("clear " + variableName);
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
                MatlabServer.Execute(command.ToString());
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
                MatlabServer.Execute(command.ToString());
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
                MatlabServer.Execute(command.ToString());
            return command.ToString();
        }
    }
}
