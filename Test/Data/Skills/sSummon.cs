using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    public class sSummon : BaseSkill
    {
        protected List<int> CharTypeIndice;
        protected int Num;
        protected string voice;

        public sSummon(Character Owner, List<int> CharTypes, int Quantity, int Cooldown = 30000, string voice = "Shoryuken") : base(Owner)
        {
            CharTypeIndice = CharTypes;
            Num = Quantity;
            this.voice = voice;

            //Set thông số
            CooldownTime = Cooldown;
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            CastingTime = 3000;
            CastingStages = Num;
            CurCastingStage = 0;
            CastingStageInterval = CastingTime / CastingStages;
            SkillManager.NewCastingSkills.Add(this);
            SoundManager.PlayByFileName(voice);
            if (Owner.charType.Index == 7)
            {
                Owner.LastDir = 0;
                Owner.charType.Source.srcPos.Y = 4;
            }

            return true;
        }

        public override void Casting()
        {
            SoundManager.PlayByFileName("Summon");

            Character c = new Character(CharTypeIndice[Globals.gen.Next(0, CharTypeIndice.Count)]);
            c.CurPos = Owner.CurPos;
            c.team = Team.Enemy;
            CharType.SetSkill(c);
            c.ResetPos();
            Game.World.NewChar.Add(c);

            if (CurCastingStage >= CastingStages)
            {
                Finished = true;
                Owner.IsCasting = false;
                if (Owner.charType.Index == 7) Owner.charType.Source.srcPos.Y = 0;
            }
        }
    }
}
