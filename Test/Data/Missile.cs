using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    public class Missile
    {
        public TileLayer Source;
        public Point Size;  //pixel
        public Character Owner;
        public BaseSkill FromSkill;
        public Dir Dir;
        public double Angle;  //Góc lệch so với hướng Dir hiện tại
        public Point curPos; //raw pos
        public double Range;  //TileSize unit
        public int Speed; //pixel
        public bool remove = false;

        private int MovedDistance;

        public Missile() { }

        public Missile(TileLayer Source, int Width, int Height, Character Owner, BaseSkill FromSkill, Point StartPos, Dir Direction, int Speed, int Range, double Angle = 0)
        {
            this.Source = Source;
            this.Size = new Point(Width, Height);
            this.Owner = Owner;
            this.FromSkill = FromSkill;
            this.Dir = Direction;
            this.Angle = Angle;
            this.curPos = StartPos;
            this.Range = Range;
            this.Speed = Speed;
            MovedDistance = 0;
        }

        public Rectangle HitBox {
            get { return new Rectangle(curPos.X, curPos.Y, Size.X, Size.Y); }
        }

        public void Update()
        {
            Move();
        }

        public void Move()
        {
            int movedX = 0;
            int movedY = 0;

            switch (Dir)
            {
                case Dir.Up:
                    movedY = Speed;
                    movedX = (int)(Math.Tan(Angle)*Speed);
                    curPos.Y -= movedY;
                    curPos.X += movedX;
                    break;
                case Dir.Down:
                    movedY = Speed;
                    movedX = (int)(Math.Tan(Angle) * Speed);
                    curPos.Y += movedY;
                    curPos.X -= movedX;
                    break;
                case Dir.Left:
                    movedX = Speed;
                    movedY = (int)(Math.Tan(Angle) * Speed);
                    curPos.X -= movedX;
                    curPos.Y -= movedY;
                    break;
                case Dir.Right:
                    movedX = Speed;
                    movedY = (int)(Math.Tan(Angle) * Speed);
                    curPos.X += movedX;
                    curPos.Y += movedY;
                    break;
            }
            checkHitCharacter();
            MovedDistance += (int)Math.Sqrt(movedX * movedX + movedY * movedY);
            if (MovedDistance >= (int)(Range * Globals.TileSize))
            {
                FromSkill.MissileEndByRange(this);
                remove = true;
            }
        }

        public void checkHitCharacter()
        {
            if (remove) return;

            if (Game.Player.team != Owner.team && Game.Player.HitBox.IntersectsWith(HitBox))
            {
                FromSkill.MissileHit(this, Game.Player);
                remove = true;
            }
            foreach (Character c in Game.World.MapChar)
            {
                if (c.team != Owner.team && c.HitBox.IntersectsWith(HitBox))
                {
                    FromSkill.MissileHit(this, c);
                    remove = true;
                }
            }
            if (GamePlay.MissileHitObject(this))
            {
                FromSkill.MissileHitObject(this);
                remove = true;
            }
        }

        public Rectangle getSprite
        {
            get
            {
                switch (Dir)
                {
                    case Dir.Up:
                        return new Rectangle(Source.srcPos.X * Size.X, 3 * Size.Y, Size.X, Size.Y);
                    case Dir.Down:
                        return new Rectangle(Source.srcPos.X * Size.X, 0 * Size.Y, Size.X, Size.Y);
                    case Dir.Left:
                        return new Rectangle(Source.srcPos.X * Size.X, 1 * Size.Y, Size.X, Size.Y);
                    case Dir.Right:
                        return new Rectangle(Source.srcPos.X * Size.X, 2 * Size.Y, Size.X, Size.Y);
                }
                return new Rectangle();
            }
        }
    }
}
