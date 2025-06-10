using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chaos_Stage
{
    public partial class gameForm: Form
    {
        // =============== Parameter Settings ================
        // Player movement
        int characterSpeed = 10;     // Player speed
        bool goLeft = false;         // Is player moving left?
        bool goRight = false;        // Is player moving right?
        bool facingRight = true;     // Is player facing right?
        bool canMove = true;         // Can player move?

        // Player jump
        bool jumping = false;        // Is player jumping?
        int jumpSpeed = 0;           // Jump speed (upward velocity)
        int gravity = 2;             // Gravity (falling velocity)
        int jumpForce = 20;          // Initial jump speed

        // Bullet
        int bulletSpeed = 10;        // Bullet speed
        bool bulletActive = false;   // Is bullet in flight?

        // Enemy movement
        int enemySpeed = 3;          // Enemy speed
        int enemy1Direction = 1;     // Movement direction (1: right, -1: left)
        int enemy1LeftLimit = 440;   // Left boundary of movement
        int enemy1RightLimit = 880;  // Right boundary of movement

        // Level status
        bool complete = false;       // Is level completed?
        int currentLevel = 1;        // Current level
        Control currentPanel;        // Current level control item
        Control nextPanel;           // Next level control item
        bool pausing = false;        // Is game pausing?
        int gameOverStatus = 0;      // Success or fail? (0: fail, 1: success)

        // Events
        bool darkMode = false;       // (Level 5) Is it dark now?

        int enemy2Direction = 1;     // (Level 6) Enemy 2 direction
        int enemy2LeftLimit = 340;   // (Level 6) Enemy 2 left boundary
        int enemy2RightLimit = 550;  // (Level 6) Enemy 2 right boundary

        int enemy3Direction = 1;     // (Level 6) Enemy 3 direction
        int enemy3LeftLimit = 340;   // (Level 6) Enemy 3 left boundary
        int enemy3RightLimit = 560;  // (Level 6) Enemy 3 right boundary

        bool flying = false;         // (Level 9) Is character being dragged?
        int sX;                      // (Level 9) Mouse x-coordinate
        int sY;                      // (Level 9) Mouse y-coordinate

        // Index of imagelist
        int i = 0, j = 0, k = 0, l = 0, m = 0;
        int n = 0, o = 0, p = 0;


        // =============== User Define Function ================
        // Get current level control item
        private Control Get_current_panel()
        {
            // Find the panel name contains "gamePlayLevel" and is currently visible
            foreach (Control c in this.Controls)
            {
                if (c.Visible && c.Name.StartsWith("gamePlayLevel"))
                {
                    return c;
                }
            }
            return null;
        }

        // Get next level control item
        private Control Get_next_panel(int currentLevel)
        {
            string nextPanelName = $"gamePlayLevel{currentLevel}Panel";

            // Find the panel for next level by name
            foreach (Control c in this.Controls)
            {
                if (c.Name.Equals(nextPanelName))
                {
                    return c;
                }
            }
            return null;
        }

        // Load the next level
        private void Load_next_level(Control next)
        {
            if (next != null)
            {
                // Add objects to the next level
                next.Controls.AddRange(new Control[] {gamePlayBG, leftside, rightside, ground0,    // Background objects
                                                      ground1, ground2, ground3, ground4, ground5, // Platform
                                                      character, enemy1, spike1, goal, bullet,     // Main objects
                                                      skipBtn, currentLevelLabel, pauseBtn,        // Control objects
                                                      keySpace, keyLeft, keyUp, keyRight});

                // Reorder
                gamePlayBG.SendToBack();
                leftside.SendToBack();
                rightside.SendToBack();
                ground0.SendToBack();

                character.BringToFront();
                enemy1.BringToFront();
                spike1.BringToFront();
                goal.BringToFront();
                bullet.BringToFront();
                skipBtn.BringToFront();
                currentLevelLabel.BringToFront();
                pauseBtn.BringToFront();

                // Reset positions
                Reset_position();

                // Reset game state
                Reset_statement();

                // Reset visibility
                Reset_visible();

                // Update level information
                currentLevelLabel.Text = $"第{currentLevel}關";

                next.Visible = true;
                this.ActiveControl = null;
                this.Focus();
            }
        }

        // Reset game state
        private void Reset_statement()
        {
            goLeft = false;
            goRight = false;
            facingRight = true;
            character.Image = characterRightImgList.Images[0];
            jumping = false;
            bulletActive = false;
            bulletSpeed = 10;
            complete = false;
            pausing = false;
        }

        // Reset visibility
        private void Reset_visible()
        {
            ground1.Visible = true;
            ground2.Visible = true;
            ground3.Visible = true;
            ground4.Visible = true;
            ground5.Visible = true;

            character.Visible = true;
            enemy1.Visible = true;
            enemy2.Visible = true;
            enemy3.Visible = true;
            spike1.Visible = true;
            goal.Visible = true;
            bullet.Visible = false;

            skipBtn.Visible = true;
            pauseBtn.Visible = true;
            currentLevelLabel.Visible = true;

            goal2.Visible = false;
            goal3.Visible = false;
            goal4.Visible = false;
        }

        // Reset positions
        private void Reset_position()
        {
            ground1.Location = new Point(655, 300);
            ground2.Location = new Point(337, 232);
            ground3.Location = new Point(53, 150);
            ground4.Location = new Point(344, 67);
            ground5.Location = new Point(657, 80);

            character.Location = new Point(0, 150);
            enemy1.Location = new Point(500, 340);
            spike1.Location = new Point(337, 363);
            goal.Location = new Point(829, 33);

            skipBtn.Location = new Point(0, 43);
            pauseBtn.Location = new Point(0, 0);
            currentLevelLabel.Location = new Point(53, 0);

            goal2.Location = new Point(2000, 2000);
            goal3.Location = new Point(2000, 2000);
            goal4.Location = new Point(2000, 2000);

            if (nextPanel.Name == "gamePlayLevel7Panel")
            {
                goal.Location = new Point(53, 103);
                fakeGoal2.Location = new Point(829, 33);
            }
        }

        // Game over
        private void Game_over()
        {
            gameTimer.Stop();

            blockPanel.Visible = false;
            currentPanel.Visible = false;

            if (gameOverStatus == 1)
            {
                // Play the sound
                SoundPlayer Success = new SoundPlayer();
                Success.SoundLocation = @"..\..\Resources\Success.wav";
                Success.Play();

                gameOverTitle.Text = "恭喜過關";
                gameOverTitle.BackColor = Color.FromArgb(255, 151, 125);
                gameOverBG.Image = Image.FromFile(@"..\..\Resources\Success_BG.jpg");
            }
            else
            {
                // Play the sound
                SoundPlayer Fail = new SoundPlayer();
                Fail.SoundLocation = @"..\..\Resources\Fail.wav";
                Fail.Play();

                gameOverTitle.Text = "闖關失敗";
                gameOverTitle.BackColor = Color.FromArgb(60, 70, 81);
                gameOverTitle.ForeColor = Color.White;
                gameOverBG.Image = Image.FromFile(@"..\..\Resources\Failed_BG.jpg");
            }

            gameOverPanel.Visible = true;
        }

        // Center the panels
        private void CenterPanel(Panel panel)
        {
            panel.Left = (this.ClientSize.Width - panel.Width) / 2;
            panel.Top = (this.ClientSize.Height - panel.Height) / 2;
        }

        // Fire
        private void Shooting()
        {
            // Determine firing direction
            if (facingRight)
            {
                bullet.Image = bulletList.Images[0]; // Switch image
                bullet.Left = character.Right + 2;   // Adjust bullet position
                bulletSpeed = Math.Abs(bulletSpeed); // Adjust velocity direction
            }
            else
            {
                bullet.Image = bulletList.Images[1];
                bullet.Left = character.Left - bullet.Width - 2;
                bulletSpeed = -Math.Abs(bulletSpeed);
            }
            bullet.Top = character.Top + character.Height / 2 - bullet.Height / 2;

            bullet.BringToFront();
            bullet.Visible = true;
            bulletActive = true;
        }

        // =============== Callback ================
        public gameForm()
        {
            InitializeComponent();
        }

        private void gameForm_Load(object sender, EventArgs e)
        {
            // Play BGM
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = @"..\..\Resources\BGM.wav";
            player.PlayLooping();
        }

        private void gameForm_Resize(object sender, EventArgs e)
        {
            Panel[] panels = new Panel[] {mainMenuPanel, gameInstructionPenal, chooseLevelPanel,
                                          gamePlayLevel1Panel, gamePlayLevel2Panel, gamePlayLevel3Panel,
                                          gamePlayLevel4Panel, gamePlayLevel5Panel, gamePlayLevel6Panel,
                                          gamePlayLevel7Panel, gamePlayLevel8Panel, gamePlayLevel9Panel,
                                          gamePlayLevel10Panel, pausePanel, nextLevelPanel, gameOverPanel, blockPanel};

            // Center all panels
            foreach (var panel in panels)
            {
                CenterPanel(panel);
            }

            // Center all items
            mainMenuTitle.Left = (mainMenuPanel.Width - mainMenuTitle.Width) / 2;
            mainMenuStartGameBtn.Left = (mainMenuPanel.Width - mainMenuStartGameBtn.Width) / 2;
            mainMenuGameInsBtn.Left = (mainMenuPanel.Width - mainMenuGameInsBtn.Width) / 2;
            mainMenuQuitBtn.Left = (mainMenuPanel.Width - mainMenuQuitBtn.Width) / 2;
            
            gameInsTitle.Left = (gameInstructionPenal.Width - gameInsTitle.Width) / 2;

            chooseLevelTitle.Left = (chooseLevelPanel.Width - chooseLevelTitle.Width) / 2;

            pauseTitle.Left = (pausePanel.Width - pauseTitle.Width) / 2;
            pauseBackToGameBtn.Left = (pausePanel.Width - pauseBackToGameBtn.Width) / 2;
            pauseReturnBtn.Left = (pausePanel.Width - pauseReturnBtn.Width) / 2;

            nextLevelTitle.Left = (nextLevelPanel.Width - nextLevelTitle.Width) / 2;
            nextLevelBtn.Left = (nextLevelPanel.Width - nextLevelBtn.Width) / 2;
            nextLevelQuitBtn.Left = (nextLevelPanel.Width - nextLevelQuitBtn.Width) / 2;

            gameOverTitle.Left = (gameOverPanel.Width - gameOverTitle.Width) / 2;
            gameOverRetryBtn.Left = (gameOverPanel.Width - gameOverRetryBtn.Width) / 2;
            gameOverQuitBtn.Left = (gameOverPanel.Width - gameOverQuitBtn.Width) / 2;
        }

        private async void startGameBtn_Click(object sender, EventArgs e)
        {
            // Play clicked sound
            SoundPlayer buttonClicked = new SoundPlayer();
            buttonClicked.SoundLocation = @"..\..\Resources\ButtonClicked.wav";
            buttonClicked.Play();

            // Show level selection
            mainMenuPanel.Visible = false;
            gameOverPanel.Visible = false;

            chooseLevelPanel.Visible = true;
            chooseLevelPanel.Enabled = true;

            await Task.Delay(500);
            
            // Play BGM
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = @"..\..\Resources\BGM.wav";
            player.PlayLooping();
        }

        private async void gameInstructionBtn_Click(object sender, EventArgs e)
        {
            // Play clicked sound
            SoundPlayer buttonClicked = new SoundPlayer();
            buttonClicked.SoundLocation = @"..\..\Resources\ButtonClicked.wav";
            buttonClicked.Play();

            // Show instructions
            mainMenuPanel.Visible = false;

            gameInstructionPenal.Visible = true;

            await Task.Delay(500);

            // Play BGM
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = @"..\..\Resources\BGM.wav";
            player.PlayLooping();
        }

        private async void quitBtn_Click(object sender, EventArgs e)
        {
            // Play clicked sound
            SoundPlayer buttonClicked = new SoundPlayer();
            buttonClicked.SoundLocation = @"..\..\Resources\ButtonClicked.wav";
            buttonClicked.Play();
            await Task.Delay(500);

            // Exit game
            Application.Exit();
        }

        private void goBackBtn_Click(object sender, EventArgs e)
        {
            // Return to main menu
            blockPanel.Visible = false;
            gameInstructionPenal.Visible = false;
            chooseLevelPanel.Visible = false;
            pausePanel.Visible = false;
            nextLevelPanel.Visible = false;
            
            foreach (Control c in this.Controls)
            {
                if (c is Panel && c.Visible && c.Name.StartsWith("gamePlayLevel"))
                {
                    c.Visible = false;
                }
            }

            gameTimer.Stop();
            level5Timer.Stop();
            MoveingTimer.Stop();

            mainMenuPanel.Visible = true;
        }

        private void level1Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            // Get the selected level
            string text = btn.Text;
            string levelNumber = text.Substring(6);
            currentLevel = int.Parse(levelNumber);

            // Get the selected level control item
            nextPanel = Get_next_panel(currentLevel);

            // Game start
            Load_next_level(nextPanel);

            if (nextPanel.Name == "gamePlayLevel5Panel")
            {
                level5Timer.Start();
                enemy1.Location = new Point(2000, 2000);
                spike1.Location = new Point(2000, 2000);
            }
            else if (nextPanel.Name == "gamePlayLevel6Panel")
            {
                enemy2.Location = new Point(419, 182);
                enemy3.Location = new Point(430, 16);
            }
            else
            {
                level5Timer.Stop();
            }

            chooseLevelPanel.Visible = false;
            chooseLevelPanel.Enabled = false;

            // Starting the timer
            gameTimer.Start();
            MoveingTimer.Start();

            this.ActiveControl = null;
            this.Focus();
        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {
            // Show pause panel
            pausing = true;
            pausePanel.Visible = true;
            pausePanel.BringToFront();
        }

        private void backToGame_Click(object sender, EventArgs e)
        {
            // Hide pause panel
            pausePanel.Visible = false;
            pausing = false;
        }

        private void nextLevelBtn_Click(object sender, EventArgs e)
        {
            Button btnClick = (Button)sender;

            // Close current level panel
            currentPanel.Visible = false;
            blockPanel.Visible = false;

            // Skip button clicked
            if (btnClick.Name == "skipBtn")
            {
                // Find the next level's control item
                currentLevel++;
                nextPanel = Get_next_panel(currentLevel);

                // At final level
                if (nextPanel == null)
                {
                    gameOverStatus = 1;
                    Game_over();
                    return;
                }
            }

            // Load the next level
            Load_next_level(nextPanel);

            // Level 5 special event - start the timer and remove enemies (adjust difficulty)
            if (nextPanel.Name == "gamePlayLevel5Panel")
            {
                level5Timer.Start();
                enemy1.Location = new Point(2000, 2000);
                spike1.Location = new Point(2000, 2000);
            }
            else
            {
                level5Timer.Stop();
            }

            nextLevelPanel.Visible = false;
            this.Focus();
        }

        private void gameForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Show pressing key
            if (e.KeyCode == Keys.Left)
            {
                keyLeft.BackColor = Color.FromArgb(160, 160, 160);
            }
            else if (e.KeyCode == Keys.Right)
            {
                keyRight.BackColor = Color.FromArgb(160, 160, 160);
            }
            else if (e.KeyCode == Keys.Up)
            {
                keyUp.BackColor = Color.FromArgb(160, 160, 160);
            }
            else if (e.KeyCode == Keys.Space)
            {
                keySpace.BackColor = Color.FromArgb(160, 160, 160);
            }

            if (currentPanel != null)
            {
                // Level 5 special event - movement disabled when it's dark
                if ((currentPanel.Name == "gamePlayLevel5Panel" && blockPanel.Visible == false) ||
                    nextLevelPanel.Visible == true ||
                    pausing == true)
                {
                    canMove = false;
                }
                else
                {
                    canMove = true;
                }

                if (canMove)
                {
                    // Level 2 special event - reverse left and right
                    if (currentPanel.Name == "gamePlayLevel2Panel")
                    {
                        // Press left -> change state (move right)
                        if (e.KeyCode == Keys.Left)
                        {
                            goRight = true;
                            facingRight = true;
                        }

                        // Press right -> change state (move left)
                        if (e.KeyCode == Keys.Right)
                        {
                            goLeft = true;
                            facingRight = false;

                        }
                    }
                    else // Normal movement
                    {
                        if (e.KeyCode == Keys.Left)
                        {
                            goLeft = true;
                            facingRight = false;
                        }

                        if (e.KeyCode == Keys.Right)
                        {
                            goRight = true;
                            facingRight = true;
                        }
                    }

                    // Level 10 special event - jumping is disabled
                    if (currentPanel.Name != "gamePlayLevel10Panel")
                    {
                        // Press up and not currently jumping -> change state (jump)
                        if (e.KeyCode == Keys.Up && !jumping)
                        {
                            jumping = true;
                            jumpSpeed = -jumpForce;
                        }
                    }

                    // Press spacebar and no bullets on screen -> Fire
                    if (e.KeyCode == Keys.Space && !bulletActive)
                    {
                        Shooting();
                    }
                }
            }
        }

        private void gameForm_KeyUp(object sender, KeyEventArgs e)
        {
            // Show pressing key
            if (e.KeyCode == Keys.Left)
            {
                keyLeft.BackColor = Color.FromArgb(50, 50, 50);
            }
            else if (e.KeyCode == Keys.Right)
            {
                keyRight.BackColor = Color.FromArgb(50, 50, 50);
            }
            else if (e.KeyCode == Keys.Up)
            {
                keyUp.BackColor = Color.FromArgb(50, 50, 50);
            }
            else if (e.KeyCode == Keys.Space)
            {
                keySpace.BackColor = Color.FromArgb(50, 50, 50);
            }

            if (currentPanel != null)
            {
                // Level 2 special event - reverse left and right
                if (currentPanel.Name == "gamePlayLevel2Panel")
                {
                    // Release left key -> change state (stop moving right)
                    if (e.KeyCode == Keys.Left)
                    {
                        goRight = false;
                    }

                    // Release right key -> change state (stop moving left)
                    if (e.KeyCode == Keys.Right)
                    {
                        goLeft = false;
                    }
                }
                else
                {
                    // Release left key -> change state (stop moving left)
                    if (e.KeyCode == Keys.Left)
                    {
                        goLeft = false;
                    }

                    // Release right key -> change state (stop moving right)
                    if (e.KeyCode == Keys.Right)
                    {
                        goRight = false;
                    }
                }
            }
        }

        private void character_MouseDown(object sender, MouseEventArgs e)
        {
            // Level 9 special event - drag character (start dragging)
            if (e.Button == MouseButtons.Left &&
                currentPanel != null &&
                currentPanel.Name == "gamePlayLevel9Panel")
            {
                flying = true;
                sX = e.X;
                sY = e.Y;
            }
        }

        private void character_MouseMove(object sender, MouseEventArgs e)
        {
            // Level 9 special event - drag character (in motion)
            if (flying)
            {
                jumping = false;
                character.Left += (e.X - sX);
                character.Top += (e.Y - sY);

                // Do not exceed screen boundaries
                if (character.Left > 832)
                {
                    character.Left = 832;
                }
                else if (character.Left < 0)
                {
                    character.Left = 0;
                }

                if (character.Top > 342)
                {
                    character.Top = 342;
                }
                else if (character.Top < 0)
                {
                    character.Top = 0;
                }
            }
        }

        private void character_MouseUp(object sender, MouseEventArgs e)
        {
            // Level 9 special event - drag character (end dragging)
            flying = false;
        }

        private void character_Click(object sender, EventArgs e)
        {
            // Level 10 special event - tap to jump
            if (currentPanel != null && currentPanel.Name == "gamePlayLevel10Panel")
            {
                if (!jumping)
                {
                    jumping = true;
                    jumpSpeed = -jumpForce;
                }
            }
        }


        private async void gameTimer_Tick(object sender, EventArgs e)
        {
            // Find the panel of the current level
            currentPanel = Get_current_panel();


            // =============== Horizontal Movement ===============
            // Wants to move left & space is available on the left -> move left
            if (goLeft && character.Left > leftside.Right)
            {
                character.Left -= characterSpeed;
            }

            // Wants to move right & space is available on the right -> move right
            if (goRight && character.Right < rightside.Left)
            {
                character.Left += characterSpeed;
            }


            // =============== Jump ===============
            // Is the player standing on the ground?
            bool onGround = false;

            // Movement direction (-1: ascending, 1: falling, 0: stationary)
            int moveStep = Math.Sign(jumpSpeed);

            // Move one frame at a time and check status
            for (int i = 0; i < Math.Abs(jumpSpeed); i++)
            {
                // Move 1 pixel per frame
                character.Top += moveStep;

                if (currentPanel != null)
                {
                    // Find all platform objects
                    foreach (Control c in currentPanel.Controls)
                    {
                        if (c is PictureBox && c.Tag?.ToString() == "platform")
                        {
                            // Check if the player has hit the ground
                            if (character.Bottom >= c.Top &&     // Player is above the platform (and)
                                character.Bottom <= c.Top + 5 && // about to reach the platform (and)
                                character.Right > c.Left &&      // is within the platform (and)
                                character.Left < c.Right &&
                                moveStep > 0)                    // currently falling
                            {
                                character.Top = c.Top - character.Height;
                                jumpSpeed = 0;
                                jumping = false;
                                onGround = true;
                                break;
                            }
                        }
                    }
                }
                // If landed -> no need to check further
                if (onGround)
                {
                    break;
                }
            }

            // If not yet landed -> apply gravity incrementally
            if (jumping)
            {
                jumpSpeed += gravity;
            }


            // =============== Fall Down ===============
            if (currentPanel != null)
            {
                // Find all platform objects
                foreach (Control c in currentPanel.Controls)
                {
                    if (c is PictureBox && c.Tag?.ToString() == "platform")
                    {
                        // Check if the player has hit the ground
                        if (Math.Abs(character.Bottom - c.Top) <= 5 && // Player is very close to the platform (and)
                            character.Right > c.Left &&                // is within the platform
                            character.Left < c.Right)
                        {
                            onGround = true;
                            break;
                        }
                    }
                }
            }

            // Not landed yet and not currently jumping -> fall down
            if (!onGround && !jumping)
            {
                jumping = true;
                jumpSpeed = 0;
            }


            // =============== Attack ===============
            if (bulletActive)
            {
                // Bullet in flight
                bullet.Left += bulletSpeed;

                // Hits screen edge -> disappears
                if (bullet.Left > rightside.Left || bullet.Right < leftside.Right)
                {
                    bullet.Visible = false;
                    bulletActive = false;
                    bulletSpeed = 10;
                }

                // Hits enemy -> enemy dies and bullet disappears
                if (bullet.Bounds.IntersectsWith(enemy1.Bounds))
                {
                    // Level 3 special event - enemy reflects bullets
                    if (currentPanel != null && currentPanel.Name == "gamePlayLevel3Panel")
                    {
                        // Adjust position
                        if (bulletSpeed > 0)
                        {
                            bullet.Left = enemy1.Left - bullet.Width - 2;
                        }
                        else
                        {
                            bullet.Left = enemy1.Right + 2;
                        }

                        // Change direction
                        bulletSpeed *= -5;
                    }
                    else
                    {
                        enemy1.Visible = false;
                        enemy1.Left = rightside.Right + 2000;
                        bullet.Visible = false;
                    }
                }

                // Reflected bullet hits the player -> game over
                if (bullet.Bounds.IntersectsWith(character.Bounds))
                {
                    gameOverStatus = 0;
                    Game_over();
                }
            }


            // =============== Enemy movement ===============
            if (currentPanel != null)
            {
                /*
                // Level 5 special event - movement allowed only when it’s dark (too difficult)
                if (currentPanel.Name == "gamePlayLevel5Panel" && blockPanel.Visible == false)
                {

                }
                */

                // Next level screen not shown -> move the screen
                if (nextLevelPanel.Visible == false && pausing == false)
                {
                    enemy1.Left += enemySpeed * enemy1Direction;

                    // Level 6 special event - three enemies
                    enemy2.Left += enemySpeed * enemy2Direction;
                    enemy3.Left += enemySpeed * enemy3Direction;
                }
            }

            // Hits boundary -> change direction
            if (enemy1.Left <= enemy1LeftLimit || enemy1.Right >= enemy1RightLimit)
            {
                enemy1Direction *= -1;
            }

            // Level 6 special event - three enemies
            if (enemy2.Left <= enemy2LeftLimit || enemy2.Right >= enemy2RightLimit)
            {
                enemy2Direction *= -1;
            }
            if (enemy3.Left <= enemy3LeftLimit || enemy3.Right >= enemy3RightLimit)
            {
                enemy3Direction *= -1;
            }


            // =============== Collision Detection ===============
            // Player collides with enemy -> game over
            if (currentPanel != null)
            {
                // Level 6 special event - three enemies
                if (currentPanel.Name == "gamePlayLevel6Panel")
                {
                    // Find all enemy objects
                    foreach (Control c in currentPanel.Controls)
                    {
                        if (c is PictureBox && c.Tag?.ToString() == "enemy")
                        {
                            // Collide with any enemy
                            if (character.Bounds.IntersectsWith(c.Bounds))
                            {
                                gameOverStatus = 0;
                                Game_over();
                            }
                        }
                    }
                }
                else
                {
                    if (character.Bounds.IntersectsWith(enemy1.Bounds))
                    {
                        gameOverStatus = 0;
                        Game_over();
                    }
                }
            }

            // Player collides with spike -> game over
            if (currentPanel != null)
            {
                // Find all obstacle spikes
                foreach (Control c in currentPanel.Controls)
                {
                    if (c is PictureBox && c.Tag?.ToString() == "spike")
                    {
                        // Collide with any of them
                        if (character.Bounds.IntersectsWith(c.Bounds))
                        {
                            gameOverStatus = 0;
                            Game_over();
                        }
                    }
                }
            }

            // Pick up fake treasure -> restart Level 7
            if (currentPanel != null)
            {
                // Level 7 special event - fake treasure
                if (currentPanel.Name == "gamePlayLevel7Panel")
                {
                    if (character.Bounds.IntersectsWith(fakeGoal1.Bounds))
                    {
                        Load_next_level(gamePlayLevel7Panel);
                        fakeGoal1.Location = new Point(2000, 2000);
                        fakeGoal1.Visible = false;
                    }
                    else if (character.Bounds.IntersectsWith(fakeGoal2.Bounds))
                    {
                        Load_next_level(gamePlayLevel7Panel);
                        fakeGoal2.Location = new Point(2000, 2000);
                        fakeGoal2.Visible = false;
                    }
                }
            }

            // Pick up treasure - next level
            if (currentPanel != null)
            {
                // Level 8 special event - multiple treasures
                if (currentPanel.Name == "gamePlayLevel8Panel")
                {
                    // Treasures appear one by one; reset the level each time a new one appears
                    if (character.Bounds.IntersectsWith(goal.Bounds))
                    {
                        Load_next_level(gamePlayLevel8Panel);
                        goal.Visible = false;
                        goal.Location = new Point(2000, 2000);
                        goal2.Location = new Point(430, 20);
                        goal2.BringToFront();
                        goal2.Visible = true;

                        ground1.Location = new Point(40, 300);
                    }
                    else if (character.Bounds.IntersectsWith(goal2.Bounds))
                    {
                        Load_next_level(gamePlayLevel8Panel);
                        goal.Visible = false;
                        goal2.Visible = false;
                        goal2.Location = new Point(2000, 2000);
                        goal3.Location = new Point(138, 64);
                        goal3.BringToFront();
                        goal3.Visible = true;

                        ground2.Location = new Point(475, 193);
                    }
                    else if (character.Bounds.IntersectsWith(goal3.Bounds))
                    {
                        Load_next_level(gamePlayLevel8Panel);
                        goal.Visible = false;
                        goal3.Visible = false;
                        goal3.Location = new Point(2000, 2000);
                        goal4.Location = new Point(430, 180);
                        goal4.BringToFront();
                        goal4.Visible = true;

                        ground2.Location = new Point(40, 283);
                    }
                    else if (!complete && character.Bounds.IntersectsWith(goal4.Bounds)) // Pick up the final treasure -> next level
                    {
                        complete = true;

                        // Find next level's control item
                        currentLevel++;
                        nextPanel = Get_next_panel(currentLevel);

                        // If not the final level -> show level complete
                        if (nextPanel != null)
                        {
                            nextLevelPanel.Visible = true;
                            nextLevelPanel.BringToFront();
                            skipBtn.Visible = false;
                            pauseBtn.Visible = false;
                        }
                        else
                        {
                            gameOverStatus = 1;
                            Game_over();
                        }
                    }
                }
                else if (!complete && character.Bounds.IntersectsWith(goal.Bounds)) // In normal condition, when treasure is collected
                {
                    goal.Visible = false;
                    complete = true;
                    enemy2.Visible = false;
                    enemy3.Visible = false;
                    enemy1.Location = new Point(2000, 2000);

                    // Find the next level's control item
                    currentLevel++;
                    nextPanel = Get_next_panel(currentLevel);

                    // If not the final level -> show level complete
                    if (nextPanel != null)
                    {
                        nextLevelPanel.Top = -282;
                        nextLevelPanel.Visible = true;
                        nextLevelPanel.BringToFront();
                        skipBtn.Visible = false;
                        pauseBtn.Visible = false;

                        while (nextLevelPanel.Top < ((this.ClientSize.Height - nextLevelPanel.Height) / 2))
                        {
                            nextLevelPanel.Top += 20;
                            await Task.Delay(1);
                        }
                    }
                    else
                    {
                        gameOverStatus = 1;
                        Game_over();
                    }
                }
            }


            // =============== Events ===============
            if (currentPanel != null)
            {
                // Level 4 special event - moving spike
                if (currentPanel.Name == "gamePlayLevel4Panel")
                {
                    if (Math.Abs(character.Right - spike1.Left) < 50 &&      // Player is next to the spike (and)
                        Math.Abs(character.Bottom - spike1.Bottom) < 100 &&
                        jumping == true)                                     // Player jumps -> the spike moves forward
                    {
                        // Spike move forward
                        spike1.Left = spike1.Left + 10;
                    }
                }
            }
            
            if (currentPanel != null)
            {
                // Level 8 special event - invisible platforms
                if (currentPanel.Name == "gamePlayLevel8Panel" || currentPanel.Name == "gamePlayLevel9Panel")
                {
                    // Find all platform objects
                    foreach (Control c in currentPanel.Controls)
                    {
                        if (c is PictureBox && c.Tag?.ToString() == "platform")
                        {
                            c.Visible = false;

                            // Level 9 special event - disappearing platforms
                            if (currentPanel.Name == "gamePlayLevel9Panel")
                            {
                                c.Location = new Point(2000, 2000);
                            }
                        }
                    }
                    ground0.Location = new Point(0, 393);
                }
            }
        }

        private void level5Timer_Tick(object sender, EventArgs e)
        {
            // Level 5 special event - can only move when it’s dark
            if (nextLevelPanel.Visible == false)
            {
                blockPanel.BringToFront();
                darkMode = !darkMode;
                blockPanel.Visible = darkMode;
            }
        }

        private void chatacterMove_Tick(object sender, EventArgs e)
        {
            // =============== Character ===============
            // Character move left
            if (goLeft && i < characterLeftImgList.Images.Count)
            {
                character.Image = characterLeftImgList.Images[i];
                i++;
            }
            else if (i >= characterLeftImgList.Images.Count)
            {
                i = 0;
            }

            // Character move right
            if (goRight && j < characterRightImgList.Images.Count)
            {
                character.Image = characterRightImgList.Images[j];
                j++;
            }
            else if (j >= characterRightImgList.Images.Count)
            {
                j = 0;
            }


            // =============== Enemy ===============
            // Enemy move left
            if (enemy1Direction == -1 && k < enemyLeftImgList.Images.Count)
            {
                enemy1.Image = enemyLeftImgList.Images[k];
                k++;
            }
            else if (k >= enemyLeftImgList.Images.Count)
            {
                k = 0;
            }

            if (enemy2Direction == -1 && l < enemyLeftImgList.Images.Count)
            {
                enemy2.Image = enemyLeftImgList.Images[l];
                l++;
            }
            else if (l >= enemyLeftImgList.Images.Count)
            {
                l = 0;
            }

            if (enemy3Direction == -1 && m < enemyLeftImgList.Images.Count)
            {
                enemy3.Image = enemyLeftImgList.Images[m];
                m++;
            }
            else if (m >= enemyLeftImgList.Images.Count)
            {
                m = 0;
            }

            // Enemy move right
            if (enemy1Direction == 1 && n < enemyRightImgList.Images.Count)
            {
                enemy1.Image = enemyRightImgList.Images[n];
                n++;
            }
            else if (n >= enemyRightImgList.Images.Count)
            {
                n = 0;
            }

            if (enemy2Direction == 1 && o < enemyRightImgList.Images.Count)
            {
                enemy2.Image = enemyRightImgList.Images[o];
                o++;
            }
            else if (o >= enemyRightImgList.Images.Count)
            {
                o = 0;
            }

            if (enemy3Direction == 1 && p < enemyRightImgList.Images.Count)
            {
                enemy3.Image = enemyRightImgList.Images[p];
                p++;
            }
            else if (p >= enemyRightImgList.Images.Count)
            {
                p = 0;
            }
        }
    }
}
