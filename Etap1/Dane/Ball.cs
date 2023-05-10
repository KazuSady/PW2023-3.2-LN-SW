using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;


namespace Dane
{
    internal class Ball : IBall, INotifyPropertyChanged
    {
        private Point _position;
        private int _xMovement;
        private int _yMovement;
        private bool _isRunning = false;
        Task task;

        public Ball(int x, int y)
        {
            _position = new Point(x, y);
            task = Task.Run(StartMovement);
        }

        private void MakeMove()
        {
            int newX = this._xMovement + this._position.X;
            int newY = this._yMovement + this._position.Y;

            this.Position = new Point(newX, newY);
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
                double speed = Math.Sqrt(Math.Pow(this.XMovement, 2) + Math.Pow(this.YMovement, 2));
                await Task.Delay((int)speed);
            }   
        }



        public override Point Position
        { get { return _position; } set { _position = value; OnPropertyChanged(); } }
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


        public override event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
