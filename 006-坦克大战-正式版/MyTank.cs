using _006_坦克大战_正式版.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _006_坦克大战_正式版
{
    class MyTank : Movething
    {
        public MyTank(int x, int y, int speed)
        {
            IsMoveing = false;
            this.X = x;
            this.Y = y;
            originalX = x;
            originalY = y;
            this.Speed = speed;
            //this.Dir = Direction.Up;
            BitmapUp = Resources.MyTankUp;
            BitmapDown = Resources.MyTankDown;
            BitmapLeft = Resources.MyTankLeft;
            BitmapRight = Resources.MyTankRight;
            this.Dir = Direction.Up;
            HP = 3;
        }

        public bool IsMoveing { get; set; }
        public int HP { get; set; }

        private int originalX;
        private int originalY;

        public override void UpDate()
        {
            MoveCheck();
            Move();

            base.UpDate();
        }

        private void MoveCheck()  //碰撞检测
        {
            /*
             * 1. 检测是否超出窗体边界
             * 2. 检测是否与其他元素发生碰撞
             */
            #region 检测是否超出窗体边界
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    IsMoveing = false;
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    IsMoveing = false;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    IsMoveing = false;
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    IsMoveing = false;
                    return;
                }
            }
            #endregion

            #region 检测是否与其他元素发生碰撞
            Rectangle rect = GetRectangle();

            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= Speed;
                    break;
                case Direction.Down:
                    rect.Y += Speed;
                    break;
                case Direction.Left:
                    rect.X -= Speed;
                    break;
                case Direction.Right:
                    rect.X += Speed;
                    break;
            }

            if (GameObjectManager.IsCollidedWall(rect) != null)
            {
                IsMoveing = false;
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                IsMoveing = false;
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                IsMoveing = false;
                return;
            }
            #endregion
        }

        private void Move()
        {
            if (IsMoveing == false) return;

            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }
        }

        //GameMainThread  KeyDown  两线程冲突
        public void KeyDown(KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    Dir = Direction.Up;
                    IsMoveing = true;
                    break;
                case Keys.Down:
                    Dir = Direction.Down;
                    IsMoveing = true;
                    break;
                case Keys.Left:
                    Dir = Direction.Left;
                    IsMoveing = true;
                    break;
                case Keys.Right:
                    Dir = Direction.Right;
                    IsMoveing = true;
                    break;
                case Keys.Space:
                    //发射子弹
                    ShootBullet();
                    break;
            }
        }

        private void ShootBullet()
        {
            int x = this.X;
            int y = this.Y;

            switch (Dir)
            {
                case Direction.Up:
                    x += Width / 2;
                    //y -= Height / 2;
                    break;
                case Direction.Down:
                    x += Width / 2;
                    y += Height;
                    break;
                case Direction.Left:
                    //x -= Width / 2;
                    y += Height / 2;
                    break;
                case Direction.Right:
                    x += Width;
                    y += Height / 2;
                    break;
            }
            SoundManager.PlayFire();
            GameObjectManager.CreateBullet(x, y, Tag.MyTank, Dir);
        }

        public void TakeDamage()
        {
            HP--;
            if (HP <= 0)
            {
                //GameObjectManager.CreateMyTank();
                this.X = originalX;
                this.Y = originalY;
                HP = 3;
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    IsMoveing = false;
                    break;
                case Keys.Down:
                    IsMoveing = false;
                    break;
                case Keys.Left:
                    IsMoveing = false;
                    break;
                case Keys.Right:
                    IsMoveing = false;
                    break;
            }
        }
    }
}
