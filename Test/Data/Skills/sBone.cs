using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    public class sBone : sPoop
    {
        public sBone(Character Owner) : base(Owner)
        {
            MissileArtNo = 5;
            MissileRange = 5;
            sfx = "Throw";

            //Set thông số
            CooldownTime = 1000;
        }
    }
}
