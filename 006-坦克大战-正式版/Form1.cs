using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _006_坦克大战_正式版
{
    public partial class Form1 : Form
    {
        private static Thread t;  //新线程
        private static Graphics g;
        private static Bitmap tempBmp;
        public Form1()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            //ThreadStart start = new ThreadStart(GameMainThread);

            g = CreateGraphics();
            //GameFrameWork.g = g;

            tempBmp = new Bitmap(450, 450);
            Graphics bmpG = Graphics.FromImage(tempBmp);
            GameFrameWork.g = bmpG; //游戏框架的画布 临时画布

            t = new Thread(GameMainThread);  //将GameMainThread方法放入线程同步执行
            t.Start();  //开始
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private static void GameMainThread()
        {
            GameFrameWork.Start();

            int sleepTime = 1000 / 60;

            while (true)
            {
                GameFrameWork.g.Clear(Color.Black);

                GameFrameWork.UpDate();
                g.DrawImage(tempBmp, 0, 0);
                Thread.Sleep(sleepTime);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }

        //事件 / 消息 / 事件消息
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }
    }
}
