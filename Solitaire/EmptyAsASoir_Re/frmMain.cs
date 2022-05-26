using EmptyAsASoir_Re.Models;
using System;
using System.Windows.Forms;

namespace EmptyAsASoir_Re
{
    public partial class frmMain : Form
    {
        private Game _game;
        public frmMain()
        {
            InitializeComponent();
        }
        private void NewGame_Click(object sender, EventArgs e)
        {
            //重置游戏
            _game = new Game();
            gameArea.Invalidate();
        }
        private void gameArea_Paint(object sender, PaintEventArgs e)
        {
            //游戏已经初始化了
            if (_game != null)
            {
                _game.Draw(e.Graphics, this.gameArea.Size);
            }
        }
        private void gameArea_Load(object sender, EventArgs e)
        {
            //初始化
            _game = new Game();
        }
        private void gameArea_MouseDown(object sender, MouseEventArgs e)
        {
            //游戏初始化了才进行
            if (_game != null)
            {
                //将鼠标坐标和游戏区域大小传入
                _game.MouseDown(e.Location, gameArea.Size);   
            }
        }
        private void gameArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (_game != null)
            {
                if (_game.Piles[16].CardsOfPiles.Count == 0)
                {
                    return;
                }
                //将鼠标坐标和游戏区域大小传入
                _game.MouseMove(e.Location, gameArea.Size);
            }
            this.gameArea.Invalidate();
        }
        private void gameArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (_game != null)
            {
                if (_game.Piles[16].CardsOfPiles.Count == 0)
                {
                    return;
                }
                //将鼠标坐标和游戏区域大小传入
                _game.MouseUp(e.Location, gameArea.Size);
                this.gameArea.Invalidate();
                //如果移动非法就显示一下最多能移动多少张
                //情况一，移动到有牌的地方
                if (_game.IsCountValid1 == false)
                {
                    MessageBox.Show($"移动到有牌的地方只能移{_game.CardsCountCanMove1()}张");
                    return;
                }
                //情况二,移动到空牌堆
                if (_game.OldPile>=8&& _game.OldPile<16 && _game.IsCountValid2 == false)
                {
                    MessageBox.Show($"移动到没有牌的地方只能移{_game.CardsCountCanMove2()}张");
                    return;
                }
            }
            if (_game.IsGameOver())
            {
                //游戏结束
                MessageBox.Show("game over");
                _game = new Game();
                this.gameArea.Invalidate();
            }
        }
        private void gameArea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_game != null)
            {
                //将鼠标坐标和游戏区域大小传入
                _game.DoubleClick(gameArea.PointToClient(Cursor.Position), gameArea.Size);
                gameArea.Invalidate();
                //双击也要判断什么时候游戏结束
                if (_game.IsGameOver())
                {
                    //游戏结束
                    MessageBox.Show("game over");
                    _game = new Game();
                    this.gameArea.Invalidate();
                }
            }
        }
        private void AboutMe_Click(object sender, EventArgs e)
        {
            frmAboutMe frm = new frmAboutMe();
            frm.ShowDialog();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //怕删了出问题就没删
        }
    }
}
