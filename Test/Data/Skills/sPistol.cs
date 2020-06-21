using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    class sPistol : BaseSkill
    {
        private List<string> sounds = new List<string>() { "Shoot1", "Shoot2", "Shoot3" };
        
        public sPistol(Character Owner) : base(Owner)
        {
            Power = 100;
            Cost = 5;
            CooldownTime = 400;
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            CastingTime = 200;
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
                newArt.SrcImage = "Pistol";
                newArt.SrcRect = new Rectangle(0, 0, Globals.TileSize, Globals.TileSize);
                newArt.Angle = Owner.LastDir;
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
                Missile m = new Missile(new TileLayer("1", 0, 0), 32, 32, Owner, this, Owner.CurRawPos, Owner.LastDir, 16, 5, 0);
                Game.World.MissileList.Add(m);
                Owner.IsCasting = false;
                SoundManager.PlayByFileName(sounds[Globals.gen.Next(0, sounds.Count)]);
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
