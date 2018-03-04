using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVINSoR_Library.PatternClassification.Inputs;

namespace AVINSoR_Library.DTControls
{
    public partial class Variable : UserControl
    {
        private PatternClassification.Inputs.Variable _variable = null;
        
        // index of variable of interest (i.e. variable to bind to) in the Variable List of the Robot component obj
        public int VariableIndex { get; set; }

        private NxtRobot _robot;

        public NxtRobot Robot
        {
            get { return _robot; }
            set
            {
                _robot = value;
                _robot.HasConnected += _robot_HasConnected;
                _robot.HasDisconnected += _robot_HasDisconnected;
                _robot.ModesReconfigured += _robot_ModesReconfigured;
            }
        }

        void _robot_ModesReconfigured(object sender, EventArgs e)
        {
            BindToSetVariable();
        }

        void _robot_HasDisconnected(object sender, EventArgs e)
        {
            _variable = null;
        }

        void _robot_HasConnected(object sender, EventArgs e)
        {
            _variable = _robot.Variables[VariableIndex];
            BindToSetVariable();
        }
    

        public void BindToSetVariable()
        {
            if (_variable == null)
                return;

            // set label
            if (_variable.GetType() == typeof (DerivativeVariable))
            {
                lblVariable.Text = @"Derivative Variable : " + _variable.Name;
            }
            else if (_variable.GetType() == typeof (IntegratorVariable))
            {
                lblVariable.Text = @"Integrator Variable : " + _variable.Name;
            }
            else
            {
                lblVariable.Text = @"Sensor Variable : " + _variable.Name;
            }
            
            // describe parent object
            lblVariableParent.Text = _variable.Owner != null ? _variable.Owner.Name : "N/A";

            // describe units
            lblVariableUnits.Text = _variable.Value.Units + @" [ min val = " + _variable.Value.MinimumAllowableValue +
                                    ", max val = " + _variable.Value.MaximumAllowableValue + " ]";

            // subscribe to value change
            _variable.NewResultAvailable += VariableValueChanged;

            if (!_variable.Enabled)
            {
                lblVariable.Text = "Disabled";
                txtVariableValue.Text = "";
            }
        }

        private void VariableValueChanged(object sender, EventArgs e)
        {
            var tempVariable = (PatternClassification.Inputs.Variable) sender;

            if (txtVariableValue.InvokeRequired)
            {
                Invoke(new MethodInvoker(() => VariableValueChanged(sender, e)));
            }

            txtVariableValue.Text = tempVariable.Value.Value.ToString();
        }

        public Variable()
        {
            InitializeComponent();
        }


    }
}
