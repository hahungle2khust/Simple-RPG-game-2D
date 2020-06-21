using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    public partial class Character
    {
        public string Name = "";

        //public Point Size = new Point(Globals.TileSize, Globals.TileSize); //Pixel
        //public TileLayer Source;
        public CharType charType = new CharType();

        #region Character's Properties
        private int _HP;     //Lượng máu hiện tại
        public int HP
        {
            get { return _HP; }
            set
            {
                if (value < 0) _HP = 0;
                else if (value > _HPMax) _HP = _HPMax;
                else _HP = value;
            }
        }
        private int _HPMax = 100;  //Lượng máu tối đa của char đó
        public int HPMax
        {
            get { return _HPMax; }
            set
            {
                if (value <= 0)
                    _HPMax = 1;
                else
                    _HPMax = value;
            }
        }
        public int _MP;
        public int MP
        {
            get { return _MP; }
            set
            {
                if (value < 0) _MP = 0;
                else if (value > MPMax) _MP = MPMax;
                else _MP = value;
            }
        }
        public int MPMax = 50;
        public int ATK;
        public int DEF;
        public int SATK;
        public int SDEF;
        public int Speed = 8;

        public int Level = 1;
        public long curEXP = 0;
        public long nextEXP = 2000;
        public long previousEXP = 0;
        public Team team;

        public bool[] CurStatus = new bool[(int)Status.Count];

        #endregion

        public Dir MoveDir;
        public Dir LastDir;
        public int FrameNum = 2;
        private int _AniFrame;
        public int AniFrame
        {
            get { return _AniFrame; }
            set
            {
                if (value > FrameNum) _AniFrame = 0;
                else _AniFrame = value;
            }
        }
        public int AniTime;  //millisecond
        public int ChangeDirTimer = 0;
        public int FindNewPathTimer = -1;
        public List<Point> AIPath;
        public bool IsCasting = false;
        
        public Point CurPos = new Point(13, 17);  //Theo TileSize
        public Point OffSet;  //Theo Pixe; ==> tọa độ raw = CurPos*TileSize+Offset

        public Point CurRawPos
        {
            get
            {
                return new Point(CurPos.X * Globals.TileSize + OffSet.X, CurPos.Y * Globals.TileSize + OffSet.Y);
            }
        }
        public Rectangle HitBox
        {
            get { return new Rectangle(CurRawPos.X, CurRawPos.Y, charType.Size.X, charType.Size.Y); }
        }

        public List<BaseSkill> Skills = new List<BaseSkill>();

        public Character Target;

        /// <summary>
        /// Constructor này chỉ dùng tạo Player khi mở editor mode.
        /// Khi tạo object của class Character thì dùng overload còn lại, không dùng cái này.
        /// </summary>
        public Character()
        {
            charType.Source = new TileLayer("Cursor", 0, 0);
            HP = HPMax;
            MP = MPMax;
        }

        public Character(int CharTypeIndex)
        {
            LastDir = Dir.Down;
            charType = Globals.CharTypeList[CharTypeIndex];

            //Set các giá stat của nhân vật
            if (charType.Index >= 3 && charType.Index <= 6)
                HPMax = (charType.baseHP + charType.luHP * (Level - 1));
            else if(charType.Index != 8 && charType.Index != 9)
                HPMax = (charType.baseHP + charType.luHP * (Level - 1)) * (int)Globals.Mode / 100;
            MPMax = charType.baseMP + charType.luMP * (Level - 1);
            ATK = charType.baseAtk + charType.luAtk * (Level - 1);
            SATK = charType.baseSAtk + charType.luSAtk * (Level - 1);
            DEF = charType.baseDef + charType.luDef * (Level - 1);
            SDEF = charType.baseSDef + charType.luSDef * (Level - 1);
            Speed = charType.baseSpeed + charType.luSpeed * (Level - 1);
            HP = HPMax;
            MP = MPMax;

            if(charType.Index == 7 || charType.Index == 10)
            {
                WaitAfterAttack = new Point(0, 5001);
            }
        }

        public void Move(Dir dir)
        {
            MoveDir = dir;
            LastDir = MoveDir;
            Move();
        }

        public void Move()
        {
            if (IsCasting || GamePlay.Collided(this, Speed, MoveDir)) return;

            if (MoveDir == Dir.Up)
            {
                OffSet.Y -= Speed;
                if (OffSet.Y <= -Globals.TileSize)
                {
                    CurPos.Y -= 1;
                    OffSet.Y = 0;
                    if(this == Game.Player && Game.World.TileList[CurPos.X, CurPos.Y].StepTrigger) TriggerHandler.HandleTriggers(Game.World.TileList[CurPos.X, CurPos.Y].Script);
                }
            }
            else if (MoveDir == Dir.Down)
            {
                OffSet.Y += Speed;
                if (OffSet.Y >= Globals.TileSize)
                {
                    CurPos.Y += 1;
                    OffSet.Y = 0;
                    if (this == Game.Player && Game.World.TileList[CurPos.X, CurPos.Y].StepTrigger) TriggerHandler.HandleTriggers(Game.World.TileList[CurPos.X, CurPos.Y].Script);
                }
            }
            else if (MoveDir == Dir.Left)
            {
                OffSet.X -= Speed;
                if (OffSet.X <= -Globals.TileSize)
                {
                    CurPos.X -= 1;
                    OffSet.X = 0;
                    if (this == Game.Player && Game.World.TileList[CurPos.X, CurPos.Y].StepTrigger) TriggerHandler.HandleTriggers(Game.World.TileList[CurPos.X, CurPos.Y].Script);
                }
            }
            else if (MoveDir == Dir.Right)
            {
                OffSet.X += Speed;
                if (OffSet.X >= Globals.TileSize)
                {
                    CurPos.X += 1;
                    OffSet.X = 0;
                    if (this == Game.Player && Game.World.TileList[CurPos.X, CurPos.Y].StepTrigger) TriggerHandler.HandleTriggers(Game.World.TileList[CurPos.X, CurPos.Y].Script);
                }
            }
        }

        public void SetPos()
        {
            if (OffSet.X >= Globals.TileSize)
            {
                CurPos.X += 1;
                OffSet.X = 0;
            }
            if (OffSet.X <= -Globals.TileSize)
            {
                CurPos.X -= 1;
                OffSet.X = 0;
            }
            if (OffSet.Y >= Globals.TileSize)
            {
                CurPos.Y += 1;
                OffSet.Y = 0;
            }
            if (OffSet.Y <= -Globals.TileSize)
            {
                CurPos.Y -= 1;
                OffSet.Y = 0;
            }
            if(this == Game.Player)
                Game.SetScreenByPlayerPos();
        }

        public void ResetPos()
        {
            while(GamePlay.Collided(this, 0, LastDir))
            {
                int rand = Globals.gen.Next(0, 5);
                if (rand == 0) OffSet.X += 1;
                else if (rand == 1) OffSet.X -= 1;
                else if (rand == 2) OffSet.Y += 1;
                else OffSet.Y -= 1;
                SetPos();
            }
            Game.SetScreenByPlayerPos();
        }

        private bool CheckCollision()
        {
            Point Des = CurPos;
            if(MoveDir == Dir.Up)
            {
                Des = new Point( (CurRawPos.X - 0) / Globals.TileSize, (CurRawPos.Y - 0 - Speed) / Globals.TileSize);
            }else if (MoveDir == Dir.Down)
            {
                Des = new Point((CurRawPos.X - 0) / Globals.TileSize, (CurRawPos.Y - 0 + Speed) / Globals.TileSize);
            }else if (MoveDir == Dir.Left)
            {
                Des = new Point((CurRawPos.X - 0 - Speed) / Globals.TileSize, (CurRawPos.Y - 0) / Globals.TileSize);
            }
            else if (MoveDir == Dir.Right)
            {
                Des = new Point((CurRawPos.X - 0 + Speed) / Globals.TileSize, (CurRawPos.Y - 0) / Globals.TileSize);
            }

            if (Des.X < 0 || Des.X >= Game.World.Size.X || Des.Y < 0 || Des.Y >= Game.World.Size.Y) return true; 

            if (Game.World.TileList[Des.X, Des.Y].IsBlocked == true) { return true; }

            //Kiểm tra xem có bị chặn bởi nhân vật khác ko?

            return false;
        }
    }
}
