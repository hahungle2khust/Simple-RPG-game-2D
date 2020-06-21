using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    public class ASTile
    {
        public Point Position;
        public int G;
        public int H;
        public int F
        {
            get { return G + H; }
        }
        public ASTile Parent;

        public ASTile(Point Postion, ASTile Parent, Point Destination)
        {
            this.Position = Postion;
            this.H = Math.Abs(Destination.X - Postion.X) + Math.Abs(Destination.Y - Postion.Y);
            if (Parent == null) G = 0;
            else
            {
                this.Parent = Parent;
                this.G = Parent.G + 1;
            }
        }

        public void ChangeParent(ASTile newParent)
        {
            this.Parent = newParent;
            this.G = newParent.G + 1;
        }
    }
}
