using System.Drawing;

namespace EmptyAsASoir_Re.Models
{
    //颜色
    enum CardStatus
    {
        Black, Red
    }
    class Card
    {
        //牌的index，从1到52
        public int index { get; set; }
        //根据index来设置颜色
        public CardStatus Status {
            get
            {
                if (index > 0 && index <= 26)
                    return CardStatus.Black;
                return CardStatus.Red;
            }
        }
        //根据index得到牌上印的数字
        public int Number
        {
            get
            {
                if (index < 14) return index;
                else if (index < 27) return index - 13;
                else if (index < 40) return index - 26;
                else return index - 39;
            }
        }
        //根据index得到花色
        public int TypeIndex
        {
            get
            {
                if (index <= 13) return 0;
                else if (index <= 26) return 1;
                else if (index <= 39) return 2;
                else return 3;
            }
        }     
        //在第几堆牌里,0-16
        public int Pile { get; set; }
        //在牌里是第几个
        public int IndexOfPile { get; set; }
        //牌的坐标，用于Game的Draw
        public Point _Point { get; set; }
        //用数字给牌初始化
        public Card(int num)
        {
            index = num;            
        }
        //绘制牌的image，通过index与图片（以_1-_52命名）对应
        public void Draw(Graphics g, Rectangle rec)
        {
            var st = $"_{index}";
            Image img = (Image)Properties.Resources.ResourceManager.GetObject(st);
            g.FillRectangle(new SolidBrush(Color.Black), rec);
            g.DrawImage(img, rec);            
            }
        }
    }
