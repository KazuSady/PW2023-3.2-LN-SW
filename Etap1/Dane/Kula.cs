using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public class Kula 
    {
        private double x;
        private double y;
        private double r;
        private double weight;
        private double[] movement = new double[2];
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Kula(double x, double y, double r, double weight)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            this.weight = weight;
            Random random = new Random();
            double xMovement = 0, yMovement = 0;
            while(xMovement == 0 || yMovement == 0)
            {
                xMovement = random.NextDouble();
                if(random.Next(-1,1) < 0)
                {
                    xMovement = -xMovement;
                }
                yMovement = random.NextDouble();
                if (random.Next(-1, 1) < 0)
                {
                    yMovement = -yMovement;
                }
            }
            movement[0] = xMovement;
            movement[1] = yMovement;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void makeMove()
        {
            X += XMovement;
            Y += YMovement;
            OnPropertyChanged("Position");
        }

        public double X
        { 
            get { 
                return x; 
            } 
            set { 
                x = value; OnPropertyChanged("X"); 
            } 
        }
        public double Y
        { get { return y; } set { y = value; OnPropertyChanged("Y"); } }
        public double R
        { get { return r; } set {  r = value; OnPropertyChanged("R"); } }
        public double Weight 
        { get { return weight; } set { weight = value; OnPropertyChanged("Weight"); } }
        public double XMovement
        { get { return movement[0]; } set { movement[0] = value; } }
        public double YMovement
        { get { return movement[1]; } set { movement[1] = value; } }

    }

    

}
