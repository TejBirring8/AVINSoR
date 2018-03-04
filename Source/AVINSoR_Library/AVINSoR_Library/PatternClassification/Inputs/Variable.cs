using System;
using AVINSoR_Library.PatternClassification.Inputs.Value;
using Microsoft.SqlServer.Server;

namespace AVINSoR_Library.PatternClassification.Inputs
{
    public class Variable : PatternClassificationInput
    {
        public string Name { get; protected set; }

        private GenericValue _value;
        public GenericValue Value
        {
            get
            {
                if (Enabled)
                {
                    return _value;
                }
                throw new ApplicationException("Variable " + Name + " cannot be used as it is currently disabled.");
            }
            set
            {
                if (!Enabled)
                    throw new ApplicationException("Variable " + Name +
                                                   " value cannot be set as it is currently disabled.");
                _value = value;
            }
        }
        public NxtRobot Owner { get; protected set; }

        public Variable(string name, GenericValue value, NxtRobot owner)
        {
            Enabled = true;
            Name = name;
            Value = value;
            value.ValueHasBeenUpdated += _ValueHasBeenUpdated;
            Owner = owner;
        }

        void _ValueHasBeenUpdated(object sender, EventArgs e)
        {
            InformOfValueChange();
        }

        public event EventHandler ValueHasBeenUpdated;

        protected void InformOfValueChange()
        {
            if (ValueHasBeenUpdated != null)
                ValueHasBeenUpdated(this, new EventArgs());
        }

        public Variable()
        {
        }


        /// <summary>
        /// The integer output of this object.
        /// </summary>
        public override int ClassifierOutput 
        {
            get
            {
                if (!_value.Value.HasValue)
                {
                    throw new ApplicationException("The variable " + Name + " does not have a value!");
                }
                return _value.Value.Value;
            }
        }

    }
}
