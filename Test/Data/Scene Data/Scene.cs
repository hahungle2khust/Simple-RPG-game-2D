using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Test
{
    class Scene
    {
        public List<SceneFrame> Frames = new List<SceneFrame>();
        public int CurFrame = 0;

        public SceneFrame GetCurFrame
        {
            get
            {
                if (CurFrame < Frames.Count) return Frames[CurFrame];
                else return null;
            }
        }

        public static Scene LoadFromTextFile(string path)
        {
            if (!File.Exists(path)) return null;

            Scene newScene = new Scene();

            string[] str;
            using(StreamReader sr = new StreamReader(path))
            {
                str = sr.ReadToEnd().Split('$');
            }

            foreach (string line in str)
            {
                string[] part = line.Split('`');
                string[] info = part[0].Split('|');

                SceneFrame frame = new SceneFrame();
                if (part[1].Substring(0, part[1].Length - 1).Split('\\').Length > 1)
                {
                    frame.Text = part[1].Substring(0, part[1].Length - 1).Split('\\')[0];
                    frame.Text = frame.Text.Substring(0, frame.Text.Length - 1);
                }
                else frame.Text = part[1].Substring(0, part[1].Length - 1);
                if (frame.Text.Length > 0) frame.Text = frame.Text.Substring(0, part[1].Length - 1);
                frame.BGFileName = info[1];
                frame.Sounds.AddRange(info[2].Split('/'));
                if (!Int32.TryParse(info[3], out frame.JumpTo))
                    frame.JumpTo = -1;
                frame.JumpCondition = info[4];
                frame.Script = info[5];
                for(int i=0; i<info.Length; i += 1)
                {
                    if(info[i].Length > 0)
                    {
                        string[] character = info[i].Split(',');
                        CharImage ci;
                        if(character.Length < 4)
                             ci = new CharImage(character[0], new System.Drawing.Rectangle(Int32.Parse(character[1]), Int32.Parse(character[2]), 0, 0));
                        else
                             ci = new CharImage(character[0], new System.Drawing.Rectangle(Int32.Parse(character[1]), Int32.Parse(character[2]), Int32.Parse(character[3]), Int32.Parse(character[4])));
                        frame.Characters.Add(ci);
                    }
                }
                newScene.Frames.Add(frame);
            }

            return newScene;
        }
    }
}
