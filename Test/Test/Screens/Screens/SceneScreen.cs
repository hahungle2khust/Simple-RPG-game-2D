using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Test
{
    class SceneScreen : BaseScreen
    {
        private string BGDir = Globals.GameDir + "\\Content\\Textures\\Scenes\\Background\\";
        private string CharDir = Globals.GameDir + "\\Content\\Textures\\Scenes\\Characters\\";

        public Scene scene;
        public string FrameName = "";
        public string Text = "";

        public Label NameBox = new Label();
        public Label Dialog = new Label();
        public PictureBox PB = new PictureBox();

        public SceneScreen(Scene scene)
        {
            Game.worldscreen.Active = false;
            Game.worldscreen.Show = false;
            this.scene = scene;

            Dialog.Parent = PB;
            //Dialog.Image = Image.FromFile(Globals.GameDir + "\\Content\\Textures\\Dialogs\\WorldDialog.png");
            Dialog.BackColor = Color.FromArgb(180, 0, 0, 0);
            Dialog.AutoSize = false;
            Dialog.BringToFront();
            Dialog.ForeColor = Color.White;
            Dialog.Location = new Point(12, 322);
            Dialog.Size = new Size(824, 146);
            Dialog.Padding = new Padding(15, 15, 15, 15);
            Dialog.Font = Globals.Font;

            NameBox.Parent = PB;
            NameBox.BackColor = Color.FromArgb(180, 0, 0, 0);
            NameBox.AutoSize = false;
            NameBox.BringToFront();
            NameBox.TextAlign = ContentAlignment.MiddleCenter;
            NameBox.ForeColor = Color.White;
            NameBox.Location = new Point(5,285);
            NameBox.Size = new Size(100, 37);
            //NameBox.Image = Image.FromFile(Globals.GameDir + "\\Content\\Textures\\Dialogs\\WorldDialog.png");
            NameBox.Padding = new Padding(10,4,10,7);
            NameBox.Font = Globals.Font;

            PB.Parent = Globals.Main;
            PB.Dock = DockStyle.Fill;
            PB.SizeMode = PictureBoxSizeMode.StretchImage;

            DrawToPB();
            FrameName = scene.Frames[0].Name;
            Text = scene.Frames[0].Text;
            NameBox.Text = FrameName;
            Dialog.Text = Text;
        }

        public override void HandleInput(char key)
        {
            if(key == (char)Keys.Enter)
            {
                NextFrame();
            }
        }

        public override void HandleInput(Keys key)
        {
            //Nếu phím Ctrl được giữ --> tua nhanh
        }

        public void NextFrame()
        {
            //Check xem có jump đến frame khác không
            //Nếu có thì check điều điện jump

            scene.CurFrame += 1;

            if(scene.CurFrame <= scene.Frames.Count - 1)
            {
                DrawToPB();
                FrameName = scene.Frames[scene.CurFrame].Name;
                Text = scene.Frames[scene.CurFrame].Text;
                NameBox.Text = FrameName;
                NameBox.Size = new Size(TextRenderer.MeasureText(FrameName, NameBox.Font).Width + 10 , TextRenderer.MeasureText(FrameName, NameBox.Font).Height +10);
                Dialog.Text = Text;
            }
            else
            {
                Unload();
            }
            ///Xem có selection ko?
            //Nếu có show ra và cho chọn
        }

        public void DrawToPB()
        {
            Bitmap newBM = new Bitmap(Game.GameWindow.Width, Game.GameWindow.Height);

            Graphics g = Graphics.FromImage(newBM);

            SceneFrame frame = scene.Frames[scene.CurFrame];

            Image BG = Image.FromFile(BGDir + frame.BGFileName + ".png");
            g.DrawImage(BG, new Rectangle(0, 0, newBM.Width, newBM.Height));
            BG.Dispose();

            foreach(CharImage c in frame.Characters)
            {
                Image tmpImage = Image.FromFile(CharDir + c.FileName + ".png");
                if(c.Position.Width == 0 && c.Position.Height == 0)
                {
                    g.DrawImage(tmpImage, new Rectangle(c.Position.X- tmpImage.Width/2, c.Position.Y, tmpImage.Width, tmpImage.Height));
                }
                else
                {
                    g.DrawImage(tmpImage, new Rectangle(c.Position.X - c.Position.Width / 2, c.Position.Y, c.Position.Width, c.Position.Height));
                }
                
                tmpImage.Dispose();
            }

            //Vẽ các lựa chọn (Selection)

            if(PB.Image != null) PB.Image.Dispose();
            PB.Image = newBM;

            g.Dispose();
        }

        public override void Unload()
        {
            base.Unload();
            //NameBox.Image.Dispose();
            NameBox.Dispose();
            //Dialog.Image.Dispose();
            Dialog.Dispose();
            PB.Image.Dispose();
            PB.Dispose();

            Game.worldscreen.Active = true;
            Game.worldscreen.Show = true;
        }
    }
}
