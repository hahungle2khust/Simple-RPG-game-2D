using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    public partial class Character
    {
        private int tmrMove = 0;
        public Point WaitAfterAttack = new Point(1000, 2000);
        private int tmrCast = 0;
        private int tmrCastInterval;
        private bool canCast = true;
        private bool MoveAround = false;

        public void Act()
        {
            if (IsCasting) return;

            tmrCast += Game.ElapsedGameTime;
            if (tmrCast >= tmrCastInterval)
            {
                tmrCast = 0;
                canCast = true;
                MoveAround = false;
                tmrCastInterval = Globals.gen.Next(WaitAfterAttack.X, WaitAfterAttack.Y + 1);
                if (Globals.Mode == HardMode.Hard && charType.Index != 7 && charType.Index != 10) tmrCast = tmrCastInterval;
            }

            if (canCast)
            {
                Rectangle area = new Rectangle(CurRawPos.X - charType.Range * Globals.TileSize, CurRawPos.Y - charType.Range * Globals.TileSize, charType.Range * 2 * Globals.TileSize + charType.Size.X, charType.Range * 2 * Globals.TileSize + charType.Size.Y);
                List<Character> EnemiesInRange = GamePlay.GetEnemies(area, team);
                if (MoveAround) EnemiesInRange.Clear();

                if (EnemiesInRange.Count > 0)
                {
                    //Có kẻ dịch trong phạm vi
                    BaseSkill SkillCanAct = GetCanActSkill();

                    if (SkillCanAct != null)
                    {
                        //Có skill xài được --> gọi hàm Cast() của skill
                        if (canCast)
                        {
                            SkillCanAct.Cast();
                            canCast = false;
                        }
                        else if (Globals.Mode == HardMode.Easy)
                        {
                            MoveAround = true;
                            Move(GetRandomDir());
                        }
                    }
                    else
                    {
                        //TH2: không có skill xài dc --> tiến đến kẻ địch. (dùng thuật toán A*)
                        //Set target (kẻ địch) gần nhất
                        SetTarget(EnemiesInRange);

                        FindNewPathTimer += Game.ElapsedGameTime;
                        if (FindNewPathTimer >= Globals.AI_FIND_NEW_PATH_INTERVAL)
                        {
                            FindNewPathTimer = 0;
                            if (Globals.Mode == HardMode.Hard || Globals.Mode == HardMode.Normal) AIPath = PathFinding.Find(CurPos, Target.CurPos, this);
                        }
                        else
                        {
                            if (AIPath == null || AIPath.Count == 0)
                            {
                                AIPath = null;
                                MoveTo(Target.CurRawPos);
                            }
                            else
                            {
                                while (AIPath.Count > 0 && HitBox.IntersectsWith(new Rectangle(AIPath[0].X * Globals.TileSize, AIPath[0].Y * Globals.TileSize, Globals.TileSize, Globals.TileSize)))
                                {
                                    AIPath.RemoveAt(0);
                                }
                                if (AIPath.Count > 1) MoveTo(new Point(AIPath[0].X * Globals.TileSize, AIPath[0].Y * Globals.TileSize));
                                else
                                {
                                    AIPath = null;
                                    MoveTo(Target.CurRawPos);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //Không có kẻ địch trong phạm vi
                    AIPath = null;
                    Move(GetRandomDir());
                }
            }
            else
            {
                AIPath = null;
                Move(GetRandomDir());
            }
        }

        private Dir GetRandomDir()
        {
            ChangeDirTimer += Game.ElapsedGameTime;
            if(ChangeDirTimer >= Globals.CHARACTER_CHANGE_DIR_INTERVAL)
            {
                ChangeDirTimer = 0;
                Dir newDir;
                do
                {
                    if(charType.Index == 7 || charType.Index == 10)
                        newDir = (Dir)Globals.gen.Next(1, 5);
                    else
                        newDir = (Dir)Globals.gen.Next(0, 5);
                } while (newDir == MoveDir);
                return newDir;
            }

            return MoveDir;
        }

        private BaseSkill GetCanActSkill()
        {
            foreach(BaseSkill bs in Skills)
            {
                if (bs.CanUse()) return bs;
            }
            return null;
        }


        /// <summary>
        /// des is by TileSize, not Pixel.
        /// </summary>
        /// <param name="des"></param>
        private void MoveTo(Point des)
        {
            tmrMove += Game.ElapsedGameTime;
            Point distance = new Point(des.X - CurRawPos.X, des.Y - CurRawPos.Y);
            if(distance.X != 0 && distance.Y != 0)
            {
                //Điểm đến không nằm cùng hàng hay cột với vị trí hiện tại
                if(tmrMove >= Globals.AI_MOVE_CYCLE_INTERVAL)
                {
                    tmrMove = 0;
                    if (distance.X > 0)
                    {
                        //Nằm bên phải
                        if (distance.Y > 0)
                        {
                            //Nằm ở dưới bên phải của vị trí hiện tại
                            if (Globals.gen.Next(0, 101) > 50) Move(Dir.Right);
                            else Move(Dir.Down);
                        }
                        else
                        {
                            //Nằm ở trên bên phải của vị trí hiện tại
                            if (Globals.gen.Next(0, 101) > 50) Move(Dir.Right);
                            else Move(Dir.Up);
                        }
                    }
                    else
                    {
                        //Nằm bên trái
                        if (distance.Y > 0)
                        {
                            //Nằm ở dưới bên trái của vị trí hiện tại
                            if (Globals.gen.Next(0, 101) > 50) Move(Dir.Left);
                            else Move(Dir.Down);
                        }
                        else
                        {
                            //Nằm ở trên bên trái của vị trí hiện tại
                            if (Globals.gen.Next(0, 101) > 50) Move(Dir.Left);
                            else Move(Dir.Up);
                        }
                    }
                }
                else
                {
                    Move(LastDir);
                }
            }
            else if(distance.X == 0 && distance.Y != 0)
            {
                tmrMove = Globals.AI_MOVE_CYCLE_INTERVAL;
                //Cùng cột nhưng khác hàng
                if(distance.Y < 0)
                {
                    //Nằm ở trên
                    Move(Dir.Up);
                }
                else
                {
                    //Nằm ở dưới
                    Move(Dir.Down);
                }
            }
            else if(distance.X != 0 && distance.Y == 0)
            {
                tmrMove = Globals.AI_MOVE_CYCLE_INTERVAL;
                //Cùng hàng nhưng khác cột
                if (distance.X < 0)
                {
                    //Nằm bên trái
                    Move(Dir.Left);
                }
                else
                {
                    //Nằm bên phải
                    Move(Dir.Right);
                }
            }
        }

        private void SetTarget(List<Character> TargetList)
        {
            Target = TargetList.First();
            foreach(Character c in TargetList)
            {
                if (Math.Abs(CurPos.X - c.CurPos.X) + Math.Abs(CurPos.Y - c.CurPos.Y) < Math.Abs(CurPos.X - Target.CurPos.X) + Math.Abs(CurPos.Y - Target.CurPos.Y))
                {
                    Target = c;
                }
            }
        }

    }
}
