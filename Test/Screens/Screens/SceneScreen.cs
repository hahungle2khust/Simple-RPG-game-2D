using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media;

namespace Test
{
    class SceneScreen : BaseScreen
    {
        private string ImageDir = Globals.GameDir + @"\Content\Textures\Scenes\";
        private Dictionary<string, Image> BGImages = new Dictionary<string, Image>();
        private Dictionary<string, Image> CharImages = new Dictionary<string, Image>();
        private Image DialogBox = Image.FromFile(Globals.GameDir + @"\Content\Textures\Dialogs\DialogBox.png");

        public Scene scene;
        private int curTextPos;
        private bool SkipText;
        private int TextTimer = 0;

        public Label Dialog;
        private Point DialogLocation = new Point(0, Game.Frame.Height * 2 / 3);
        private MediaPlayer Click = new MediaPlayer();

        public SceneScreen(Scene scene)
        {
            Name = "Scene Screen";

            Game.InScene = true;
            if(Game.worldscreen != null)
            {
                Game.worldscreen.Active = false;
                Game.worldscreen.MapName.Visible = false;
            }
            this.scene = scene;

            LoadSceneImages();

            Click.Open(new Uri(Globals.GameDir + @"\Content\SFX\Select2.wav"));

            Dialog = new Label();
            Dialog.Parent = Game.Frame;
            Dialog.BackColor = System.Drawing.Color.Transparent;
            Dialog.AutoSize = false;
            Dialog.BringToFront();
            Dialog.ForeColor = System.Drawing.Color.Lime;
            Dialog.Size = new Size(Game.Frame.Width - 10, Game.Frame.Height / 3 - 10);
            Dialog.Location = new Point(5, DialogLocation.Y + 45);
            Dialog.Padding = new Padding(10, 10, 10, 10);
            Dialog.Font = Globals.DialogFont;

            scene.CurFrame = -1;
            NextFrame();
        }

        private void LoadSceneImages()
        {
            foreach(SceneFrame f in scene.Frames)
            {
                if (f.BGFileName.Length > 0 && !BGImages.ContainsKey(f.BGFileName))
                    BGImages.Add(f.BGFileName, Image.FromFile(ImageDir + @"Backgrounds\" + f.BGFileName + ".png"));
                
                foreach(CharImage ci in f.Characters)
                {
                    if(!CharImages.ContainsKey(ci.FileName))
                        CharImages.Add(ci.FileName, Image.FromFile(ImageDir + @"Characters\" + ci.FileName + ".png"));
                }
            }
        }

        public override void HandleInputs()
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                SkipText = true;
                NextFrame();
            }
            else SkipText = false;
        }

        public override void KeyUp(Keys key)
        {
            if (key == Keys.Enter || key == Keys.Space)
            {
                Click.Stop();
                Click.Play();
                if (SkipText)
                {
                    curTextPos = scene.GetCurFrame.Text.Length - 1;
                    NextFrame();
                }
                else
                {
                    if (curTextPos < scene.GetCurFrame.Text.Length - 1)
                        curTextPos = scene.GetCurFrame.Text.Length - 1;
                    else NextFrame();
                }
            }
        }

        public override void Update()
        {
            if (Settings.TextSpeed == 0 || SkipText) curTextPos = scene.GetCurFrame.Text.Length - 1;
            if (curTextPos < scene.GetCurFrame.Text.Length - 1)
            {
                TextTimer += Game.ElapsedGameTime;
                if (TextTimer >= Settings.TextSpeed)
                {
                    TextTimer = 0;
                    int count = 1;
                    if (Settings.TextSpeed < 25) count += (25 - Settings.TextSpeed) / 2;
                    curTextPos += count;
                    if (curTextPos > scene.GetCurFrame.Text.Length - 1) curTextPos = scene.GetCurFrame.Text.Length - 1;
                }
                Dialog.Text = scene.GetCurFrame.Text.Substring(0, curTextPos) + Settings.TextCursor;
            }
            else Dialog.Text = scene.GetCurFrame.Text;
        }

        public override void Draw(Graphics g)
        {
            if (Removed) return;

            //Draw Background
            if (scene.GetCurFrame.BGFileName.Length > 0)
                g.DrawImage(BGImages[scene.GetCurFrame.BGFileName], new Rectangle(0, 0, Game.Frame.Width, Game.Frame.Height));
            
            //Draw Character
            foreach(CharImage c in scene.GetCurFrame.Characters)
            {
                if(c.Position.Width == 0 && c.Position.Height == 0)
                    g.DrawImage(CharImages[c.FileName], new Point(c.Position.X- CharImages[c.FileName].Width/2, c.Position.Y));
                else
                    g.DrawImage(CharImages[c.FileName], new Rectangle(c.Position.X - CharImages[c.FileName].Width / 2, c.Position.Y, c.Position.Width, c.Position.Height));
            }

            //Draw DialogBox
            g.DrawImage(DialogBox, new Rectangle(DialogLocation.X, DialogLocation.Y, Game.Frame.Width, Game.Frame.Height - DialogLocation.Y));

            //Draw Frame's Name
            if (scene.GetCurFrame != null && scene.GetCurFrame.Name.Length > 0)
                g.DrawString(scene.GetCurFrame.Name, Globals.NameBoxFont, System.Drawing.Brushes.Gold, new Point((Game.Frame.Width * 43 / 100 - TextRenderer.MeasureText(scene.GetCurFrame.Name, Globals.NameBoxFont).Width) / 2 - 20, DialogLocation.Y + 10));
        }

        public void NextFrame()
        {
            //Check xem có jump đến frame khác không
            //Nếu có thì check điều điện jump

            scene.CurFrame += 1;

            if(scene.CurFrame < scene.Frames.Count)
            {
                curTextPos = 0;
                scene.GetCurFrame.RunScripts();
                scene.GetCurFrame.PlaySounds();
            }
            else
            {
                Unload();
                if (Game.Ending)
                {
                    BGM.StopBGM();
                    Game.InGame = false;
                    Game.worldscreen.Unload();
                    Globals.Main.videoPlayer.Visible = true;
                    Globals.Main.videoPlayer.URL = Globals.GameDir + "\\Title\\Credit.mp4";
                    Globals.Main.videoPlayer.Ctlcontrols.play();
                }
            }
        }

        public override void Unload()
        {
            Click.Close();
            DialogBox.Dispose();
            DialogBox = null;
            foreach (var i in CharImages)
                i.Value.Dispose();
            CharImages.Clear();
            foreach (var i in BGImages)
                i.Value.Dispose();
            BGImages.Clear();
            Dialog.Dispose();
            Dialog = null;
            if (Game.worldscreen != null)
            {
                Game.worldscreen.Active = true;
                Game.worldscreen.MapName.Visible = true;
            }
            Game.InScene = false;
            base.Unload();
        }

        public override void MouseClick(MouseButtons Button)
        {
            
        }

    }
}
