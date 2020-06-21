using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;

namespace Test
{
    [Serializable]
    public class CharType
    {
        public string Name = "";
        public int Index;
        public string Suffix = "";

        public Point Size = new Point(Globals.TileSize, Globals.TileSize); //Pixel
        public TileLayer Source;

        public Point WaitAfterAttack;

        //Base: level 1 nó có nhiêu đây stat
        public int baseHP;
        public int baseMP;
        public int baseAtk;
        public int baseDef;
        public int baseSAtk;
        public int baseSDef;
        public int baseSpeed;

        //levelup stat: cứ 1 level (sau level 1) thì nó tăng nhiêu đây stat
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

        public static void SetSkill(Character c)
        {
            if(c.charType.Index == 5)
            {
                //Hà Hưng
                Game.SkillButtons.Add(Key.Space, new sSword(Game.Player));
                Game.SkillButtons.Add(Key.D, new sPistol(Game.Player));
                Game.SkillButtons.Add(Key.S, new sShotgun(Game.Player));
                Game.SkillButtons.Add(Key.A, new sThrowBomb(Game.Player));
            }
            else if(c.charType.Index == 3)
            {
                //Đạt
                Game.SkillButtons.Add(Key.Space, new sSword(Game.Player));
                Game.SkillButtons.Add(Key.D, new sBow(Game.Player));
                Game.SkillButtons.Add(Key.A, new sCharge(Game.Player));
                Game.SkillButtons.Add(Key.S, new sShotgun(Game.Player));
            }
            else if(c.charType.Index == 4)
            {
                //Khai
                Game.SkillButtons.Add(Key.Space, new sSaber(Game.Player));
                Game.SkillButtons.Add(Key.D, new sPistol(Game.Player));
                Game.SkillButtons.Add(Key.S, new sThrowBomb(Game.Player));
                Game.SkillButtons.Add(Key.A, new sCharge(Game.Player));
            }
            else if(c.charType.Index == 6)
            {
                //Quang Hưng
                Game.SkillButtons.Add(Key.Space, new sSaber(Game.Player));
                Game.SkillButtons.Add(Key.D, new sBow(Game.Player));
                Game.SkillButtons.Add(Key.S, new sDash(Game.Player));
                Game.SkillButtons.Add(Key.A, new sThrowShield(Game.Player));
            }
            else if (c.charType.Index == 7)
            {
                if (Globals.Mode != HardMode.Easy) c.Skills.Add(new sBossBall(c));
                if (Globals.Mode != HardMode.Easy) c.Skills.Add(new sSummon(c, new List<int>() { 10 }, 1, 59000, "Laugh"));
                c.Skills.Add(new sBossOOP(c));
                c.Skills.Add(new sBossCharge(c));
                c.Skills.Add(new sOOP(c));
                c.Skills.Add(new sBoss(c));
            }
            else if (c.charType.Index == 8 || c.charType.Index == 9)
                c.team = Team.Ally;
            else if (c.charType.Index == 10)
            {
                if (Globals.Mode != HardMode.Easy) c.Skills.Add(new sSummon(c, new List<int>() { 11, 12, 13 }, 2, 23000));
                c.Skills.Add(new sCharge(c));
                c.Skills.Add(new sOOP(c));
                c.Skills.Add(new sAttack(c));
            }
            else if (c.charType.Index == 11)
            {
                c.Skills.Add(new sAttack(c));
                c.Skills.Add(new sPoop(c));
            }
            else if (c.charType.Index == 12)
            {
                c.Skills.Add(new sBite(c));
                c.Skills.Add(new sAttack(c));
            }
            else if (c.charType.Index == 13)
            {
                c.Skills.Add(new sAttack(c));
                c.Skills.Add(new sBone(c));
            }

        }
    }
}
