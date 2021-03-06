﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AVINSoR_Library.Movement
{
    [Serializable()]
    public class WheelMovementPatternManager : IEnumerable<WheelMovementPattern>
    {
        private readonly List<WheelMovementPattern> _motorPatterns = new List<WheelMovementPattern>();

        public IEnumerator<WheelMovementPattern> GetEnumerator()
        {
            var motorPatternArray = _motorPatterns.Cast<WheelMovementPattern>().ToArray();
            return motorPatternArray.TakeWhile(c => c != null).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _motorPatterns.GetEnumerator();
        }

        public void Add(WheelMovementPattern mp)
        {
            _motorPatterns.Add(mp);
        }

        public void Remove(WheelMovementPattern mp)
        {
            _motorPatterns.Remove(mp);
        }

        public void Save(string filename)
        {
            // To serialize the hashtable and its key/value pairs,   
            // you must first open a stream for writing.  
            // In this case, use a file stream.
            var fs = new FileStream(filename, FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            var formatter = new BinaryFormatter();
            formatter.Serialize(fs, this);
            fs.Close();
        }

        public static WheelMovementPattern Load(string filename)
        {
            // Open the file containing the data that you want to deserialize.
            var fs = new FileStream(filename, FileMode.Open);
            var formatter = new BinaryFormatter();
            var output = (WheelMovementPattern)formatter.Deserialize(fs);
            fs.Close();
            return output;
        }
    }
}

