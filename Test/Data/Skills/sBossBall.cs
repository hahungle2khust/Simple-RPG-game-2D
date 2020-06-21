using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    public class sBossBall : BaseSkill
    {
        public sBossBall(Character Owner) : base(Owner)
        {
            //Set thông số
            CooldownTime = 73000;
            Power = 1000;
        }

        public override bool Cast()
        {
            if (base.Cast() == false) return false;

            CastingTime = 8000;
            CastingStages = 160;
            CurCastingStage = 0;
            CastingStageInterval = CastingTime / CastingStages;
            SkillManager.NewCastingSkills.Add(this);
            SoundManager.PlayByFileName("Laugh");
            Owner.LastDir = 0;
            Owner.charType.Source.srcPos.Y = 4;
            Casting();

            return true;
        }

        public override void Casting()
        {
            Owner.AniFrame += 1;

            foreach(Art a in ArtList)
            {
                a.Position.Y += Globals.TileSize * 2;
                a.SkillCounter += 1;
                if(a.SkillCounter >= 5 && !a.remove)
                {
                    a.remove = true;
                    SoundManager.PlayByFileName("Explosion");
                    Rectangle area = new Rectangle(a.Position.X + 8, a.Position.Y + 8, a.Position.Width - 16, a.Position.Height - 16);

                    Art explosion = new Art();
                    explosion.SrcImage = "Explosion";
                    explosion.AutoRemove = true;
                    explosion.Frames = 7;
                    explosion.FrameInterval = 50;
                    explosion.SrcRect = new Rectangle(0, 0, Globals.TileSize * 2, Globals.TileSize * 2);
                    explosion.Position = a.Position;
                    Game.World.AfterCharArts.Add(explosion);

                    List<Character> hitTargets = GamePlay.GetEnemies(area, Team.Enemy);
                    foreach (Character c in hitTargets)
                    {
                        if (GamePlay.CheckHit(Owner, c, this))
                            GamePlay.InfictDamage(Owner, c, GamePlay.DamageCalculation(Owner, c, this));
                    }
                }
            }

            if(CurCastingStage % 5 == 0 && CurCastingStage < CastingStages - 10)
            {
                SoundManager.PlayByFileName("ThrowBomb");
                Art newArt = new Art();
                newArt.SrcImage = "Ball";
                newArt.SrcRect = new Rectangle(0, 0, Globals.TileSize * 4, Globals.TileSize * 4);
                Rectangle loc = new Rectangle( 
                    Globals.gen.Next(Game.Player.CurRawPos.X - 3 * Globals.TileSize, Game.Player.CurRawPos.X + Game.Player.charType.Size.X + 2 * Globals.TileSize), 
                    Globals.gen.Next(Game.Player.CurRawPos.Y - 3 * Globals.TileSize, Game.Player.CurRawPos.Y + Game.Player.charType.Size.Y + 2 * Globals.TileSize), 
                    Globals.TileSize * 4, Globals.TileSize * 4);
                newArt.Position = new Rectangle(loc.X, loc.Y - 10 * Globals.TileSize, loc.Width, loc.Height);
                ArtList.Add(newArt);
                Game.World.AfterCharArts.Add(newArt);
            }

            if(CurCastingStage % 20 == 0)
                SoundManager.PlayByFileName("Laugh");

            if (CurCastingStage >= CastingStages)
            {
                Finished = true;
                foreach(Art a in ArtList)
                    a.remove = true;
                ArtList.Clear();
                Owner.IsCasting = false;
                Owner.charType.Source.srcPos.Y = 0;
            }
        }

        public override bool CanUse()
        {
            if (base.CanUse() == false) return false;

            Rectangle area = new Rectangle();
            int range = 10 - 1;

            area = new Rectangle(Owner.CurRawPos.X - range * Globals.TileSize, Owner.CurRawPos.Y - range * Globals.TileSize, Owner.charType.Size.X + 2 * range * Globals.TileSize, Owner.charType.Size.Y + 2 * range * Globals.TileSize);
            if (GamePlay.GetEnemies(area, Owner.team).Count > 0) return true;

            return false;
        }
    }
}
