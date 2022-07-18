using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006_坦克大战_正式版
{
    /*
     * 不可移动对象
     */
    class NoMovething : GameObject
    {
        protected Image img;
        public Image Image 
        {
            get
            {
                return img;
            }
            set
            {
                img = value;
                Width = img.Width;
                Height = img.Height;
            }
        }

        protected override Image GetImage()
        {
            return Image;
        }
        public NoMovething(int x, int y, Image image)
        {
            this.X = x;
            this.Y = y;
            this.Image = image;
        }
    }
}
