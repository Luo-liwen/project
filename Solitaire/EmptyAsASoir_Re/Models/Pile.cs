using System.Collections.Generic;
using System.Drawing;

namespace EmptyAsASoir_Re.Models
{
    class Pile
    {
        //用来放牌
        public List<Card> CardsOfPiles = new List<Card>();
        //用来记牌堆坐标
        public Point PointOfPiles;
        //初始化
        public Pile(int x, int y)
        {
            PointOfPiles = new Point(x, y);
        }
    }
}