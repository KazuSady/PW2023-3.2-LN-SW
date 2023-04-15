using Dane;

namespace Logika
{
    public class Obszar
    {
        private int height;
        private int width;
        private bool isRunning = false;
        private List<Kula> kule = new List<Kula>();


        public Obszar(int height, int width)
        {
            this.height = height;
            this.width = width;
        }

        public void CreateKulaList(int ballsAmount, int ballsSize)
        {
            kule.Clear();
            Random random = new Random();
            for (int i = 0; i < ballsAmount; i++)
            {
                int x = random.Next(ballsSize, this.width-ballsSize);
                int y = random.Next(ballsSize, this.height-ballsSize);
                this.kule.Add(new Kula(x, y, ballsSize));
            }
        }


        public int Height { get { return height; } }
        public int Width { get { return width; } }
        public List<Kula> Kule { get { return kule; } }
        public bool IsRunning { get { return isRunning; } set { isRunning = value; } }
    }
}
