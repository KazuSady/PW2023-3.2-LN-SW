namespace Dane
{
    public class Obszar
    {
        private int height;
        private int width;
        private bool isRunning = false;
        private List<Kula> kule = new List<Kula>();
        public Obszar(int height, int width, int ballsAmount, int ballsSize)
        {
            this.height = height;
            this.width = width;
            CreateKulaList(ballsAmount, ballsSize);
        }

        public Kula CreateKula(int ballSize)
        {
            Random random = new Random();
            int x, y;
            x = y = ballSize;
            bool check = true;
            do
            {
                x = random.Next(ballSize, width - ballSize);
                y = random.Next(ballSize, height - ballSize);
                //We're checking if the new ball isn't inside of another
                foreach(Kula k in this.kule)
                {
                    double dist = Math.Sqrt(Math.Pow(k.X - x, 2) + Math.Pow(k.Y - y, 2));
                    if ( dist <= k.R + ballSize)
                    {
                        check = false;
                        break;
                    }             
                }
                if (!check) continue;
                check = true;
            } while (!check);

            Kula kula = new Kula(x, y, ballSize, 1);
            return kula;
        }

        public void CreateKulaList(int ballsAmount, int ballsSize)
        {
            kule.Clear();
            for (int i = 0; i < ballsAmount; i++)
            {
                Kula kula = CreateKula(ballsSize);
                this.kule.Add(kula);
            }
        }

        public int Height { get { return height; } }
        public int Width { get { return width; } }
        public List<Kula> Kule { get { return kule; } }
        public bool IsRunning { get { return isRunning; } set { isRunning = value; } }
    }
}
