using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    class DummyScreen : BaseScreen
    {
        private int Counter = 0;
        private int WaitTime;

        public DummyScreen(int Time)
        {
            WaitTime = Time;
            Game.worldscreen.Active = false;
        }

        public override void Draw(Graphics g)
        {
           
        }

        public override void KeyUp(Keys key)
        {
            
        }

        public override void MouseClick(MouseButtons Button)
        {
            
        }

        public override void Update()
        {
            Counter += Game.ElapsedGameTime;
            if (Counter >= WaitTime) Unload();
        }

        public override void Unload()
        {
            Game.worldscreen.Active = true;
            if (Game.Ending) ScreenManager.AddScreen(new SceneScreen(DataHandler.LoadScreneByName("FinalScene")));
            base.Unload();
        }
    }
}
