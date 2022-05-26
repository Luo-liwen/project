
namespace EmptyAsASoir_Re
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStripOfGame = new System.Windows.Forms.MenuStrip();
            this.NewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMe = new System.Windows.Forms.ToolStripMenuItem();
            this.gameArea = new EmptyAsASoir_Re.UserControlNoBlinks();
            this.menuStripOfGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripOfGame
            // 
            this.menuStripOfGame.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripOfGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewGame,
            this.AboutMe});
            this.menuStripOfGame.Location = new System.Drawing.Point(0, 0);
            this.menuStripOfGame.Name = "menuStripOfGame";
            this.menuStripOfGame.Size = new System.Drawing.Size(1152, 28);
            this.menuStripOfGame.TabIndex = 0;
            this.menuStripOfGame.Text = "menuStrip1";
            // 
            // NewGame
            // 
            this.NewGame.Name = "NewGame";
            this.NewGame.Size = new System.Drawing.Size(68, 24);
            this.NewGame.Text = "新游戏";
            this.NewGame.Click += new System.EventHandler(this.NewGame_Click);
            // 
            // AboutMe
            // 
            this.AboutMe.Name = "AboutMe";
            this.AboutMe.Size = new System.Drawing.Size(68, 24);
            this.AboutMe.Text = "关于我";
            this.AboutMe.Click += new System.EventHandler(this.AboutMe_Click);
            // 
            // gameArea
            // 
            this.gameArea.AutoSize = true;
            this.gameArea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gameArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameArea.Location = new System.Drawing.Point(0, 28);
            this.gameArea.Name = "gameArea";
            this.gameArea.Size = new System.Drawing.Size(1152, 1027);
            this.gameArea.TabIndex = 1;
            this.gameArea.Load += new System.EventHandler(this.gameArea_Load);
            this.gameArea.Paint += new System.Windows.Forms.PaintEventHandler(this.gameArea_Paint);
            this.gameArea.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gameArea_MouseDoubleClick);
            this.gameArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gameArea_MouseDown);
            this.gameArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gameArea_MouseMove);
            this.gameArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gameArea_MouseUp);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 1055);
            this.Controls.Add(this.gameArea);
            this.Controls.Add(this.menuStripOfGame);
            this.MainMenuStrip = this.menuStripOfGame;
            this.MaximumSize = new System.Drawing.Size(1170, 1500);
            this.MinimumSize = new System.Drawing.Size(1170, 1078);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "空当接龙";
            this.menuStripOfGame.ResumeLayout(false);
            this.menuStripOfGame.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripOfGame;
        private System.Windows.Forms.ToolStripMenuItem NewGame;
        private System.Windows.Forms.ToolStripMenuItem AboutMe;
        private UserControlNoBlinks gameArea;
    }
}

