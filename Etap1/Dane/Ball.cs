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

        public Ball(int x, int y)
        {
            _position = new Point(x, y);
        }

        public override void MakeMove()
        {
            int newX = this._xMovement + this._position.X;
            int newY = this._yMovement + this._position.Y;

            this.Position = new Point(newX, newY);
        }

        public override Point Position
        { get { return _position; } set { _position = value; OnPropertyChanged(); } }
        public override int XMovement
        { get { return _xMovement; } set { _xMovement = value; } }
        public override int YMovement
        { get { return _yMovement; } set { _yMovement = value; } }


        public override event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
