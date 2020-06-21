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

            Dialog.Parent = Globals.Main;
            Dialog.AutoSize = false;
            Dialog.BringToFront();
            Dialog.ForeColor = Color.White;
            Dialog.Location = new Point(12, 322);
            Dialog.Size = new Size(824, 146);
            Dialog.Image = Image.FromFile(Globals.GameDir + "\\Content\\Textures\\Dialogs\\WorldDialog.png");
            Dialog.Padding = new Padding(15, 15, 15, 15);
            Dialog.Font = Globals.Font;
            Dialog.Text = Texts[0];
        }

        public override void HandleInput(char key)
        {
            if (key == 'j')
            {
                CurLine += 1;
                if (CurLine > Texts.Count - 1)
                {
                    Unload();
                }
                else
                {
                    NextText();
                }
            }
        }

        public override void Update()
        {
            base.Update();
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
    }
}
