using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using InActionLibrary;
using System.IO;

namespace TSE
{
    public partial class FormLocalStreamDump : TSE.FormLocalStreamWorker
    {
        public FormLocalStreamDump()
        {
            InitializeComponent();

            tableLayoutPanelAll.Dock = DockStyle.Fill;
            tableLayoutPanelBottom.SendToBack();
            comboBoxPercentage.SelectedIndex = 0;
        }

        public int Percentage
        {
            get
            {
                return Int32.Parse(comboBoxPercentage.Text);
            }
        }

        public bool Enforce188Bytes
        {
            get
            {
                return checkBoxTo188.Checked;
            }
        }
    }
}
