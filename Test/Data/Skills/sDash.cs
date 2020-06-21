using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    class sDash : BaseSkill
    {
        private int distance = 32;

        public sDash(Character Owner) : base(Owner)
        {
            Power = 130;
            Cost = 20;
            CooldownTime = 5000;
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            CastingTime = 140;
            CastingStages = 7;
            CurCastingStage = 0;
            CastingStageInterval = CastingTime / CastingStages;
            SkillManager.NewCastingSkills.Add(this);

            Art shield = new Art();
            shield.SrcImage = "Shield";
            shield.Frames = 1;
            shield.SrcRect = new Rectangle(0, 0, Globals.TileSize, Globals.TileSize);
            shield.Angle = Owner.LastDir;
            ArtList.Add(shield);
            SetShieldByOwnerPos();
            Game.World.AfterCharArts.Add(shield);

            return true;
        }

        public override void Casting()
        {
            if (CurCastingStage == 1) SoundManager.PlayByFileName("Dash");

            if (GamePlay.IsBlocked(Owner, distance, Owner.LastDir))
                CurCastingStage = CastingStages;
            else
            {
                if (Owner.LastDir == Dir.Up) Owner.OffSet.Y -= distance;
                else if (Owner.LastDir == Dir.Down) Owner.OffSet.Y += distance;
                else if (Owner.LastDir == Dir.Left) Owner.OffSet.X -= distance;
                else if (Owner.LastDir == Dir.Right) Owner.OffSet.X += distance;
                Owner.SetPos();
                Owner.AniFrame += 1;

                SetShieldByOwnerPos();

                List<Character> hitTargets = GamePlay.GetEnemies(Owner.HitBox, Owner.team);
                foreach (Character c in hitTargets)
                {
                    if (GamePlay.CheckHit(Owner, c, this) && targetList.Contains(c) == false)
                    {
                        targetList.Add(c);
                        SoundManager.PlayByFileName("Charge");
                        Art slash = new Art();
                        slash.SrcImage = "Slash";
                        slash.AutoRemove = true;
                        slash.Frames = 6;
                        slash.FrameInterval = 30;
                        slash.SrcRect = new Rectangle(0, 0, Globals.TileSize * 2, Globals.TileSize * 2);
                        slash.Position = new Rectangle(c.CurRawPos.X - 16, c.CurRawPos.Y - 16, Globals.TileSize * 2, Globals.TileSize * 2);
                        ArtList.Add(slash);
                        Game.World.AfterCharArts.Add(slash);
                        GamePlay.InfictDamage(Owner, c, GamePlay.DamageCalculation(Owner, c, this));
                    }
                }
            }

            if (CurCastingStage >= CastingStages)
            {
                Owner.ResetPos();
                Finished = true;
                ArtList[0].remove = true;
                ArtList.Clear();
                targetList.Clear();
                Owner.IsCasting = false;
            }
        }

        private void SetShieldByOwnerPos()
        {
            if (Owner.LastDir == Dir.Up)
            {
                ArtList[0].Position = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y - 12, 32, 32);
                Game.World.BeforeCharArts.Add(ArtList[0]);
            }
            else if (Owner.LastDir == Dir.Down || Owner.LastDir == Dir.Stand)
            {
                ArtList[0].Position = new Rectangle(Owner.CurRawPos.X, Owner.CurRawPos.Y + 12, 32, 32);
                Game.World.AfterCharArts.Add(ArtList[0]);
            }
            else if (Owner.LastDir == Dir.Left)
            {
                ArtList[0].Position = new Rectangle(Owner.CurRawPos.X - 12, Owner.CurRawPos.Y, 32, 32);
                Game.World.AfterCharArts.Add(ArtList[0]);
            }
            else
            {
                ArtList[0].Position = new Rectangle(Owner.CurRawPos.X + 12, Owner.CurRawPos.Y, 32, 32);
                Game.World.AfterCharArts.Add(ArtList[0]);
            }
        }
    }
}
