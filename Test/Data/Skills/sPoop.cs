using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    public class sPoop : BaseSkill
    {
        protected int MissileArtNo = 4;
        protected int MissileRange = 7;
        protected string sfx = "ThrowBomb";

        public sPoop(Character Owner) : base(Owner)
        {
            //Set thông số
            CooldownTime = 1000;
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            CastingTime = 200;
            CastingStages = 1;
            CurCastingStage = 0;
            CastingStageInterval = CastingTime / CastingStages;
            SkillManager.NewCastingSkills.Add(this);

            return true;
        }

        public override void Casting()
        {
            //Tạo missile
            Missile m = new Missile(new TileLayer("1", MissileArtNo, 0), 32, 32, Owner, this, Owner.CurRawPos, Owner.LastDir, 8, MissileRange, 0);
            Game.World.MissileList.Add(m);
            Owner.IsCasting = false;
            SoundManager.PlayByFileName(sfx);

            if (CurCastingStage >= CastingStages)
            {
                Finished = true;
                Owner.IsCasting = false;
            }
        }

        public override void MissileHit(Missile missile, Character target)
        {
            if (target == null) return;

            if (GamePlay.CheckHit(Owner, target, this))
                GamePlay.InfictDamage(Owner, target, GamePlay.DamageCalculation(Owner, target, this));
        }

        public override bool CanUse()
        {
            if (base.CanUse() == false) return false;

            Rectangle area = new Rectangle();
            int range = MissileRange - 1;

            if (Owner.LastDir == Dir.Up) area = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y - range * Globals.TileSize, Globals.TileSize, range * Globals.TileSize);
            else if (Owner.LastDir == Dir.Down || Owner.LastDir == Dir.Stand) area = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y + Owner.charType.Size.Y, Globals.TileSize, range * Globals.TileSize);
            else if (Owner.LastDir == Dir.Left) area = new Rectangle(Owner.CurRawPos.X - range * Globals.TileSize, Owner.CurRawPos.Y, range * Globals.TileSize, Globals.TileSize);
            else if (Owner.LastDir == Dir.Right) area = new Rectangle(Owner.CurRawPos.X + Owner.charType.Size.X, Owner.CurRawPos.Y, range * Globals.TileSize, Globals.TileSize);

            if (GamePlay.GetEnemies(area, Owner.team).Count > 0)
                return true;

            return false;
        }
    }
}
