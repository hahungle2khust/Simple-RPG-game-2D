using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    public class sSword : BaseSkill
    {
        private double PercentManaRecover = 0.020;

        protected string swordArt = "Sword";
        protected string sfx = "SwordAttack";

        public sSword(Character Owner) : base(Owner)
        {
            Power = 110;
            Cost = 0;
            CooldownTime = 0;
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            sSword CastingSkill = new sSword(Owner);
            CastingSkill.Owner = Owner;
            CastingSkill.CastingTime = 300;
            CastingSkill.CastingStages = 10;
            CastingSkill.CurCastingStage = 0;
            CastingSkill.CastingStageInterval = CastingSkill.CastingTime / CastingSkill.CastingStages;
            SkillManager.NewCastingSkills.Add(CastingSkill);
            CastingSkill.Casting();

            SoundManager.PlayByFileName(sfx);
            return true;
        }

        public override void Casting()
        {
            if(CurCastingStage == 0)
            {
                //Tạo art
                Art newArt = new Art();
                newArt.SrcImage = swordArt;
                newArt.SrcRect = new Rectangle(0, 0, Globals.TileSize, Globals.TileSize);
                newArt.Position = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y, Globals.TileSize, Globals.TileSize);
                //newArt.Angle = Game.GetAngleByCharacterFacing(Owner, true, true);
                newArt.Angle = Owner.LastDir;
                ArtList.Add(newArt);
                if (Owner.LastDir == Dir.Up)
                    Game.World.BeforeCharArts.Add(newArt);
                else
                    Game.World.AfterCharArts.Add(newArt);
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
                        int damage = GamePlay.DamageCalculation(Owner, c, this);
                        GamePlay.InfictDamage(Owner, c, damage);
                        Owner.MP += (int)(Owner.MPMax * PercentManaRecover);
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
