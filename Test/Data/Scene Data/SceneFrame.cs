using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class SceneFrame
    {
        public string BGFileName = "";
        public List<CharImage> Characters = new List<CharImage>();
        public string Name = "";
        public string Text = "";
        public string Script = ""; //Action|Values|Conditions
        public int JumpTo = -1;
        public string JumpCondition = "";

        public bool IsSelection = false;
        public List<string> Selections = new List<string>();
        public List<int> JumpAfterSelect = new List<int>();

        public List<string> Sounds = new List<string>();

        public void PlaySounds()
        {
            foreach(string s in Sounds)
                SoundManager.PlayByFileName(s);
        }

        public void RunScripts()
        {
            if (Script.Length > 0)
                TriggerHandler.HandleTriggers(Script);
        }
    }
}
