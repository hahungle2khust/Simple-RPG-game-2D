using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    public class iRestoreMP : Item
    {
        private List<string> SFXs = new List<string>() { "Slurping1", "Slurping2", "Slurping3" };

        public iRestoreMP(Character Owner) : base(Owner)
        {
            Amount = 30;
            AsPercent = true;
            Source = new TileLayer("Restore", 1, 0);
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
            Owner.MP += Owner.MPMax * Amount / 100 / EffectingStages;
            if (Owner.MP >= Owner.MPMax) CurStage = EffectingStages;
        }
    }
}
