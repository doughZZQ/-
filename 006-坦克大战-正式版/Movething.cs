using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006_坦克大战_正式版
{
    enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }
    class Movething : GameObject
    {
        private Object _lock = new object();
        public Bitmap BitmapUp { get; set; }
        public Bitmap BitmapDown { get; set; }
        public Bitmap BitmapLeft { get; set; }
        public Bitmap BitmapRight { get; set; }

        public int Speed { get; set; }

        protected Direction dir;
        public Direction Dir 
        {
            get
            {
                return dir;
            }
            set
            {
                dir = value;

                Bitmap bmp = null;
                switch (dir)
                {
                    case Direction.Up:
                        bmp = BitmapUp;
                        break;
                    case Direction.Down:
                        bmp = BitmapDown;
                        break;
                    case Direction.Left:
                        bmp = BitmapLeft;
                        break;
                    case Direction.Right:
                        bmp = BitmapRight;
                        break;
                }
                lock (_lock)
                {
                    Width = bmp.Width;
                    Height = bmp.Height;
                }
            }
        }

        protected override Image GetImage()
        {
            Bitmap bitMap = null;
            switch(Dir)
            {
                case Direction.Up:
                    bitMap = BitmapUp;
                    break;
                case Direction.Down:
                    bitMap = BitmapDown;
                    break;
                case Direction.Left:
                    bitMap = BitmapLeft;
                    break;
                case Direction.Right:
                    bitMap = BitmapRight;
                    break;
            }
            bitMap.MakeTransparent(Color.Black);

            return bitMap;
        }

        

        public override void DrawSelf()
        {
            lock (_lock)
            {
                base.DrawSelf();
            }       
        }
    }
}
