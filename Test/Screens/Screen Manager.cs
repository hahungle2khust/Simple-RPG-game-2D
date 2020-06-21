using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    static class ScreenManager
    {
        public static List<BaseScreen> Screens = new List<BaseScreen>();

        public static List<BaseScreen> NewScreens = new List<BaseScreen>();

        private static BaseScreen FocusedScreen;

        public static void AddScreen(BaseScreen screen)
        {
            NewScreens.Add(screen);
        }

        public static void HandleInputs()
        {
            if (Game.EditorMode) return;
            for (int j = Screens.Count - 1; j >= 0; j -= 1)
            {
                if (Screens[j].Focus)
                {
                    Screens[j].HandleInputs();
                    break;
                }
            }
        }

        public static void KeyUp(System.Windows.Forms.Keys key)
        {
            if (Game.EditorMode) return;
            for (int j = Screens.Count - 1; j >= 0; j -= 1)
            {
                if (Screens[j].Focus)
                {
                    Screens[j].KeyUp(key);
                    break;
                }
            }
        }

        public static void MouseClick(MouseButtons Button)
        {
            for (int j = Screens.Count - 1; j >= 0; j -= 1)
            {
                if (Screens[j].Focus)
                {
                    Screens[j].MouseClick(Button);
                    break;
                }
            }
        }

        public static void Update()
        {
            //Lặp trong list screen và loại bỏ những screen nào có biến Removed = true ra khỏi list
            int i = 0;
            while (i <= Screens.Count - 1)
            {
                if (Screens[i].Removed == true)
                {
                    Screens.RemoveAt(i);
                    i -= 1;
                }
                i += 1;
            }

            foreach(BaseScreen screen in NewScreens)
            {
                Screens.Add(screen);
            }
            NewScreens.Clear();

            for (int j = Screens.Count - 1; j >= 0; j -= 1)
            {
                if (Screens[j].GrabFocus)
                {
                    Screens[j].Focus = true;
                    Screens[j].Active = true;
                    FocusedScreen = Screens[j];
                    break;
                }
            }

            foreach (BaseScreen screen in Screens)
            {
                if (screen.Active) screen.Update();
                else if (screen.Name == "World Screen")
                    ((WorldScreen)screen).MapName.Visible = false;
            }
        }

        public static void Draw(Graphics g)
        {
            foreach (BaseScreen screen in Screens)
                if (screen.Show) screen.Draw(g);
        }

        public static void Unload(string ScreenName)
        {
            for(int i=0; i<Screens.Count; i += 1)
            {
                if (Screens[i].Name.Equals(ScreenName))
                {
                    Screens[i].Unload();
                    return;
                }
            }
        }

    }
}
