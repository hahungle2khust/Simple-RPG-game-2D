using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    class TriggerHandler
    {
        public static void HandleTriggers(string script)
        {
            if (script.Length == 0) return;

            //Action|Value1, Value2, Values3,...|Conditions
            //Load Map|Map2, X, Y
            //Action1; Action2; Action3;...| Value1, Value2,...; Values;...
            string[] str = script.Split('|');
            string[] Action = str[0].Split(';');
            string[] Values = str[1].Split(';');
            string[] Conditions = { "" };
            if (str.Length > 2) Conditions = script.Split('|')[2].Split(';');

            for (int i = 0 ; i <= Action.Length-1 ; i += 1)
            {
                string[] Value = Values[i].Split(',');
                string[] Condition = Conditions[i].Split(',');

                if (CheckConditions(Condition) == false) continue;

                switch (Action[i].ToLower())
                {
                    case "load map":
                        //Tên map, vị trí X, vị trí Y
                        //Game.Player.CurPos = new Point(0, 0);
                        MapHandler.LoadMap(Value[0]);
                        Game.Player.CurPos.X = Convert.ToInt32(Value[1]);
                        Game.Player.CurPos.Y = Convert.ToInt32(Value[2]);
                        Game.Player.OffSet = new Point(0, 0);
                        break;
                    case "load scene":
                        //FileName
                        //Load Scene|1
                        ScreenManager.AddScreen(new SceneScreen(DataHandler.LoadScene(Globals.GameDir + "\\Data\\Scenes\\" + Value[0] + ".dat")));
                        break;
                    case "play sound":
                        //FileName
                        SoundManager.PlayByFileName(Value[0]);
                        break;
                    case "play bgm":
                        //FileName
                        BGM.PlayByFileName(Value[0]);
                        break;
                    case "stop bgm":
                        BGM.StopBGM();
                        break;
                    case "Pause BGM":
                        BGM.PauseBGM();
                        break;
                    case "resume bgm":
                        BGM.ResumeBGM();
                        break;
                    case "tom":
                        ScreenManager.AddScreen(new TrollScreen());
                        break;
                    default:

                        break;
                }
            }
        }

        public static bool CheckConditions(string[] conditions)
        {
            foreach(string c in conditions)
            {
                if(c.Length > 0)
                {
                    string[] str = c.Split('=');
                    if (Globals.Flags.ContainsKey(str[0]) && Globals.Flags[str[0]] != str[1])
                        return false;
                }
            }

            return true;
        }
    }
}
