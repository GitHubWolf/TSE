using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TSE
{
    public partial class FormLocalStreamUpdate : TSE.FormLocalStreamWorker
    {
        public FormLocalStreamUpdate()
        {
            InitializeComponent();
            tableLayoutPanelAll.Dock = DockStyle.Fill;
            EnforceNewPid();//Force the user to enter a new PID.
        }
    }
}
