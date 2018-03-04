using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVINSoR_Library.Auxiliary;
using MatlabServer = MLApp.MLApp;

namespace AVINSoR_Library.DTControls
{
    public partial class Matlab : Component
    {
        public MatlabServer Server;

        public Matlab()
        {
            MatlabInterface.InitializeMatlab();
            Server = MatlabInterface.MatlabServer;
            InitializeComponent();
        }

        public Matlab(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Execute(string command)
        {
            var s = Server.Execute(command);

            if (s != "")
            {
                MessageBox.Show(s);
            }
        }
    }
}
