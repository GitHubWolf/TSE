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
    public partial class FormCrc32 : Form
    {
        public FormCrc32()
        {
            InitializeComponent();
            
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            byte[] byteArray = null;
            bool isHexValid = false;
            int dataLength = 0;

            hexEditCrc32Result.Clear();

            isHexValid = this.hexEditData.GetByteArray(out byteArray, ref dataLength);
            if (isHexValid)
            {
                UInt32 crcResult = Utility.GetCrc32(byteArray, 0, dataLength);
                hexEditCrc32Result.Text = String.Format("{0, 8:X8}", crcResult);
            }
        }
    }
}
