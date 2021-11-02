using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {

        bool goLeft, goRight, goForward, goBack;
        string facing = "up";
        int speed = 10;
        int monster1Speed = 2;
        int monster2Speed = 4;
        int monster1Life = 2;
        int monster2Life = 5;
        int bossLife = 10;
        int life = 30; //처음 life
        int bossX, bossY = 5;
        int boxX = 15;
        Random randNum = new Random();
        


        public Form1()
        {
            InitializeComponent();
            //Thread.Sleep(1000); 윈폼에 딜레이 
            //label1.BackColor = Color.Transparent;
            txtlife.Text = life.ToString();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true); //버벅거림 없애기
            this.UpdateStyles();

            this.StartPosition = FormStartPosition.Manual; //Form1 실행 위치 조정
            this.Location = new Point(400, 300);
            this.Show();

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            txtlife.Text = "Life : " + life; //label1             
            

            if (goLeft == true && player.Left > 70)
            {
                player.Left -= speed;
            }
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width -70)
            {
                player.Left += speed;
            }
            if (goForward == true && player.Top > 70)
            {
                player.Top -= speed;
            }
            if (goBack == true && player.Top + player.Height < this.ClientSize.Height -70)
            {
                player.Top += speed;
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "life")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds)) //player가 life tag를 가진 picturebox와 접촉하였을때에 
                    {
                        this.Controls.Remove(x); 
                        ((PictureBox)x).Dispose(); // picturebox는 제거하고 life++
                        life +=30;
                    }
                }

                
                if (x is PictureBox && (string)x.Tag == "monster1") //player 따라오는 monster1
                {
                    if (player.Bounds.IntersectsWith(x.Bounds)) //player랑 monster 만날때에
                    {
                        life--;
                        if (life == 0) //player 사망시에
                        {
                            this.Controls.Remove(x);
                            GameOver();
                         
                        }
                    }
                    if (x.Left > player.Left)
                    {
                        x.Left -= monster1Speed;
                        ((PictureBox)x).Image = Properties.Resources.monster1_left;
                    }
                    if (x.Left < player.Left)
                    {
                        x.Left += monster1Speed;
                        ((PictureBox)x).Image = Properties.Resources.monster1_right;
                    }
                    if (x.Top > player.Top)
                    {
                        x.Top -= monster1Speed;       
                    }
                    if (x.Top < player.Top)
                    {
                        x.Top += monster1Speed;                      
                    }
                }

                if (x is PictureBox && (string)x.Tag == "monster2") // monster2 and player
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        life--;
                        if (life == 0)
                        {
                            this.Controls.Remove(x);
                            GameOver();
                        }
                    }
                    if (x.Left > player.Left)
                    {
                        x.Left -= monster2Speed;
                    }
                    if (x.Left < player.Left)
                    {
                        x.Left += monster2Speed;
                    }
                    if (x.Top > player.Top)
                    {
                        x.Top -= monster2Speed;
                    }
                    if (x.Top < player.Top)
                    {
                        x.Top += monster2Speed;
                    }
                }

                if (x is PictureBox && (string)x.Tag == "boss") //boss and player
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        life--;
                        if (life == 0)
                        {
                            this.Controls.Remove(x);
                            GameOver();
                        }
                    }
                    x.Left += bossX;
                    x.Top += bossY;

                    if (x.Left < 0 || x.Left > 570)
                    {
                        bossX = -bossX;
                    }
                    if (x.Top < 0 || x.Top > 220)
                    {
                        bossY = -bossY;
                    }
                    if (x.Top == 220 || x.Top == 0)
                    {
                        bossX += 4;
                    }

                }
                if (x is PictureBox && (string)x.Tag == "portal" ) //portal and player
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        BackgroundImage = Properties.Resources.map2;
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        Boss();
                    }
                }
                if (x is PictureBox && (string)x.Tag == "buttonup") //button and player
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        ButtonDown();
                        Portal();
                        Box();
                    }
                }

                if (x is PictureBox && (string)x.Tag == "box") //box and player
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        player.Top = 75;
                        player.Left = 385;
                        life -= 10;
                        if (life <= 0)
                        {
                            GameOver();
                        }
                    }
                    x.Left += boxX;
                    if (x.Left <= 50 || x.Left >= 680)
                    {
                        boxX = -boxX;
                    }
                }

                    foreach (Control j in this.Controls) //monster2 and tear
                {
                    if (j is PictureBox && (string)j.Tag == "tear" && x is PictureBox && (string)x.Tag == "monster2")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            monster2Life--;
                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            if (monster2Life == 0)
                            {
                                ((PictureBox)x).Dispose();
                                this.Controls.Remove(x);
                                Life();
                                ButtonUp();
                                                     
                            }
                            if (x.Left > j.Left) //moster가 tear보다 오른쪽에 있을때에
                            {
                                x.Left += x.Width/3;
                            }
                            if (x.Left < j.Left)
                            {
                                x.Left -= x.Width/3;
                            }
                            if (x.Top > j.Top)
                            {
                                x.Top += x.Width/3;
                            }
                            if (x.Top < j.Top)
                            {
                                x.Top -= x.Width/3;
                            }
                        }
                    }
                }
                foreach (Control j in this.Controls) // box and button disable
                {
                    foreach(Control k in this.Controls)
                    { 
                        if (j is PictureBox && (string)j.Tag == "portal" && x is PictureBox && (string)x.Tag == "box" && k is PictureBox && (string)k.Tag =="buttondown")
                        {
                            if (player.Bounds.IntersectsWith(j.Bounds))
                            {
                                this.Controls.Remove(x);
                                ((PictureBox)x).Dispose();
                                this.Controls.Remove(k);
                                ((PictureBox)k).Dispose();
                            }
                        }
                    }
                }

                foreach (Control j in this.Controls) //monster1 and tear
                {
                    if (j is PictureBox && (string)j.Tag == "tear" && x is PictureBox && (string)x.Tag == "monster1") //tear and monster 만날때에 
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            monster1Life--; //monsterLife -1
                            this.Controls.Remove(j); //tear 제거
                            ((PictureBox)j).Dispose();
                            if (monster1Life == 0) // monster 사망시 Life드롭, monster 제거 monster2 호출
                            {
                                ((PictureBox)x).Dispose(); 
                                this.Controls.Remove(x);
                                Life();
                                MakeMonster2();
                            }
                            if (x.Left > j.Left) //tear가 monster에 피격시 monster의 width 만큼 tear의 반대 방향으로 멀어짐
                            {
                                x.Left += x.Width;
                            }
                            if (x.Left < j.Left)
                            {
                                x.Left -= x.Width;
                            }
                            if (x.Top > j.Top)
                            {
                                x.Top += x.Width;
                            }
                            if (x.Top < j.Top)
                            {
                                x.Top -= x.Width;
                            }
                        }
                    }
                }

                foreach (Control j in this.Controls) //Boss and tear
                {
                    if (j is PictureBox && (string)j.Tag == "tear" && x is PictureBox && (string)x.Tag == "boss") //tear랑 boss
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            bossLife--;
                            this.Controls.Remove(j);

                            ((PictureBox)j).Dispose();
                            if (bossLife == 0)
                            {
                                ((PictureBox)x).Dispose();
                                this.Controls.Remove(x);
                                Win();
                            }
                        }
                    }
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.player_left;
            }
            if (e.KeyCode == Keys.D)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.player_right;
            }
            if (e.KeyCode == Keys.W)
            {
                goForward = true;
                facing = "forward";
                player.Image = Properties.Resources.player_front;
            }
            if (e.KeyCode == Keys.S)
            {
                goBack = true;
                facing = "back";
                player.Image = Properties.Resources.player_back;
            }

        }



        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.W)
            {
                goForward = false;
            }
            if (e.KeyCode == Keys.S)
            {
                goBack = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                ShootTear(facing);
            }

            if (e.KeyCode == Keys.R)
            {
                Application.Restart();
            }
        }

        private void ShootTear(string direction) 
        {
            Tear ShootTear = new Tear();
            ShootTear.direction = direction;
            ShootTear.tearLeft = player.Left + (player.Width / 2);
            ShootTear.tearForward = player.Top + (player.Height / 2);
            ShootTear.MakeTear(this);
        }

        private void Monster1Timer_Tick(object sender, EventArgs e)
        {
            MakeMonster1();
            Monster1Timer.Stop();
        }


        private void GameOver()
        {
            player.Image = Properties.Resources.player_dead;
            FadeTimer.Start();
            MessageBox.Show("You Died \nPress 'R' to Restart");      
        }

        private void Win()
        {
            FadeTimer.Start();
            MessageBox.Show("You Win");
        }

        private void Life() //Life 정의
        {
            PictureBox life = new PictureBox();
            life.Image = Properties.Resources.lifever2;
            life.SizeMode = PictureBoxSizeMode.CenterImage;
            life.Size = new Size(30, 30);
            life.BackColor = Color.Transparent;
            life.Left = randNum.Next(70, 690);
            life.Top = randNum.Next(70, 370);
            life.Tag = "life";
            this.Controls.Add(life);

            life.BringToFront();
            player.BringToFront();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.025;
            }
            else
            {
                FadeTimer.Stop();
            }
        }



        private void MakeMonster1()
        {
            PictureBox monster1 = new PictureBox();
            monster1.Image = Properties.Resources.monster1_left;
            monster1.Tag = "monster1";
            monster1.SizeMode = PictureBoxSizeMode.CenterImage;
            monster1.Size = new Size(90, 90);
            monster1.BackColor = Color.Transparent;
            monster1.Left = randNum.Next(70, 690);
            monster1.Top = randNum.Next(70, 370);
            this.Controls.Add(monster1);

            
            monster1.BringToFront();
            player.BringToFront();
        }

        private void MakeMonster2()
        {
            PictureBox monster2 = new PictureBox();
            monster2.Image = Properties.Resources.monster2;
            monster2.Tag = "monster2";
            monster2.SizeMode = PictureBoxSizeMode.CenterImage;
            monster2.Size = new Size(100, 125);
            monster2.BackColor = Color.Transparent;
            monster2.Left = randNum.Next(70, 690);
            monster2.Top = randNum.Next(70, 370);
            this.Controls.Add(monster2);

            monster2.BringToFront();
            player.BringToFront();
        }

        private void Boss()
        {
            PictureBox boss = new PictureBox();
            boss.Image = Properties.Resources.boss;
            boss.Tag = "boss";
            boss.SizeMode = PictureBoxSizeMode.CenterImage;
            boss.Size = new Size(280, 280);
            boss.BackColor = Color.Transparent;
            this.Controls.Add(boss);

            boss.BringToFront();
            player.BringToFront();
            txtlife.BringToFront();
        }



        private void Portal()
        {
            PictureBox portal = new PictureBox();
            portal.Image = Properties.Resources.portal;
            portal.Tag = "portal";
            portal.SizeMode = PictureBoxSizeMode.CenterImage;
            portal.Size = new Size(220, 220);
            portal.BackColor = Color.Transparent;
            portal.Left = 300;
            portal.Top = 300;
            this.Controls.Add(portal);

            portal.BringToFront();
            player.BringToFront();
        }

        private void Box()
        {
            PictureBox box = new PictureBox();
            box.Image = Properties.Resources.box;
            box.Tag = "box";
            box.SizeMode = PictureBoxSizeMode.CenterImage;
            box.Size = new Size(55, 55);
            box.BackColor = Color.Transparent;
            box.Left = 50;
            box.Top = 170;
            this.Controls.Add(box);

            box.BringToFront();
            player.BringToFront();
        }

        private void ButtonUp()
        {
            PictureBox buttonup =  new PictureBox();
            buttonup.Image = Properties.Resources.buttonup;
            buttonup.Tag = "buttonup";
            buttonup.Size = new Size(40, 40);
            buttonup.SizeMode = PictureBoxSizeMode.AutoSize;
            buttonup.Left = 380;
            buttonup.Top = 90;
            this.Controls.Add(buttonup);

            buttonup.BringToFront();
            player.BringToFront();

        }

        private void ButtonDown()
        {
            PictureBox buttondown = new PictureBox();
            buttondown.Image = Properties.Resources.buttondown;
            buttondown.Tag = "buttondown";
            buttondown.Size = new Size(40, 0);
            buttondown.SizeMode = PictureBoxSizeMode.AutoSize;
            buttondown.Left = 380;
            buttondown.Top = 90;
            this.Controls.Add(buttondown);

            buttondown.BringToFront();
            player.BringToFront();

        }
    }
}
