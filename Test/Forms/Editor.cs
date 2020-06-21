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
        public string CurrentMap = "";
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
            Game.Frame.Visible = false;
            MapHandler.LoadMap("Test");
            Game.ScreenX = 0;
            Game.ScreenY = 0;
            Game.EditorMode = true;
            Location = Globals.Main.Location;
            cbxPalette.SelectedIndex = 0;
            Game.bbg = CreateGraphics();
            Select();
            Focus();
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

            btnSetName.Focus();
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(CurrentPalette == null)) CurrentPalette.Close();
            Game.Frame.Visible = true;
            Game.EditorMode = false;
            Game.DoResetGame = true;
            ScreenManager.AddScreen(new TitleScreen());
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
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.S || e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
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
                if(Globals.TilePalette.cbxMode.SelectedIndex == 0)
                {
                    if (DownButton == MouseButtons.Left)
                    {
                        TileLayer tmpTileLayer = Game.World.TileList[X, Y].Layers[Globals.selectedLayer];
                        tmpTileLayer.srcImg = Globals.SelectedName;
                        if (ShiftIsDown) tmpTileLayer.srcPos = new Point(Globals.SelectedX + X - Game.ScreenX - MouseDownPos.X, Globals.SelectedY + Y - Game.ScreenY - MouseDownPos.Y);
                        else tmpTileLayer.srcPos = new Point(Globals.SelectedX, Globals.SelectedY);
                    }
                    else if (DownButton == MouseButtons.Right)
                    {
                        Game.World.TileList[X, Y].Layers[Globals.selectedLayer].srcImg = "Tiny";
                        Game.World.TileList[X, Y].Layers[Globals.selectedLayer].srcPos = new Point(0, 0);
                    }
                }
                else if(Globals.TilePalette.cbxMode.SelectedIndex == 1)
                {
                    if (DownButton == MouseButtons.Left)
                        Game.World.TileList[X, Y].IsBlocked = Globals.TilePalette.cbBlocked.Checked;
                    else if (DownButton == MouseButtons.Right)
                        Game.World.TileList[X, Y].IsBlocked = false;    
                }
                else if(Globals.TilePalette.cbxMode.SelectedIndex == 2)
                {
                    if (DownButton == MouseButtons.Left)
                    {
                        Game.World.TileList[X, Y].StepTrigger = Globals.TilePalette.cbStepTrigger.Checked;
                        Game.World.TileList[X, Y].TouchTrigger = Globals.TilePalette.cbTouchTrigger.Checked;
                        Game.World.TileList[X, Y].Script = Globals.TilePalette.tbxTrigAct.Text + "|" + Globals.TilePalette.tbxTrigValues.Text + "|" + Globals.TilePalette.tbxConditions.Text;
                    }
                    else if(DownButton == MouseButtons.Right)
                    {
                        Game.World.TileList[X, Y].StepTrigger = false;
                        Game.World.TileList[X, Y].TouchTrigger = false;
                        Game.World.TileList[X, Y].Script = "";
                    }
                    else if(DownButton == MouseButtons.Middle)
                    {
                        if(Game.World.TileList[X, Y].Script.Length > 0)
                        {
                            string[] str = Game.World.TileList[X, Y].Script.Split('|');
                            Globals.TilePalette.tbxTrigAct.Text = str[0];
                            Globals.TilePalette.tbxTrigValues.Text = str[1];
                            if (str.Length > 2) Globals.TilePalette.tbxConditions.Text = str[2];
                        }
                    }
                }
            }
            else if (cbxPalette.SelectedIndex == 1)
            {
                if(DownButton == MouseButtons.Left)
                {
                    int selectedIndex = Convert.ToInt32(Globals.CharacterPalette.lbCharList.Items[Globals.CharacterPalette.lbCharList.SelectedIndex].ToString().Split(':')[0]);
                    Character newChar = new Character(selectedIndex);
                    newChar.CurPos.X = X;
                    newChar.CurPos.Y = Y;
                    Game.World.MapChar.Add(newChar);
                }
                else if(DownButton == MouseButtons.Right)
                {
                    for (int i = 0; i < Game.World.MapChar.Count; i += 1)
                    {
                        if (Game.World.MapChar[i].CurPos.X == X && Game.World.MapChar[i].CurPos.Y == Y)
                        {
                            Game.World.MapChar.RemoveAt(i);
                            return;
                        }
                    }
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMapWindow newMap = new NewMapWindow();
            newMap.ShowDialog();
            newMap.Dispose();
        }

        public void CreateMap(int Width, int Height, string DefaultSrcName = "Tiny", int DefaultSrcX = 3, int DefaultSrcY = 1)
        {
            CurrentMap = "";
            Game.World = new Map(tbxName.Text, Width + 2, Height + 2, DefaultSrcName, DefaultSrcX, DefaultSrcY);
            for (int i = 0; i < Width + 3; i += 1)
            {
                for (int j = 0; j < Height + 3; j += 1)
                {
                    if (i == 0 || i == Width + 2 || j == 0 || j == Height + 2)
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

                Game.ScreenX = 0;
                Game.ScreenY = 0;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( CurrentMap.Length > 0)
            {
                SaveMap(CurrentMap);
            }
            else
            {
                saveAsToolStripMenuItem_Click(null, null);
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

        private void Editor_Move(object sender, EventArgs e)
        {
            if(CurrentPalette != null)
                CurrentPalette.Location = new Point(Location.X + Size.Width - 10, Location.Y);
        }

        private void btnSetName_Click(object sender, EventArgs e)
        {
            Game.World.Name = tbxName.Text;
            this.Focus();
        }

        private void Editor_MouseMove(object sender, MouseEventArgs e)
        {
            int X = Game.ScreenX + (int)Math.Floor(e.X / Convert.ToDouble(Globals.TileSize));
            int Y = Game.ScreenY + (int)Math.Floor(e.Y / Convert.ToDouble(Globals.TileSize));

            lblPos.Text = X.ToString() + " : " + Y.ToString();
        }
    }
}
