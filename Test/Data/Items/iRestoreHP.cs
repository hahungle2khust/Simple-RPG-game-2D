using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    public class iRestoreHP : Item
    {
        private List<string> SFXs = new List<string>() { "Bite1", "Bite2", "Bite3" };

        public iRestoreHP(Character Owner) : base(Owner)
        {
            Amount = 30;
            AsPercent = true;
            Source = new TileLayer("Restore", 0, 0);
        }

        public override bool Use()
        {
            if (!base.Use()) return false;

            SoundManager.PlayByFileName(SFXs[Globals.gen.Next(0, 3)]);
            EffectingStages = 10;
            CurStage = 0;
            StageInterval = 100;

            return true;
        }

        public override void Update()
        {
            Owner.HP += Owner.HPMax * Amount / 100 / EffectingStages;
            if (Owner.HP >= Owner.HPMax) CurStage = EffectingStages;
        }
    }
}
