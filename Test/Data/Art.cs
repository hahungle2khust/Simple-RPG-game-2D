using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    public class Art
    {
        public string SrcImage = "";
        public Rectangle SrcRect;
        public Rectangle Position;
        public Dir Angle;
        public bool AutoRemove = false;
        public bool remove = false;
        private int _CurFrame = 0;
        public int SkillCounter = 0;

        public int CurFrame
        {
            get { return _CurFrame; }
            set
            {
                if (value > Frames - 1)
                {
                    _CurFrame = 0;
                    if (AutoRemove) remove = true;
                }
                else _CurFrame = value;
            }
        }
        public int Frames = 1;
        public int FrameInterval = 0;
        public int FrameTimer = 0;

        public void Update()
        {
            FrameTimer += Game.ElapsedGameTime;
            if (FrameTimer >= FrameInterval)
            {
                FrameTimer = 0;
                CurFrame += 1;
            }
        }

        public Rectangle getSprite()
        {
            int Y;
            if (Angle == Dir.Up) Y = 3;
            else if (Angle == Dir.Left) Y = 1;
            else if (Angle == Dir.Right) Y = 2;
            else Y = 0;

            return new Rectangle(SrcRect.Width * CurFrame, SrcRect.Height * Y, SrcRect.Width, SrcRect.Height);
        }
    }
}
