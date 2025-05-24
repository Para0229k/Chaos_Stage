using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chaos_Stage
{
    public partial class gameForm: Form
    {
        // =============== 參數設定 ================
        // 玩家移動
        int characterSpeed = 10;     // 玩家移動速度
        bool goLeft = false;         // 是否往左移動
        bool goRight = false;        // 是否往右移動
        bool facingRight = true;     // 是否面向右邊
        
        // 玩家跳躍
        bool jumping = false;        // 是否正在跳躍

        int jumpSpeed = 0;           // 跳躍速度 (垂直速度)
        int gravity = 2;             // 重力 (下降速度)
        int jumpForce = 20;          // 起跳速度

        // 子彈
        int bulletSpeed = 10;        // 子彈速度
        bool bulletActive = false;   // 是否正在飛行

        // 敵人移動
        int enemy1Speed = 3;         // 敵人移動速度
        int enemy1Direction = 1;     // 移動方向 (1->右 -1->左)
        int enemy1LeftLimit = 300;   // 移動左界
        int enemy1RightLimit = 880;  // 移動右界

        // 關卡
        bool complete = false;           // 是否過關
        int currentLevel = 1;        // 目前關卡 等級
        Control currentPanel;        // 目前關卡物件
        Control nextPanel;           // 下個關卡物件
        bool darkMode = false;       // （第六關）是否為黑畫面
        

        // =============== 自訂函式 ================
        // 發射子彈
        private void Shooting()
        {
            // 決定發射方向
            if (facingRight)
            {
                bullet.Left = character.Right; // 調整子彈位置
                bulletSpeed = Math.Abs(bulletSpeed); // 調整速度方向
            }
            else
            {
                bullet.Left = character.Left - bullet.Width;
                bulletSpeed = -Math.Abs(bulletSpeed);
            }
            bullet.Top = character.Top + character.Height / 2 - bullet.Height / 2;

            bullet.Visible = true;
            bulletActive = true;
        }

        // 取得當前關卡
        private Control Get_current_panel()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Panel && c.Visible && c.Name.StartsWith("gamePlayLevel"))
                {
                    return c; // 回傳目前顯示中的 Panel
                }
            }
            return null;
        }

        private Control Get_next_panel(int currentLevel)
        {
            string nextPanelName = $"gamePlayLevel{currentLevel}Panel";
            Control nextPanelObj = this.Controls.Find(nextPanelName, true).FirstOrDefault();
            return nextPanelObj;
        }

        // 載入下一關
        private void Load_next_level(Control next)
        {
            if (next != null)
            {
                // 加入物件
                next.Controls.AddRange(new Control[] {background, leftside, rightside, ground0,    // 背景物件
                                                      ground1, ground2, ground3, ground4, ground5, // 地板
                                                      character, enemy1, spike1, goal, bullet});   // 主要物件

                // 重新排序
                background.SendToBack();
                leftside.SendToBack();
                rightside.SendToBack();
                ground0.SendToBack();

                character.BringToFront();
                enemy1.BringToFront();
                spike1.BringToFront();
                goal.BringToFront();
                bullet.BringToFront();

                // 重製位置
                Reset_position();

                // 重製狀態
                Reset_statement();

                // 重製顯示
                Reset_visible();

                next.Visible = true;
                this.Focus();
            }
        }

        private void Reset_statement()
        {
            goLeft = false;
            goRight = false;
            facingRight = true;
            jumping = false;
            bulletActive = false;
            complete = false;
        }
        private void Reset_visible()
        {
            character.Visible = true;
            enemy1.Visible = true;
            spike1.Visible = true;
            goal.Visible = true;
            bullet.Visible = false;
        }

        // 重製位置
        private void Reset_position()
        {
            character.Location = new Point(0, 100);
            enemy1.Location = new Point(500, 340);
            spike1.Location = new Point(300, 370);
            goal.Location = new Point(830, 40);
        }

        
        public gameForm()
        {
            InitializeComponent();
        }

        private void startGameBtn_Click(object sender, EventArgs e)
        {
            // 關閉視窗
            mainMenuPanel.Visible = false;
            gameOverPanel.Visible = false;

            // 重製等級
            currentLevel = 1;

            // 進入第一關
            Load_next_level(gamePlayLevel1Panel);

            this.BeginInvoke((MethodInvoker)delegate // 延遲啟動timer
            {
                gameTimer.Start();
            });
        }

        private void gameInstructionBtn_Click(object sender, EventArgs e)
        {
            mainMenuPanel.Visible = false;
            gameInstructionPenal.Visible = true; // 顯示說明介面
        }

        private void goBackBtn_Click(object sender, EventArgs e)
        {
            gameInstructionPenal.Visible = false;
            mainMenuPanel.Visible = true; // 顯示主畫面
        }

        private void quitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit(); // 退出遊戲
        }

        private void nextLevelBtn_Click(object sender, EventArgs e)
        {
            // 關閉目前關卡畫面
            currentPanel.Visible = false;
            blockPanel.Visible = false;

            // 載入下一關
            Load_next_level(nextPanel);

            // level3要開timer, 調整難度
            if (nextPanel.Name == "gamePlayLevel3Panel")
            {
                level5Timer.Start();
                enemy1.Location = new Point (2000, 2000);
                spike1.Location = new Point (2000, 2000);
            }
            else
            {
                level5Timer.Stop();
            }

            // 關閉自己
            nextLevelPanel.Visible = false;

            this.Focus();
        }

        private void nextLevelQuitBtn_Click(object sender, EventArgs e)
        {
            // 回到主選單
            gameTimer.Stop();
            blockPanel.Visible = false;
            currentPanel.Visible = false;
            mainMenuPanel.Visible = true;
            nextLevelPanel.Visible = false;
        }

        private void gameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentPanel != null)
            {
                if (currentPanel.Name == "gamePlayLevel3Panel" && blockPanel.Visible == false) // 第三關黑畫面時才能移動
                {

                }
                else if (nextLevelPanel.Visible == false) // 下一關畫面出現時不能移動
                {
                    // 按左 -> 更改狀態 (往左)
                    if (e.KeyCode == Keys.Left)
                    {
                        goLeft = true;
                        facingRight = false;
                    }

                    // 按右 -> 更改狀態 (往右)
                    if (e.KeyCode == Keys.Right)
                    {
                        goRight = true;
                        facingRight = true;
                    }

                    // 按上 & 不是正在跳 -> 更改狀態 (跳躍)
                    if (e.KeyCode == Keys.Up && !jumping)
                    {
                        jumping = true;
                        jumpSpeed = -jumpForce;
                    }

                    // 按空白鍵 & 場上沒有子彈 -> 攻擊
                    if (e.KeyCode == Keys.Space && !bulletActive)
                    {
                        Shooting();
                    }
                }
            }
            
        }

        private void gameForm_KeyUp(object sender, KeyEventArgs e)
        {
            // 放開左 -> 更改狀態 (不移動)
            if (e.KeyCode == Keys.Left) 
            {
                goLeft = false;
            }

            // 放開右 -> 更改狀態 (不移動)
            if (e.KeyCode == Keys.Right) 
            {
                goRight = false;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            currentPanel = Get_current_panel(); // 找到當前level的panel

            // =============== 左右移動 ===============
            // 想要往左 & 左邊還有空間 -> 往左
            if (goLeft && character.Left > leftside.Right) 
            {
                character.Left -= characterSpeed;
            }

            // 想要往右 & 右邊還有空間 -> 往右
            if (goRight && character.Right < rightside.Left) 
            {
                character.Left += characterSpeed;
            }


            // =============== 跳躍 ===============
            int moveStep = Math.Sign(jumpSpeed); // 移動方向 (-1上升 1下降 0靜止)
            for (int i = 0; i < Math.Abs(jumpSpeed); i++)
            {
                character.Top += moveStep; // 拆成每次移動1pixel（避免速度太快）

                // 檢查是否碰到地板
                if (currentPanel != null)
                {
                    foreach (Control c in currentPanel.Controls)
                    {
                        if (c is PictureBox && c.Tag?.ToString() == "platform") // 找到地板的tag
                        {
                            if (character.Bottom >= c.Top && // 玩家穿過一點點地板
                                character.Bottom <= c.Top + 5 && // 容忍5pixel誤差
                                character.Right > c.Left && // 玩家在地板內
                                character.Left < c.Right &&
                                moveStep > 0) // 下墜中
                            {
                                character.Top = c.Top - character.Height;
                                jumpSpeed = 0;
                                jumping = false;
                                goto FinishFall;
                            }
                        }
                    }
                }
            }

            // 無落地 -> 繼續累加重力
            FinishFall:
            if (jumping)
                jumpSpeed += gravity;


            // =============== 自動落下 ===============
            bool onGround = false; // 是否站在地上
            if (currentPanel != null)
            {
                foreach (Control c in currentPanel.Controls)
                {
                    if (c is PictureBox && c.Tag?.ToString() == "platform")
                    {
                        // 落地了
                        if (Math.Abs(character.Bottom - c.Top) <= 5 &&
                            character.Right > c.Left &&
                            character.Left < c.Right)
                        {
                            onGround = true;
                            break;
                        }
                    }
                }
            }

            if (!onGround && !jumping)
            {
                jumping = true;
                jumpSpeed = 0;
            }


            // =============== 攻擊 ===============
            if (bulletActive)
            {
                bullet.Left += bulletSpeed;

                // 碰到畫面邊緣 -> 消失
                if (bullet.Left > rightside.Left || bullet.Right < leftside.Right)
                {
                    bullet.Visible = false;
                    bulletActive = false;
                }

                // 檢查碰撞敵人
                if (bullet.Bounds.IntersectsWith(enemy1.Bounds))
                {
                    enemy1.Visible = false;               // 敵人消失
                    enemy1.Left = rightside.Right + 2000; // 把敵人的判定框移走
                    bullet.Visible = false;               // 子彈消失
                    bulletActive = false;
                }
            }


            // =============== 敵人移動 ===============
            if (currentPanel != null)
            {
                if (currentPanel.Name == "gamePlayLevel3Panel" && blockPanel.Visible == false) // 第三關黑畫面時才能移動
                {

                }
                else if (nextLevelPanel.Visible == false) // 下一關畫面出現時不能移動
                {
                    enemy1.Left += enemy1Speed * enemy1Direction; // 移動
                }
            }
            
            // 碰到邊界 -> 轉向
            if (enemy1.Left <= enemy1LeftLimit || enemy1.Right >= enemy1RightLimit)
            {
                enemy1Direction *= -1;
            }


            // =============== 碰撞判定 ===============
            // 角色框碰到敵人框 -> 遊戲結束
            if (character.Bounds.IntersectsWith(enemy1.Bounds))
            {
                gameTimer.Stop();
                blockPanel.Visible = false;
                currentPanel.Visible = false;
                gameOverPanel.Visible = true;
            }

            // 角色框碰到障礙物框 -> 遊戲結束
            if (currentPanel != null)
            {
                foreach (Control c in currentPanel.Controls)
                {
                    if (c is PictureBox && c.Tag?.ToString() == "spike") // 找到spike的tag
                    {
                        if (character.Bounds.IntersectsWith(c.Bounds))
                        {
                            gameTimer.Stop();
                            blockPanel.Visible = false;
                            currentPanel.Visible = false;
                            gameOverPanel.Visible = true;
                        }
                    }
                }
            }

            // 拿到寶藏 -> 下一關
            if (character.Bounds.IntersectsWith(goal.Bounds))
            {
                if (!complete)
                {
                    complete = true;
                    // 找到下一關的物件
                    currentLevel++;
                    nextPanel = Get_next_panel(currentLevel);

                    // 不是最後一關 -> 跳出過關畫面
                    if (nextPanel != null)
                    {
                        nextLevelPanel.Visible = true;
                        nextLevelPanel.BringToFront();
                    }
                    else
                    {
                        // 最後一關了
                        gameTimer.Stop();
                        blockPanel.Visible = false;
                        currentPanel.Visible = false;
                        gameOverPanel.Visible = true;
                    }
                }
            }


            // =============== 關卡事件 ===============
            // 第二關特殊事件 - 陰險地刺
            if (currentPanel != null)
            {
                if (currentPanel.Name == "gamePlayLevel2Panel")
                {
                    if (Math.Abs(character.Right - spike1.Left) < 50 && // 角色在障礙物旁邊
                        Math.Abs(character.Bottom - spike1.Bottom) < 100 && 
                        jumping == true) // 角色跳起來了
                    {
                        spike1.Left = spike1.Left + 10; // 地刺往前移動
                    }
                }
            }
        }

        private void level5Timer_Tick(object sender, EventArgs e)
        {
            if (nextLevelPanel.Visible == false) // 過關後就不用了
            {
                // 第五關特殊事件 - 123木頭人
                blockPanel.BringToFront();
                darkMode = !darkMode;
                blockPanel.Visible = darkMode;
            }
        } 
    }
}
