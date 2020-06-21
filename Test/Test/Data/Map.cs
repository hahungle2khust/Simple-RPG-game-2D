using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    class Map
    {
        public Tile[ , ] TileList;
        public string Name;
        public Point Size;

        public List<Character> MapChar = new List<Character>();

        public List<BaseSkill> CastingSkills = new List<BaseSkill>(); //List của những skill đang được nhân vật triển khai
        public List<BaseSkill> EffectingSkills = new List<BaseSkill>(); //List của những skill đã được nhân vật triển khai và đang phát huy hiệu ứng

        public List<Art> BeforeCharArts = new List<Art>();
        public List<Art> AfterCharArts = new List<Art>();

        public Map(string Name, int Width, int Height)
        {
            this.Name = Name;
            this.Size.X = Width;
            this.Size.Y = Height;
            TileList = new Tile[Width + 1, Height + 1];

            for (int i=0; i<=Width; i += 1)
            {
                for (int j=0; j<=Height; j += 1)
                {
                    TileList[i, j] = new Tile();
                }
            }
        }

    }
}
