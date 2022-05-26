using System;
using System.Collections.Generic;
using System.Drawing;

namespace EmptyAsASoir_Re.Models
{
    class Game
    {
        private const int WidthOfCard = 71;
        public const int HeightOfCard = 96;
        public const int HalfCard = HeightOfCard / 3;
        public int OldPile;
        public int NewPile;
        public int selectedIndex;
        public Point seletedPoint;
        public Point pointOfMouseDown;
        public Pile[] Piles = new Pile[17];
        public List<Card> Cards { get; set; }
        public bool IsMove = true;
        public bool IsCountValid1 = true;
        public bool IsCountValid2 = true;
        bool IsAdd;
        int countOfEmptyPiles = 0;
        int countOfEmptyLeftPiles = 0;
        /// <summary>
        /// 初始化
        /// </summary>
        public Game()
        {
            //初始化牌
            Cards = new List<Card>();
            for (int i = 0; i < 52; i++)
            {
                //初始化，1-52，一个数字对应一张牌，index与绘制的图片一一对应
                Card card = new Card(i + 1);
                Cards.Add(card);
            }
            IniPointsOfPiles();//初始化牌堆坐标
            Shuffle();//洗牌
            DealCard();//给下面8堆发牌
            ArrangePoints();//计算每张卡的坐标,后面detect要用到
        }
        /// <summary>
        /// //初始化牌堆坐标
        /// </summary>
        void IniPointsOfPiles()
        {
            //左上角四个牌堆初始坐标
            for (int i = 0; i < 4; i++)
            {
                Piles[i] = new Pile(30 + 2 * i * 50, 30);
            }
            //右上角四个
            for (int i = 4; i < 8; i++)
            {
                Piles[i] = new Pile(415 + (2 * (i - 4) + 1) * 50, 30);
            }
            //下面八个
            for (int i = 8; i < 16; i++)
            {
                Piles[i] = new Pile((2 * (i - 8) + 1) * 50, 150);
            }
            //Piles[16]先随便初始化一下,让它不是空的就行
            Piles[16] = new Pile(0, 0);
        }
        /// <summary>
        /// 洗牌
        /// </summary>
        void Shuffle()
        {
            Random rand = new Random();
            int target;
            for (int i = 0; i < 52; i++)
            {
                //随机抽一张牌和第i+1张牌的index进行交换
                target = rand.Next(0, i);//每次都随机生成一个>=0且<i的随机数
                //值交换
                int temp = Cards[i].index;
                Cards[i].index = Cards[target].index;
                Cards[target].index = temp;
            }
        }
        /// <summary>
        /// 最开始发牌(下面8堆
        /// </summary>
        void DealCard()
        {
            //下方从左至右数，左边四堆每堆7张
            for (int i = 0; i < 7; i++)
            {
                Piles[8].CardsOfPiles.Add(Cards[i]);
            }
            for (int i = 7; i < 14; i++)
            {
                Piles[9].CardsOfPiles.Add(Cards[i]);
            }
            for (int i = 14; i < 21; i++)
            {
                Piles[10].CardsOfPiles.Add(Cards[i]);
            }
            for (int i = 21; i < 28; i++)
            {
                Piles[11].CardsOfPiles.Add(Cards[i]);
            }
            //右边四堆每堆6张
            for (int i = 28; i < 34; i++)
            {
                Piles[12].CardsOfPiles.Add(Cards[i]);
            }
            for (int i = 34; i < 40; i++)
            {
                Piles[13].CardsOfPiles.Add(Cards[i]);
            }
            for (int i = 40; i < 46; i++)
            {
                Piles[14].CardsOfPiles.Add(Cards[i]);
            }
            for (int i = 46; i < 52; i++)
            {
                Piles[15].CardsOfPiles.Add(Cards[i]);
            }
        }
        /// <summary>
        /// 整理一下牌的坐标
        /// Detect要用到
        /// </summary>
        public void ArrangePoints()
        {
            //上面八组，每张牌的坐标与牌堆的坐标相同
            for (int i = 0; i < 8; i++)
            {
                if (Piles[i].CardsOfPiles.Count > 0)
                {
                    Piles[i].CardsOfPiles[Piles[i].CardsOfPiles.Count - 1]._Point = new Point(Piles[i].PointOfPiles.X, Piles[i].PointOfPiles.Y);
                    Piles[i].CardsOfPiles[Piles[i].CardsOfPiles.Count - 1].Pile = i;
                }
            }
            //下面八组+移动的那一组，每张牌的坐标与牌堆的坐标和该牌在牌堆中的位置关联
            for (int i = 8; i < 17; i++)
            {
                if (Piles[i].CardsOfPiles.Count > 0)
                {
                    for (int j = 0; j < Piles[i].CardsOfPiles.Count; j++)
                    {
                        Piles[i].CardsOfPiles[j]._Point = new Point(Piles[i].PointOfPiles.X, Piles[i].PointOfPiles.Y + j * HalfCard);
                        Piles[i].CardsOfPiles[j].Pile = i;
                    }
                }
            }
        }
        /// <summary>
        /// 绘制当前游戏的场景
        /// </summary>
        /// <param name="g"> 绘图句柄</param>
        /// <param name="size">游戏区域的尺寸</param>
        public void Draw(Graphics g, Size size)
        {
            Rectangle rec = new Rectangle(new Point(0, 0), size);
            g.DrawImage(Properties.Resources.background, rec);
            //画几个框
            for (int i = 0; i < 8; i++)
            {
                Pen pen = new Pen(Color.Black, 2);
                g.DrawRectangle(pen, Piles[i].PointOfPiles.X, Piles[i].PointOfPiles.Y, WidthOfCard, HeightOfCard);
            }
            //上面八个
            for (int i = 0; i < 8; i++)
            {
                if (Piles[i].CardsOfPiles.Count != 0)
                {
                    //只画最上面那张
                    Piles[i].CardsOfPiles[Piles[i].CardsOfPiles.Count - 1].Draw(g, new Rectangle
                    {
                        Location = Piles[i].PointOfPiles,
                        Size = new Size(WidthOfCard, HeightOfCard)
                    });
                }
            }
            //下面8个+鼠标移动的那组
            for (int i = 8; i < 17; i++)
            {
                //画牌堆
                for (int j = 0; j < Piles[i].CardsOfPiles.Count; j++)
                {
                    Piles[i].CardsOfPiles[j].Draw(g, new Rectangle
                    {
                        Location = Piles[i].CardsOfPiles[j]._Point,
                        Size = new Size(WidthOfCard, HeightOfCard)
                    });
                }
            }
        }
        /// <summary>
        /// 检测选的是哪张牌，返回能移动的牌的集合
        /// </summary>
        /// <param name="e_x">鼠标坐标横坐标</param>
        /// <param name="e_y">鼠标坐标纵坐标</param>
        /// <returns></returns>
        public List<Card> Detect(int e_x, int e_y)
        {
            //先假设能动
            IsMove = true;
            //等下要返回temp
            List<Card> temp = new List<Card>();
            int jMin = -1;//搞个比1小的数
            //上面8个选中就直接返回
            for (int i = 0; i < 8; i++)
            {
                //如果牌堆里有牌
                if (Piles[i].CardsOfPiles.Count > 0)
                {
                    //且鼠标坐标在牌堆范围内
                    if (e_x - Piles[i].PointOfPiles.X > 0 &&
                       e_x - Piles[i].PointOfPiles.X < WidthOfCard &&
                       e_y - Piles[i].PointOfPiles.Y > 0 &&
                       e_y - Piles[i].PointOfPiles.Y < WidthOfCard)
                    {
                        //就把最上面那张牌加到temp里
                        temp.Add(Piles[i].CardsOfPiles[Piles[i].CardsOfPiles.Count - 1]);
                    }
                }
            }
            //下方8个
            for (int i = 8; i < 16; i++)
            {
                //先确定牌堆
                if (e_x - Piles[i].PointOfPiles.X > 0 &&
                      e_x - Piles[i].PointOfPiles.X < WidthOfCard)
                {
                    //再从下到上遍历牌
                    for (int j = Piles[i].CardsOfPiles.Count - 1; j >= 0; j--)
                    {
                        //最下面那张的判断范围与除它以外的判断范围有所不同，故分情况讨论
                        if (j == Piles[i].CardsOfPiles.Count - 1 &&
                            e_y - Piles[i].CardsOfPiles[j]._Point.Y > 0 &&//需要用到牌的坐标
                            e_y - Piles[i].CardsOfPiles[j]._Point.Y <= HeightOfCard)
                        {
                            //只选中最下面那一张
                            temp.Add(Piles[i].CardsOfPiles[Piles[i].CardsOfPiles.Count - 1]);
                        }
                        else if (e_y - Piles[i].CardsOfPiles[j]._Point.Y > 0 &&
                            e_y - Piles[i].CardsOfPiles[j]._Point.Y <= HalfCard)
                        {
                            //从下到上，记录包括鼠标坐标的牌中最上面那张在牌中的顺序
                            jMin = j;
                        }
                    }
                    //如果是不只选中了一张的情况
                    if (jMin != -1)
                    {
                        for (int j = jMin; j < Piles[i].CardsOfPiles.Count - 1; j++)
                        {
                            ////数字递减且颜色不同
                            if (Piles[i].CardsOfPiles[j].Number == Piles[i].CardsOfPiles[j + 1].Number + 1 &&
                                   Piles[i].CardsOfPiles[j].Status != Piles[i].CardsOfPiles[j + 1].Status)
                            {
                                temp.Add(Piles[i].CardsOfPiles[j]);
                            }
                            else
                            {
                                //如果从上到下加牌的过程中有一张不满足数字递减且颜色不同的条件则不可移动，且返回null
                                IsMove = false;
                                return null;
                            }
                        }
                        //由于上面的只加到倒数第二张，这里再把最下面那张加进去
                        temp.Add(Piles[i].CardsOfPiles[Piles[i].CardsOfPiles.Count - 1]);
                    }
                }
            }
            return temp;
        }
        /// <summary>
        /// MouseDown
        /// </summary>
        /// <param name="e_Location">鼠标坐标</param>
        /// <param name="size">传进来游戏区域大小</param>
        int mouseDownOnce = 0;
        int countOfOldePile = 0;
        public void MouseDown(Point e_Location, Size size)
        {
            pointOfMouseDown = e_Location;//记录传进来的鼠标点击的坐标           
            List<Card> selectedCard = Detect(e_Location.X, e_Location.Y); //返回选中的牌的集合
            //如果返回的非空
            if (selectedCard != null && selectedCard.Count != 0)
            {
                //记下选中的牌的最下面那张的坐标，等等用来算鼠标和牌的相对位置
                seletedPoint = selectedCard[0]._Point;

                //记下原先所在的牌堆
                OldPile = selectedCard[0].Pile;
                countOfOldePile = Piles[OldPile].CardsOfPiles.Count;//后面判断要用
                //右上角4牌堆无法mousedown
                if (OldPile >= 8 || OldPile < 4)
                {
                    selectedIndex = selectedCard[0].index;
                    //画个图比较好理解这里是在干嘛,有点难讲清楚
                    int beforeCount = Piles[OldPile].CardsOfPiles.Count;
                    int startNum = beforeCount - selectedCard.Count;
                    while (Piles[OldPile].CardsOfPiles.Count > beforeCount - selectedCard.Count)
                    {
                        Card temp = Piles[OldPile].CardsOfPiles[startNum];
                        Piles[OldPile].CardsOfPiles.RemoveAt(startNum);
                        Piles[16].CardsOfPiles.Add(temp);
                    }
                }
            }
            mouseDownOnce++;

        }
        /// <summary>
        ///移动的过程
        /// </summary>
        /// <param name="PointMove">传进来鼠标坐标</param>
        /// <param name="size">游戏区大小</param>
        public void MouseMove(Point PointMove, Size size)
        {
            //鼠标移动的那组为空就直接返回
            if (Piles[16].CardsOfPiles.Count == 0)
            {
                return;
            }
            //如果能移
            if (IsMove)
            {
                //鼠标移动的那组牌的最底下那张的坐标，前面记下的selectedPoint发挥作用，使鼠标和选中的牌的左上角在移动过程中始终保持着原来那种相对位置
                Piles[16].PointOfPiles = new Point(seletedPoint.X + PointMove.X - pointOfMouseDown.X,
                  seletedPoint.Y + PointMove.Y - pointOfMouseDown.Y);
                ArrangePoints();
            }
        }
        /// <summary>
        /// 重中之重MouseUp
        /// </summary>
        /// <param name="point">传进来鼠标坐标</param>
        /// <param name="size">游戏区大小</param>
        public void MouseUp(Point point, Size size)
        {
            //如果鼠标移动的牌堆没牌，即相当于没有在移动东西，就不再往下运行
            if (Piles[16].CardsOfPiles.Count < 1)
            {
                return;
            }
            if (Piles[16].CardsOfPiles.Count > 0)
            {
                IsCountValid1 = true;//假设移动的牌数合法           
                IsCountValid2 = true;
                //用来判断能不能加到新牌堆里,以此来达到一个弹回去的效果
                IsAdd = false;
                //如果mouseup的地方有牌，就可以通过这个方法的到要去的新牌堆是什么
                List<Card> MouseUpCard = Detect(point.X, point.Y);
                if (MouseUpCard != null && MouseUpCard.Count > 0)
                {
                    //如果移动的地方有牌，就用第一种计算方法
                    if (Piles[16].CardsOfPiles.Count > CardsCountCanMove1())
                    {
                        IsCountValid1 = false;
                        goto FLAG1;//办法很烂，，，，直接弹回原来的牌堆
                    }
                    //将要去的牌堆
                    NewPile = MouseUpCard[0].Pile;
                    //将要去的牌堆的最下面那一张，要用于判断能否接上
                    Card bottomOfNewPile = Piles[NewPile].CardsOfPiles[Piles[NewPile].CardsOfPiles.Count - 1];
                    //下面8个牌堆
                    if (NewPile >= 8 && NewPile <= 16)
                    {
                        //如果移动的牌堆的最上面那张和牌堆最下面那张满足条件，就能接上
                        if (bottomOfNewPile.Number - 1 == Piles[16].CardsOfPiles[0].Number &&
                       bottomOfNewPile.Status != Piles[16].CardsOfPiles[0].Status)
                        {
                            IsAdd = true;
                            //在移动的牌堆变空之前，把移动的牌堆里的牌添加到新牌堆
                            while (Piles[16].CardsOfPiles.Count > 0)
                            {
                                Card temp = Piles[16].CardsOfPiles[0];
                                Piles[16].CardsOfPiles.RemoveAt(0);
                                Piles[NewPile].CardsOfPiles.Add(temp);
                            }
                            ArrangePoints();
                        }
                    }
                    //右上角四堆(有牌的时候
                    else if (NewPile < 8 && NewPile >= 4)
                    {
                        //数字相同且花色相同，没有固定哪堆放那个花色，根据放的第一张来
                        if (Piles[16].CardsOfPiles.Count == 1 &&
                          bottomOfNewPile.TypeIndex == Piles[16].CardsOfPiles[0].TypeIndex &&
                            bottomOfNewPile.Number + 1 == Piles[16].CardsOfPiles[0].Number)
                        {
                            IsAdd = true;
                            while (Piles[16].CardsOfPiles.Count > 0)
                            {
                                Card temp = Piles[16].CardsOfPiles[0];
                                Piles[16].CardsOfPiles.RemoveAt(0);
                                Piles[NewPile].CardsOfPiles.Add(temp);
                            }
                            ArrangePoints();
                        }
                    }
                }
                //mouseup选中的地方没牌
                else if (MouseUpCard != null && MouseUpCard.Count == 0)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        //左上角四个
                        if (i >= 0 && i < 4)
                        {
                            if (Piles[16].CardsOfPiles.Count == 1 &&//移动的牌堆只能有一张
                                Piles[i].CardsOfPiles.Count < 1 &&//左上角四个顶多有一张
                                point.X - Piles[i].PointOfPiles.X > 0 &&//判断是否在牌堆范围内
                                point.X - Piles[i].PointOfPiles.X < WidthOfCard &&
                                point.Y - Piles[i].PointOfPiles.Y > 0 &&
                                point.Y - Piles[i].PointOfPiles.Y < HeightOfCard)
                            {
                                //能加
                                IsAdd = true;
                                while (Piles[16].CardsOfPiles.Count > 0)
                                {
                                    Card temp = Piles[16].CardsOfPiles[0];
                                    Piles[16].CardsOfPiles.RemoveAt(0);
                                    Piles[i].CardsOfPiles.Add(temp);
                                }
                                ArrangePoints();
                                break;
                            }
                        }
                        //右上角四个
                        else if (i >= 4 && i < 8)
                        {
                            if (Piles[16].CardsOfPiles.Count > 0)
                            {
                                if (Piles[16].CardsOfPiles[0].Number == 1 &&//只能是a
                                    Piles[16].CardsOfPiles.Count == 1 &&//只能移动一张
                                    point.X - Piles[i].PointOfPiles.X > 0 &&//判断是否在牌堆范围内
                                    point.X - Piles[i].PointOfPiles.X < WidthOfCard &&
                                   point.Y - Piles[i].PointOfPiles.Y > 0 &&
                                  point.Y - Piles[i].PointOfPiles.Y < HeightOfCard)
                                {
                                    //能加
                                    IsAdd = true;
                                    while (Piles[16].CardsOfPiles.Count > 0)
                                    {
                                        Card temp = Piles[16].CardsOfPiles[0];
                                        Piles[16].CardsOfPiles.RemoveAt(0);
                                        Piles[i].CardsOfPiles.Add(temp);
                                    }
                                    ArrangePoints();
                                    break;
                                }
                            }
                        }
                        //如果要去的地方没牌，就用第二种计算方法
                        if (Piles[16].CardsOfPiles.Count > CardsCountCanMove2())
                        {
                            IsCountValid2 = false;
                            goto FLAG1;
                        }
                        //下面八堆
                        else if (i >= 8 && i < 16)
                        {
                            if (Piles[i].CardsOfPiles.Count == 0 &&//新牌堆为空
                                point.X - Piles[i].PointOfPiles.X > 0 &&//判断是否在牌堆范围内
                                point.X - Piles[i].PointOfPiles.X < WidthOfCard &&
                               point.Y - Piles[i].PointOfPiles.Y > 0 &&
                              point.Y - Piles[i].PointOfPiles.Y < HeightOfCard)
                            {
                                IsAdd = true;
                                while (Piles[16].CardsOfPiles.Count > 0)
                                {
                                    Card temp = Piles[16].CardsOfPiles[0];
                                    Piles[16].CardsOfPiles.RemoveAt(0);
                                    Piles[i].CardsOfPiles.Add(temp);
                                }
                                ArrangePoints();
                                break;
                            }
                        }
                    }
                }
            //不能加就弹回去，，，
            FLAG1:
                if (!IsAdd)
                {
                    //原路返回
                    while (Piles[16].CardsOfPiles.Count > 0)
                    {
                        Card temp = Piles[16].CardsOfPiles[0];
                        Piles[16].CardsOfPiles.RemoveAt(0);
                        Piles[OldPile].CardsOfPiles.Add(temp);
                    }
                    ArrangePoints();
                }
            }
        }
        /// <summary>
        /// 双击，去上面八个框
        /// </summary>
        /// <param name="e_location"></param>
        /// <param name="size"></param>
        public void DoubleClick(Point e_location, Size size)
        {
            //沉默，，，
            //原路返回，，，，先把mousedown那一步移到Piles[16]的牌移回原位
            while (Piles[16].CardsOfPiles.Count > 0)
            {
                Card temp = Piles[16].CardsOfPiles[0];
                Piles[16].CardsOfPiles.RemoveAt(0);
                Piles[OldPile].CardsOfPiles.Add(temp);
            }
            ArrangePoints();
            //再detect
            List<Card> selectedCards = Detect(e_location.X, e_location.Y);
            //双击只能移动一张
            if (selectedCards != null)
            {
                if (selectedCards.Count != 1)
                {
                    return;
                }
                //双击只对下面八个牌堆起作用
                if (OldPile >= 8 && OldPile < 16)
                {
                    //先判断能不能上右边！！！
                    for (int i = 4; i < 8; i++)
                    {
                        //如果牌堆不为空,选中的那张牌的花色和数字与右边牌堆的最上面那一张进行对比
                        if (Piles[i].CardsOfPiles.Count > 0)
                        {
                            //数字递增，花色相同
                            if (selectedCards[0].TypeIndex == Piles[i].CardsOfPiles[Piles[i].CardsOfPiles.Count - 1].TypeIndex &&
                               selectedCards[0].Number == Piles[i].CardsOfPiles[Piles[i].CardsOfPiles.Count - 1].Number + 1)
                            {
                                //移
                                Card temp = selectedCards[0];
                                Piles[i].CardsOfPiles.Add(temp);
                                Piles[OldPile].CardsOfPiles.RemoveAt(Piles[OldPile].CardsOfPiles.Count - 1);
                                //把牌从selectedCards里移走，等下要拿这个来判断要不要进行下一步
                                selectedCards.RemoveAt(0);
                                break;
                            }
                        }
                        //如果牌堆为空，当且仅当数字为1时能放上去
                        else if (Piles[i].CardsOfPiles.Count == 0 && selectedCards[selectedCards.Count - 1].Number == 1)
                        {
                            Card temp = selectedCards[selectedCards.Count - 1];
                            Piles[i].CardsOfPiles.Add(temp);
                            Piles[OldPile].CardsOfPiles.RemoveAt(Piles[OldPile].CardsOfPiles.Count - 1);
                            selectedCards.RemoveAt(0);
                            break;
                        }
                    }
                    //如果selectedCards里还有牌，即上不了右边，再判断能不能上左边
                    if (selectedCards.Count == 1)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            //左上角的牌堆没有牌就能加
                            if (Piles[i].CardsOfPiles.Count == 0)
                            {
                                Card temp = selectedCards[0];
                                Piles[i].CardsOfPiles.Add(temp);
                                Piles[OldPile].CardsOfPiles.RemoveAt(Piles[OldPile].CardsOfPiles.Count - 1);
                                selectedCards.RemoveAt(0);
                                break;
                            }
                        }
                    }
                    ArrangePoints();
                }
            }
        }
        /// <summary>
        /// 判断游戏是否结束
        /// </summary>
        /// <returns></returns>
        public bool IsGameOver()
        {
            var temp = true;
            for (int i = 4; i < 8; i++)
            {
                //右上角四个牌堆都是13张就算游戏结束
                if (Piles[i].CardsOfPiles.Count != 13)
                {
                    temp = false;
                    break;
                }
            }
            return temp;
        }
        /// <summary>
        /// 当前一次性最多能移动的牌的数量(要把牌移到有牌的地方
        /// </summary>
        /// <returns></returns>
        public int CardsCountCanMove1()
        {
            //左上角四个为空的个数
            //  var countOfEmptyLeftPiles = 0;
            countOfEmptyLeftPiles = 0;
            countOfEmptyPiles = 0;
            for (int i = 0; i < 4; i++)
            {
                if (Piles[i].CardsOfPiles.Count == 0)
                {
                    countOfEmptyLeftPiles++;
                }
            }
            //下方8个为空的个数
            for (int i = 8; i < 16; i++)
            {
                if (Piles[i].CardsOfPiles.Count == 0)
                {
                    countOfEmptyPiles++;
                }
            }
            //如果一整列移动，那么会把移了之后空的那个牌堆也算进去,如果算进去后大于1，需要-1
            //Piles[16].CardsOfPiles.Count == countOfOldePile用于判断是否移动一整列
            if (countOfEmptyPiles > 0 && OldPile >= 8 && OldPile < 16 && Piles[16].CardsOfPiles.Count == countOfOldePile)
            {
                return (int)Math.Pow(2, countOfEmptyPiles - 1) * (countOfEmptyLeftPiles + 1);
            }
            return (int)Math.Pow(2, countOfEmptyPiles) * (countOfEmptyLeftPiles + 1);
        }
        /// <summary>
        /// 当前一次性最多能移动的牌的数量(要把牌移到没有牌的地方
        /// </summary>
        /// <returns></returns>
        public int CardsCountCanMove2()
        {
            //左上角四个为空的个数
            countOfEmptyLeftPiles = 0;
            countOfEmptyPiles = 0;
            for (int i = 0; i < 4; i++)
            {
                if (Piles[i].CardsOfPiles.Count == 0)
                {
                    countOfEmptyLeftPiles++;
                }
            }
            //下方8个为空的个数
            for (int i = 8; i < 16; i++)
            {
                if (Piles[i].CardsOfPiles.Count == 0)
                {
                    countOfEmptyPiles++;
                }
            }
            //如果下面八个有空的，就返回下面八个中除移动的那列牌原先所在的牌堆外为空的个数*（左上角为空的个数+1）
            if (countOfEmptyPiles > 0)
            {
                //如果一整列移动，那么会把移了之后空的那个牌堆也算进去,如果算进去后大于1，需要-1
                if (countOfEmptyPiles > 1 && OldPile >= 8 && OldPile < 16 && Piles[16].CardsOfPiles.Count == countOfOldePile)
                {
                    return (countOfEmptyPiles - 1) * (countOfEmptyLeftPiles + 1);
                }
                return countOfEmptyPiles * (countOfEmptyLeftPiles + 1);
            }
            //如果下面八个没有空的，就返回   左上角四个为空的个数+1
            return countOfEmptyLeftPiles + 1;
        }
    }
}
