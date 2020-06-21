using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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

        public static void HandleInput(char key)
        {
            for (int j = Screens.Count - 1; j >= 0; j -= 1)
            {
                if (Screens[j].Focus)
                {
                    Screens[j].HandleInput(key);
                    break;
                }
            }
        }

        public static void HandleInput(System.Windows.Forms.Keys key)
        {
            for (int j = Screens.Count - 1; j >= 0; j -= 1)
            {
                if (Screens[j].Focus)
                {
                    Screens[j].HandleInput(key);
                    break;
                }
            }
        }

        public static void KeyUp(System.Windows.Forms.Keys key)
        {
            for (int j = Screens.Count - 1; j >= 0; j -= 1)
            {
                if (Screens[j].Focus)
                {
                    Screens[j].KeyUp(key);
                    break;
                }
            }
        }

        public static void Update()
        {
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
                if (screen != FocusedScreen) screen.Active = false;
                if (screen.Active) screen.Update();
            }
        }

        public static void Draw(Graphics g)
        {
            foreach (BaseScreen screen in Screens)
            {
                if (screen.Show) screen.Draw(g);
            }
        }



    }
}
