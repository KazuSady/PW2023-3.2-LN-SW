using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        }

        public void makeMove()
        {
            X += xMovement;
            Y += yMovement;
            OnPropertyChanged("Position");
        }

        public double X
        { get { return x; } set { x = value; OnPropertyChanged("X"); } }
        public double Y
        { get { return y; } set { y = value; OnPropertyChanged("Y"); } }
        public double R
        { get { return r; } set { r = value; OnPropertyChanged("R"); } }
        public double XMovement
        { get { return xMovement; } set { xMovement = value; } }
        public double YMovement
        { get { return yMovement; } set { yMovement = value; } }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
