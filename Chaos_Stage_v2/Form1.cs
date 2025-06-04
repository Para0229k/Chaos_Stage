using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
        int enemy1LeftLimit = 300;   // Left boundary of movement
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


        // =============== User Define Function ================
        // Fire
        private void Shooting()
        {
            // Determine firing direction
            if (facingRight)
            {
                bullet.Left = character.Right + 2;   // Adjust bullet position
                bulletSpeed = Math.Abs(bulletSpeed); // Adjust velocity direction
            }
            else
            {
                bullet.Left = character.Left - bullet.Width - 2;
                bulletSpeed = -Math.Abs(bulletSpeed);
            }
            bullet.Top = character.Top + character.Height / 2 - bullet.Height / 2;

            bullet.BringToFront();
            bullet.Visible = true;
            bulletActive = true;
        }

        // Get current level control item
        private Control Get_current_panel()
        {
            // Find the panel name contains "gamePlayLevel" and is currently visible
            foreach (Control c in this.Controls)
            {
                if (c is Panel && c.Visible && c.Name.StartsWith("gamePlayLevel"))
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
            Control nextPanelObj = this.Controls.Find(nextPanelName, true).FirstOrDefault();
            return nextPanelObj;
        }

        // Load the next level
        private void Load_next_level(Control next)
        {
            if (next != null)
            {
                // Add objects to the next level
                next.Controls.AddRange(new Control[] {background, leftside, rightside, ground0,    // Background objects
                                                      ground1, ground2, ground3, ground4, ground5, // Platform
                                                      character, enemy1, spike1, goal, bullet,     // Main objects
                                                      skipBtn, currentLevelLabel, pauseBtn});      // Control objects

                // Reorder
                background.SendToBack();
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
                skipBtn.BringToFront();

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
            jumping = false;
            bulletActive = false;
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
            spike1.Location = new Point(300, 370);
            goal.Location = new Point(830, 40);

            skipBtn.Location = new Point(0, 43);
            pauseBtn.Location = new Point(0, 0);
            currentLevelLabel.Location = new Point(53, 0);

            goal2.Location = new Point(2000, 2000);
            goal3.Location = new Point(2000, 2000);
            goal4.Location = new Point(2000, 2000);
        }

        // Game over
        private void Game_over()
        {
            gameTimer.Stop();

            blockPanel.Visible = false;
            currentPanel.Visible = false;

            if (gameOverStatus == 1)
            {
                gameover.Text = "恭喜過關!!!";
            }
            else
            {
                gameover.Text = "闖關失敗";
            }

            gameOverPanel.Visible = true;
        }


        // =============== Callback ================
        public gameForm()
        {
            InitializeComponent();
        }

        private void gameForm_Load(object sender, EventArgs e)
        {
            pause.Left = 959 - pause.Width/2;
        }

        private void startGameBtn_Click(object sender, EventArgs e)
        {
            // Close panel
            mainMenuPanel.Visible = false;
            gameOverPanel.Visible = false;

            // Show level selection
            chooseLevelPanel.Visible = true;
            chooseLevelPanel.Enabled = true;
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
            else
            {
                level5Timer.Stop();
            }

            chooseLevelPanel.Visible = false;
            chooseLevelPanel.Enabled = false;

            // Starting the timer
            gameTimer.Start();

            this.ActiveControl = null;
            this.Focus();
        }

        private void gameInstructionBtn_Click(object sender, EventArgs e)
        {
            // Show instructions
            mainMenuPanel.Visible = false;
            gameInstructionPenal.Visible = true;
        }

        private void goBackBtn_Click(object sender, EventArgs e)
        {
            // Show main menu
            gameInstructionPenal.Visible = false;
            chooseLevelPanel.Visible = false;
            pause.Visible = false;

            foreach (Control c in this.Controls)
            {
                if (c is Panel && c.Visible && c.Name.StartsWith("gamePlayLevel"))
                {
                    c.Visible = false;
                }
            }

            gameTimer.Stop();
            level5Timer.Stop();

            mainMenuPanel.Visible = true;
        }

        private void quitBtn_Click(object sender, EventArgs e)
        {
            // Exit game
            Application.Exit();
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

        private void nextLevelQuitBtn_Click(object sender, EventArgs e)
        {
            // Return to main menu
            gameTimer.Stop();
            blockPanel.Visible = false;
            currentPanel.Visible = false;
            nextLevelPanel.Visible = false;
            mainMenuPanel.Visible = true;
        }

        private void gameForm_KeyDown(object sender, KeyEventArgs e)
        {
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


        private void gameTimer_Tick(object sender, EventArgs e)
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
                        bulletSpeed *= -1;
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

            // Player collides with spike -> ame over
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
                        goal2.Location = new Point(833, 355);
                        goal2.BringToFront();
                        goal2.Visible = true;
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
                    enemy1.Location = new Point(2000, 2000);

                    // Find the next level's control item
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

        private void backToGame_Click(object sender, EventArgs e)
        {
            pause.Visible = false;
            pausing = false;
        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {
            pausing = true;
            pause.Visible = true;
            pause.BringToFront();
        }
    }
}
