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

            //Action1; Action2; Action3;...| (Value1, Value2),...; Values;...
            string[] Action = script.Split('|')[0].Split(';');
            string[] Values = script.Split('|')[1].Split(';');

            for (int i = 0 ; i <= Action.Length-1 ; i += 1)
            {
                string[] Value = Values[i].Split(',');
               
                switch (Action[i])
                {
                    case "Load Map":
                        //Tên map, vị trí X, vị trí Y.
                        //Game.Player.CurPos = new Point(0, 0);
                        MapHandler.LoadMap(Value[0]);
                        Game.Player.CurPos.X = Convert.ToInt32(Value[1]);
                        Game.Player.CurPos.Y = Convert.ToInt32(Value[2]);
                        Game.Player.OffSet = new Point(0, 0);
                        break;
                    case "Show Dialog":
                        //Text(s)
                        List<string> s = new List<string>();
                        s.AddRange(Value[0].Split('\n'));
                        
                        ScreenManager.AddScreen(new DialogScreen(s));
                        break;
                    case "Load Scene":
                        //FileName
                        ScreenManager.AddScreen(new SceneScreen(DataHandler.LoadScene(Globals.GameDir + "\\Data\\Scenes\\" + Values[0] + ".dat")));
                        break;
                    default:

                        break;
                }



            }

        }
    }
}
