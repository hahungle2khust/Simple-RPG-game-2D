using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    abstract class BaseScreen
    {
        public string Name = "";
        public bool Active = true;  //Có chạy hàm Update của screen hiện hay ko
        public bool Show = true;    //CÓ chạy hàm Draw hay không
        public bool Focus = true;   //Có handle input hay không. (chỉ có screen cuối)
        public bool GrabFocus = true; //Khi cái screen hiện tại được tạo ra, nó có tự động bật focus hay không.
        public bool Removed = false;

        public virtual void HandleInput(char key)
        {

        }

        public virtual void HandleInput(System.Windows.Forms.Keys key)
        {

        }

        public virtual void KeyUp(System.Windows.Forms.Keys key)
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(System.Drawing.Graphics g)
        {

        }

        public virtual void Unload()
        {
            Removed = true;
        }

    }
}
