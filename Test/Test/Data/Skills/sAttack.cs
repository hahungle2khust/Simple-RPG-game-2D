using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    class sAttack : BaseSkill
    {
        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            //CastingTime = 300;
            //CastingStages = 10;
            //CurCastingStage = 0;
            //CastingStageInterval = CastingTime / CastingStages;
            //SkillManager.NewCastingSkills.Add(this);
            //Casting();

            sAttack CastingSkill = new sAttack();
            CastingSkill.Owner = Owner;
            CastingSkill.CastingTime = 300;
            CastingSkill.CastingStages = 10;
            CastingSkill.CurCastingStage = 0;
            CastingSkill.CastingStageInterval = CastingSkill.CastingTime / CastingSkill.CastingStages;
            SkillManager.NewCastingSkills.Add(CastingSkill);
            CastingSkill.Casting();

            SFX.PlaySFX(Globals.GameDir + "\\Content\\SFX\\SwordAttack.wav");
            return true;
        }

        public override void Casting()
        {
            if(CurCastingStage == 0)
            {
                //Tạo art
                Art newArt = new Art();
                newArt.SrcImage = "1";
                newArt.SrcRect = new Rectangle(Globals.TileSize, 0, Globals.TileSize, Globals.TileSize);
                newArt.Position = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y, Globals.TileSize, Globals.TileSize);
                newArt.Angle = Game.GetAngleByCharacterFacing(Owner);
                ArtList.Add(newArt);
                Game.World.BeforeCharArts.Add(newArt);
            }
            else if(CurCastingStage <= 5)
            {
                //Di chuyển art của cây kiếm đi về phía trước
                if (Owner.LastDir == Dir.Up) ArtList[0].Position.Y -= 4;
                else if (Owner.LastDir == Dir.Down) ArtList[0].Position.Y += 4;
                else if (Owner.LastDir == Dir.Left) ArtList[0].Position.X -= 4;
                else if (Owner.LastDir == Dir.Right) ArtList[0].Position.X += 4;
            }
            else
            {
                //Di chuyển art của cây kiếm lùi ra sau
                if (Owner.LastDir == Dir.Up) ArtList[0].Position.Y += 4;
                else if (Owner.LastDir == Dir.Down) ArtList[0].Position.Y -= 4;
                else if (Owner.LastDir == Dir.Left) ArtList[0].Position.X += 4;
                else if (Owner.LastDir == Dir.Right) ArtList[0].Position.X -= 4;
            }

            if(CurCastingStage == 5)
            {
                //Gây damage
                Rectangle area = new Rectangle();
                if (Owner.LastDir == Dir.Up) area = new Rectangle(Owner.CurRawPos.X + 6, Owner.CurRawPos.Y - 20, 20,20);
                else if (Owner.LastDir == Dir.Down) area = new Rectangle(Owner.CurRawPos.X + 6, Owner.CurRawPos.Y + Owner.charType.Size.Y, 20, 20);
                else if (Owner.LastDir == Dir.Left) area = new Rectangle(Owner.CurRawPos.X - 20, Owner.CurRawPos.Y + 6, 20, 20);
                else if (Owner.LastDir == Dir.Right) area = new Rectangle(Owner.CurRawPos.X + Owner.charType.Size.X, Owner.CurRawPos.Y + 6, 20, 20);
                List<Character> targets = GamePlay.GetEnemies(area, Owner.team);
                foreach(Character c in targets)
                {
                    if (GamePlay.CheckHit(Owner, c, this))
                    {
                        GamePlay.InfictDamage(Owner, c, GamePlay.DamageCalculation(Owner, c, this));
                    }
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
