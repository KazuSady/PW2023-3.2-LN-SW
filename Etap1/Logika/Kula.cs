using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logika
{
     public class Kula : INotifyPropertyChanged
     {
        private double x;
        private double y;
        private double r;
        private double xMovement;
        private double yMovement;

        public Kula(double x, double y, double r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            this.xMovement = 0;
            this.yMovement = 0;
        }

        public void MakeMove()
        {
            X += xMovement;
            Y += yMovement;
        }

        public double X
        { get { return x; } set { x = value; OnPropertyChanged(); } }
        public double Y
        { get { return y; } set { y = value; OnPropertyChanged(); } }
        public double R
        { get { return r; } set { r = value; OnPropertyChanged(); } }
        public double XMovement
        { get { return xMovement; } set { xMovement = value; } }
        public double YMovement
        { get { return yMovement; } set { yMovement = value; } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
     }
}
