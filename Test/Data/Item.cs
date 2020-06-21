using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    [Serializable]
    public class Item
    {
        public int Index;
        public string Name = "";
        public TileLayer Source;
        public int Amount;
        public bool AsPercent;
        public int Count;
        public Character Owner;
        public Rectangle LocInMap;
        public int EffectingStages;
        public int CurStage;
        public int StageInterval;
        public int StageTimer;

        public static List<Item> EffectingItems = new List<Item>();

        public Item(Character Owner)
        {
            this.Owner = Owner;
        }

        public virtual bool Use()
        {
            if (Count == 0) return false;

            Count -= 1;
            EffectingItems.Add(this);

            return true;
        }

        public virtual void Update() { }

        public static void UpdateList()
        {
            for(int i=0; i<EffectingItems.Count; i += 1)
            {
                if (EffectingItems[i].CurStage >= EffectingItems[i].EffectingStages)
                {
                    EffectingItems.RemoveAt(i);
                    i -= 1;
                }
                else
                {
                    EffectingItems[i].StageTimer += Game.ElapsedGameTime;
                    if(EffectingItems[i].StageTimer >= EffectingItems[i].StageInterval)
                    {
                        EffectingItems[i].StageTimer = 0;
                        EffectingItems[i].CurStage += 1;
                        EffectingItems[i].Update();
                    }
                }
            }
        }
    }
}
