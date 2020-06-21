using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    class sThowBomb : BaseSkill
    {
        public sThowBomb(Character Owner) : base(Owner)
        {
            //Set Power
            //Set Accuracy
            //Set Type
            //Set Cost
            CooldownTime = 2000;
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
                newArt.SrcImage = "Bomb";
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
                Missile m = new Missile(new TileLayer("1", 3, 0), 32, 32, Owner, this, Owner.CurRawPos, Owner.LastDir, 12, 5, 0);
                Game.World.MissileList.Add(m);
                Owner.IsCasting = false;
                SoundManager.PlayByFileName("ThrowBomb");
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

            List<Character> hitTargets = GamePlay.GetEnemies(area, Owner.team);
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
