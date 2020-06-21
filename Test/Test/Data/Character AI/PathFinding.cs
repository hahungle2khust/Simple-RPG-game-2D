using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    public static class PathFinding
    {
        private static List<Point> Directions = new List<Point>() { new Point(0, -1), new Point(0,1), new Point(-1,0), new Point(1,0) };

        public static List<Point> Find(Point Start, Point End, Character character)
        {
            Dictionary<Point, ASTile> openTiles = new Dictionary<Point, ASTile>();
            Dictionary<Point, ASTile> closeTiles = new Dictionary<Point, ASTile>();
            ASTile curTile = new ASTile(Start, null, End);
            closeTiles.Add(curTile.Position, curTile);

            Point tmpTile = new Point();
            while(curTile.H != 0)
            {
                foreach (Point dir in Directions)
                {
                    tmpTile.X = curTile.Position.X + dir.X;
                    tmpTile.Y = curTile.Position.Y + dir.Y;
                    if(closeTiles.ContainsKey(tmpTile) == false)
                    {
                        if (openTiles.ContainsKey(tmpTile))
                        {
                            if( curTile.G+1 < openTiles[tmpTile].G) openTiles[tmpTile].ChangeParent(curTile);
                        }
                        else
                        {
                            if (Collided(tmpTile, character)) closeTiles.Add(tmpTile, null);
                            else openTiles.Add(tmpTile, new ASTile(tmpTile, curTile, End));
                        }
                    }
                }

                if (openTiles.Count > 0)
                {
                    curTile = openTiles.First().Value;
                    foreach(ASTile at in openTiles.Values)
                    {
                        if (at.F < curTile.F || (at.F == curTile.F && at.H <= curTile.H)) curTile = at;
                    }
                    openTiles.Remove(curTile.Position);
                    closeTiles.Add(curTile.Position, curTile);
                }
                else return null;
            }

            List<Point> path = new List<Point>();
            while( curTile.G != 0)
            {
                path.Add(curTile.Position);
                curTile = curTile.Parent;
            }
            
            if (path.Count < 2) return null;
            path.RemoveAt(path.Count - 1);
            path.Reverse();

            return path;
        }

        private static bool Collided(Point Pos, Character character)
        {
            if( character != null )
            {
                for(int x=0; x<character.charType.Size.X / Globals.TileSize; x += 1)
                {
                    for(int y=0; y<character.charType.Size.Y / Globals.TileSize; y += 1)
                    {
                        if(Game.World.TileList[Pos.X + x, Pos.Y + y].IsBlocked)
                        {
                            return true;
                        }  
                    }
                }
            }
            else
            {
                if (Game.World.TileList[Pos.X, Pos.Y].IsBlocked || GamePlay.GetCharactersInTiles(new Point(Pos.X, Pos.Y)).Count > 0) return true;
            }

            return false;
        }

    }
}
