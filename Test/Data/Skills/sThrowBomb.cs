using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    public class sThrowBomb : BaseSkill
    {
        protected string missileArt = "Bomb";
        protected string contactArt = "Explosion";
        protected int contactArtFrames = 7;
        protected string throwSFX = "ThrowBomb";
        protected string explodeSFX = "Explosion";
        protected int missileArtNo = 3;
        protected int missileSpeed = 8;
        protected int missileRange = 5;
        protected Team team = Team.Ally;

        public sThrowBomb(Character Owner) : base(Owner)
        {
            Power = 150;
            Cost = 20;
            CooldownTime = 5000;
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            CastingTime = 300;
            CastingStages = 1;
            CurCastingStage = 0;
            CastingStageInterval = CastingTime / CastingStages;
            SkillManager.NewCastingSkills.Add(this);
            Casting();

            return true;
        }

        public override void Casting()
        {
            if (CurCastingStage == 0)
            {
                //Tạo art cây súng
                Art newArt = new Art();
                newArt.SrcImage = missileArt;
                newArt.SrcRect = new Rectangle(0, 0, Globals.TileSize, Globals.TileSize);
                if (Owner.LastDir == Dir.Up)
                {
                    newArt.Position = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y - 12, 32, 32);
                    Game.World.BeforeCharArts.Add(newArt);
                }
                else if (Owner.LastDir == Dir.Down || Owner.LastDir == Dir.Stand)
                {
                    newArt.Position = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y + 12, 32, 32);
                    Game.World.AfterCharArts.Add(newArt);
                }
                else if (Owner.LastDir == Dir.Left)
                {
                    newArt.Position = new Rectangle(Owner.CurRawPos.X - 12, Owner.CurRawPos.Y, 32, 32);
                    Game.World.AfterCharArts.Add(newArt);
                }
                else
                {
                    newArt.Position = new Rectangle(Owner.CurRawPos.X + 12, Owner.CurRawPos.Y, 32, 32);
                    Game.World.AfterCharArts.Add(newArt);
                }
                ArtList.Add(newArt);
            }
            else
            {
                //Tạo missile
                Missile m = new Missile(new TileLayer("1", missileArtNo, 0), 32, 32, Owner, this, Owner.CurRawPos, Owner.LastDir, missileSpeed, missileRange, 0);
                Game.World.MissileList.Add(m);
                Owner.IsCasting = false;
                SoundManager.PlayByFileName(throwSFX);
            }

            if (CurCastingStage >= CastingStages)
            {
                Finished = true;
                ArtList[0].remove = true;
                ArtList.Clear();
                Owner.IsCasting = false;
            }
        }

        private void Explode(Missile m)
        {
            SoundManager.PlayByFileName(explodeSFX);
            Rectangle area = new Rectangle(m.curPos.X - 12, m.curPos.Y - 12, 56, 56);

            Art explosion = new Art();
            explosion.SrcImage = contactArt;
            explosion.AutoRemove = true;
            explosion.Frames = contactArtFrames;
            explosion.FrameInterval = 50;
            explosion.SrcRect = new Rectangle(0, 0, Globals.TileSize * 2, Globals.TileSize * 2);
            explosion.Position = area;
            Game.World.AfterCharArts.Add(explosion);

            List<Character> hitTargets = GamePlay.GetEnemies(area, team);
            foreach (Character c in hitTargets)
            {
                if (GamePlay.CheckHit(Owner, c, this))
                    GamePlay.InfictDamage(Owner, c, GamePlay.DamageCalculation(Owner, c, this));
            }
        }

        public override bool CanUse()
        {
            if (base.CanUse() == false) return false;

            Rectangle area = new Rectangle();
            int range = missileRange - 1;

            if (Owner.LastDir == Dir.Up) area = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y - range * Globals.TileSize, Globals.TileSize, range * Globals.TileSize);
            else if (Owner.LastDir == Dir.Down || Owner.LastDir == Dir.Stand) area = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y + Owner.charType.Size.Y, Globals.TileSize, range * Globals.TileSize);
            else if (Owner.LastDir == Dir.Left) area = new Rectangle(Owner.CurRawPos.X - range * Globals.TileSize, Owner.CurRawPos.Y, range * Globals.TileSize, Globals.TileSize);
            else if (Owner.LastDir == Dir.Right) area = new Rectangle(Owner.CurRawPos.X + Owner.charType.Size.X, Owner.CurRawPos.Y, range * Globals.TileSize, Globals.TileSize);

            if (GamePlay.GetEnemies(area, Owner.team).Count > 0)
                return true;

            return false;
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
