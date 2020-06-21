using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    public class sSaber : sSword
    {
        public sSaber(Character Owner) : base(Owner)
        {
            swordArt = "Saber";
            sfx = "SaberAttack";
            Power = 100;
            Cost = 0;
            CooldownTime = 0;
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            sSaber CastingSkill = new sSaber(Owner);
            CastingSkill.Owner = Owner;
            CastingSkill.CastingTime = 200;
            CastingSkill.CastingStages = 10;
            CastingSkill.CurCastingStage = 0;
            CastingSkill.CastingStageInterval = CastingSkill.CastingTime / CastingSkill.CastingStages;
            SkillManager.NewCastingSkills.Add(CastingSkill);
            CastingSkill.Casting();

            SoundManager.PlayByFileName(sfx);
            return true;
        }
    }
}
