using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InActionLibrary;
using System.IO;

namespace TSE
{
    public partial class FormLocalStreamWorker : Form
    {
        //If set to true, we will force the user to enter a new PID.
        private bool enforceNewPid = false;

        public SortedDictionary<UInt16, PidProfile> PidListIn
        {
            get;
            set;
        }

        public SortedDictionary<UInt16, PidUpdate> PidListOut
        {
            get;
            set;
        }

        public string OutputFileName
        {
            get;
            set;
        }

        protected void EnforceNewPid()
        {
            enforceNewPid = true;
            groupBoxNewPid.Show();
        }

        public FormLocalStreamWorker()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Result result = new Result();
            PidProfile selectedItem = (PidProfile)listBoxPidIn.SelectedItem;
            int leftSideSelectedIndex = listBoxPidIn.SelectedIndex;

            if (result.Fine)
            {
                if (null == selectedItem)
                {
                    MessageBox.Show("Please select one PID one the left side.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result.SetResult(ResultCode.INVALID_DATA);
                }
            }

            UInt16 newPid = 0;
            if (result.Fine)
            {
                //Force the user to enter a new PID.
                if (enforceNewPid)
                {
                    try
                    {
                        //Get the new PID from user input.
                        newPid = UInt16.Parse(maskedTextBoxNewPid.Text);

                        Console.WriteLine("New PID is " + newPid);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid PID. Please enter a valid UInt16 PID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        result.SetResult(ResultCode.INVALID_DATA);
                        maskedTextBoxNewPid.Focus();
                    }
                }
                else
                {
                    //New PID in fact is the same with the old PID on left side. Just to reuse the same structurer to make the code simple.
                    newPid = selectedItem.PID;
                }
            }

            if (result.Fine)
            {
                ListBox.SelectedObjectCollection selectedProfileCollection = listBoxPidIn.SelectedItems;
                Array selectedProfileArray = Array.CreateInstance(typeof(PidProfile), listBoxPidIn.SelectedItems.Count);
                selectedProfileCollection.CopyTo(selectedProfileArray, 0);//Make a copy of all the selected items.

                foreach (PidProfile pidProfile in selectedProfileArray)
                {
                    int leftIndex = listBoxPidIn.Items.IndexOf(pidProfile);
                    //Remove the left item.
                    listBoxPidIn.Items.Remove(pidProfile);

                    //Add to the right side.
                    int rightIndex = listBoxPidOut.Items.Add(new PidUpdate(pidProfile, newPid, enforceNewPid));
                    listBoxPidOut.SelectedIndex = rightIndex;

                    //Select the next item.
                    if (listBoxPidIn.Items.Count > 0)
                    {
                        if (leftIndex < listBoxPidIn.Items.Count)
                        {
                            listBoxPidIn.SelectedIndex = leftIndex;
                        }
                        else
                        {
                            if ((leftIndex - 1) >= 0)
                            {
                                listBoxPidIn.SelectedIndex = leftIndex - 1;
                            }

                        }
                    }//if (listBoxPid.Items.Count > 0)
                }//foreach (PidProfile pidProfile in selectedProfileArray)
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {

            Result result = new Result();

            if (0 == listBoxPidOut.SelectedItems.Count)
            {
                MessageBox.Show("Please select one item on the right side.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result.SetResult(ResultCode.INVALID_DATA);
            }

            if (result.Fine)
            {
                ListBox.SelectedObjectCollection selectedProfileCollection = listBoxPidOut.SelectedItems;
                Array selectedPidUpdateArray = Array.CreateInstance(typeof(PidUpdate), listBoxPidOut.SelectedItems.Count);
                selectedProfileCollection.CopyTo(selectedPidUpdateArray, 0);//Make a copy of all the selected items.

                foreach (PidUpdate pidUpdate in selectedPidUpdateArray)
                {
                    int rightIndex = listBoxPidOut.Items.IndexOf(pidUpdate);
                    //Remove the right item.
                    listBoxPidOut.Items.Remove(pidUpdate);

                    //Recover the item on the left side and insert it into the right position.
                    int leftIndex = AddPidProfile(pidUpdate.GetOldPidProfile());
                    listBoxPidIn.SelectedIndex = leftIndex;

                    if (listBoxPidOut.Items.Count > 0)
                    {
                        if (rightIndex < listBoxPidOut.Items.Count)
                        {
                            listBoxPidOut.SelectedIndex = rightIndex;
                        }
                        else
                        {
                            if ((rightIndex - 1) >= 0)
                            {
                                listBoxPidOut.SelectedIndex = rightIndex - 1;
                            }
                        }
                    }

                }//foreach (PidProfile pidUpdate in selectedPidUpdateArray)
            }

        }//buttonRemove_Click

        private int AddPidProfile(PidProfile oldPidProfile)
        {
            int index = 0;

            foreach (PidProfile pidProfile in listBoxPidIn.Items)
            {
                if (pidProfile.PID > oldPidProfile.PID)
                {
                    break;
                }

                index++;
            }

            listBoxPidIn.Items.Insert(index, oldPidProfile);

            return index;
        }//AddPidProfile

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            //Open a bitstream file.
            SaveFileDialog saveStreamFileDialog = new SaveFileDialog();
            saveStreamFileDialog.Filter = "Transport stream files(*.ts;*.mpeg;*.mpg;*.m2t)|*.ts;*.mpeg;*.mpg;*.m2t|All files(*.*)|*.*";
            saveStreamFileDialog.Title = "Set output stream";
            saveStreamFileDialog.CheckPathExists = true;
            if (DialogResult.OK == saveStreamFileDialog.ShowDialog())
            {
                textBoxOutputFile.Text = saveStreamFileDialog.FileName;
            }
        }

        private void FormLocalStreamWorker_Shown(object sender, EventArgs e)
        {
            int index = 0;

            if (null != PidListIn)
            {
                foreach (KeyValuePair<UInt16, PidProfile> pair in PidListIn)
                {
                    if (0 != pair.Value.PacketCount)
                    {
                        index = listBoxPidIn.Items.Add(pair.Value);
                    }
                }

                if (listBoxPidIn.Items.Count > 0)
                {
                    listBoxPidIn.SelectedIndex = 0;
                }
            }//if (null != PidListIn)
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Result result = PerformCommonChecking();

            if (result.Fine)
            {
                result = OnDoIt();
            }

            if (result.Fine)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        //We expect the base class will override this method.
        protected virtual Result OnDoIt()
        {
            return new Result();
        }

        private Result PerformCommonChecking()
        {
            Result result = new Result();

            if (result.Fine)
            {
                if (0 == listBoxPidOut.Items.Count)
                {
                    MessageBox.Show("No PID has been selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result.SetResult(ResultCode.INVALID_DATA);
                    listBoxPidOut.Focus();
                }
            }

            if (result.Fine)
            {
                //Check whether the output file name is valid.

                string outputFileName = textBoxOutputFile.Text;

                try
                {
                    FileStream outpuFileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write);
                    outpuFileStream.Close();

                    OutputFileName = outputFileName;
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid output file name. Please enter a valid file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result.SetResult(ResultCode.FAILED_TO_OPEN_FILE);
                    textBoxOutputFile.Focus();
                }
            }

            if (result.Fine)
            {
                SortedDictionary<UInt16, PidUpdate> pidListOutput = new SortedDictionary<UInt16, PidUpdate>();
                //Get all the PID to process.
                foreach (PidUpdate pidUpdate in listBoxPidOut.Items)
                {
                    pidListOutput.Add(pidUpdate.OldPid, pidUpdate);
                }

                PidListOut = pidListOutput;
            }

            return result;
        }
    }
}
