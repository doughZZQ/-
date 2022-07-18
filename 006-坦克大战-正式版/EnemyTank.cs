using _006_坦克大战_正式版.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _006_坦克大战_正式版.GameObjectManager;

namespace _006_坦克大战_正式版
{
    class EnemyTank : Movething
    {
        public EnemyTank(int x, int y, int speed, Bitmap bmpUp, Bitmap bmpDown, Bitmap bmpLeft, Bitmap bmpRight)
        {
            //IsMoveing = true;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            //this.Dir = Direction.Up;
            BitmapUp = bmpUp;
            BitmapDown = bmpDown;
            BitmapLeft = bmpLeft;
            BitmapRight = bmpRight;
            this.Dir = Direction.Down;
            AttackSpeed = 45;
            ChangeDirSpeed = 60;
        }

        //private object _lock = new object();
        public int AttackSpeed { get; private set; }
        private int attackCount = 0;

        public int ChangeDirSpeed { get; private set; }
        private int changeDirCount = 0;

        private static Random r = new Random();

        public override void UpDate()
        {
            MoveCheck();
            Move();
            AttackCheck();
            AutoChangeDirection();

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
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    ChangeDirection();
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
                ChangeDirection();
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                ChangeDirection();
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                ChangeDirection();
                return;
            }
            #endregion
            
        }

        private void ChangeDirection()
        {
            while (true)
            {
                Direction dir = (Direction)r.Next(0, 4);
                if (dir != Dir)
                {
                    Dir = dir;
                    break;
                }
            }

            MoveCheck();
        }
        public void AutoChangeDirection()
        {
            changeDirCount++;
            if (changeDirCount >= ChangeDirSpeed)
            {
                changeDirCount = 0;
                ChangeDirection();
                //Direction direction = (Direction)r.Next(0, 4);
                //Dir = direction;
            }
        }

        private void Move()
        {
            //if (IsMoveing == false) return;

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

        private void AttackCheck()
        {
            attackCount++;
            if (attackCount >= AttackSpeed)
            {
                ShootBullet();
                attackCount = 0;
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

            GameObjectManager.CreateBullet(x, y, Tag.EnemyTank, Dir);
        }
    }
}
