using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logika
{
     public class Ball : IBall, INotifyPropertyChanged
     {
        private double x;
        private double y;
        private double r;
        private double xMovement;
        private double yMovement;

        public Ball(double x, double y, double r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
        }

        public override void MakeMove()
        {
            X += xMovement;
            Y += yMovement;
        }

        public override double X
        { get { return x; } set { x = value; OnPropertyChanged(); } }
        public override double Y
        { get { return y; } set { y = value; OnPropertyChanged(); } }
        public override double R
        { get { return r; } set { r = value; } }
        public override double XMovement
        { get { return xMovement; } set { xMovement = value; } }
        public override double YMovement
        { get { return yMovement; } set { yMovement = value; } }

        public override event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
     }
}
