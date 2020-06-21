using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    public class sBite : BaseSkill
    {
        public sBite(Character Owner) : base(Owner)
        {
            Cost = 0;
            CooldownTime = 5000;
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            sBite CastingSkill = new sBite(Owner);
            CastingSkill.Owner = Owner;
            CastingSkill.CastingTime = 300;
            CastingSkill.CastingStages = 5;
            CastingSkill.CurCastingStage = 0;
            CastingSkill.CastingStageInterval = CastingSkill.CastingTime / CastingSkill.CastingStages;
            SkillManager.NewCastingSkills.Add(CastingSkill);
            CastingSkill.Casting();

            SoundManager.PlayByFileName("Bite4");
            return true;
        }

        public override void Casting()
        {
            if (CurCastingStage == 0)
            {
                //Tạo art
                Art newArt = new Art();
                newArt.SrcImage = "Bite";
                newArt.Frames = 5;
                newArt.FrameInterval = 10000;
                newArt.SrcRect = new Rectangle(0, 0, Globals.TileSize, Globals.TileSize);
                if (Owner.LastDir == Dir.Up) newArt.Position = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y - Globals.TileSize, Globals.TileSize, Globals.TileSize);
                else if (Owner.LastDir == Dir.Down) newArt.Position = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y + Owner.charType.Size.Y, Globals.TileSize, Globals.TileSize);
                else if (Owner.LastDir == Dir.Left) newArt.Position = new Rectangle(Owner.CurRawPos.X - Globals.TileSize, Owner.CurRawPos.Y, Globals.TileSize, Globals.TileSize);
                else if (Owner.LastDir == Dir.Right) newArt.Position = new Rectangle(Owner.CurRawPos.X + Owner.charType.Size.X, Owner.CurRawPos.Y, Globals.TileSize, Globals.TileSize);
                ArtList.Add(newArt);
                Game.World.AfterCharArts.Add(newArt);
            }
            else ArtList[0].CurFrame += 1;
            
            
            if (CurCastingStage == 3)
            {
                //Gây damage
                Rectangle area = new Rectangle();
                if (Owner.LastDir == Dir.Up) area = new Rectangle(Owner.CurRawPos.X + 6, Owner.CurRawPos.Y - 20, 20, 20);
                else if (Owner.LastDir == Dir.Down) area = new Rectangle(Owner.CurRawPos.X + 6, Owner.CurRawPos.Y + Owner.charType.Size.Y, 20, 20);
                else if (Owner.LastDir == Dir.Left) area = new Rectangle(Owner.CurRawPos.X - 20, Owner.CurRawPos.Y + 6, 20, 20);
                else if (Owner.LastDir == Dir.Right) area = new Rectangle(Owner.CurRawPos.X + Owner.charType.Size.X, Owner.CurRawPos.Y + 6, 20, 20);
                List<Character> targets = GamePlay.GetEnemies(area, Owner.team);
                foreach (Character c in targets)
                {
                    if (GamePlay.CheckHit(Owner, c, this))
                        GamePlay.InfictDamage(Owner, c, GamePlay.DamageCalculation(Owner, c, this));
                }
            }

            if (CurCastingStage >= CastingStages)
            {
                Finished = true;
                ArtList[0].remove = true;
                Owner.IsCasting = false;
            }
        }

        public override bool CanUse()
        {
            if (base.CanUse() == false) return false;

            Rectangle area = new Rectangle();
            area = new Rectangle(Owner.CurRawPos.X + 6, Owner.CurRawPos.Y - 20, 20, 20);
            if (GamePlay.GetEnemies(area, Owner.team).Count > 0)
            {
                Owner.LastDir = Dir.Up;
                return true;
            }
            area = new Rectangle(Owner.CurRawPos.X + 6, Owner.CurRawPos.Y + Owner.charType.Size.Y, 20, 20);
            if (GamePlay.GetEnemies(area, Owner.team).Count > 0)
            {
                Owner.LastDir = Dir.Down;
                return true;
            }
            area = new Rectangle(Owner.CurRawPos.X - 20, Owner.CurRawPos.Y + 6, 20, 20);
            if (GamePlay.GetEnemies(area, Owner.team).Count > 0)
            {
                Owner.LastDir = Dir.Left;
                return true;
            }
            area = new Rectangle(Owner.CurRawPos.X + Owner.charType.Size.X, Owner.CurRawPos.Y + 6, 20, 20);
            if (GamePlay.GetEnemies(area, Owner.team).Count > 0)
            {
                Owner.LastDir = Dir.Right;
                return true;
            }

            return false;
        }
    }
}
