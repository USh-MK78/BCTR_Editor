using BCTRLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCTREditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenBCTR = new OpenFileDialog
            {
                Title = "Open BCTR",
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "bctr file|*.bctr"
            };

            if (OpenBCTR.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(OpenBCTR.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                BCTR BCTRData = new BCTR();
                BCTRData.ReadBCTR(br, EndianConvert.GetEnumEndianToBytes(EndianConvert.Endian.LittleEndian));

                br.Close();
                fs.Close();
            }
        }
    }
}
