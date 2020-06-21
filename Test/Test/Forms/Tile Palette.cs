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
    public partial class Tile_Palette : Form
    {
        Graphics g;

        public Tile_Palette()
        {
            InitializeComponent();
        }

        private void Tile_Palette_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Globals.Editor.Location.X + Globals.Editor.Size.Width-10, Globals.Editor.Location.Y);


            cbxSource.Items.Clear();
            foreach (KeyValuePair<string, Image> source in Textures.TileImg)
            {
                cbxSource.Items.Add(source.Key);
            }
            cbxSource.SelectedIndex = 0;
            cbxLayer.SelectedIndex = 0;
        }

        private void cbxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image tmpImage = Textures.TileImg[cbxSource.Items[cbxSource.SelectedIndex].ToString()];
            pbSource.Width = tmpImage.Width;
            pbSource.Height = tmpImage.Height;
            this.Height = tmpImage.Height + 75;
            this.Width = tmpImage.Width + 200;
            pbSource.Image = tmpImage;
            Globals.SelectedName = cbxSource.Items[cbxSource.SelectedIndex].ToString();
        }

        private void pbSource_Click(object sender, EventArgs e)
        {

        }

        private void pbSource_MouseClick(object sender, MouseEventArgs e)
        {
            Globals.SelectedX = (int)Math.Floor( e.X / Convert.ToDouble(Globals.TileSize) );
            Globals.SelectedY = (int)Math.Floor(e.Y/ Convert.ToDouble(Globals.TileSize));

            Bitmap newImg = new Bitmap(pbSource.Width, pbSource.Height);
            g = Graphics.FromImage(newImg);

            Image tmpImage = Textures.TileImg[cbxSource.Items[cbxSource.SelectedIndex].ToString()];
            g.DrawImage(tmpImage, new Rectangle(0,0,newImg.Width, newImg.Height), new Rectangle(0,0,tmpImage.Width,tmpImage.Height), GraphicsUnit.Pixel);
            g.DrawImage(Properties.Resources.CursorRed , new Rectangle(Globals.SelectedX * Globals.TileSize, Globals.SelectedY * Globals.TileSize,Globals.TileSize, Globals.TileSize), new Rectangle(0,0,32, 32), GraphicsUnit.Pixel);

            if (pbSource.Image != Textures.TileImg[cbxSource.Items[cbxSource.SelectedIndex].ToString()])
            {
                pbSource.Image.Dispose();
            }
            pbSource.Image = newImg;
            g.Dispose();
        }

        private void cbxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.selectedLayer = cbxLayer.SelectedIndex;
        }
    }
}
