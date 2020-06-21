using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    static class SkillManager
    {
        public static List<BaseSkill> NewCastingSkills = new List<BaseSkill>();
        public static List<BaseSkill> NewEffectingSkills = new List<BaseSkill>();

        public static void Update()
        {
            if(Game.World != null)
            {
                for (int i = 0; i<Game.World.CastingSkills.Count; i += 1)
                {
                    if(Game.World.CastingSkills[i].Finished || Game.World.CastingSkills[i].FinishedCasting)
                    {
                        var tmpSkill = Game.World.CastingSkills[i];
                        Game.World.CastingSkills.RemoveAt(i);
                        tmpSkill.Reset();
                        i -= 1;
                    }
                }

                for (int i = 0; i < Game.World.EffectingSkills.Count; i += 1)
                {
                    if (Game.World.EffectingSkills[i].Finished)
                    {
                        Game.World.EffectingSkills[i].Reset();
                        Game.World.EffectingSkills.RemoveAt(i);
                        i -= 1;
                    }
                }

                Game.World.CastingSkills.AddRange(NewCastingSkills);
                NewCastingSkills.Clear();
                Game.World.EffectingSkills.AddRange(NewEffectingSkills);
                NewEffectingSkills.Clear();

                foreach (BaseSkill s in Game.World.CastingSkills)
                {
                    s.CastingStageCounter += Game.ElapsedGameTime;
                    if (s.CastingStageCounter >= s.CastingStageInterval)
                    {
                        s.CastingStageCounter = 0;
                        s.CurCastingStage += 1;
                        s.Casting();
                    }
                }
                foreach (BaseSkill s in Game.World.EffectingSkills)
                {
                    s.EffectingStageCounter += Game.ElapsedGameTime;
                    if (s.EffectingStageCounter >= s.EffectingStageInterval)
                    {
                        s.EffectingStageCounter = 0;
                        s.CurEffectingStage += 1;
                        s.Effecting();
                    }
                }
            }
        }
    }
}
