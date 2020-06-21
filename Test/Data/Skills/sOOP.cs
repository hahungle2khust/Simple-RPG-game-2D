using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    public class sOOP : sThrowBomb
    {
        private List<string> SFXs = new List<string>() { "Hit1", "Hit2", "Hit3" };

        public sOOP(Character Owner) : base(Owner)
        {
            missileArt = "OOP";
            contactArt = "Slash";
            contactArtFrames = 6;
            throwSFX = "Throw";
            explodeSFX = "None";
            missileArtNo = 6;
            missileRange = 10;
            missileSpeed = 24;
            team = Team.Enemy;

            //Set thông số
            Power = 150;
            CooldownTime = 1000;
            Cost = 0;
        }

        public override bool Cast()
        {
            if (!base.Cast()) return false;
            SoundManager.PlayByFileName("Hadouken");
            return true;
        }

        public override void Casting()
        {
            base.Casting();
            if (CurCastingStage >= CastingStages)
            {
                Game.World.MissileList.Add(new Missile(new TileLayer("1", missileArtNo, 0), 32, 32, Owner, this, Owner.CurRawPos, Owner.LastDir, missileSpeed, missileRange, Math.PI / 6));
                Game.World.MissileList.Add(new Missile(new TileLayer("1", missileArtNo, 0), 32, 32, Owner, this, Owner.CurRawPos, Owner.LastDir, missileSpeed, missileRange, -Math.PI / 6));
            }
        }

        public override void MissileHit(Missile missile, Character target)
        {
            base.MissileHit(missile, target);
            SoundManager.PlayByFileName(SFXs[Globals.gen.Next(0, SFXs.Count)]);
        }
    }
}
