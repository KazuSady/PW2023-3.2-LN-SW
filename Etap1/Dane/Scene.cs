using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    internal class Scene
    {
        private int height;
        private int width;
        private bool isRunning;

        public Scene(int height, int width)
        {
            this.height = height;
            this.width = width;
            this.isRunning = false;
        }

        public int Height { get { return height; } }
        public int Width { get { return width; } }

        public bool IsRunning { get { return isRunning; } set { isRunning = value; } }

    }
}
