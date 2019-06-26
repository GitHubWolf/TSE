using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using InActionLibrary;

namespace TSE
{
    public partial class FormLocalStreamScrambler : TSE.FormLocalStreamWorker
    {
        public bool DoEntropy
        {
            get
            {
                return checkBoxEnforceER.Checked;
            }
        }
        public SortedDictionary<UInt16, MuxBitrate> MuxBitrateList
        {
            get;
            set;
        }

        public bool GetControlWordSerials(out byte[] byteArray, ref int length)
        {
            return hexEditControlWordSerials.GetByteArray(out byteArray, ref length);
        }

        public int EvenOddStart
        {
            get
            {
                return comboBoxEvenOdd.SelectedIndex;
            }
        }

        public Int64 Bitrate
        {
            get
            {
                return Convert.ToInt64(maskedTextBoxBitrate.Text.Trim());//Get the bitrate from the user.
            }
        }

        public int CwPeriod
        {
            get
            {
                return Convert.ToInt32(maskedTextBoxCwPeriod.Text.Trim());//Get the CW period from the user.
            }
        }


        public FormLocalStreamScrambler()
        {
            InitializeComponent();

            tableLayoutPanelAll.Dock = DockStyle.Fill;
            tableLayoutPanelBottom.SendToBack();
            comboBoxEvenOdd.SelectedIndex = 0;
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

            if (result.Fine)
            {
                string bitrateText = maskedTextBoxBitrate.Text.Trim();
                if (bitrateText == "")
                {
                    MessageBox.Show(null, "Please select an existing bitrate or enter a new one!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    maskedTextBoxBitrate.Focus();
                    result.SetResult(ResultCode.INVALID_DATA);
                }
                else
                {
                    Int64 bitrate = Convert.ToInt64(bitrateText);
                    if (bitrate <= 0)
                    {
                        MessageBox.Show(null, "Invalid bitrate!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        maskedTextBoxBitrate.Focus();
                        result.SetResult(ResultCode.INVALID_DATA);
                    }
                }
            }

            if (result.Fine)
            {
                string cwPeriodText = maskedTextBoxCwPeriod.Text.Trim();
                if (cwPeriodText == "")
                {
                    MessageBox.Show(null, "Please enter a CW period!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    maskedTextBoxCwPeriod.Focus();
                    result.SetResult(ResultCode.INVALID_DATA);
                }
                else
                {
                    Int64 cwPeriod = Convert.ToInt64(cwPeriodText);
                    if (cwPeriod <= 0)
                    {
                        MessageBox.Show(null, "Invalid CW period!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        maskedTextBoxCwPeriod.Focus();
                        result.SetResult(ResultCode.INVALID_DATA);
                    }
                }
            }

            return result;
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            FormMuxBitrate formMuxBitrate = new FormMuxBitrate();
            formMuxBitrate.ForceEnterBitrate(); //Force the user to select a bitrate.
            formMuxBitrate.SetStreamBitrateList(this.MuxBitrateList);

            if (DialogResult.OK == formMuxBitrate.ShowDialog())
            {
                maskedTextBoxBitrate.Text = formMuxBitrate.GetSelectedBitrate().ToString();
            }
        }
    }
}
