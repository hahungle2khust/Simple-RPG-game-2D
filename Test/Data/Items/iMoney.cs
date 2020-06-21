using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class iMoney : Item
    {
        public iMoney(Character Owner, int Amount = -1) : base(Owner)
        {
            if (Amount >= 0)
                this.Amount = Amount;
            else
                this.Amount = Globals.gen.Next(10, 101) * 1000;
            Source = new TileLayer("Money", 0, 0);
        }

        public override bool Use()
        {
            Globals.Money += Amount;
            return true;
        }
    }
}
