using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    public class sBossCharge : sCharge
    {
        public sBossCharge(Character Owner) : base(Owner)
        {
            voice = "Laugh";
            Power = 150;
            CooldownTime = 7000;
        }

        public override bool Cast()
        {
            Owner.charType.Source.srcPos.Y = 5;
            Owner.FrameNum = 0;
            return base.Cast();
        }

        public override void Casting()
        {
            base.Casting();

            if(CurCastingStage >= CastingStages)
            {
                Owner.FrameNum = 2;
                Owner.charType.Source.srcPos.Y = 0;
            }
        }
    }
}
