using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    class CharacterSelectScreen : BaseScreen
    {
        private Image Characters = Image.FromFile(Application.StartupPath + @"\Title\Characters.png");
        private Image Text = Image.FromFile(Application.StartupPath + @"\Title\Text.png");
        private Image Mask = Image.FromFile(Application.StartupPath + @"\Title\Mask.png");

        private List<int> Order = new List<int>() { 5, 3, 6, 4 };

        private int _SelectedIndex = 0;
        private int TextX = 50;
        private int TextY = 170;

        private int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (value > 3) _SelectedIndex = 0;
                else if (value < 0) _SelectedIndex = 3;
                else _SelectedIndex = value;
            }
        }

        public CharacterSelectScreen()
        {
            Name = "Character Select Screen";

            BGM.PlayByFileName("CharacterSelect");
            Game.Frame.Visible = true;

        }

        private void Character_MouseClick(object sender, MouseEventArgs e)
        {
            SoundManager.PlayByFileName("Character" + SelectedIndex.ToString() +"Selected");
            int SelectedCharIndex;
            if (e.X <= Game.Frame.Width / 4)
            {
                SelectedCharIndex = 5;
            } else if (e.X <= Game.Frame.Width / 2)
            {
                SelectedCharIndex = 3;
            } else if (e.X <= Game.Frame.Width * 3 / 4)
            {
                SelectedCharIndex = 6;
            }
            else
            {
                SelectedCharIndex = 4;
            }
            Unload();
            ScreenManager.AddScreen(new ModeSelectScreen(SelectedCharIndex));
        }

        public override void Draw(Graphics g)
        {
            if (Removed) return;

            g.DrawImage(Characters, new Rectangle(0, 0, Game.Frame.Width, Game.Frame.Height));
            int i = 0;
            foreach (int n in Order)
            {
                int spacing = 25;
                g.DrawString("HP:         " + Globals.CharTypeList[n].baseHP.ToString(), Globals.InfoFont, Brushes.Black, new Point(TextX + i * Game.Frame.Width / 4, TextY));
                g.DrawString("MP:        " + Globals.CharTypeList[n].baseMP.ToString(), Globals.InfoFont, Brushes.Black, new Point(TextX + i * Game.Frame.Width / 4, TextY + spacing * 1));
                g.DrawString("Attack:   " + Globals.CharTypeList[n].baseAtk.ToString(), Globals.InfoFont, Brushes.Black, new Point(TextX + i * Game.Frame.Width / 4, TextY + spacing * 2));
                g.DrawString("Defense: " + Globals.CharTypeList[n].baseDef.ToString(), Globals.InfoFont, Brushes.Black, new Point(TextX + i * Game.Frame.Width / 4, TextY + spacing * 3));
                g.DrawString("Speed:     " + Globals.CharTypeList[n].baseSpeed.ToString(), Globals.InfoFont, Brushes.Black, new Point(TextX + i * Game.Frame.Width / 4, TextY + spacing * 4));
                for(int j=0; j<Globals.Skills[n].Count; j += 1)
                {
                    Rectangle des = new Rectangle(TextX + i * Game.Frame.Width / 4 + j * 32, TextY + spacing * 5, 32, 32);
                    g.DrawImage(Textures.ArtImg[Globals.Skills[n][j].srcImg], des, new Rectangle(Globals.Skills[n][j].srcPos.X, Globals.Skills[n][j].srcPos.Y * 32, 32, 32), GraphicsUnit.Pixel);
                    g.DrawRectangle(Pens.Black, des);
                }
                i += 1;
            }

            for(int k=0; k<4; k += 1)
            {
                if (SelectedIndex != k)
                    g.DrawImage(Mask, new Rectangle(k * Game.Frame.Width / 4, 0, Game.Frame.Width / 2, Game.Frame.Height * 2), new Rectangle(0, 0, 1, 1), GraphicsUnit.Pixel);
            }

            g.DrawImage(Text, new Rectangle((Game.Frame.Width - Text.Width) / 2, 0, Text.Width, Text.Height));
        }

        public override void KeyUp(Keys key)
        {
            if (key == Keys.Left)
            {
                SelectedIndex -= 1;
                SoundManager.PlayByFileName("Select1");
                //Cursor.Position = new Point(Globals.Main.Location.X + SelectedIndex * Character.Width / 4 + Character.Width / 4 / 2, Globals.Main.Location.Y + Character.Height / 2);
            }
            else if (key == Keys.Right)
            {
                SelectedIndex += 1;
                SoundManager.PlayByFileName("Select1");
                //Cursor.Position = new Point(Globals.Main.Location.X + SelectedIndex * Character.Width / 4 + Character.Width / 4 / 2, Globals.Main.Location.Y + Character.Height / 2);
            }
            else if(key == Keys.Enter || key == Keys.Space)
            {
                SoundManager.PlayByFileName("Select2");
                //Character_MouseClick(null, new MouseEventArgs(MouseButtons.Left, 1, Character.PointToClient(Cursor.Position).X, Character.PointToClient(Cursor.Position).Y, 0));
                Character_MouseClick(null, new MouseEventArgs(MouseButtons.Left, 1, SelectedIndex * Game.Frame.Width / 4 + 20, Game.Frame.PointToClient(Cursor.Position).Y, 0));
            }
        }

        public override void MouseClick(MouseButtons Button)
        {
            
        }

        public override void Update()
        {
          
        }

        public override void Unload()
        {
            Characters.Dispose();
            Characters = null;
            Text.Dispose();
            Text = null;
            Mask.Dispose();
            Mask = null;
            base.Unload();
        }
    }
}
