using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    public class sCharge : BaseSkill
    {
        protected int distance = 32;
        protected string voice = "None";

        public sCharge(Character Owner) : base(Owner)
        {
            Power = 150;
            Cost = 20;
            CooldownTime = 5000;
            if(Owner.charType.Index < 3 || Owner.charType.Index > 6)
            {
                Cost = 0;
                voice = "ChargeVoice";
            }
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            CastingTime = 140;
            CastingStages = 7;
            CurCastingStage = 0;
            CastingStageInterval = CastingTime / CastingStages;
            SkillManager.NewCastingSkills.Add(this);
            SoundManager.PlayByFileName(voice);

            return true;
        }

        public override void Casting()
        {
            if (CurCastingStage == 1)
            {
                SoundManager.PlayByFileName("Dash");
                ChangeFrameWhenCharging();
            }

            if (GamePlay.IsBlocked(Owner, distance, Owner.LastDir))
            {
                CurCastingStage = CastingStages;
            }
            else
            {
                if (Owner.LastDir == Dir.Up) Owner.OffSet.Y -= distance;
                else if (Owner.LastDir == Dir.Down) Owner.OffSet.Y += distance;
                else if (Owner.LastDir == Dir.Left) Owner.OffSet.X -= distance;
                else if (Owner.LastDir == Dir.Right) Owner.OffSet.X += distance;
                Owner.SetPos();
                Owner.AniFrame += 1;

                List<Character> hitTargets = GamePlay.GetEnemies(Owner.HitBox, Owner.team);
                if (hitTargets.Count > 0)
                {
                    SoundManager.PlayByFileName("Explosion");
                    Rectangle area = new Rectangle(Owner.CurRawPos.X - 12, Owner.CurRawPos.Y - 12, 56, 56);

                    Art explosion = new Art();
                    explosion.SrcImage = "Explosion";
                    explosion.AutoRemove = true;
                    explosion.Frames = 7;
                    explosion.FrameInterval = 50;
                    explosion.SrcRect = new Rectangle(0, 0, Globals.TileSize * 2, Globals.TileSize * 2);
                    explosion.Position = area;
                    Game.World.AfterCharArts.Add(explosion);

                    List<Character> targets = GamePlay.GetEnemies(area, Owner.team);
                    foreach (Character c in hitTargets)
                    {
                        if (GamePlay.CheckHit(Owner, c, this))
                            GamePlay.InfictDamage(Owner, c, GamePlay.DamageCalculation(Owner, c, this));
                    }

                    CurCastingStage = CastingStages;
                }
            }

            if (CurCastingStage >= CastingStages)
            {
                Owner.ResetPos();
                Finished = true;
                Owner.IsCasting = false;
            }
        }

        public override bool CanUse()
        {
            if (base.CanUse() == false) return false;

            Rectangle area = new Rectangle();
            int range = 7 - 1;

            if (Owner.LastDir == Dir.Up) area = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y - range * Globals.TileSize, Globals.TileSize, range * Globals.TileSize);
            else if (Owner.LastDir == Dir.Down || Owner.LastDir == Dir.Stand) area = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y + Owner.charType.Size.Y, Globals.TileSize, range * Globals.TileSize);
            else if (Owner.LastDir == Dir.Left) area = new Rectangle(Owner.CurRawPos.X - range * Globals.TileSize, Owner.CurRawPos.Y, range * Globals.TileSize, Globals.TileSize);
            else if (Owner.LastDir == Dir.Right) area = new Rectangle(Owner.CurRawPos.X + Owner.charType.Size.X, Owner.CurRawPos.Y, range * Globals.TileSize, Globals.TileSize);

            if (GamePlay.GetEnemies(area, Owner.team).Count > 0)
                return true;

            return false;
        }

        protected virtual void ChangeFrameWhenCharging()
        {

        }
    }
}
