using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    internal class Obszar
    {
        private int height;
        private int width;
        private List<Kula> kule = new List<Kula>();

        public Kula CreateKula(int ballSize)
        {
            Random random = new Random();
            int x, y;
            x = y = ballSize;
            bool check = false;
            while(!check)
            {
                x = random.Next(ballSize, width - ballSize);
                y = random.Next(ballSize, height - ballSize);
                //We're checking if the new ball isn't inside of another
                foreach(Kula k in kule)
                {
                    double dist = Math.Sqrt(Math.Pow(k.X - x, 2) + Math.Pow(k.Y - y, 2));
                    if ( dist <= k.R + ballSize)
                    {
                        check = false; break;
                    }             
                }
                if (!check) continue;
                check = true;
            }
            Kula kula = new Kula(x, y, ballSize, 1);
            return kula;
        }

        public void CreateKulaList(int ballsAmount, int ballsSize)
        {
            kule.Clear();
            for (int i = 0; i < ballsAmount; i++)
            {
                Kula kula = CreateKula(ballsSize);
                kule.Add(kula);
            }
        }

        public Obszar(int height, int width, int ballsAmount, int ballsSize)
        {
            this.height = height;
            this.width = width;
            CreateKulaList(ballsAmount, ballsSize);
        }
        public int Height { get { return height; } }
        public int Width { get { return width; } }
        public List<Kula> Kule { get { return kule; } }
    }
}
