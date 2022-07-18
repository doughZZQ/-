using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _006_坦克大战_正式版.Properties;

namespace _006_坦克大战_正式版
{
    internal class Explosion : GameObject
    {
        private int playSpeed = 1;
        private int playCount = 0;
        private int index = 0;

        public bool IsDestroy { get; private set; }

        private List<Bitmap> bitmaps = new List<Bitmap>()
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5
        };
        public Explosion(int x, int y)
        {
            IsDestroy = false;
            foreach (Bitmap bmp in bitmaps)
            {
                bmp.MakeTransparent(Color.Black);
            }
            this.X = x - bitmaps[0].Width / 2;
            this.Y = y - bitmaps[0].Height / 2;
        }
        protected override Image GetImage()
        {
            return bitmaps[index];
        }
        public override void UpDate()
        {
            if (index < 4)
            {
                playCount++;
                index = (playCount - 1) / playSpeed;
                //base.UpDate();
            }
            else //if (index >= 4)
            {
                IsDestroy = true;
            }
            base.UpDate();
        }
        //public override void DrawSelf()
        //{
        //    //base.DrawSelf();

        //}
    }
}
