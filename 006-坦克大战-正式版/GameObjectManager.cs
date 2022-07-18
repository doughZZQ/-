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
    class GameObjectManager
    {
        //创建一个NoMovething列表来存储 墙
        private static List<NoMovething> wallList = new List<NoMovething>();
        private static List<NoMovething> steelList = new List<NoMovething>();
        private static NoMovething boss = new NoMovething(7 * 30, 14 * 30, Resources.Boss);
        public static MyTank myTank;
        private static List<EnemyTank> enemyTanks = new List<EnemyTank>();
        private static List<Bullet> bullets = new List<Bullet>();
        private static List<Explosion> explosions = new List<Explosion>();

        //private static Object _BulletLock = new object();

        private static int enemyBornSpeed = 60;
        private static int enemyBornCount = 60;
        private static Point[] points = new Point[3];

        public static void Start()
        {            
            points[0].X = 0;
            points[0].Y = 0;

            points[1].X = 7 * 30;
            points[1].Y = 0;

            points[2].X = 14 * 30;
            points[2].Y = 0;
        }

        public static void UpDate()
        {
            foreach (NoMovething temp in wallList)
            {
                temp.UpDate();
            }
            foreach (NoMovething temp in steelList)
            {
                temp.UpDate();
            }
            foreach (EnemyTank tank in enemyTanks)
            {
                tank.UpDate();
            }
            CheckAndDestroyBullet();
            foreach (Bullet bullet in bullets)
            {
                bullet.UpDate();
            }
            CheckAndDestroyExplosion();
            foreach (Explosion exp in explosions)
            {
                exp.UpDate();
            }

            boss.UpDate();

            myTank.UpDate();

            EnemyBorn();
        }

        public static void EnemyBorn()
        {
            enemyBornCount++;
            SoundManager.PlayAdd();
            if (enemyBornCount >= enemyBornSpeed)
            {
                Random rd = new Random();
                int index = rd.Next(0, 3);
                Point position = points[index];
                int enemyTankType = rd.Next(1, 5);
                switch (enemyTankType)
                {
                    case 1:
                        CreateEnemyTank1(position.X, position.Y);
                        break;
                    case 2:
                        CreateEnemyTank2(position.X, position.Y);
                        break;
                    case 3:
                        CreateEnemyTank3(position.X, position.Y);
                        break;
                    case 4:
                        CreateEnemyTank4(position.X, position.Y);
                        break;
                }

                enemyBornCount = 0;
            }
        }

        public static void CreateExplosion(int x, int y)
        {
            Explosion explosion = new Explosion(x, y);
            explosions.Add(explosion);
        }

        public static void CreateBullet(int x, int y, Tag tag, Direction dir)
        {
            Bullet bullet = new Bullet(x, y, 10, dir, tag);
            bullets.Add(bullet);
        }
        public static void CheckAndDestroyBullet()
        {
            List<Bullet> needToDestroy = new List<Bullet>();
            foreach (Bullet bullet in bullets)
            {
                if (bullet.IsDestroy == true)
                {
                    needToDestroy.Add(bullet);
                }
            }
            foreach (Bullet bullet in needToDestroy)
            {
                bullets.Remove(bullet);
            }
        }
        public static void CheckAndDestroyExplosion()
        {
            List<Explosion> needToDestroy = new List<Explosion>();
            foreach (Explosion exp in explosions)
            {
                if (exp.IsDestroy == true)
                {
                    needToDestroy.Add(exp);
                }
            }
            foreach (Explosion exp in needToDestroy)
            {
                explosions.Remove(exp);
            }
        }

        //public static void DestroyBullet(Bullet bullet)
        //{           
        //    bullets.Remove(bullet);          
        //}

        private static void CreateEnemyTank1(int x, int y)
        {
            EnemyTank enemy1 = new EnemyTank(x, y, 3, Resources.GrayUp, 
                Resources.GrayDown, Resources.GrayLeft, Resources.GrayRight);
            enemyTanks.Add(enemy1);
        }
        private static void CreateEnemyTank2(int x, int y)
        {
            EnemyTank enemy2 = new EnemyTank(x, y, 3, Resources.GreenUp,
                Resources.GreenDown, Resources.GreenLeft, Resources.GreenRight);
            enemyTanks.Add(enemy2);
        }
        private static void CreateEnemyTank3(int x, int y)
        {
            EnemyTank enemy3 = new EnemyTank(x, y, 4, Resources.QuickUp,
                Resources.QuickDown, Resources.QuickLeft, Resources.QuickRight);
            enemyTanks.Add(enemy3);
        }
        private static void CreateEnemyTank4(int x, int y)
        {
            EnemyTank enemy4 = new EnemyTank(x, y, 2, Resources.SlowUp,
                Resources.SlowDown, Resources.SlowLeft, Resources.SlowRight);
            enemyTanks.Add(enemy4);
        }

        public static NoMovething IsCollidedWall(Rectangle rt)
        {
            foreach (NoMovething wall in wallList)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
                
            }
            return null;
        }

        public static NoMovething IsCollidedSteel(Rectangle rt)
        {
            foreach (NoMovething steel in steelList)
            {
                if (steel.GetRectangle().IntersectsWith(rt))
                {
                    return steel;
                }

            }
            return null;
        }

        public static bool IsCollidedBoss(Rectangle rt)
        {
            if (boss.GetRectangle().IntersectsWith(rt))
            {
                return true;
            }
            return false;
        }

        public static EnemyTank IsCollidedEnemyTank(Rectangle rt)
        {
            foreach (EnemyTank enemy in enemyTanks)
            {
                if (enemy.GetRectangle().IntersectsWith(rt))
                {
                    return enemy;
                }
            }
            return null;
        }
        public static MyTank IsCollidedMyTank(Rectangle rt)
        {
            if (myTank.GetRectangle().IntersectsWith(rt))
                return myTank;
            else
                return null;
        }

        #region 修改掉的代码
        //public static void DrawMap()
        //{
        //    foreach(NoMovething temp in wallList)
        //    {
        //        temp.DrawSelf();
        //    }
        //    foreach(NoMovething temp in steelList)
        //    {
        //        temp.DrawSelf();
        //    }

        //    boss.DrawSelf();
        //}
        //public static void DrawMyTank()
        //{
        //    myTank.DrawSelf();
        //}
        #endregion

        public static void CreateMap()
        {
            CreateWall(1, 1, 5, Resources.wall, wallList);  
            CreateWall(3, 1, 5, Resources.wall, wallList);
            CreateWall(5, 1, 4, Resources.wall, wallList);
            CreateWall(7, 1, 3, Resources.wall, wallList);
            CreateWall(9, 1, 4, Resources.wall, wallList);
            CreateWall(11, 1, 5, Resources.wall, wallList);
            CreateWall(13, 1, 5, Resources.wall, wallList);

            CreateWall(7, 5, 1, Resources.steel, steelList);

            CreateWall(2, 7, 1, Resources.wall, wallList);
            CreateWall(3, 7, 1, Resources.wall, wallList);
            CreateWall(4, 7, 1, Resources.wall, wallList);
            CreateWall(6, 7, 1, Resources.wall, wallList);
            CreateWall(7, 7, 1, Resources.wall, wallList);
            CreateWall(8, 7, 1, Resources.wall, wallList);
            CreateWall(7, 8, 1, Resources.wall, wallList);
            CreateWall(10, 7, 1, Resources.wall, wallList);
            CreateWall(11, 7, 1, Resources.wall, wallList);
            CreateWall(12, 7, 1, Resources.wall, wallList);
            CreateWall(1, 9, 5, Resources.wall, wallList);
            CreateWall(3, 9, 5, Resources.wall, wallList);
            CreateWall(5, 10, 4, Resources.wall, wallList);
            CreateWall(7, 10, 2, Resources.wall, wallList);
            CreateWall(9, 10, 4, Resources.wall, wallList);
            CreateWall(11, 9, 5, Resources.wall, wallList);
            CreateWall(13, 9, 5, Resources.wall, wallList);

            CreateWall(0, 7, 1, Resources.steel, steelList);
            CreateWall(14, 7, 1, Resources.steel, steelList);

            CreateWall(6, 11, 1, Resources.wall, wallList);
            CreateWall(8, 11, 1, Resources.wall, wallList);

            CreateBossWall(13, 27, 3, Resources.wall, wallList);
            CreateBossWall(14, 27, 1, Resources.wall, wallList);
            CreateBossWall(15, 27, 1, Resources.wall, wallList);
            CreateBossWall(16, 27, 3, Resources.wall, wallList);

        }
        private static void CreateWall(int x, int y, int count, Image image, List<NoMovething> imageList)
        {
            //List<NoMovething> wallList = new List<NoMovething>();
            int xPosition = x * 30;
            int yPosition = y * 30;

            for (int i = yPosition; i < yPosition + count * 30; i += 15)
            {
                // i, xPosition      i, xPosition + 15
                NoMovething wall1 = new NoMovething(xPosition, i, image);
                NoMovething wall2 = new NoMovething(xPosition + 15, i, image);
                imageList.Add(wall1);
                imageList.Add(wall2);
            }
            
        }

        private static void CreateBossWall(int x, int y, int count, Image image, List<NoMovething> imageList)
        {
            int xPosition = x * 15;
            int yPosition = y * 15;

            for (int i = yPosition; i < yPosition + count * 15; i += 15) 
            {
                NoMovething wall1 = new NoMovething(xPosition, i, image);
                imageList.Add(wall1);
            }
        }

        public static void DestroyWall(NoMovething wall)
        {
            //List<NoMovething> needToDestroy = new List<NoMovething>();
            wallList.Remove(wall);

        }
        public static void DestroyEnemyTank(EnemyTank enemy)
        {
            enemyTanks.Remove(enemy);
        }

        public static void CreateMyTank()
        {
            int xP = 5 * 30;
            int yP = 14 * 30;
            myTank = new MyTank(xP, yP, 4);
        }

        public static void KeyDown(KeyEventArgs e)
        {
            myTank.KeyDown(e);
        }
        public static void KeyUp(KeyEventArgs e)
        {
            myTank.KeyUp(e);
        }
    }
}
