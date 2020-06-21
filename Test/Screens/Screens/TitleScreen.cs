using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Media;

namespace Test
{
    class TitleScreen : BaseScreen
    {
        public PictureBox BG;
        public Label Press;
        public Font lFont = new Font("Courier New", 20, FontStyle.Bold);
        public static MediaPlayer TitleTheme = new MediaPlayer();

        private int TextTimer = 0;
        private int TextInterval = 500;

        private int _BGCurFrame = 0;
        private int BGCurFrame
        {
            get { return _BGCurFrame; }
            set
            {
                if (value > Textures.Title.Count - 1) _BGCurFrame = 0;
                else _BGCurFrame = value;
            }
        }
        private int BGTimer = 0;
        private int BGAniInterval = 250;

        private int BGMoveTimer = 0;
        private int BGMoveInterval = 55;
        private bool MenuShowed = false;

        public TitleScreen()
        {
            Name = "Title Screen";
            BGM.StopBGM();

            BG = new PictureBox();
            BG.Parent = Game.Frame;
            BG.SizeMode = PictureBoxSizeMode.StretchImage;
            BG.Dock = DockStyle.Fill;
            Size s = new Size(BG.Width, BG.Height);
            BG.Dock = DockStyle.None;
            BG.Size = s;
            BG.Location = new Point(BG.Width, 0);
            BG.Image = Textures.Title[BGCurFrame];
            BG.MouseClick += BG_MouseClick;

            Press = new Label();
            Press.AutoSize = true;
            Press.Parent = BG;
            Press.BackColor = System.Drawing.Color.Transparent;
            Press.Text = "Press any key to continue";
            Press.Font = lFont;
            Press.Location = new Point(100 + BG.Width / 2 - Press.Width / 2, BG.Height - Press.Height - 23);
            Press.Visible = false;

            PlayTitleTheme();
        }

        public override void KeyUp(Keys key)
        {
            if(key != Keys.Escape)
            {
                if (BG.Location.X > 0)
                    BG.Location = new Point(0, 0);
                else
                {
                    SoundManager.PlayByFileName("Select2");
                    Press.Visible = false;
                    MenuShowed = true;
                    ScreenManager.AddScreen(new MainMenu(BG, this));
                }
            }
        }

        private void BG_MouseClick(Object sender, MouseEventArgs e)
        {
            KeyUp(Keys.Enter);
        }

        public override void MouseClick(MouseButtons Button)
        {
            KeyUp(Keys.Enter);
        }

        public override void Update()
        {
            if (BG.Location.X > 0)
            {
                //Trước khi tấm ảnh chạy ra
                BGMoveTimer += Game.ElapsedGameTime;
                if (BGMoveTimer >= BGMoveInterval)
                {
                    BGMoveTimer = 0;
                    BG.Location = new Point(Math.Max(BG.Location.X - 25, 0), 0);
                }
            }
            else
            {
                //Sau khi tấm ảnh chạy ra
                
                //Chữ nhấp nháy
                if(MenuShowed == false)
                {
                    TextTimer += Game.ElapsedGameTime;
                    if (TextTimer >= TextInterval)
                    {
                        TextTimer = 0;
                        Press.Visible = !Press.Visible;
                    }
                }

                //Thay các frame củ Background --> hiệu ứng lửa
                BGTimer += Game.ElapsedGameTime;
                if (BGTimer >= BGAniInterval)
                {
                    BGTimer = 0;
                    BGCurFrame += 1;
                    BG.Image = Textures.Title[BGCurFrame];
                }
            }
        }

        private static void PlayTitleTheme()
        {
            TitleTheme.Open(new Uri(Globals.GameDir + "\\Title\\TitleTheme.wav"));
            TitleTheme.Play();
        }

        public override void Unload()
        {
            BG.Dispose();
            BG = null;
            TitleTheme.Stop();
            base.Unload();
        }

        public override void Draw(Graphics g)
        {
           
        }
    }

}
