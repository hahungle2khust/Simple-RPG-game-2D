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
using System.IO.Compression;

namespace Test.Forms
{
    public partial class Editor : Form
    {
        string CurrentMap = "";
        Form CurrentPalette;
        public bool MouseIsDown = false;
        public bool ShiftIsDown = false;
        public Point MouseDownPos;
        public MouseButtons DownButton;

        public Editor()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            Game.EditorMode = true;
            this.Location = Globals.Main.Location;
            cbxPalette.SelectedIndex = 0;
            Game.Scale(this);
            Select();
        }

        private void cbxPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( !(CurrentPalette == null) ) CurrentPalette.Close();

            if( cbxPalette.SelectedIndex == 0)
            {
                Globals.TilePalette = new Tile_Palette();
                Globals.TilePalette.Show();
                CurrentPalette = Globals.TilePalette;
            }else if(cbxPalette.SelectedIndex == 1)
            {
                Globals.CharacterPalette = new Character_Palette();
                Globals.CharacterPalette.Show();
                CurrentPalette = Globals.CharacterPalette;
            }else if(cbxPalette.SelectedIndex == 3)
            {
                Globals.ScenePalette = new ScenePalette();
                Globals.ScenePalette.Show();
                CurrentPalette = Globals.ScenePalette;
            }
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.EditorMode = false;
            Game.Scale(Globals.Main);
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                Game.MoveDir = Dir.Up;
            }
            else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                Game.MoveDir = Dir.Down;
            }
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                Game.MoveDir = Dir.Left;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                Game.MoveDir = Dir.Right;
            }else if(e.Modifiers == Keys.Shift)
            {
                ShiftIsDown = true;
            }
        }

        private void Editor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.S || e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                Game.MoveDir = Dir.Stand;
            }else if(e.Modifiers == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                ShiftIsDown = false;
            }
        }

        public void DoMouseIsDown()
        {
            if (MouseIsDown == false) return;

            Point e = PointToClient(Cursor.Position);

            int X = Game.ScreenX + (int)Math.Floor(e.X / Convert.ToDouble(Globals.TileSize));
            int Y = Game.ScreenY + (int)Math.Floor(e.Y / Convert.ToDouble(Globals.TileSize));

            if (cbxPalette.SelectedIndex == 0)
            {
                if(DownButton == MouseButtons.Left)
                {
                    TileLayer tmpTileLayer = Game.World.TileList[X, Y].Layers[Globals.selectedLayer];
                    tmpTileLayer.srcImg = Globals.SelectedName;
                    if (ShiftIsDown) tmpTileLayer.srcPos = new Point(Globals.SelectedX + X - Game.ScreenX - MouseDownPos.X, Globals.SelectedY + Y - Game.ScreenY - MouseDownPos.Y);
                    else tmpTileLayer.srcPos = new Point(Globals.SelectedX, Globals.SelectedY);
                    Game.World.TileList[X, Y].IsBlocked = Globals.TilePalette.cbBlocked.Checked;
                    Game.World.TileList[X, Y].StepTrigger = Globals.TilePalette.cbStepTrigger.Checked;
                    Game.World.TileList[X, Y].TouchTrigger = Globals.TilePalette.cbTouchTrigger.Checked;
                    Game.World.TileList[X, Y].Script = Globals.TilePalette.tbxTrigAct.Text + "|" + Globals.TilePalette.tbxTrigValues.Text;
                }else if (DownButton == MouseButtons.Right)
                {
                    Game.World.TileList[X, Y].Layers[Globals.selectedLayer].srcImg = "Tiny";
                    Game.World.TileList[X, Y].Layers[Globals.selectedLayer].srcPos = new Point(0, 0);
                }
                
            }
            else if (cbxPalette.SelectedIndex == 1)
            {
                int selectedIndex = Convert.ToInt32(Globals.CharacterPalette.lbCharList.Items[Globals.CharacterPalette.lbCharList.SelectedIndex].ToString().Split(':')[0]);
                Character newChar = new Character(selectedIndex);
                newChar.CurPos.X = X;
                newChar.CurPos.Y = Y;
                Game.World.MapChar.Add(newChar);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int w = (int)nudWidth.Value;
            int h = (int)nudHeight.Value;

            Game.World = new Map(tbxName.Text, w+2, h+2);
            for(int i=0; i<w+3; i += 1)
            {
                for(int j=0; j<h+3; j += 1)
                {
                    if(i==0 || i==w+2 || j==0 || j == h+2)
                    {
                        Game.World.TileList[i, j].IsBlocked = true;
                        Game.World.TileList[i, j].Layers[0].srcImg = "Tiny";
                        Game.World.TileList[i, j].Layers[0].srcPos = new Point(0, 0);
                    }
                }
            }
        }

        private void SaveMap(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            GZipStream zipStream = new GZipStream(fs, CompressionMode.Compress);
            BinaryWriter writer = new BinaryWriter(zipStream);

            //Sửa từ đây
            writer.Write(Game.World.Name);
            writer.Write(Game.World.Size.X);
            writer.Write(Game.World.Size.Y);

            foreach(Tile t in Game.World.TileList)
            {
                writer.Write(t.Location.X);
                writer.Write(t.Location.Y);
                foreach( TileLayer tl in t.Layers)
                {
                    writer.Write(tl.srcImg);
                    writer.Write(tl.srcPos.X);
                    writer.Write(tl.srcPos.Y);
                }
                writer.Write(t.IsBlocked);
                writer.Write(t.TouchTrigger);
                writer.Write(t.StepTrigger);
                writer.Write(t.Script);
            }

            writer.Write(Game.World.MapChar.Count);
            foreach (Character c in Game.World.MapChar)
            {
                writer.Write(c.charType.Index);
                writer.Write((int)c.team);
                writer.Write(c.Level);
                writer.Write((int)c.LastDir);
                writer.Write(c.CurPos.X);
                writer.Write(c.CurPos.Y);
            }
            //End sửa

            writer.Close();
            writer.Dispose();
            zipStream.Close();
            zipStream.Dispose();
            fs.Close();
            fs.Dispose();
        }

        private void LoadMap(string path)
        {
            if ( File.Exists(path) == true)
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                GZipStream zipStream = new GZipStream(fs, CompressionMode.Decompress);
                BinaryReader reader = new BinaryReader(zipStream);

                Game.World = new Map(reader.ReadString(), reader.ReadInt32(), reader.ReadInt32());
                
                foreach(Tile t in Game.World.TileList)
                {
                    t.Location.X = reader.ReadInt32();
                    t.Location.Y = reader.ReadInt32();
                    foreach(TileLayer tl in t.Layers)
                    {
                        tl.srcImg = reader.ReadString();
                        tl.srcPos.X = reader.ReadInt32();
                        tl.srcPos.Y = reader.ReadInt32();
                    }
                    t.IsBlocked = reader.ReadBoolean();
                    t.TouchTrigger = reader.ReadBoolean();
                    t.StepTrigger = reader.ReadBoolean();
                    t.Script = reader.ReadString();
                }

                int count = reader.ReadInt32() - 1;
                for (int i = 0; i <= count; i += 1)
                {
                    Character newChar = new Character(reader.ReadInt32());
                    newChar.team = (Team)reader.ReadInt32();
                    newChar.Level = reader.ReadInt32();
                    newChar.LastDir = (Dir)reader.ReadInt32();
                    newChar.CurPos = new Point(reader.ReadInt32(), reader.ReadInt32());
                    Game.World.MapChar.Add(newChar);
                }

                reader.Close();
                reader.Dispose();
                zipStream.Close();
                zipStream.Dispose();
                fs.Close();
                fs.Dispose();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( CurrentMap.Length > 0)
            {
                SaveMap(CurrentMap);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if( sfdSave.ShowDialog() == DialogResult.OK)
            {
                CurrentMap = sfdSave.FileName;
                SaveMap(sfdSave.FileName);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ofdLoad.ShowDialog() == DialogResult.OK)
            {
                CurrentMap = ofdLoad.FileName;
                LoadMap(ofdLoad.FileName);
            }
        }

        private void Editor_MouseDown(object sender, MouseEventArgs e)
        {

            if (MouseIsDown == false)
            {
                MouseDownPos = new Point((int)Math.Floor(e.X * 1.0 / Globals.TileSize), (int)Math.Floor(e.Y * 1.0 / Globals.TileSize));
            }
            MouseIsDown = true;
            DownButton = e.Button;
        }

        private void Editor_MouseUp(object sender, MouseEventArgs e)
        {
            MouseIsDown = false;
            MouseDownPos = new Point(-1, -1);
        }
    }
}
