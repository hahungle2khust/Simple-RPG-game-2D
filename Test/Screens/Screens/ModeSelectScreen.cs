using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    class ModeSelectScreen : BaseScreen
    {
        public PictureBox Mode;
        private int SelectedCharacterIndex;
        private List<PictureBox> Masks = new List<PictureBox>();

        private int _SelectedIndex = 1;

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


        public ModeSelectScreen(int SelectedCharacterIndex)
        {
            Name = "Mode Select Screen";

            this.SelectedCharacterIndex = SelectedCharacterIndex;

            Mode = new PictureBox();
            Mode.Parent = Game.Frame;
            Mode.Dock = DockStyle.Fill;
            Mode.SizeMode = PictureBoxSizeMode.StretchImage;
            //Mode.MouseClick += Mode_MouseClick;
            Mode.Image = Image.FromFile(Application.StartupPath + @"\Title\ModeSelect.png");

            PictureBox mask;
            for (int i = 0; i < 3; i += 1)
            {
                mask = new PictureBox();
                mask.Parent = Mode;
                if (i == SelectedIndex) mask.Visible = false;
                mask.BackColor = Color.FromArgb(180, 0, 0, 0);
                mask.Size = new Size(Mode.Width, Mode.Height / 3);
                mask.Location = new Point(0, i * mask.Height);
                //mask.MouseEnter += Mask_MouseEnter;
                Masks.Add(mask);
            }
        }

        private void Mode_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Y <= Mode.Height / 3)
            {
                Globals.Mode = HardMode.Easy;
            }
            else if (e.Y <= Mode.Height * 2 / 3)
            {
                Globals.Mode = HardMode.Normal;
            }
            else
            {
                Globals.Mode = HardMode.Hard;
            }
            Unload();
            Game.Player = new Character(SelectedCharacterIndex);
            BGM.StopBGM();
            Game.StartGame();
        }

        private void Mask_MouseEnter(object sender, EventArgs e)
        {
            SelectedIndex = Masks.IndexOf((PictureBox)sender);
        }

        public override void Unload()
        {
            Mode.Dispose();
            Mode = null;
            base.Unload();
        }

        public override void Draw(Graphics g)
        {
            g.Clear(Globals.Main.BackColor);
        }

        public override void KeyUp(Keys key)
        {
            if (key == Keys.Up)
            {
                SelectedIndex -= 1;
                SoundManager.PlayByFileName("Select1");
                //Cursor.Position = new Point(Globals.Main.Location.X + Mode.Width / 2, Globals.Main.Location.Y + 20 + Mode.Height / 3 * SelectedIndex + Mode.Height / 6);
            }
            else if (key == Keys.Down)
            {
                SelectedIndex += 1;
                SoundManager.PlayByFileName("Select1");
                //Cursor.Position = new Point(Globals.Main.Location.X + Mode.Width / 2, Globals.Main.Location.Y + 20 + Mode.Height / 3 * SelectedIndex + Mode.Height / 6);
            }
            else if(key == Keys.Enter || key == Keys.Space)
            {
                BGM.StopBGM();
                SoundManager.PlayByFileName("Select2");
                //Mode_MouseClick(null, new MouseEventArgs(MouseButtons.Left, 1, Mode.PointToClient(Cursor.Position).X, Mode.PointToClient(Cursor.Position).Y, 0));
                Mode_MouseClick(null, new MouseEventArgs(MouseButtons.Left, 1, Mode.PointToClient(Cursor.Position).X, SelectedIndex * Mode.Height / 3 + 20, 0));
            }
        }

        public override void MouseClick(MouseButtons Button)
        {
           
        }

        public override void Update()
        {
            for (int i = 0; i < 3; i += 1)
            {
                if (i == SelectedIndex) Masks[i].Visible = false;
                else Masks[i].Visible = true;
            }
        }
    }
}
