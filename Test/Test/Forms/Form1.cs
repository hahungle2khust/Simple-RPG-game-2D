using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Show();
            this.Focus();

            Globals.Main = this;
            Game.Initialize(this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.IsRunning = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ScreenManager.HandleInput(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W || e.KeyCode == Keys.S || e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                Game.MoveDir = Dir.Stand;
                Game.Player.MoveDir = Dir.Stand;
                Game.Player.AniFrame = 0;
            }
            ScreenManager.KeyUp(e.KeyCode);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Game.Scale(this);
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            if(Globals.DEBUGMODE == true)
            {
                Globals.Editor = new Forms.Editor();
                Globals.Editor.Show();
            }
            
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ScreenManager.HandleInput(e.KeyChar);

            if (e.KeyChar == 'j') 
            {
                ScreenManager.AddScreen(new SceneScreen(DataHandler.LoadScene(Globals.GameDir + "\\Data\\Scenes\\1.dat")));
                //Scene newScene = new Scene();
                //SceneFrame newFrame = new SceneFrame();
                //newFrame.BGFileName = "Meadow";
                //newFrame.Characters.Add(new CharImage("Loli", new Rectangle(108, 50, 0, 0)));
                //newFrame.Characters.Add(new CharImage("Toad", new Rectangle(660, 50, 200, 200)));
                //newFrame.Name = "Chiyo";
                //newFrame.Text = "Hello Toad";
                //newScene.Frames.Add(newFrame);
                //newFrame = new SceneFrame();
                //newFrame.BGFileName = "Meadow";
                //newFrame.Characters.Add(new CharImage("Loli", new Rectangle(424, 50, 0, 0)));
                //newFrame.Name = "Chiyo";
                //newFrame.Text = "Con cóc biến mất rồi!";
                //newScene.Frames.Add(newFrame);
                //ScreenManager.AddScreen(new SceneScreen(newScene));
            }
        }
    }
}
