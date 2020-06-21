using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    class TrollScreen : BaseScreen
    {
        private Image Tom = Image.FromFile(Globals.GameDir + @"\Content\Textures\Tom.png");
        private List<Image> RPS = new List<Image>();
        private Image Char;
        private int _SelectedIndex = 0;
        private int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (value > 2) _SelectedIndex = 0;
                else if (value < 0) _SelectedIndex = 2;
                else _SelectedIndex = value;
            }
        }
        private bool Selected = false;
        private int SelectedTimer = 0;
        private int SelectedInterval = 2000;
        private int Round = 0;


        public TrollScreen()
        {
            Name = "Troll Screen";
            if (Game.worldscreen != null) Game.worldscreen.Active = false;

            BGM.PlayByFileName("RPS");

            if (Game.Player.charType.Index == 5)
                Char = Image.FromFile(Globals.GameDir + @"\Content\Textures\Scenes\Characters\HHung.png");
            else if(Game.Player.charType.Index == 3)
                Char = Image.FromFile(Globals.GameDir + @"\Content\Textures\Scenes\Characters\Dat.png");
            else if (Game.Player.charType.Index == 4)
                Char = Image.FromFile(Globals.GameDir + @"\Content\Textures\Scenes\Characters\QHung.png");
            else if (Game.Player.charType.Index == 6)
                Char = Image.FromFile(Globals.GameDir + @"\Content\Textures\Scenes\Characters\Khai.png");

            RPS.Add(Image.FromFile(Globals.GameDir + @"\Content\Textures\Arts\Rock.png"));
            RPS.Add(Image.FromFile(Globals.GameDir + @"\Content\Textures\Arts\Paper.png"));
            RPS.Add(Image.FromFile(Globals.GameDir + @"\Content\Textures\Arts\Scissors.png"));

        }

        public override void Draw(Graphics g)
        {
            if (Removed) return;

            g.FillRectangle(Brushes.Aqua, new Rectangle(0, 0, Game.Frame.Width, Game.Frame.Height));
            g.DrawImage(Char, new Point(10, Game.Frame.Height - Char.Height));
            g.DrawImage(Tom, new Point(Game.GameWindow.Width * 3 / 4 - Tom.Width / 2, Game.Frame.Height - Tom.Height - 50));
            for(int i=0; i<RPS.Count; i += 1)
            {
                g.DrawImage(RPS[i], new Rectangle(90, 20 + i * 90, 75, 75));
                g.DrawImage(RPS[i], new Rectangle(Game.Frame.Width - 90 - 75, 20 + i * 90, 75, 75));
            }
            g.DrawImage(Textures.ArtImg["Sword"], new Rectangle(15, 25 + SelectedIndex * 90, 64, 64), new Rectangle(0, 2 *Globals.TileSize, Globals.TileSize, Globals.TileSize), GraphicsUnit.Pixel);
            g.DrawString("Minigame: oẳn tù tì với Tom.\nAi thắng 10 lần trước sẽ chiến thắng.", Globals.NameBoxFont, Brushes.Black, new Point(190, 40));
            g.DrawString("\n\n0               :               " + Round.ToString(), Globals.NameBoxFont, Brushes.Black, new Point(200, 100));

            if (Selected)
            {
                g.DrawImage(RPS[SelectedIndex], new Rectangle(300, 140, 75, 75));
                int s = SelectedIndex + 1;
                if (s > 2) s = 0;
                g.DrawImage(RPS[s], new Rectangle(570, 140, 75, 75));
            }
        }

        public override void KeyUp(Keys key)
        {
            if (Selected) return;

            if(key == Keys.Up)
            {
                SelectedIndex -= 1;
                SoundManager.PlayByFileName("Select1");
            }
            else if(key == Keys.Down)
            {
                SelectedIndex += 1;
                SoundManager.PlayByFileName("Select1");
            }
            else if(key == Keys.Space || key == Keys.Enter)
            {
                SoundManager.PlayByFileName("Select2");
                Selected = true;
                SoundManager.PlayByFileName("Tom");
                Round += 1;
            }
        }

        public override void MouseClick(MouseButtons Button)
        {
            
        }

        public override void Update()
        {
            if (Selected)
            {
                SelectedTimer += Game.ElapsedGameTime;
                if(SelectedTimer >= SelectedInterval)
                {
                    SelectedTimer = 0;
                    Selected = false;
                    if(Round >= 10)
                    {
                        Unload();
                        BGM.StopBGM();
                        Game.InGame = false;
                        Game.worldscreen.Unload();
                        Game.Tomming = 10;
                        Globals.Main.videoPlayer.Visible = true;
                        Globals.Main.videoPlayer.URL = Globals.GameDir + @"\Content\Video\Tom.mp4";
                        Globals.Main.videoPlayer.Ctlcontrols.play();
                    }
                }
            }
            
        }

        public override void Unload()
        {
            Tom.Dispose();
            Tom = null;
            Char.Dispose();
            Char = null;
            foreach(Image i in RPS)
            {
                i.Dispose();
            }
            RPS.Clear();
            if (Game.worldscreen != null) Game.worldscreen.Active = true;
            BGM.StopBGM();
            base.Unload();
        }
    }
}
