using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    public class sThrowShield : sThrowBomb
    {
        public sThrowShield(Character Owner) : base(Owner)
        {
            missileArt = "Shield";
            contactArt = "Explosion";
            contactArtFrames = 7;
            throwSFX = "Throw";
            explodeSFX = "Explosion";
            missileArtNo = 7;
            missileRange = 7;
            missileSpeed = 10;
            team = Team.Both;

            //Set thông số
            Power = 150;
            CooldownTime = 3000;
            Cost = 20;
        }

        public override void Casting()
        {
            base.Casting();
            if (ArtList.Count > 0) ArtList[0].Angle = Owner.LastDir;
        }
    }
}
