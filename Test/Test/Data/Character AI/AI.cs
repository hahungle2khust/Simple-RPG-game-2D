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
        public void Act()
        {
            if (IsCasting) return;

            Rectangle area = new Rectangle(CurRawPos.X - charType.Range * Globals.TileSize, CurRawPos.Y - charType.Range * Globals.TileSize, charType.Range * 2 * Globals.TileSize + charType.Size.X, charType.Range * 2 * Globals.TileSize + charType.Size.Y);
            
            List<Character> EnemiesInRange = GamePlay.GetEnemies(area, team);

            if (EnemiesInRange.Count > 0)
            {
                //Có kẻ dịch trong phạm vi
                BaseSkill SkillCanAct = GetCanActSkill();

                if(SkillCanAct != null)
                {
                    //Có skill xài được --> gọi hàm Cast() của skill
                    SkillCanAct.Cast();
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
                        if(Globals.Mode == HardMode.Hard) AIPath = PathFinding.Find(CurPos, Target.CurPos, this);
                    }
                    else
                    {
                        if (AIPath == null || AIPath.Count == 0)
                        {
                            AIPath = null;
                            MoveTo(Target.CurPos);
                        }
                        else
                        {
                            MoveTo(AIPath[0]);
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

        private Dir GetRandomDir()
        {
            ChangeDirTimer += Game.ElapsedGameTime;
            if(ChangeDirTimer >= Globals.CHARACTER_CHANGE_DIR_INTERVAL)
            {
                ChangeDirTimer = 0;
                Dir newDir;
                do
                {
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
            Point distance = new Point(des.X - CurPos.X, des.Y - CurPos.Y);
            if(distance.X != 0 && distance.Y != 0)
            {
                //Điểm đến không nằm cùng hàng hay cột với vị trí hiện tại
                if(distance.X > 0)
                {
                    //Nằm bên phải
                    if(distance.Y > 0)
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
            else if(distance.X == 0 && distance.Y != 0)
            {
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
                //Cùng hàn nhưng khác cột
                if(distance.X < 0)
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
