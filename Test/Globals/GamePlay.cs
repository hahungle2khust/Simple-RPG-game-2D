using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    public static class GamePlay
    {
        public static int DamageCalculation(Character attacker, Character target, BaseSkill skill)
        {
            //chạy code tính damage
            int damage;

            if(skill.Type == SkillType.Normal)
                damage = attacker.ATK * skill.Power / (100 + target.DEF) * Globals.gen.Next(80, 121) / 100;
            else
                damage = attacker.SATK * skill.Power / (100 + target.SDEF) * Globals.gen.Next(80, 121) / 100;

            if (target == Game.Player && damage < target.HPMax / 40)
                damage = Globals.gen.Next(target.HPMax / 100, target.HPMax / 40);

            if (attacker.team == Team.Enemy && target == Game.Player)
                damage = damage * (int)Globals.Mode / 100;

            return damage;
        }

        public static bool CheckHit(Character attacker, Character target, BaseSkill skill)
        {
            if (Globals.gen.Next(0, 100) <= skill.Accuracy) return true;
            else return false;
        }

        public static void InfictDamage(Character attacker, Character target, int Amount)
        {
            if (target.HP == 0) return;

            target.HP -= Amount;

            //Check death
            if (target.HP == 0)
                EventManager.OnCharacterDies(new CharacterDies(attacker, target));   
        }

        public static List<Character> GetEnemies(Rectangle area, Team team)
        {
            List<Character> result = new List<Character>();

            foreach(Character c in Game.World.MapChar)
            {
                Rectangle rect = new Rectangle(c.CurRawPos.X, c.CurRawPos.Y, c.charType.Size.X, c.charType.Size.Y);
                if (team != c.team && area.IntersectsWith(rect)) result.Add(c);
            }
            Rectangle Prect = new Rectangle(Game.Player.CurRawPos.X, Game.Player.CurRawPos.Y, Game.Player.charType.Size.X, Game.Player.charType.Size.Y);
            if (team != Game.Player.team && area.IntersectsWith(Prect)) result.Add(Game.Player);

            return result;
        }

        public static bool IsBlocked(Character chr, int Speed, Dir direction = Dir.Stand) {
            Rectangle chrBound;
            int tileX;
            int tileY;
            Rectangle tileBound;
            int chrposX = chr.CurRawPos.X;
            int chrposY = chr.CurRawPos.Y;

            switch (direction)
            {
                case Dir.Up:
                    chrBound = new Rectangle(chrposX, chrposY - Speed, chr.charType.Size.X, chr.charType.Size.Y);
                    tileY = chrBound.Y / Globals.TileSize;
                    for (int i = chrBound.X / Globals.TileSize; i <= (chrBound.X + chrBound.Width) / Globals.TileSize; i+=1)
                    {
                        if (tileY < Game.World.TileList.GetLength(1) && i < Game.World.TileList.GetLength(0))
                        {
                            tileX = i;
                            if (Game.World.TileList[tileX, tileY].IsBlocked)
                            {
                                tileBound = new Rectangle(tileX * Globals.TileSize + 4, tileY * Globals.TileSize + 4, Globals.TileSize - 8, Globals.TileSize - 14);
                                if (chrBound.IntersectsWith(tileBound)) return true;
                            }
                        }
                        else return true;
                    }
                    break;
                case Dir.Down:
                    chrBound = new Rectangle(chrposX, chrposY + Speed, chr.charType.Size.X, chr.charType.Size.Y);
                    tileY = (chrBound.Y + chrBound.Height) / Globals.TileSize;
                    for(int i= chrBound.X / Globals.TileSize; i<= (chrBound.X + chrBound.Width) / Globals.TileSize; i += 1)
                    {
                        if (tileY < Game.World.TileList.GetLength(1) && i < Game.World.TileList.GetLength(0))
                        {
                            tileX = i;
                            if (Game.World.TileList[tileX, tileY].IsBlocked)
                            {
                                tileBound = new Rectangle(tileX * Globals.TileSize + 4, tileY * Globals.TileSize + 4, Globals.TileSize - 8, Globals.TileSize - 14);
                                if (chrBound.IntersectsWith(tileBound)) return true;
                            }
                        }
                        else return true;
                    }
                    break;
                case Dir.Left:
                    chrBound = new Rectangle(chrposX - Speed, chrposY, chr.charType.Size.X, chr.charType.Size.Y);
                    tileX = chrBound.X / Globals.TileSize;
                    for(int i = chrBound.Y / Globals.TileSize; i<= (chrBound.Y + chrBound.Height) / Globals.TileSize; i += 1)
                    {
                        if (tileX < Game.World.TileList.GetLength(0) && i < Game.World.TileList.GetLength(1))
                        {
                            tileY = i;
                            if (Game.World.TileList[tileX, tileY].IsBlocked)
                            {
                                tileBound = new Rectangle(tileX * Globals.TileSize + 4, tileY * Globals.TileSize + 4, Globals.TileSize - 8, Globals.TileSize - 14);
                                if (chrBound.IntersectsWith(tileBound)) return true;
                            }
                        }
                        else return true;
                    }
                    break;
                case Dir.Right:
                    chrBound = new Rectangle(chrposX + Speed, chrposY, chr.charType.Size.X, chr.charType.Size.Y);
                    tileX = (chrBound.X + chrBound.Width) / Globals.TileSize;
                    for(int i= chrBound.Y / Globals.TileSize; i <= (chrBound.Y + chrBound.Height) / Globals.TileSize; i += 1)
                    {
                        if (tileX < Game.World.TileList.GetLength(0) && i < Game.World.TileList.GetLength(1))
                        {
                            tileY = i;
                            if (Game.World.TileList[tileX, tileY].IsBlocked)
                            {
                                tileBound = new Rectangle(tileX * Globals.TileSize + 4, tileY * Globals.TileSize + 4, Globals.TileSize - 8, Globals.TileSize - 14);
                                if (chrBound.IntersectsWith(tileBound)) return true;
                            }
                        }
                        else return true;
                    }
                    break;
            }

            return false;
        }

        public static bool HasCharInFront(Character chr, int MoveSpeed, Dir direction = Dir.Stand)
        {
            int chrposX = chr.CurRawPos.X;
            int chrposY = chr.CurRawPos.Y;
            Rectangle chrBound = new Rectangle();
            Rectangle cBound;

            switch (direction)
            {
                case Dir.Up:
                    chrBound = new Rectangle(chrposX, chrposY - MoveSpeed, chr.charType.Size.X, chr.charType.Size.Y);
                    break;
                case Dir.Down:
                    chrBound = new Rectangle(chrposX, chrposY + MoveSpeed, chr.charType.Size.X, chr.charType.Size.Y);
                    break;
                case Dir.Left:
                    chrBound = new Rectangle(chrposX - MoveSpeed, chrposY, chr.charType.Size.X, chr.charType.Size.Y);
                    break;
                case Dir.Right:
                    chrBound = new Rectangle(chrposX + MoveSpeed, chrposY, chr.charType.Size.X, chr.charType.Size.Y);
                    break;
            }
            foreach(Character c in Game.World.MapChar)
            {
                if(c != chr)
                {
                    cBound = new Rectangle(c.CurRawPos.X + 6, c.CurRawPos.Y + 12, c.charType.Size.X - 12, c.charType.Size.Y - 24);
                    if (chrBound.IntersectsWith(cBound)) return true;
                }
            }
            if(chr != Game.Player)
            {
                cBound = new Rectangle(Game.Player.CurRawPos.X+ 6, Game.Player.CurRawPos.Y + 12, Game.Player.charType.Size.X - 12, Game.Player.charType.Size.Y - 24);
                if (chrBound.IntersectsWith(cBound)) return true;
            }

            return false;
        }

        public static bool Collided(Character chr, int Speed, Dir direction = Dir.Stand)
        {
            if (direction == Dir.Stand) return true;
            else
            {
                if (IsBlocked(chr, Speed, direction) || HasCharInFront(chr, Speed, direction)) return true;
                else return false;
            }
        }

        public static bool MissileHitObject(Missile missile)
        {
            int tileX;
            int tileY;
            Rectangle tileBound;

            switch (missile.Dir)
            {
                case Dir.Up:
                    tileY = missile.HitBox.Y / Globals.TileSize;
                    for (int i = missile.HitBox.X / Globals.TileSize; i <= (missile.HitBox.X + missile.HitBox.Width) / Globals.TileSize; i += 1)
                    {
                        if (tileY < Game.World.TileList.GetLength(1) && i < Game.World.TileList.GetLength(0))
                        {
                            tileX = i;
                            if (Game.World.TileList[tileX, tileY].IsBlocked)
                            {
                                tileBound = new Rectangle(tileX * Globals.TileSize + 4, tileY * Globals.TileSize + 4, Globals.TileSize - 8, Globals.TileSize - 14);
                                if (missile.HitBox.IntersectsWith(tileBound)) return true;
                            }
                        }
                    }
                    break;
                case Dir.Down:
                    tileY = (missile.HitBox.Y + missile.HitBox.Height) / Globals.TileSize;
                    for (int i = missile.HitBox.X / Globals.TileSize; i <= (missile.HitBox.X + missile.HitBox.Width) / Globals.TileSize; i += 1)
                    {
                        if (tileY < Game.World.TileList.GetLength(1) && i < Game.World.TileList.GetLength(0))
                        {
                            tileX = i;
                            if (Game.World.TileList[tileX, tileY].IsBlocked)
                            {
                                tileBound = new Rectangle(tileX * Globals.TileSize + 4, tileY * Globals.TileSize + 4, Globals.TileSize - 8, Globals.TileSize - 14);
                                if (missile.HitBox.IntersectsWith(tileBound)) return true;
                            }
                        }
                    }
                    break;
                case Dir.Left:
                    tileX = missile.HitBox.X / Globals.TileSize;
                    for (int i = missile.HitBox.Y / Globals.TileSize; i <= (missile.HitBox.Y + missile.HitBox.Height) / Globals.TileSize; i += 1)
                    {
                        if (tileX < Game.World.TileList.GetLength(0) && i < Game.World.TileList.GetLength(1))
                        {
                            tileY = i;
                            if (Game.World.TileList[tileX, tileY].IsBlocked)
                            {
                                tileBound = new Rectangle(tileX * Globals.TileSize + 4, tileY * Globals.TileSize + 4, Globals.TileSize - 8, Globals.TileSize - 14);
                                if (missile.HitBox.IntersectsWith(tileBound)) return true;
                            }
                        }
                    }
                    break;
                case Dir.Right:
                    tileX = (missile.HitBox.X + missile.HitBox.Width) / Globals.TileSize;
                    for (int i = missile.HitBox.Y / Globals.TileSize; i <= (missile.HitBox.Y + missile.HitBox.Height) / Globals.TileSize; i += 1)
                    {
                        if (tileX < Game.World.TileList.GetLength(0) && i < Game.World.TileList.GetLength(1))
                        {
                            tileY = i;
                            if (Game.World.TileList[tileX, tileY].IsBlocked)
                            {
                                tileBound = new Rectangle(tileX * Globals.TileSize + 4, tileY * Globals.TileSize + 4, Globals.TileSize - 8, Globals.TileSize - 14);
                                if (missile.HitBox.IntersectsWith(tileBound)) return true;
                            }
                        }
                    }
                    break;
            }

            return false;
        }

        public static List<Character> GetCharactersInTiles(Point Tile)
        {
            Rectangle TileBound = new Rectangle(Tile.X, Tile.Y, Globals.TileSize, Globals.TileSize);
            List<Character> result = new List<Character>();

            foreach(Character c in Game.World.MapChar)
            {
                Rectangle cBound = new Rectangle(c.CurRawPos.X, c.CurRawPos.Y, c.charType.Size.X, c.charType.Size.Y);
                if (TileBound.IntersectsWith(cBound)) result.Add(c);
            }

            return result;
        }

        public static Brush GetHPColor(Character c)
        {
            int percent = c.HP * 100 / c.HPMax;
            if (percent > 60) return Brushes.Lime;
            else if (percent > 30) return Brushes.Yellow;
            else return Brushes.Red;
        }

        public static Brush GetSkillStringColor(BaseSkill s)
        {
            if (s.Cooldown > 0) return Brushes.Gray;
            else return Brushes.Gold;
        }
    }
}
