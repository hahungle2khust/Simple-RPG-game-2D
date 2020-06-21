using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class CharacterDies : EventArgs
    {
        public Character Attacker;
        public Character Target;

        public CharacterDies(Character attacker, Character Target)
        {
            this.Attacker = attacker;
            this.Target = Target;
        }
    }
}
