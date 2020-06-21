using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.Forms
{
    public partial class NewMapWindow : Form
    {
        public NewMapWindow()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (Textures.TileImg.ContainsKey(tbxSrcName.Text))
            {
                Globals.Editor.CreateMap((int)nudWidth.Value, (int)nudHeight.Value, tbxSrcName.Text, (int)nudSrcX.Value, (int)nudSrcY.Value);
                Close();
            }
            else
                MessageBox.Show(tbxSrcName.Text + " not loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void NewMapWindow_Load(object sender, EventArgs e)
        {
            Location = new Point(Globals.Editor.Location.X + 10, Globals.Editor.Location.Y + 10);
        }
    }
}
