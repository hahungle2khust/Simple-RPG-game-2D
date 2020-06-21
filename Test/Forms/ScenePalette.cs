using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.Forms
{
    public partial class ScenePalette : Form
    {
        private Scene scene;
        private SceneFrame SelectedFrame;

        public ScenePalette()
        {
            InitializeComponent();
        }

        private void ScenePalette_Load(object sender, EventArgs e)
        {
            this.Location = Globals.Editor.Location;
            //Thêm nút Edit cho CharImage và FrameList
        }

        private void RefreshFrameList()
        {
            lbFrames.Items.Clear();
            for(int i=0; i <= scene.Frames.Count - 1; i += 1)
            {
                lbFrames.Items.Add("[" + i.ToString() + "] " + scene.Frames[i].Text);
            }
        }

        private void RefreshCharList()
        {
            lbCharacters.Items.Clear();
            foreach(CharImage c in SelectedFrame.Characters)
            {
                lbCharacters.Items.Add(c.FileName + " at " + c.Position.X.ToString() + ":" + c.Position.Y.ToString() + " size " + c.Position.Width.ToString() + ":" + c.Position.Height.ToString());
            }
        }

        private void btnAddFrame_Click(object sender, EventArgs e)
        {
            if (scene == null) scene = new Scene();

            SceneFrame newFrame = new SceneFrame();
            newFrame.BGFileName = tbxBGFile.Text;
            newFrame.Script = tbxScript.Text;
            newFrame.Name = tbxName.Text;
            newFrame.Text = tbxText.Text;
            if(tbxSounds.Text.Length > 0) newFrame.Sounds.AddRange(tbxSounds.Text.Split(','));
            scene.Frames.Add(newFrame);
            //Tạo thêm các control cho các properties của class SceneFrame.
            RefreshFrameList();
        }

        private void btnDelFrame_Click(object sender, EventArgs e)
        {
            if (lbFrames.SelectedIndex > -1)
            {
                scene.Frames.RemoveAt(lbFrames.SelectedIndex);
                RefreshFrameList();
            }
        }

        private void lbFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbFrames.SelectedIndex > -1)
            {
                SelectedFrame = scene.Frames[lbFrames.SelectedIndex];
                tbxBGFile.Text = SelectedFrame.BGFileName;
                tbxScript.Text = SelectedFrame.Script;
                tbxSounds.Text = "";
                foreach(string s in SelectedFrame.Sounds)
                {
                    if(SelectedFrame.Sounds.IndexOf(s) != SelectedFrame.Sounds.Count-1)
                        tbxSounds.Text += s + ",";
                    else tbxSounds.Text += s;
                }
                tbxName.Text = SelectedFrame.Name;
                tbxText.Text = SelectedFrame.Text;
                RefreshCharList();
            }
        }


        private void btnAddChar_Click(object sender, EventArgs e)
        {
            SelectedFrame.Characters.Add(new CharImage(tbxCharFile.Text,new Rectangle((int)nudX.Value,(int)nudY.Value,(int)nudWidth.Value,(int)nudHeight.Value)));
            RefreshCharList();
        }

        private void btnDelChar_Click(object sender, EventArgs e)
        {
            if(lbCharacters.SelectedIndex > -1)
            {
                SelectedFrame.Characters.RemoveAt(lbCharacters.SelectedIndex);
                RefreshCharList();
            }
        }

        private void btnSaveScene_Click(object sender, EventArgs e)
        {
            if (scene != null && sfdSave.ShowDialog() == DialogResult.OK)
            {
                DataHandler.SaveScene(scene, sfdSave.FileName);
            }
        }

        private void btnLoadScene_Click(object sender, EventArgs e)
        {
            if(ofdLoad.ShowDialog() == DialogResult.OK)
            {
                scene = DataHandler.LoadScene(ofdLoad.FileName);
                RefreshFrameList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(lbFrames.SelectedIndex > -1)
            {
                if (scene == null) scene = new Scene();

                SceneFrame newFrame = new SceneFrame();
                newFrame.BGFileName = tbxBGFile.Text;
                newFrame.Script = tbxScript.Text;
                newFrame.Name = tbxName.Text;
                newFrame.Text = tbxText.Text;
                if (tbxSounds.Text.Length > 0) newFrame.Sounds.AddRange(tbxSounds.Text.Split(','));
                scene.Frames.Insert(lbFrames.SelectedIndex, newFrame);
                //Tạo thêm các control cho các properties của class SceneFrame.
                RefreshFrameList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(lbFrames.SelectedIndex > -1)
            {
                SceneFrame newFrame = scene.Frames[lbFrames.SelectedIndex];
                newFrame.BGFileName = tbxBGFile.Text;
                newFrame.Script = tbxScript.Text;
                newFrame.Name = tbxName.Text;
                newFrame.Text = tbxText.Text;
                newFrame.Sounds.Clear();
                if (tbxSounds.Text.Length > 0) newFrame.Sounds.AddRange(tbxSounds.Text.Split(','));
                //Tạo thêm các control cho các properties của class SceneFrame.
                RefreshFrameList();
            }
        }
    }
}
