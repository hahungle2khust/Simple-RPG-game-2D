using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Game.GameWindow = this;
            Globals.Main = this;
            Icon = Properties.Resources.icon;
            Text = "BK's Knights";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            Size = new Size(Globals.resWidth, Globals.resHeight);
            FormBorderStyle = FormBorderStyle.Sizable;

            videoPlayer.uiMode = "none";

            videoPlayer.Visible = false;
            videoPlayer.PlayStateChange += videoPlayer_Finished;
            videoPlayer.settings.volume = 100;
            this.Show();
            this.Focus();
            Location = new Point((Screen.PrimaryScreen.Bounds.Width-Width)/2, (Screen.PrimaryScreen.Bounds.Height-Height)/2-20);

            Globals.Main = this;
            Game.Initialize(this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.IsRunning = false;
            BGM.BGMPlayer.controls.stop();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            ScreenManager.KeyUp(e.KeyCode);
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            //Đã chuyển sang button TO EDITOR bên MainMenuScreen
            //if(Globals.DEBUGMODE == true)
            //{
            //    Globals.Editor = new Forms.Editor();
            //    Globals.Editor.Show();
            //}
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        public void videoPlayer_Finished(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if ((WMPLib.WMPPlayState)e.newState == WMPLib.WMPPlayState.wmppsStopped)
            {
                string name = ((AxWMPLib.AxWindowsMediaPlayer)sender).currentMedia.name;
                if(name == "GameOver" || name == "Credit")
                {
                    if(Game.Tomming > 0)
                    {
                        Game.Tomming -= 1;
                        videoPlayer.Ctlcontrols.play();
                    }
                    else
                    {
                        videoPlayer.Visible = false;
                        ScreenManager.AddScreen(new TitleScreen());
                    }
                    
                }
                else if(name == "Tom")
                {
                    if(Game.Tomming > 1)
                    {
                        Game.Tomming -= 1;
                        videoPlayer.Ctlcontrols.play();
                    }
                    else
                    {
                        videoPlayer.URL = Globals.GameDir + "\\Title\\GameOver.mp4";
                        //videoPlayer.Ctlcontrols.currentPosition = 1;
                        videoPlayer.Ctlcontrols.play();
                    }
                }
                else videoPlayer.Visible = false;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            ScreenManager.MouseClick(e.Button);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ClientSize = new Size(ClientSize.Width, (int)(ClientSize.Width * Globals.resPercent));
        }
    }
}
