using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

// Change to allow first commit.

namespace FileDuplicator
{
    public partial class frmMain : Form
    {
        // Keep track of the name of the currently opened file.
        private string strCurrentFilePath = "";
        private string strNewFilePath = "";
        private const int iBufferSize = 4096;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            DialogResult drOpenResult = ofdSource.ShowDialog();
            //Only Proceed if the dialog result is OK.
            if (drOpenResult == DialogResult.OK)
            {
                // Get the path of the file sleteced by the user and open it.
                strCurrentFilePath = ofdSource.FileName;
                txtSource.Text = strCurrentFilePath.ToString();
            }
        }

        private void btnDestination_Click(object sender, EventArgs e)
        {
            DialogResult drOpenResult = sfdDestination.ShowDialog();
            //Only Proceed if the dialog result is OK.
            if (drOpenResult == DialogResult.OK)
            {
                // Get the path of the file sleteced by the user and open it.
                strNewFilePath = sfdDestination.FileName;
                txtDestination.Text = strNewFilePath.ToString();
            }
        }

        private void btn_Duplicate_Click(object sender, EventArgs e)
        {
            if (txtSource.Text != "" && txtDestination.Text != "")
            {
                //FileStream for the old file.
                FileStream fsFile = File.Open(strCurrentFilePath, FileMode.Open, FileAccess.Read);
                //FileStream for the new file.
                FileStream fsNewFile = File.Create(strNewFilePath,);
                // Read the data in the file. Set up buffer to hold data read from file.
                byte[] byBuffer = new byte[iBufferSize];
                // Initial read to start the process. Then loop as long as we read in more bytes.
                int iBytesRead = fsFile.Read(byBuffer, 0, iBufferSize);
                
                while (iBytesRead > 0)
                {
                    // Read a new block of bytes from the old file
                    iBytesRead = fsFile.Read(byBuffer, 0, iBufferSize);
                    // Write a new block of bytes to the new file
                    fsNewFile.Write(byBuffer, 0, iBufferSize);
                }

                // Finished. Close the File.
                fsFile.Close();
            }
            else
                MessageBox.Show("You must have a source file and destination for the new file.", "Duplication Error", MessageBoxButtons.OK);
        }
    }
}
