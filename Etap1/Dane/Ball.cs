using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Dane
{
    internal class Ball : IBall
    {
        public int _iD;
        private Vector2 _Position;
        public override Vector2 Movement { get; set; }
        private bool _isRunning = false;

        public Ball(int id, float x, float y)
        {
            _iD = id;
            _Position = new Vector2(x, y);
            Task.Run(StartMovement);
        }

        private void MakeMove()
        {
            //Vector2 newPosition = _Position;
            //Vector2 currMovement = Movement;

            Vector2 newPosition = new Vector2(Movement.X + Position.X, Movement.Y + Position.Y);
            _Position = newPosition;
            DataEvent args = new DataEvent(this);
            PropertyChanged?.Invoke(this, args);
        }

        private async void StartMovement()
        {
            _isRunning = true;
            while (_isRunning)
            {
                MakeMove();
                double speed = Math.Sqrt( Math.Pow(Movement.X, 2) + Math.Pow(Movement.Y, 2));
                await Task.Delay((int)speed);
            }
        }

        public override Vector2 Position
        { get { return _Position; } }
        public override bool IsRunning
        { get { return _isRunning; } set { _isRunning = value; } }

        public override void TurnOff()
        {
            _isRunning = false;
        }


        public override event EventHandler<DataEvent> PropertyChanged;
        
    }
}
