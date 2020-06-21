using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    public class BaseSkill
    {
        public string Name = "";
        public int Power = 100;
        public int Accuracy = 100;
        public SkillType Type = SkillType.Normal;
        public int Cost;
        public int Cooldown;
        public int CooldownTime;
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
        public List<Character> targetList = new List<Character>();

        public BaseSkill(Character Owner)
        {
            this.Owner = Owner;
        }

        public virtual bool Cast()
        {
            //Chạy khi người chơi sử dụng skill đấy
            if (Owner.IsCasting || Cooldown > 0 || Owner.MP < Cost) return false;
            Owner.IsCasting = true;
            Owner.MP -= Cost;
            StartCooldown(CooldownTime);
            return true;
        }

        public virtual void Casting()
        {

        }

        public virtual void Effecting()
        {

        }

        public virtual void MissileHit(Missile missile, Character target)
        {

        }

        public virtual void MissileHitObject(Missile missile)
        {

        }

        public virtual void MissileEndByRange(Missile missile)
        {

        }

        public virtual bool CanUse()
        {
            if (Owner.IsCasting || Cooldown > 0 || Owner.MP < Cost) return false;
            //Check status (paralyzed)
            //Check vũ khí
            //...
            return true;
        }

        public void StartCooldown(int CooldownTime)
        {
            Cooldown = CooldownTime;
            SkillManager.CooldownSkills.Add(this);
        }

        public void Reset()
        {
            CurCastingStage = 0;
            CastingStageCounter = 0;

            CurEffectingStage = 0;
            EffectingStageCounter = 0;

            Finished = false;
            FinishedCasting = false;

            //ArtList.Clear();
        }
    }
}
