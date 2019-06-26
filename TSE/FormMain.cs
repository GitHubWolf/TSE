using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TSE
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open a bitstream file.
            OpenFileDialog openStreamFileDialog = new OpenFileDialog();
            openStreamFileDialog.Filter = "Transport stream files(*.ts;*.mpeg;*.mpg;*.m2t)|*.ts;*.mpeg;*.mpg;*.m2t|All files(*.*)|*.*";
            openStreamFileDialog.Title = "Select a transport stream";
            openStreamFileDialog.CheckFileExists = true;
            openStreamFileDialog.CheckPathExists = true;
            if (DialogResult.OK == openStreamFileDialog.ShowDialog())
            {
                OpenLocalFile(openStreamFileDialog.FileName);
            }
        }

        private void OpenLocalFile(string fileName)
        {
            FormFileStreamParser fileStreamParserForm = new FormFileStreamParser();
            fileStreamParserForm.MdiParent = this;
            fileStreamParserForm.WindowState = FormWindowState.Maximized;
            //Set the stream file to the parser.
            fileStreamParserForm.SetStreamFile(fileName);
            fileStreamParserForm.Show();

            //Save into the MRU.
            ManagerMRU.AddMruItem(fileName);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            List<string> recentFileList = ManagerMRU.GetMruList();

            toolStripMenuItemRecentFiles.DropDownItems.Clear();

            foreach (string file in recentFileList)
            {
                ToolStripMenuItem fileMenu = new ToolStripMenuItem(file);
                toolStripMenuItemRecentFiles.DropDownItems.Add(fileMenu);
                fileMenu.Click += new System.EventHandler(this.OpenRecentFile_Click); ;
            }
        }

        private void OpenRecentFile_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            OpenLocalFile(menuItem.Text);
        }

        private void calculateCRC32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCrc32 formCrc32 = new FormCrc32();
            formCrc32.MdiParent = this;
            formCrc32.WindowState = FormWindowState.Normal;
            formCrc32.Show();
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string fileToParse in fileList)
            {
                OpenLocalFile(fileToParse);
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void uDPHelperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUDPHelper udpHelperForm = new FormUDPHelper();
            udpHelperForm.MdiParent = this;
            udpHelperForm.WindowState = FormWindowState.Normal;
            udpHelperForm.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void sectionParserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSectionParser sectionParserForm = new FormSectionParser();
            sectionParserForm.MdiParent = this;
            sectionParserForm.WindowState = FormWindowState.Normal;
            sectionParserForm.Show();
        }

        private void toolStripButtonRecord_Click(object sender, EventArgs e)
        {
            //A ts media gateway.
            FormGateway recordForm = new FormGateway();
            recordForm.MdiParent = this;
            recordForm.WindowState = FormWindowState.Normal;
            recordForm.HideFileInput();
            recordForm.Show();
        }
    }
}
