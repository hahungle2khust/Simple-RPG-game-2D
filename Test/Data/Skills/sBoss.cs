using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    class sBoss : BaseSkill
    {
        public sBoss(Character Owner) : base(Owner)
        {
            //Set Power
            //Set Accuracy
            //Set Type
            Cost = 0;
            CooldownTime = 0;
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            sBoss CastingSkill = new sBoss(Owner);
            CastingSkill.Owner = Owner;
            CastingSkill.CastingTime = 300;
            CastingSkill.CastingStages = 6;
            CastingSkill.CurCastingStage = 0;
            CastingSkill.CastingStageInterval = CastingSkill.CastingTime / CastingSkill.CastingStages;
            SkillManager.NewCastingSkills.Add(CastingSkill);
            Owner.FrameNum = 3;
            Owner.AniFrame = 0;

            return true;
        }

        public override void Casting()
        {
            int forORback;
            if (CurCastingStage <= 3) forORback = 1;
            else forORback = -1;

            Owner.AniFrame += 1;
                
            if (Owner.LastDir == Dir.Up) Owner.OffSet.Y -= forORback * 4;
            else if (Owner.LastDir == Dir.Down) Owner.OffSet.Y += forORback * 4;
            else if (Owner.LastDir == Dir.Left) Owner.OffSet.X -= forORback * 4;
            else if (Owner.LastDir == Dir.Right) Owner.OffSet.X += forORback * 4;

            if (CurCastingStage == 1) SoundManager.PlayByFileName("Attack");
            else if (CurCastingStage == 3)
            {
                Rectangle area = new Rectangle(Owner.CurRawPos.X + 12, Owner.CurRawPos.Y + 12, Owner.charType.Size.X - 24, Owner.charType.Size.Y - 24);
                List<Character> hitTargets = GamePlay.GetEnemies(area, Owner.team);
                foreach (Character c in hitTargets)
                {
                    if (GamePlay.CheckHit(Owner, c, this))
                        GamePlay.InfictDamage(Owner, c, GamePlay.DamageCalculation(Owner, c, this));
                }
            }
            else if (CurCastingStage >= CastingStages)
            {
                Owner.FrameNum = 2;
                Owner.AniFrame = Owner.AniFrame;
                Finished = true;
                Owner.IsCasting = false;
            }
        }

        public override bool CanUse()
        {
            if (base.CanUse() == false) return false;

            Rectangle area = new Rectangle(Owner.CurRawPos.X + 12, Owner.CurRawPos.Y + 12, Owner.charType.Size.X - 24, Owner.charType.Size.Y - 24);

            if (Owner.LastDir == Dir.Up) area.Y -= 3 * 4;
            else if (Owner.LastDir == Dir.Down) area.Y += 3 * 4;
            else if (Owner.LastDir == Dir.Left) area.X -= 3 * 4;
            else if (Owner.LastDir == Dir.Right) area.X += 3 * 4;

            if (GamePlay.GetEnemies(area, Owner.team).Count > 0)
                return true;

            return false;
        }
    }
}
