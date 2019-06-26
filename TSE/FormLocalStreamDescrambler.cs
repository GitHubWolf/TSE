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
    public partial class FormLocalStreamDescrambler : TSE.FormLocalStreamWorker
    {
        public bool DoEntropy
        {
            get
            {
                return checkBoxEnforceER.Checked;
            }
        }

        public bool GetControlWordSerials(out byte[] byteArray, ref int length)
        {
            return hexEditControlWordSerials.GetByteArray(out byteArray, ref length);
        }

        public FormLocalStreamDescrambler()
        {
            InitializeComponent();

            tableLayoutPanelAll.Dock = DockStyle.Fill;
            tableLayoutPanelBottom.SendToBack();
        }

        protected override Result OnDoIt()
        {
            Result result = new Result();
            if (result.Fine)
            {
                byte[] byteArray = null;
                bool isHexValid = false;
                int dataLength = 0;

                isHexValid = hexEditControlWordSerials.GetByteArray(out byteArray, ref dataLength);

                if (!isHexValid)
                {
                    result.SetResult(ResultCode.INVALID_DATA);
                }
                else
                {
                    if (dataLength < 8)
                    {
                        result.SetResult(ResultCode.INVALID_DATA);
                        MessageBox.Show("You must enter at least 8 bytes control words.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        hexEditControlWordSerials.Focus();
                    }
                }
            }
            return result;
        }
    }
}
