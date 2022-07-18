using _006_坦克大战_正式版.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006_坦克大战_正式版
{
    enum Tag
    {
        MyTank = 0,
        EnemyTank = 1
    }
    class Bullet : Movething
    {
        public Tag Tag { get; set; }
        public bool IsDestroy { get; set; }

        public Bullet(int x, int y, int speed, Direction dir, Tag tag)
        {
            //IsMoveing = false;
            IsDestroy = false;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            //this.Dir = Direction.Up;
            BitmapUp = Resources.BulletUp;
            BitmapDown = Resources.BulletDown;
            BitmapLeft = Resources.BulletLeft;
            BitmapRight = Resources.BulletRight;
            this.Dir = dir;
            this.Tag = tag;

            this.X -= Width / 2;
            this.Y -= Height / 2;
        }

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
                if (Y + Height/2 < 0)
                {
                    IsDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Height/2 > 450)
                {
                    IsDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X + Width/2 < 0)
                {
                    IsDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Width/2 > 450)
                {
                    IsDestroy = true;
                    return;
                }
            }
            #endregion

            #region 检测是否与其他元素发生碰撞
            Rectangle rect = GetRectangle();

            rect.X = X + Width / 2 - 3;
            rect.Y = Y + Height / 2 - 3;
            rect.Height = 6;
            rect.Width = 6;

            //子弹的碰撞检测
            //1.墙 2.钢铁墙 3.坦克（敌方与我方）
            int xExplosion = this.X + Width / 2;
            int yExplosion = this.Y + Height / 2;

            NoMovething wall = null;
            if ((wall = GameObjectManager.IsCollidedWall(rect)) != null)
            {
                IsDestroy = true;
                SoundManager.PlayBlast();
                GameObjectManager.DestroyWall(wall);
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                IsDestroy = true;
                SoundManager.PlayBlast();
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                IsDestroy = true;
                SoundManager.PlayBlast();
                GameFrameWork.ChangeToGameOver();
                return;
            }
            if (Tag == Tag.MyTank)
            {
                EnemyTank enemy = null;
                if ((enemy = GameObjectManager.IsCollidedEnemyTank(rect)) != null)
                {
                    IsDestroy = true;
                    SoundManager.PlayBlast();
                    GameObjectManager.DestroyEnemyTank(enemy);
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    
                    return;
                }
            }
            else if (Tag == Tag.EnemyTank)
            {
                MyTank myTank = null;
                if ((myTank = GameObjectManager.IsCollidedMyTank(rect)) != null)
                {
                    IsDestroy = true;
                    SoundManager.PlayHit();
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    SoundManager.PlayHit();
                    myTank.TakeDamage();
                    return;
                }
            }
            #endregion
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
    }
}
