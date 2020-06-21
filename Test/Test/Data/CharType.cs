using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    public class CharType
    {
        public string Name = "";
        public int Index;
        public string Suffix = "";

        public Point Size = new Point(Globals.TileSize, Globals.TileSize); //Pixel
        public TileLayer Source;

        public int baseHP;
        public int baseMP;
        public int baseAtk;
        public int baseDef;
        public int baseSAtk;
        public int baseSDef;
        public int baseSpeed;

        public int luHP;
        public int luMP;
        public int luAtk;
        public int luDef;
        public int luSAtk;
        public int luSDef;
        public int luSpeed;

        public int Range;   //Aquision Range
                            //mỗi 1 chu kỳ các char map --> chạy 1 hàm: Act()
                            //Act() có: check các character trong range
                            // --> Có --> check team --> có địch --> check xem có skill nào dùng dc hay không
                            //Có skill dùng dc --> Cast() của skill đấy
                            //Ko có skill dùng dc --> Di chuyển tiếp cận Player. (A* pathfinding)

        public bool IsNPC;
        public bool IsMerchant;
        public int MerchantMoney;
    }
}
