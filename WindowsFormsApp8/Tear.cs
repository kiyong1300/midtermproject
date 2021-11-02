using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace WindowsFormsApp8
{
    class Tear
    {
        public string direction;
        public int tearLeft;
        public int tearForward;

        private int speed = 5; //눈물 속도
        private PictureBox tear = new PictureBox();
        
        private Timer tearTimer = new Timer();

        public void MakeTear(Form form)
        {
            tear.Image = Properties.Resources.tear;
            tear.Tag = "tear";
            tear.Left = tearLeft;
            tear.Top = tearForward;
            tear.Size = new Size(20, 20);
            tear.BringToFront();
            tear.BackColor = Color.Transparent; //picturebox background color 투명
            form.Controls.Add(tear);

            tearTimer.Interval = speed;
            tearTimer.Tick += new EventHandler(TearTimerEvent);
            tearTimer.Start();
        }

        private void TearTimerEvent(object sender, EventArgs e)
        {
            if (direction == "left")
            {
                tear.Left -= speed;
            }
            if (direction == "right")
            {
                tear.Left += speed;
            }
            if (direction == "forward")
            {
                tear.Top -= speed;
            }
            if (direction == "back")
            {
                tear.Top += speed;
            }

            if (tear.Left < 70 || tear.Left > 710 || tear.Top < 70 || tear.Top > 410) //tear가 지정범위 벗어날시 tear 사라짐 나중에 지정범위 제대로 설정하기!!
            {
                tearTimer.Stop(); 
                tearTimer.Dispose();
                tear.Dispose();
                tearTimer = null;
                tear = null;
            }
        }
    }
}
