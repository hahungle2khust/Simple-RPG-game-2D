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
            foreach (var source in Textures.TileImg)
            {
                cbxSource.Items.Add(source.Key);
            }
            cbxSource.SelectedIndex = 0;
            cbxLayer.SelectedIndex = 0;
            cbxMode.SelectedIndex = 0;
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

        private void cbxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxMode.SelectedIndex == 0)
            {
                cbBlocked.Enabled = false;
                cbStepTrigger.Enabled = false;
                cbTouchTrigger.Enabled = false;
                tbxTrigAct.Enabled = false;
                tbxTrigValues.Enabled = false;
                tbxConditions.Enabled = false;
            }
            else if(cbxMode.SelectedIndex == 1)
            {
                cbBlocked.Enabled = true;
                cbStepTrigger.Enabled = false;
                cbTouchTrigger.Enabled = false;
                tbxTrigAct.Enabled = false;
                tbxTrigValues.Enabled = false;
                tbxConditions.Enabled = false;
            }
            else if(cbxMode.SelectedIndex == 2)
            {
                cbBlocked.Enabled = false;
                cbStepTrigger.Enabled = true;
                cbTouchTrigger.Enabled = true;
                tbxTrigAct.Enabled = true;
                tbxTrigValues.Enabled = true;
                tbxConditions.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selected = cbxSource.Items[cbxSource.SelectedIndex].ToString();
            int w = Textures.TileImg[selected].Width / Globals.TileSize - 1;
            int h = Textures.TileImg[selected].Height / Globals.TileSize - 1;
            Map map = new Map("", w+2, h+2, selected);

            for (int x = 0; x < w + 3; x += 1)
            {
                for (int y = 0; y < h + 3; y += 1)
                {
                    if (x == 0 || x == w + 2 || y == 0 || y == h + 2)
                    {
                        map.TileList[x, y].IsBlocked = true;
                        map.TileList[x, y].Layers[0].srcImg = "Tiny";
                        map.TileList[x, y].Layers[0].srcPos = new Point(0, 0);
                    }
                    else
                    {
                        map.TileList[x, y].IsBlocked = false;
                        map.TileList[x, y].Layers[0].srcPos = new Point(x - 1, y - 1);
                    }
                }
            }

            Globals.Editor.CurrentMap = "";
            Game.World = map;
        }
    }
}
