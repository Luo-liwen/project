
namespace EmptyAsASoir_Re
{
    partial class frmAboutMe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.TextBox();
            this.phoneNo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(367, 447);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 36);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.name.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name.Location = new System.Drawing.Point(460, 110);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(150, 31);
            this.name.TabIndex = 2;
            this.name.Text = "姓名：罗丽雯";
            // 
            // phoneNo
            // 
            this.phoneNo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.phoneNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.phoneNo.Font = new System.Drawing.Font("微软雅黑", 13.8F);
            this.phoneNo.Location = new System.Drawing.Point(460, 194);
            this.phoneNo.Name = "phoneNo";
            this.phoneNo.Size = new System.Drawing.Size(218, 31);
            this.phoneNo.TabIndex = 3;
            this.phoneNo.Text = "电话：13377791928";
            // 
            // frmAboutMe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(881, 552);
            this.Controls.Add(this.phoneNo);
            this.Controls.Add(this.name);
            this.Controls.Add(this.btnOK);
            this.DoubleBuffered = true;
            this.Name = "frmAboutMe";
            this.Text = "关于我";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox phoneNo;
    }
}