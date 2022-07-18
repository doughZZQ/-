using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _006_坦克大战_正式版.Properties;

namespace _006_坦克大战_正式版
{
    enum GameState
    {
        Running,
        GameOver
    }
    class GameFrameWork
    {
        public static Graphics g;
        private static GameState gameState = GameState.Running;
        public static void Start()
        {
            //SoundManager.InitSound();
            SoundManager.PlayStart();
            GameObjectManager.CreateMap();
            GameObjectManager.CreateMyTank();
            GameObjectManager.Start();
            
        }
        public static void UpDate()  //FPS 帧率
        {
            //GameObjectManager.DrawMap();
            //GameObjectManager.DrawMyTank();
            if (gameState == GameState.Running)
            {
                GameObjectManager.UpDate();
            }
            else if (gameState == GameState.GameOver)
            {
                GameOverUpdate();
            }
            
        }

        public static void GameOverUpdate()
        {
            Resources.GameOver.MakeTransparent(Color.Black);
            int x = 450 / 2 - Resources.GameOver.Width / 2;
            int y = 450 / 2 - Resources.GameOver.Height / 2;
            g.DrawImage(Resources.GameOver, x, y);
        }

        public static void ChangeToGameOver()
        {
            gameState = GameState.GameOver;
        }

    }
}
