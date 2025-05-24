namespace Chaos_Stage
{
    partial class gameForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.title = new System.Windows.Forms.Label();
            this.startGameBtn = new System.Windows.Forms.Button();
            this.gameInstructionBtn = new System.Windows.Forms.Button();
            this.mainMenuQuitBtn = new System.Windows.Forms.Button();
            this.mainMenuPanel = new System.Windows.Forms.Panel();
            this.gamePlayLevel1Panel = new System.Windows.Forms.Panel();
            this.nextLevelPanel = new System.Windows.Forms.Panel();
            this.nextLevelQuitBtn = new System.Windows.Forms.Button();
            this.nextLevelBtn = new System.Windows.Forms.Button();
            this.nextLevelTitle = new System.Windows.Forms.Label();
            this.ground5 = new System.Windows.Forms.PictureBox();
            this.ground4 = new System.Windows.Forms.PictureBox();
            this.ground3 = new System.Windows.Forms.PictureBox();
            this.ground2 = new System.Windows.Forms.PictureBox();
            this.ground1 = new System.Windows.Forms.PictureBox();
            this.goal = new System.Windows.Forms.PictureBox();
            this.bullet = new System.Windows.Forms.PictureBox();
            this.spike1 = new System.Windows.Forms.PictureBox();
            this.enemy1 = new System.Windows.Forms.PictureBox();
            this.character = new System.Windows.Forms.PictureBox();
            this.background = new System.Windows.Forms.PictureBox();
            this.rightside = new System.Windows.Forms.Label();
            this.leftside = new System.Windows.Forms.Label();
            this.ground0 = new System.Windows.Forms.PictureBox();
            this.gameInstructionPenal = new System.Windows.Forms.Panel();
            this.gameInstruction = new System.Windows.Forms.Label();
            this.goBackBtn = new System.Windows.Forms.Button();
            this.gameInstructionTitle = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.gameOverPanel = new System.Windows.Forms.Panel();
            this.gameOverQuitBtn = new System.Windows.Forms.Button();
            this.retryBtn = new System.Windows.Forms.Button();
            this.gameover = new System.Windows.Forms.Label();
            this.gamePlayLevel2Panel = new System.Windows.Forms.Panel();
            this.gamePlayLevel3Panel = new System.Windows.Forms.Panel();
            this.gamePlayLevel4Panel = new System.Windows.Forms.Panel();
            this.level5Timer = new System.Windows.Forms.Timer(this.components);
            this.blockPanel = new System.Windows.Forms.Panel();
            this.gamePlayLevel5Panel = new System.Windows.Forms.Panel();
            this.gamePlayLevel6Panel = new System.Windows.Forms.Panel();
            this.mainMenuPanel.SuspendLayout();
            this.gamePlayLevel1Panel.SuspendLayout();
            this.nextLevelPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ground5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bullet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spike1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.character)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.background)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground0)).BeginInit();
            this.gameInstructionPenal.SuspendLayout();
            this.gameOverPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("微軟正黑體", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.title.Location = new System.Drawing.Point(811, 172);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(241, 67);
            this.title.TabIndex = 0;
            this.title.Text = "初始標題";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startGameBtn
            // 
            this.startGameBtn.Location = new System.Drawing.Point(746, 401);
            this.startGameBtn.Name = "startGameBtn";
            this.startGameBtn.Size = new System.Drawing.Size(370, 113);
            this.startGameBtn.TabIndex = 1;
            this.startGameBtn.Text = "進入遊戲";
            this.startGameBtn.UseVisualStyleBackColor = true;
            this.startGameBtn.Click += new System.EventHandler(this.startGameBtn_Click);
            // 
            // gameInstructionBtn
            // 
            this.gameInstructionBtn.Location = new System.Drawing.Point(746, 566);
            this.gameInstructionBtn.Name = "gameInstructionBtn";
            this.gameInstructionBtn.Size = new System.Drawing.Size(370, 113);
            this.gameInstructionBtn.TabIndex = 2;
            this.gameInstructionBtn.Text = "遊戲說明";
            this.gameInstructionBtn.UseVisualStyleBackColor = true;
            this.gameInstructionBtn.Click += new System.EventHandler(this.gameInstructionBtn_Click);
            // 
            // mainMenuQuitBtn
            // 
            this.mainMenuQuitBtn.Location = new System.Drawing.Point(746, 726);
            this.mainMenuQuitBtn.Name = "mainMenuQuitBtn";
            this.mainMenuQuitBtn.Size = new System.Drawing.Size(370, 113);
            this.mainMenuQuitBtn.TabIndex = 3;
            this.mainMenuQuitBtn.Text = "退出";
            this.mainMenuQuitBtn.UseVisualStyleBackColor = true;
            this.mainMenuQuitBtn.Click += new System.EventHandler(this.quitBtn_Click);
            // 
            // mainMenuPanel
            // 
            this.mainMenuPanel.Controls.Add(this.title);
            this.mainMenuPanel.Controls.Add(this.mainMenuQuitBtn);
            this.mainMenuPanel.Controls.Add(this.startGameBtn);
            this.mainMenuPanel.Controls.Add(this.gameInstructionBtn);
            this.mainMenuPanel.Location = new System.Drawing.Point(0, 0);
            this.mainMenuPanel.Name = "mainMenuPanel";
            this.mainMenuPanel.Size = new System.Drawing.Size(1892, 1006);
            this.mainMenuPanel.TabIndex = 4;
            // 
            // gamePlayLevel1Panel
            // 
            this.gamePlayLevel1Panel.Controls.Add(this.ground5);
            this.gamePlayLevel1Panel.Controls.Add(this.ground4);
            this.gamePlayLevel1Panel.Controls.Add(this.ground3);
            this.gamePlayLevel1Panel.Controls.Add(this.ground2);
            this.gamePlayLevel1Panel.Controls.Add(this.ground1);
            this.gamePlayLevel1Panel.Controls.Add(this.goal);
            this.gamePlayLevel1Panel.Controls.Add(this.bullet);
            this.gamePlayLevel1Panel.Controls.Add(this.spike1);
            this.gamePlayLevel1Panel.Controls.Add(this.enemy1);
            this.gamePlayLevel1Panel.Controls.Add(this.character);
            this.gamePlayLevel1Panel.Controls.Add(this.background);
            this.gamePlayLevel1Panel.Controls.Add(this.rightside);
            this.gamePlayLevel1Panel.Controls.Add(this.leftside);
            this.gamePlayLevel1Panel.Controls.Add(this.ground0);
            this.gamePlayLevel1Panel.Location = new System.Drawing.Point(0, 0);
            this.gamePlayLevel1Panel.Name = "gamePlayLevel1Panel";
            this.gamePlayLevel1Panel.Size = new System.Drawing.Size(1892, 977);
            this.gamePlayLevel1Panel.TabIndex = 4;
            this.gamePlayLevel1Panel.Visible = false;
            // 
            // nextLevelPanel
            // 
            this.nextLevelPanel.Controls.Add(this.nextLevelQuitBtn);
            this.nextLevelPanel.Controls.Add(this.nextLevelBtn);
            this.nextLevelPanel.Controls.Add(this.nextLevelTitle);
            this.nextLevelPanel.Location = new System.Drawing.Point(658, 127);
            this.nextLevelPanel.Name = "nextLevelPanel";
            this.nextLevelPanel.Size = new System.Drawing.Size(649, 565);
            this.nextLevelPanel.TabIndex = 19;
            this.nextLevelPanel.Visible = false;
            // 
            // nextLevelQuitBtn
            // 
            this.nextLevelQuitBtn.Font = new System.Drawing.Font("微軟正黑體", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nextLevelQuitBtn.Location = new System.Drawing.Point(238, 380);
            this.nextLevelQuitBtn.Name = "nextLevelQuitBtn";
            this.nextLevelQuitBtn.Size = new System.Drawing.Size(163, 86);
            this.nextLevelQuitBtn.TabIndex = 2;
            this.nextLevelQuitBtn.Text = "返回";
            this.nextLevelQuitBtn.UseVisualStyleBackColor = true;
            this.nextLevelQuitBtn.Click += new System.EventHandler(this.nextLevelQuitBtn_Click);
            // 
            // nextLevelBtn
            // 
            this.nextLevelBtn.Font = new System.Drawing.Font("微軟正黑體", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nextLevelBtn.Location = new System.Drawing.Point(238, 258);
            this.nextLevelBtn.Name = "nextLevelBtn";
            this.nextLevelBtn.Size = new System.Drawing.Size(163, 86);
            this.nextLevelBtn.TabIndex = 1;
            this.nextLevelBtn.Text = "下一關";
            this.nextLevelBtn.UseVisualStyleBackColor = true;
            this.nextLevelBtn.Click += new System.EventHandler(this.nextLevelBtn_Click);
            // 
            // nextLevelTitle
            // 
            this.nextLevelTitle.AutoSize = true;
            this.nextLevelTitle.Font = new System.Drawing.Font("微軟正黑體", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nextLevelTitle.Location = new System.Drawing.Point(226, 112);
            this.nextLevelTitle.Name = "nextLevelTitle";
            this.nextLevelTitle.Size = new System.Drawing.Size(213, 67);
            this.nextLevelTitle.TabIndex = 0;
            this.nextLevelTitle.Text = "過關!!!";
            // 
            // ground5
            // 
            this.ground5.Image = global::Chaos_Stage.Properties.Resources.地板素材;
            this.ground5.Location = new System.Drawing.Point(1423, 159);
            this.ground5.Name = "ground5";
            this.ground5.Size = new System.Drawing.Size(469, 80);
            this.ground5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ground5.TabIndex = 15;
            this.ground5.TabStop = false;
            this.ground5.Tag = "platform";
            // 
            // ground4
            // 
            this.ground4.Image = global::Chaos_Stage.Properties.Resources.地板素材;
            this.ground4.Location = new System.Drawing.Point(746, 134);
            this.ground4.Name = "ground4";
            this.ground4.Size = new System.Drawing.Size(469, 80);
            this.ground4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ground4.TabIndex = 17;
            this.ground4.TabStop = false;
            this.ground4.Tag = "platform";
            // 
            // ground3
            // 
            this.ground3.Image = global::Chaos_Stage.Properties.Resources.地板素材;
            this.ground3.Location = new System.Drawing.Point(115, 301);
            this.ground3.Name = "ground3";
            this.ground3.Size = new System.Drawing.Size(469, 80);
            this.ground3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ground3.TabIndex = 16;
            this.ground3.TabStop = false;
            this.ground3.Tag = "platform";
            // 
            // ground2
            // 
            this.ground2.Image = global::Chaos_Stage.Properties.Resources.地板素材;
            this.ground2.Location = new System.Drawing.Point(730, 463);
            this.ground2.Name = "ground2";
            this.ground2.Size = new System.Drawing.Size(469, 80);
            this.ground2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ground2.TabIndex = 14;
            this.ground2.TabStop = false;
            this.ground2.Tag = "platform";
            // 
            // ground1
            // 
            this.ground1.Image = global::Chaos_Stage.Properties.Resources.地板素材;
            this.ground1.Location = new System.Drawing.Point(1420, 599);
            this.ground1.Name = "ground1";
            this.ground1.Size = new System.Drawing.Size(469, 80);
            this.ground1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ground1.TabIndex = 12;
            this.ground1.TabStop = false;
            this.ground1.Tag = "platform";
            // 
            // goal
            // 
            this.goal.Image = global::Chaos_Stage.Properties.Resources.螢幕擷取畫面_2023_10_09_230551;
            this.goal.Location = new System.Drawing.Point(1796, 86);
            this.goal.Name = "goal";
            this.goal.Size = new System.Drawing.Size(86, 80);
            this.goal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.goal.TabIndex = 4;
            this.goal.TabStop = false;
            // 
            // bullet
            // 
            this.bullet.Image = global::Chaos_Stage.Properties.Resources.NOOOO_4x;
            this.bullet.Location = new System.Drawing.Point(246, 687);
            this.bullet.Name = "bullet";
            this.bullet.Size = new System.Drawing.Size(51, 52);
            this.bullet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bullet.TabIndex = 3;
            this.bullet.TabStop = false;
            this.bullet.Visible = false;
            // 
            // spike1
            // 
            this.spike1.Image = global::Chaos_Stage.Properties.Resources.images;
            this.spike1.Location = new System.Drawing.Point(730, 739);
            this.spike1.Name = "spike1";
            this.spike1.Size = new System.Drawing.Size(159, 47);
            this.spike1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.spike1.TabIndex = 2;
            this.spike1.TabStop = false;
            this.spike1.Tag = "spike";
            // 
            // enemy1
            // 
            this.enemy1.Image = global::Chaos_Stage.Properties.Resources.pipimi;
            this.enemy1.Location = new System.Drawing.Point(1296, 687);
            this.enemy1.Name = "enemy1";
            this.enemy1.Size = new System.Drawing.Size(87, 102);
            this.enemy1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.enemy1.TabIndex = 1;
            this.enemy1.TabStop = false;
            // 
            // character
            // 
            this.character.Image = global::Chaos_Stage.Properties.Resources.anime_pop_team_epic_popuko_pop_team_epic_wallpaper_preview;
            this.character.InitialImage = null;
            this.character.Location = new System.Drawing.Point(228, 684);
            this.character.Name = "character";
            this.character.Size = new System.Drawing.Size(87, 102);
            this.character.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.character.TabIndex = 0;
            this.character.TabStop = false;
            // 
            // background
            // 
            this.background.Image = global::Chaos_Stage.Properties.Resources.背景素材;
            this.background.Location = new System.Drawing.Point(0, 0);
            this.background.Name = "background";
            this.background.Size = new System.Drawing.Size(1892, 977);
            this.background.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.background.TabIndex = 11;
            this.background.TabStop = false;
            // 
            // rightside
            // 
            this.rightside.Location = new System.Drawing.Point(1873, 3);
            this.rightside.Name = "rightside";
            this.rightside.Size = new System.Drawing.Size(19, 786);
            this.rightside.TabIndex = 8;
            this.rightside.Text = "|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n" +
    "|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|";
            this.rightside.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // leftside
            // 
            this.leftside.Location = new System.Drawing.Point(-4, 0);
            this.leftside.Name = "leftside";
            this.leftside.Size = new System.Drawing.Size(19, 786);
            this.leftside.TabIndex = 6;
            this.leftside.Text = "|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n" +
    "|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|";
            this.leftside.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ground0
            // 
            this.ground0.Location = new System.Drawing.Point(0, 786);
            this.ground0.Name = "ground0";
            this.ground0.Size = new System.Drawing.Size(1892, 50);
            this.ground0.TabIndex = 18;
            this.ground0.TabStop = false;
            this.ground0.Tag = "platform";
            // 
            // gameInstructionPenal
            // 
            this.gameInstructionPenal.Controls.Add(this.gameInstruction);
            this.gameInstructionPenal.Controls.Add(this.goBackBtn);
            this.gameInstructionPenal.Controls.Add(this.gameInstructionTitle);
            this.gameInstructionPenal.Location = new System.Drawing.Point(0, 0);
            this.gameInstructionPenal.Name = "gameInstructionPenal";
            this.gameInstructionPenal.Size = new System.Drawing.Size(1892, 1006);
            this.gameInstructionPenal.TabIndex = 5;
            this.gameInstructionPenal.Visible = false;
            // 
            // gameInstruction
            // 
            this.gameInstruction.Font = new System.Drawing.Font("微軟正黑體", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gameInstruction.Location = new System.Drawing.Point(730, 281);
            this.gameInstruction.Name = "gameInstruction";
            this.gameInstruction.Size = new System.Drawing.Size(547, 176);
            this.gameInstruction.TabIndex = 2;
            this.gameInstruction.Text = "操作說明：\r\n左右";
            this.gameInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // goBackBtn
            // 
            this.goBackBtn.Location = new System.Drawing.Point(30, 28);
            this.goBackBtn.Name = "goBackBtn";
            this.goBackBtn.Size = new System.Drawing.Size(132, 72);
            this.goBackBtn.TabIndex = 1;
            this.goBackBtn.Text = "返回";
            this.goBackBtn.UseVisualStyleBackColor = true;
            this.goBackBtn.Click += new System.EventHandler(this.goBackBtn_Click);
            // 
            // gameInstructionTitle
            // 
            this.gameInstructionTitle.AutoSize = true;
            this.gameInstructionTitle.Font = new System.Drawing.Font("微軟正黑體", 28.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gameInstructionTitle.Location = new System.Drawing.Point(757, 71);
            this.gameInstructionTitle.Name = "gameInstructionTitle";
            this.gameInstructionTitle.Size = new System.Drawing.Size(340, 95);
            this.gameInstructionTitle.TabIndex = 0;
            this.gameInstructionTitle.Text = "遊戲說明";
            this.gameInstructionTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 10;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // gameOverPanel
            // 
            this.gameOverPanel.Controls.Add(this.gameOverQuitBtn);
            this.gameOverPanel.Controls.Add(this.retryBtn);
            this.gameOverPanel.Controls.Add(this.gameover);
            this.gameOverPanel.Location = new System.Drawing.Point(0, 0);
            this.gameOverPanel.Name = "gameOverPanel";
            this.gameOverPanel.Size = new System.Drawing.Size(1895, 974);
            this.gameOverPanel.TabIndex = 8;
            this.gameOverPanel.Visible = false;
            // 
            // gameOverQuitBtn
            // 
            this.gameOverQuitBtn.Location = new System.Drawing.Point(867, 599);
            this.gameOverQuitBtn.Name = "gameOverQuitBtn";
            this.gameOverQuitBtn.Size = new System.Drawing.Size(185, 93);
            this.gameOverQuitBtn.TabIndex = 13;
            this.gameOverQuitBtn.Text = "退出";
            this.gameOverQuitBtn.UseVisualStyleBackColor = true;
            this.gameOverQuitBtn.Click += new System.EventHandler(this.quitBtn_Click);
            // 
            // retryBtn
            // 
            this.retryBtn.Location = new System.Drawing.Point(867, 364);
            this.retryBtn.Name = "retryBtn";
            this.retryBtn.Size = new System.Drawing.Size(185, 93);
            this.retryBtn.TabIndex = 12;
            this.retryBtn.Text = "重新開始";
            this.retryBtn.UseVisualStyleBackColor = true;
            this.retryBtn.Click += new System.EventHandler(this.startGameBtn_Click);
            // 
            // gameover
            // 
            this.gameover.AutoSize = true;
            this.gameover.Location = new System.Drawing.Point(904, 190);
            this.gameover.Name = "gameover";
            this.gameover.Size = new System.Drawing.Size(106, 24);
            this.gameover.TabIndex = 11;
            this.gameover.Text = "遊戲結束";
            // 
            // gamePlayLevel2Panel
            // 
            this.gamePlayLevel2Panel.Location = new System.Drawing.Point(0, 0);
            this.gamePlayLevel2Panel.Name = "gamePlayLevel2Panel";
            this.gamePlayLevel2Panel.Size = new System.Drawing.Size(1892, 977);
            this.gamePlayLevel2Panel.TabIndex = 19;
            this.gamePlayLevel2Panel.Visible = false;
            // 
            // gamePlayLevel3Panel
            // 
            this.gamePlayLevel3Panel.Location = new System.Drawing.Point(0, 0);
            this.gamePlayLevel3Panel.Name = "gamePlayLevel3Panel";
            this.gamePlayLevel3Panel.Size = new System.Drawing.Size(1892, 977);
            this.gamePlayLevel3Panel.TabIndex = 20;
            this.gamePlayLevel3Panel.Visible = false;
            // 
            // gamePlayLevel4Panel
            // 
            this.gamePlayLevel4Panel.Location = new System.Drawing.Point(0, 0);
            this.gamePlayLevel4Panel.Name = "gamePlayLevel4Panel";
            this.gamePlayLevel4Panel.Size = new System.Drawing.Size(1892, 977);
            this.gamePlayLevel4Panel.TabIndex = 21;
            this.gamePlayLevel4Panel.Visible = false;
            // 
            // level5Timer
            // 
            this.level5Timer.Interval = 5000;
            this.level5Timer.Tick += new System.EventHandler(this.level5Timer_Tick);
            // 
            // blockPanel
            // 
            this.blockPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.blockPanel.Location = new System.Drawing.Point(0, 0);
            this.blockPanel.Name = "blockPanel";
            this.blockPanel.Size = new System.Drawing.Size(1892, 977);
            this.blockPanel.TabIndex = 22;
            this.blockPanel.Visible = false;
            // 
            // gamePlayLevel5Panel
            // 
            this.gamePlayLevel5Panel.Location = new System.Drawing.Point(0, 0);
            this.gamePlayLevel5Panel.Name = "gamePlayLevel5Panel";
            this.gamePlayLevel5Panel.Size = new System.Drawing.Size(1892, 977);
            this.gamePlayLevel5Panel.TabIndex = 21;
            this.gamePlayLevel5Panel.Visible = false;
            // 
            // gamePlayLevel6Panel
            // 
            this.gamePlayLevel6Panel.Location = new System.Drawing.Point(0, 0);
            this.gamePlayLevel6Panel.Name = "gamePlayLevel6Panel";
            this.gamePlayLevel6Panel.Size = new System.Drawing.Size(1892, 977);
            this.gamePlayLevel6Panel.TabIndex = 22;
            this.gamePlayLevel6Panel.Visible = false;
            // 
            // gameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1894, 1009);
            this.Controls.Add(this.nextLevelPanel);
            this.Controls.Add(this.gamePlayLevel1Panel);
            this.Controls.Add(this.gameInstructionPenal);
            this.Controls.Add(this.gamePlayLevel2Panel);
            this.Controls.Add(this.gamePlayLevel3Panel);
            this.Controls.Add(this.gamePlayLevel4Panel);
            this.Controls.Add(this.gamePlayLevel5Panel);
            this.Controls.Add(this.gamePlayLevel6Panel);
            this.Controls.Add(this.blockPanel);
            this.Controls.Add(this.mainMenuPanel);
            this.Controls.Add(this.gameOverPanel);
            this.KeyPreview = true;
            this.Name = "gameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "期末遊戲專題";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gameForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gameForm_KeyUp);
            this.mainMenuPanel.ResumeLayout(false);
            this.mainMenuPanel.PerformLayout();
            this.gamePlayLevel1Panel.ResumeLayout(false);
            this.nextLevelPanel.ResumeLayout(false);
            this.nextLevelPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ground5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bullet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spike1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.character)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.background)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground0)).EndInit();
            this.gameInstructionPenal.ResumeLayout(false);
            this.gameInstructionPenal.PerformLayout();
            this.gameOverPanel.ResumeLayout(false);
            this.gameOverPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button startGameBtn;
        private System.Windows.Forms.Button gameInstructionBtn;
        private System.Windows.Forms.Button mainMenuQuitBtn;
        private System.Windows.Forms.Panel mainMenuPanel;
        private System.Windows.Forms.Panel gamePlayLevel1Panel;
        private System.Windows.Forms.PictureBox bullet;
        private System.Windows.Forms.PictureBox spike1;
        private System.Windows.Forms.PictureBox enemy1;
        private System.Windows.Forms.PictureBox goal;
        private System.Windows.Forms.Panel gameInstructionPenal;
        private System.Windows.Forms.Label gameInstructionTitle;
        private System.Windows.Forms.Label leftside;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Panel gameOverPanel;
        private System.Windows.Forms.Label gameover;
        private System.Windows.Forms.PictureBox background;
        private System.Windows.Forms.Label rightside;
        private System.Windows.Forms.PictureBox ground1;
        private System.Windows.Forms.PictureBox ground2;
        private System.Windows.Forms.PictureBox ground4;
        private System.Windows.Forms.PictureBox ground3;
        private System.Windows.Forms.PictureBox ground5;
        private System.Windows.Forms.PictureBox ground0;
        private System.Windows.Forms.PictureBox character;
        private System.Windows.Forms.Panel gamePlayLevel2Panel;
        private System.Windows.Forms.Panel gamePlayLevel3Panel;
        private System.Windows.Forms.Panel gamePlayLevel4Panel;
        private System.Windows.Forms.Timer level5Timer;
        private System.Windows.Forms.Panel blockPanel;
        private System.Windows.Forms.Panel gamePlayLevel5Panel;
        private System.Windows.Forms.Panel gamePlayLevel6Panel;
        private System.Windows.Forms.Button gameOverQuitBtn;
        private System.Windows.Forms.Button retryBtn;
        private System.Windows.Forms.Button goBackBtn;
        private System.Windows.Forms.Label gameInstruction;
        private System.Windows.Forms.Panel nextLevelPanel;
        private System.Windows.Forms.Button nextLevelBtn;
        private System.Windows.Forms.Label nextLevelTitle;
        private System.Windows.Forms.Button nextLevelQuitBtn;
    }
}

