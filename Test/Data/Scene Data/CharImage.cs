using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    class CharImage
    {
        public string FileName = "";
        public Rectangle Position;

        public CharImage(string FileName, Rectangle Position)
        {
            this.FileName = FileName;
            this.Position = Position;
        }
    }
}
