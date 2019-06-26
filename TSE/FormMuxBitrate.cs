using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InActionLibrary;

namespace TSE
{
    public partial class FormMuxBitrate : Form
    {
        private SortedDictionary<UInt16, MuxBitrate> streamBitrateList = null;
        private bool toPickupBitrate = false;

        public FormMuxBitrate()
        {
            InitializeComponent();
        }

        public void SetStreamBitrateList(SortedDictionary<UInt16, MuxBitrate> streamBitrateList)
        {
            this.streamBitrateList = streamBitrateList;
        }

        private void StreamBitrateForm_Shown(object sender, EventArgs e)
        {
            if (null != streamBitrateList)
            {
                foreach (KeyValuePair<UInt16, MuxBitrate> pair in streamBitrateList)
                {
                    Int64 bitrate = 0;
                    bool bitrateValid = pair.Value.GetBitrate(ref bitrate);
                    if (bitrateValid)
                    {
                        dataGridViewStreamBitrate.Rows.Add(Utility.GetValueHexString(pair.Key, 16), Convert.ToString(bitrate));
                    }
                }
            }
        }

        private void dataGridViewStreamBitrate_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell currentCell = dataGridViewStreamBitrate.CurrentCell;

            if (null != currentCell)
            {
                DataGridViewRow currentRow = dataGridViewStreamBitrate.Rows[currentCell.RowIndex];

                //Show the bitrate in the text box.
                maskedTextBoxBitrate.Text = (string)currentRow.Cells[1].Value;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (toPickupBitrate)
            {
                string bitrateText = maskedTextBoxBitrate.Text.Trim();
                if (bitrateText == "")
                {
                    MessageBox.Show(null, "Please select an existing bitrate or enter a new one!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    maskedTextBoxBitrate.Focus();
                }
                else
                {
                    Int64 bitrate = Convert.ToInt64(bitrateText);
                    if (bitrate <= 0)
                    {
                        MessageBox.Show(null, "Invalid bitrate!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        maskedTextBoxBitrate.Focus();
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
            else
            {
                Close();
            }
        }

        public void ForceEnterBitrate()
        {
            toPickupBitrate = true;
        }

        public Int64 GetSelectedBitrate()
        {
            return Convert.ToInt64(maskedTextBoxBitrate.Text.Trim());//Get the bitrate from the user.
        }
    }
}
