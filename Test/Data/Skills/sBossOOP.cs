using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    public class sBossOOP : BaseSkill
    {
        public sBossOOP(Character Owner) : base(Owner)
        {
            //Set thông số
            Power = 150;
            Cost = 0;
            CooldownTime = 10000;
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            CastingTime = 1000;
            CastingStages = 10;
            CurCastingStage = 0;
            CastingStageInterval = CastingTime / CastingStages;
            SkillManager.NewCastingSkills.Add(this);
            Owner.charType.Source.srcPos.Y = 4;
            Owner.LastDir = Dir.Down;

            SoundManager.PlayByFileName("Throw");
            SoundManager.PlayByFileName("Throw");
            SoundManager.PlayByFileName("Throw");
            SoundManager.PlayByFileName("Throw");
            return true;
        }

        public override void Casting()
        {
            Owner.AniFrame += 1;
            
            if (CurCastingStage >= CastingStages)
            {
                SoundManager.PlayByFileName("Laugh");

                for(int i=1; i<5; i += 1)
                {
                    Missile m = new Missile(new TileLayer("1", 6, 0), 32, 32, Owner, this, Owner.CurRawPos, (Dir)i, 16, 10, 0);
                    Game.World.MissileList.Add(m);
                    m = new Missile(new TileLayer("1", 6, 0), 32, 32, Owner, this, Owner.CurRawPos, (Dir)i, 16, 10, Math.PI / 6);
                    Game.World.MissileList.Add(m);
                    m = new Missile(new TileLayer("1", 6, 0), 32, 32, Owner, this, Owner.CurRawPos, (Dir)i, 16, 10, -Math.PI / 6);
                    Game.World.MissileList.Add(m);
                }

                Owner.charType.Source.srcPos.Y = 0;
                Finished = true;
                Owner.IsCasting = false;
            }
        }

        public override bool CanUse()
        {
            if (base.CanUse() == false) return false;

            Rectangle area = new Rectangle();
            int range = 10 - 1;

            //area = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y - range * Globals.TileSize, Globals.TileSize, range * Globals.TileSize);
            //if (GamePlay.GetEnemies(area, Owner.team).Count > 0) return true;
            //area = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y + Owner.charType.Size.Y, Globals.TileSize, range * Globals.TileSize);
            //if (GamePlay.GetEnemies(area, Owner.team).Count > 0) return true;
            //area = new Rectangle(Owner.CurRawPos.X - range * Globals.TileSize, Owner.CurRawPos.Y, range * Globals.TileSize, Globals.TileSize);
            //if (GamePlay.GetEnemies(area, Owner.team).Count > 0) return true;
            //area = new Rectangle(Owner.CurRawPos.X + Owner.charType.Size.X, Owner.CurRawPos.Y, range * Globals.TileSize, Globals.TileSize);
            //if (GamePlay.GetEnemies(area, Owner.team).Count > 0) return true;

            area = new Rectangle(Owner.CurRawPos.X - range * Globals.TileSize, Owner.CurRawPos.Y - range * Globals.TileSize, Owner.charType.Size.X + 2 * range * Globals.TileSize, Owner.charType.Size.Y + 2 * range * Globals.TileSize);
            if (GamePlay.GetEnemies(area, Owner.team).Count > 0) return true;

            return false;
        }

        private void Explode(Missile m)
        {
            SoundManager.PlayByFileName("Explosion");
            Rectangle area = new Rectangle(m.curPos.X - 12, m.curPos.Y - 12, 56, 56);

            Art explosion = new Art();
            explosion.SrcImage = "Explosion";
            explosion.AutoRemove = true;
            explosion.Frames = 7;
            explosion.FrameInterval = 50;
            explosion.SrcRect = new Rectangle(0, 0, Globals.TileSize * 2, Globals.TileSize * 2);
            explosion.Position = area;
            Game.World.AfterCharArts.Add(explosion);

            List<Character> hitTargets = GamePlay.GetEnemies(area, Team.Enemy);
            foreach (Character c in hitTargets)
            {
                if (GamePlay.CheckHit(Owner, c, this))
                    GamePlay.InfictDamage(Owner, c, GamePlay.DamageCalculation(Owner, c, this));
            }
        }

        public override void MissileHit(Missile missile, Character target)
        {
            if (target == null) return;
            Explode(missile);
        }

        public override void MissileEndByRange(Missile missile)
        {
            Explode(missile);
        }

        public override void MissileHitObject(Missile missile)
        {
            Explode(missile);
        }
    }
}
