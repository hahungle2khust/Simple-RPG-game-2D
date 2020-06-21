using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Test
{
    class MainMenu : BaseScreen
    {
        public List<Button> Buttons = new List<Button>();
        private Font bFont = new Font("Times New Roman", 25);
        private TitleScreen Title;
        private PictureBox Mask;
        private const int SPACING = 30;

        private int _SelectedIndex;
        private int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (value > Buttons.Count - 1) _SelectedIndex = 0;
                else if (value < 0) _SelectedIndex = Buttons.Count - 1;
                else _SelectedIndex = value;
            }
        }

        public MainMenu(PictureBox BG, TitleScreen Title)
        {
            Name = "Main Menu";
            this.Title = Title;

            Button b;
            Buttons.Add(new Button());
            b = Buttons.Last();
            b.Text = "New Game";
            b.FlatStyle = FlatStyle.Flat;
            b.Font = bFont;
            b.Parent = BG;
            b.Size = new Size(275, 86);
            //b.Image = GetButtonImage(b, Globals.GameDir + "\\Title.jpg");
            b.Location = new Point(BG.Width / 2 - b.Width / 2, BG.Height/2 - (b.Height*4+SPACING*3)/2);
            b.Click += NewGame_Click;

            Buttons.Add(new Button());
            b = Buttons.Last();
            b.Text = "Load Game";
            b.Font = bFont;
            b.Parent = BG;
            b.FlatStyle = FlatStyle.Flat;
            b.Size = new Size(275, 86);
            //b.Image = GetButtonImage(b, Globals.GameDir + "\\Title.jpg");
            b.Location = new Point(BG.Width / 2 - b.Width / 2, BG.Height / 2 - (b.Height * 4 + SPACING * 3) / 2 + 1 * (b.Height + SPACING));
            b.Click += LoadGame_Click;

            Buttons.Add(new Button());
            b = Buttons.Last();
            b.Text = "About";
            b.FlatStyle = FlatStyle.Flat;
            b.Font = bFont;
            b.Parent = BG;
            b.Size = new Size(275, 86);
            //b.Image = GetButtonImage(b, Globals.GameDir + "\\Title.jpg");
            b.Location = new Point(BG.Width / 2 - b.Width / 2, BG.Height / 2 - (b.Height * 4 + SPACING * 3) / 2 + 2 * (b.Height + SPACING));
            b.Click += About_Click;

            Buttons.Add(new Button());
            b = Buttons.Last();
            b.Text = "Exit";
            b.FlatStyle = FlatStyle.Flat;
            b.Font = bFont;
            b.Parent = BG;
            b.Size = new Size(275, 86);
            //b.Image = GetButtonImage(b, Globals.GameDir + "\\Title.jpg");
            b.Location = new Point(BG.Width / 2 - b.Width / 2, BG.Height / 2 - (b.Height * 4 + SPACING * 3) / 2 + 3 * (b.Height + SPACING));
            b.Click += Close_Click;

            SelectedIndex = 0;

            if(Globals.DEBUGMODE == true)
            {
                Buttons.Add(new Button());
                b = Buttons.Last();
                b.Text = "TO EDITOR";
                b.FlatStyle = FlatStyle.Flat;
                b.Font = bFont;
                b.Parent = BG;
                b.Size = new Size(275, 86);
                //b.Image = GetButtonImage(b, Globals.GameDir + "\\Title.jpg");
                b.Location = new Point(BG.Width -b.Width - 10, BG.Height - b.Height - 10);
                b.Click += ToEditor_Click;
            }

            foreach (Button bu in Buttons)
            {
                bu.TabStop = false;
                bu.FlatAppearance.BorderSize = 1;
                bu.MouseMove += MouseEnter;
            }

            Mask = new PictureBox();
            Mask.SendToBack();
            Mask.Parent = BG;
            Mask.Location = new Point(Buttons.First().Location.X - 50, Buttons.First().Location.Y - 30);
            Mask.Size = new Size(Buttons.First().Width + 100, (b.Height * 4 + SPACING * 3) + 60);
            Mask.BackColor = Color.FromArgb( 150, 0, 0, 0);
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            Unload();
            ScreenManager.AddScreen(new CharacterSelectScreen());
        }

        private void LoadGame_Click(object sender, EventArgs e)
        {
            Unload();
            ScreenManager.AddScreen(new SaveLoadScreen("load"));
            //SaveHandler.LoadGame(Globals.GameDir + @"\Save\Save.sav");
        }

        private void About_Click(object sender, EventArgs e)
        {
            DialogResult rsMB = MessageBox.Show("***********BK's Knight**********\n"+"Versoin 0.0.1\n" + 
                "Created by: OOP Team\n" + "Team Leader: Tuấn Đạt\n" + "Game engine: Duy Khai\n" 
                + "Graphic design and visual effects: Hà Hưng\n" + "Story by: Quang Hưng \n" + "\n********************\n"
                + "Contact with us?\n", "Infomations About Us\n", MessageBoxButtons.OKCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
            if (rsMB == DialogResult.OK)
            {
                // truy cap link fb
                Process.Start(@"https://www.facebook.com/shinya.Ft");
                //Process.Start("cmd"); - mo bat cu thu gi
            }
            EnterKeyUp = true;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Game.IsRunning = false;
            Globals.Main.Close();
        }

        private void MouseEnter(object sender, EventArgs e)
        {
            SelectedIndex = Buttons.IndexOf((Button)sender);
        }

        private void ToEditor_Click(object sender, EventArgs e)
        {
            Unload();
            Game.StartGame(true);
            Globals.Editor = new Forms.Editor();
            Globals.Editor.Show();
        }

        private Bitmap GetButtonImage(Button button, string Imagepath)
        {
            Bitmap newBM = new Bitmap(button.Size.Width, button.Size.Height);

            using (Graphics g = Graphics.FromImage(newBM))
            {
                using (Image i = Image.FromFile(Imagepath))
                {
                    g.DrawImage(i, new Rectangle(0, 0, newBM.Width, newBM.Height));
                }
            }

            return newBM;
        }

        public override void Unload()
        {
            foreach (Button b in Buttons)
                b.Dispose();
            Buttons.Clear();
            Mask.Dispose();
            base.Unload();
            Title.Unload();
        }

        private bool EnterKeyUp = false;
        public override void KeyUp(Keys key)
        {
            if(key == Keys.Up)
            {
                SelectedIndex -= 1;
                SoundManager.PlayByFileName("Select1");
                //Cursor.Position = new Point(Globals.Main.Location.X + Buttons[SelectedIndex].Location.X + Buttons[SelectedIndex].Width / 2, Globals.Main.Location.Y + 20 + Buttons[SelectedIndex].Location.Y + Buttons[SelectedIndex].Height / 2);
            }
            else if(key == Keys.Down)
            {
                SelectedIndex += 1;
                SoundManager.PlayByFileName("Select1");
                //Cursor.Position = new Point(Globals.Main.Location.X + Buttons[SelectedIndex].Location.X + Buttons[SelectedIndex].Width / 2, Globals.Main.Location.Y + 20 + Buttons[SelectedIndex].Location.Y + Buttons[SelectedIndex].Height / 2);
            }
            else if(key == Keys.Enter || key == Keys.Space)
            {
                if(key == Keys.Enter && EnterKeyUp)
                {
                    EnterKeyUp = false;
                    return;
                }

                SoundManager.PlayByFileName("Select2");
                if (SelectedIndex == 0)
                    NewGame_Click(null, null);
                else if (SelectedIndex == 1)
                    LoadGame_Click(null, null);
                else if (SelectedIndex == 2)
                    About_Click(null, null);
                else if (SelectedIndex == 3)
                    Close_Click(null, null);
            }
        }

        public override void MouseClick(MouseButtons Button)
        {
           
        }

        public override void Update()
        {
            //Mask.Focus();
            for(int i=0; i<Buttons.Count; i += 1)
            {
                if (SelectedIndex == i)
                {
                    Buttons[i].BackColor = Color.FromArgb(200, 255, 215, 0);
                    Buttons[i].ForeColor = Color.Black;
                    Buttons[i].FlatAppearance.BorderColor = Color.White;
                }
                else
                {
                    Buttons[i].BackColor = Color.FromArgb(230, 0, 0, 0);
                    Buttons[i].ForeColor = Color.White;
                    Buttons[i].FlatAppearance.BorderColor = Color.White;
                }
            }
        }

        public override void Draw(Graphics g)
        {
          
        }
    }
}
