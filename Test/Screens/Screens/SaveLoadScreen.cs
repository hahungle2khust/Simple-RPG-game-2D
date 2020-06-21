using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Test
{
    class SaveLoadScreen : BaseScreen
    {
        private PictureBox BG;
        private Image mask = Image.FromFile(Textures.TextureDir + @"Images\Mask.png");
        private bool Save;
        private bool Load;
        private int _SelectedPage;
        private int _SelectedX;
        private int _SelectedY;
        private int SelectedX
        {
            get { return _SelectedX; }
            set
            {
                if (value < 0)
                {
                    if (SelectedPage > 0)
                    {
                        _SelectedX = 3;
                        SelectedPage -= 1;
                    }
                    else _SelectedX = 0;
                }
                else if (value > 3)
                {
                    _SelectedX = 0;
                    SelectedPage += 1;
                }
                else _SelectedX = value;
            }
        }
        private int SelectedY
        {
            get { return _SelectedY; }
            set
            {
                if (value < 0) _SelectedY = 3;
                else if (value > 3) _SelectedY = 0;
                else _SelectedY = value;
            }
        }
        private int SelectedPage
        {
            get { return _SelectedPage; }
            set
            {
                if (value < 0) _SelectedPage = 0;
                else _SelectedPage = value;
            }
        }
        private int FileWidth;
        private int FileHeight;

        private HashSet<int> Saves = new HashSet<int>();

        public SaveLoadScreen(string SaveOrLoad)
        {
            Name = "Save Load Screen";

            if (Game.worldscreen != null) Game.worldscreen.Active = false;

            BG = new PictureBox();
            BG.Visible = false;
            BG.Size = new Size(Game.bb.Width, Game.bb.Height);
            BG.Dock = DockStyle.Fill;
            BG.Parent = Globals.Main;
            BG.SizeMode = PictureBoxSizeMode.StretchImage;
            FileWidth = BG.Width / 4;
            FileHeight = BG.Height / 4;

            if (SaveOrLoad.ToLower() == "save")
            {
                Save = true;
                BG.Image = Image.FromFile(Globals.GameDir + @"\Content\Textures\Images\Saving.png");
            }
            else if(SaveOrLoad.ToLower() == "load")
            {
                Load = true;
                BG.Image = Image.FromFile(Globals.GameDir + @"\Content\Textures\Images\Loading.png");
            }

            ScanSaveFiles();
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(Textures.SaveLoadBG, new Rectangle(0, 0, BG.Width, BG.Height));
            for(int i=0; i<4; i += 1)
            {
                for(int j=0; j<4; j += 1)
                {
                    int num = SelectedPage * 16 + i * 4 + j;
                    g.DrawRectangle(Pens.White, new Rectangle(j * FileWidth, i * FileHeight,  FileWidth, FileHeight));
                    Brush c;
                    if (SelectedX == j && SelectedY == i) c = Brushes.Gold;
                    else if (Saves.Contains(num)) c = Brushes.LightSteelBlue;
                    else c = Brushes.DimGray;

                    g.DrawString("File " + num.ToString(), Globals.InfoFont, c, new Point(j * FileWidth + 10, i * FileHeight + 10));
                    if (Saves.Contains(num))
                    {
                        if(SelectedX == j && SelectedY == i)
                            g.DrawString("Load this save.", Globals.InfoFont, c, new Point(j * FileWidth + 10, i * FileHeight + 40));
                        else
                            g.DrawString("SAVED", Globals.InfoFont, c, new Point(j * FileWidth + 10, i * FileHeight + 40));
                    }  
                    else g.DrawString("[NO DATA]", Globals.InfoFont, c, new Point(j * FileWidth + 10, i * FileHeight + 40));
                    
                    if (SelectedX != j || SelectedY != i)
                        g.DrawImage(mask, new Rectangle(j * FileWidth, i * FileHeight, FileWidth, FileHeight));
                }
            }
            g.DrawRectangle(Pens.White, new Rectangle(SelectedX * FileWidth, SelectedY * FileHeight, FileWidth, FileHeight));
        }

        public override void KeyUp(Keys key)
        {
            if(key == Keys.Up)
            {
                SoundManager.PlayByFileName("Select1");
                SelectedY -= 1;
            }
            else if(key == Keys.Down)
            {
                SoundManager.PlayByFileName("Select1");
                SelectedY += 1;
            }
            else if(key == Keys.Left)
            {
                SoundManager.PlayByFileName("Select1");
                SelectedX -= 1;
            }
            else if(key == Keys.Right)
            {
                SoundManager.PlayByFileName("Select1");
                SelectedX += 1;
            }
            else if(key == Keys.Enter || key == Keys.Space)
            {
                SoundManager.PlayByFileName("Select2");
                int num = SelectedPage * 16 + SelectedY * 4 + SelectedX;
                if (Load && Saves.Contains(num))
                {
                    BG.Visible = true;
                    SaveHandler.LoadGame(Globals.GameDir + @"\Save\" + num.ToString() + ".sav");
                    BG.Visible = false;
                    Unload();
                }
                else if (Save)
                {
                    BG.Visible = true;
                    SaveHandler.SaveGame(Globals.GameDir + @"\Save\" + num.ToString() + ".sav");
                    BG.Visible = false;
                    if (!Saves.Contains(num)) Saves.Add(num);
                }
            }
            else if(key == Keys.ShiftKey || key == Keys.Escape || key == Keys.F4)
            {
                Unload();
                if (Load)
                {
                    Game.g.Clear(Globals.Main.BackColor);
                    ScreenManager.AddScreen(new TitleScreen());
                }
            }
        }

        private void ScanSaveFiles()
        {
            Saves.Clear();
            DirectoryInfo saveDir = new DirectoryInfo(Globals.GameDir + @"\Save\");
            FileInfo[] files = saveDir.GetFiles("*.sav");
            foreach (FileInfo f in files)
                Saves.Add(Int32.Parse(f.Name.Split('.')[0]));
        }

        public override void MouseClick(MouseButtons Button)
        {
           
        }

        public override void Update()
        {
            
        }

        public override void Unload()
        {
            mask.Dispose();
            mask = null;
            BG.Dispose();
            BG = null;
            if (Game.worldscreen != null) Game.worldscreen.Active = true;
            base.Unload();
        }
    }
}
