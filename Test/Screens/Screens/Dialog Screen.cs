using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    class DialogScreen : BaseScreen
    {
        public List<string> Texts = new List<string>();

        private int CurLine = 0;

        public Label Dialog = new Label();

        public DialogScreen()
        {
            Initialize();
        }

        public DialogScreen(string Text)
        {
            Texts.Add(Text);
            Initialize();
        }

        public DialogScreen(List<string> Texts)
        {
            this.Texts.AddRange(Texts);
            Initialize();
        }

        private void Initialize()
        {
            Name = "Dialog Screen";

            Dialog.Parent = Game.Frame;
            Dialog.AutoSize = false;
            Dialog.BringToFront();
            Dialog.ForeColor = Color.White;
            Dialog.Size = new Size(Globals.Main.Width - 24, 146);
            Dialog.Location = new Point(12, Globals.Main.Height - 20 - Dialog.Size.Height);
            Dialog.Image = Image.FromFile(Globals.GameDir + "\\Content\\Textures\\Dialogs\\WorldDialog.png");
            Dialog.Padding = new Padding(10, 10, 10, 10);
            Dialog.Font = Globals.DialogFont;
            Dialog.Text = Texts[0];
        }

        public override void KeyUp(Keys key)
        {
            if (key == Keys.J)
            {
                CurLine += 1;
                if (CurLine > Texts.Count - 1)
                    Unload();
                else
                    NextText();
            }
        }

        public override void Update()
        {
            
        }

        private void NextText()
        {
            Dialog.Text = Texts[CurLine];
        }

        public override void Unload()
        {
            Removed = true;
            Dialog.Image.Dispose();
            Dialog.Dispose();
        }

        public override void MouseClick(MouseButtons Button)
        {
            
        }

        public override void Draw(Graphics g)
        {
            
        }
    }
}
