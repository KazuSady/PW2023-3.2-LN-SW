namespace Logika
{
    internal class Field
    {
        private int height;
        private int width;
        private bool isRunning;
        private List<IBall> _Balls = new List<IBall>();

        public Field(int height, int width)
        {
            this.height = height;
            this.width = width;
            this.isRunning = false;
        }

        public void CreateBallsList(int ballsAmount, int ballsSize)
        {
            _Balls.Clear();
            Random random = new Random();
            for (int i = 0; i < ballsAmount; i++)
            {
                int x = random.Next(ballsSize, this.width-ballsSize);
                int y = random.Next(ballsSize, this.height-ballsSize);
                _Balls.Add(new Ball(x, y, ballsSize));
            }
        }

        public List<IBall> GetBalls()
        {
            return _Balls;
        }
        public void ClearBalls()
        {
            _Balls.Clear();
        }

        public int Height { get { return height; } }
        public int Width { get { return width; } }
        public bool IsRunning { get { return isRunning; } set { isRunning = value; } }
    }
}
