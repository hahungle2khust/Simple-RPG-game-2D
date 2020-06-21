using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    class sShotgun : BaseSkill
    {
        public sShotgun(Character Owner) : base(Owner)
        {
            Power = 120;
            Cost = 50;
            CooldownTime = 800;
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

            SoundManager.PlayByFileName("Shotgun");
            return true;
        }

        public override void Casting()
        {
            if(CurCastingStage == 0)
            {
                //Tạo art cây súng
                Art newArt = new Art();
                newArt.SrcImage = "Shotgun";
                newArt.SrcRect = new Rectangle(0, 0, Globals.TileSize, Globals.TileSize);
                newArt.Angle = Owner.LastDir;
                if (Owner.LastDir == Dir.Up)
                {
                    newArt.Position = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y - 16, 32, 32);
                    Game.World.BeforeCharArts.Add(newArt);
                }
                else if (Owner.LastDir == Dir.Down || Owner.LastDir == Dir.Stand)
                {
                    newArt.Position = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y + 16, 32, 32);
                    Game.World.AfterCharArts.Add(newArt);
                }
                else if (Owner.LastDir == Dir.Left)
                {
                    newArt.Position = new Rectangle(Owner.CurRawPos.X - 16, Owner.CurRawPos.Y, 32, 32);
                    Game.World.AfterCharArts.Add(newArt);
                }
                else
                {
                    newArt.Position = new Rectangle(Owner.CurRawPos.X + 16, Owner.CurRawPos.Y, 32, 32);
                    Game.World.AfterCharArts.Add(newArt);
                }
                ArtList.Add(newArt);
            }
            else
            {
                //Tạo missile
                Missile m = new Missile(new TileLayer("1", 1, 0), 32, 32, Owner, this, Owner.CurRawPos, Owner.LastDir, 16, 3, 0);
                Game.World.MissileList.Add(m);
                m = new Missile(new TileLayer("1", 1, 0), 32, 32, Owner, this, Owner.CurRawPos, Owner.LastDir, 16, 3, Math.PI / 6);
                Game.World.MissileList.Add(m);
                m = new Missile(new TileLayer("1", 1, 0), 32, 32, Owner, this, Owner.CurRawPos, Owner.LastDir, 16, 3, -Math.PI / 6);
                Game.World.MissileList.Add(m);
                Owner.IsCasting = false;
            }

            if (CurCastingStage >= CastingStages)
            {
                Finished = true;
                ArtList[0].remove = true;
                ArtList.Clear();
                Owner.IsCasting = false;
            }
        }

        public override void MissileHit(Missile missile, Character target)
        {
            if (target == null) return;

            if (GamePlay.CheckHit(Owner, target, this))
                GamePlay.InfictDamage(Owner, target, GamePlay.DamageCalculation(Owner, target, this));
        }
    }
}
