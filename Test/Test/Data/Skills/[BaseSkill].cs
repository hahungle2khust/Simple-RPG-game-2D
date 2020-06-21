using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    public class BaseSkill
    {
        public string Name = "";
        public int Power;
        public int Accuracy;
        public int Cost;
        public int Cooldown;
        public int CastingTime;
        public bool FinishedCasting = false;
        public bool Finished = false;

        public Character Owner;    //Character sở hữu skill hiện tại

        public int CastingStages;  //Tổng số casting stages của skill
        public int CurCastingStage; //Đếm stage
        public int CastingStageInterval; //Chu kỳ (khoảng thời gian giữa stage này với stage tiếp theo)
        public int CastingStageCounter;  //Đếm thời gian

        public int EffectingStages;  //Tổng số casting stages của skill
        public int CurEffectingStage; //Đếm stage
        public int EffectingStageInterval; //Chu kỳ (khoảng thời gian giữa stage này với stage tiếp theo)
        public int EffectingStageCounter;  //Đếm thời gian

        public List<Art> ArtList = new List<Art>();

        public virtual bool Cast()
        {
            //Chạy khi người chơi sử dụng skill đấy
            if (Owner.IsCasting) return false;
            Owner.IsCasting = true;
            return true;
        }

        public virtual void Casting()
        {

        }

        public virtual void Effecting()
        {

        }

        public virtual bool CanUse()
        {
            if (Owner.IsCasting) return false;
            //Check mana ko đủ
            //Check status (paralyzed)
            //Check vũ khí
            //...
            return true;
        }

        public void Reset()
        {
            CurCastingStage = 0;
            CastingStageCounter = 0;

            CurEffectingStage = 0;
            EffectingStageCounter = 0;

            Finished = false;
            FinishedCasting = false;

            ArtList.Clear();
        }
    }
}
