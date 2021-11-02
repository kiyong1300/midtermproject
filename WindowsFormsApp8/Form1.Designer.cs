
namespace WindowsFormsApp8
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.player = new System.Windows.Forms.PictureBox();
            this.txtlife = new System.Windows.Forms.Label();
            this.Monster1Timer = new System.Windows.Forms.Timer(this.components);
            this.FadeTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 25;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimerEvent);
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.Color.Transparent;
            this.player.Image = global::WindowsFormsApp8.Properties.Resources.player_back;
            this.player.Location = new System.Drawing.Point(379, 221);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(44, 55);
            this.player.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.player.TabIndex = 0;
            this.player.TabStop = false;
            // 
            // txtlife
            // 
            this.txtlife.AutoSize = true;
            this.txtlife.BackColor = System.Drawing.Color.Transparent;
            this.txtlife.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtlife.ForeColor = System.Drawing.Color.OldLace;
            this.txtlife.Location = new System.Drawing.Point(83, 26);
            this.txtlife.Name = "txtlife";
            this.txtlife.Size = new System.Drawing.Size(93, 24);
            this.txtlife.TabIndex = 1;
            this.txtlife.Text = "Life : 0";
            // 
            // Monster1Timer
            // 
            this.Monster1Timer.Enabled = true;
            this.Monster1Timer.Tick += new System.EventHandler(this.Monster1Timer_Tick);
            // 
            // FadeTimer
            // 
            this.FadeTimer.Tick += new System.EventHandler(this.FadeTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackgroundImage = global::WindowsFormsApp8.Properties.Resources.map;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(809, 495);
            this.Controls.Add(this.txtlife);
            this.Controls.Add(this.player);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label txtlife;
        private System.Windows.Forms.Timer Monster1Timer;
        private System.Windows.Forms.Timer FadeTimer;
    }
}

