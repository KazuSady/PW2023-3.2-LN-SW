using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Dane
{
    internal class Ball : IBall
    {
        private Point _position;
        private int _xMovement;
        private int _yMovement;
        private bool _isRunning = false;

        public Ball(int x, int y)
        {
            _position = new Point(x, y);
            Task.Run(StartMovement);
        }

        private void MakeMove()
        {
            int newX = _xMovement + _position.X;
            int newY = _yMovement + _position.Y;

            Position = new Point(newX, newY);
            DataEvent args = new DataEvent(this);
            PropertyChanged?.Invoke(this, args);
        }

        private async void StartMovement()
        {
            _isRunning = true;
            while (_isRunning)
            {
                lock (this)
                {
                    MakeMove();
                }
                double speed = Math.Sqrt(Math.Pow(XMovement, 2) + Math.Pow(YMovement, 2));
                await Task.Delay((int)speed);
            }
        }

        public override Point Position
        { get { return _position; } set { _position = value; } }
        public override int XMovement
        { get { return _xMovement; } set { _xMovement = value; } }
        public override int YMovement
        { get { return _yMovement; } set { _yMovement = value; } }
        public override bool IsRunning
        { get { return _isRunning; } set { _isRunning = value; } }

        public override void TurnOff()
        {
            _isRunning = false;
        }


        public override event EventHandler<DataEvent> PropertyChanged;
        
    }
}
